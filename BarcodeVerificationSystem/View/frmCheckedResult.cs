﻿using BarcodeVerificationSystem.Controller;
using BarcodeVerificationSystem.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UILanguage;

namespace BarcodeVerificationSystem.View
{
    public partial class FrmCheckedResult : Form
    {
        public FrmMain _frmParent = null;
        private Thread _ThreadUpdateCheckedResult;
        public int _TotalColumns = 0;
        public List<string[]> _CheckedResult = new List<string[]>();
        public ConcurrentDictionary<string, CompareStatus> _CheckedData = new ConcurrentDictionary<string, CompareStatus>();
        public List<string[]> _CodeData = new List<string[]>();
        public List<string> _ColumnNames = new List<string>();
        public List<PODModel> _PODFormat = new List<PODModel>();
        public string _JobName = "";
        public bool _IsAfterProduction = false;
        public bool _IsRSeries = false;
        public int _TotalCode = 0;
        public int _TotalChecked = 0;
        public int _NumberOfCheckedPassed = 0;
        public int _NumberOfCheckedFailed = 0;
        public int _NumberOfPrinted = 0;
        private List<string> _ImageNameList = null;
        // fillValue = 0: Load all
        // fillValue = 1: Load passed result
        // fillValue > 1: Load failed
        public string _FillValue = "All";
        //Paging dataGridview
        private int _CurrentPage = 1;
        private int _PagesCount = 1;
        private readonly int _PageRows = 5000;
        private readonly string[] _FilterFailed = new string[] { "All", "Valid", "Invalided", "Duplicated", "Null", "Unknown/Missed", "Failed" };
        // END

        private const int CS_DropShadow = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ClassStyle = CS_DropShadow;
                return createParams;
            }
        }

        public FrmCheckedResult()
        {
            InitializeComponent();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            InitControls();
            InitEvents();
            SetLangguage();
        }

        public void Reload()
        {
            txtSearchDatabase.Text = "";
            int index = _FilterFailed.ToList().FindIndex(x => x == _FillValue);
            if (cbxFilter.SelectedIndex != index)
            {
                cbxFilter.SelectedIndex = index;
            }
            else
            {
                GetNeededDataToUpdateAsync();
            }
        }

        private void SetLangguage()
        {
            //lblCheckedResult.Text = Lang.CheckedResult1;
            lblFormName.Text = Lang.CheckedResult1.ToUpper();
            this.Text = Lang.CheckedResult1.ToUpper();
            lblTotalCodeOfTheJob.Text = Lang.TotalCodeOfTheJob;
            lblNumberOfCodesPrinted.Text = Lang.NumberOfCodesPrinted;
            lblNumberOfFailed.Text = Lang.NumberOfFailedCodes;
            lblNumberOfVerifiedCodes.Text = Lang.NumberOfVerifiedCode;
            lblValidCode.Text = Lang.NumberOfValidCodes;
            btnRePrint.Text = Lang.RePrint;
            btnSearch.Text = Lang.Search;
            btnRefeshDatabase.Text = Lang.Refresh;
            lblFilter.Text = Lang.Filter;
        }

        private void InitControls()
        {
            _ImageNameList = GetImageNameList();
            AssignColumnNameToTable(_ColumnNames);
            for (int i = 0; i < _FilterFailed.Count(); i++)
            {
                cbxFilter.Items.Add(_FilterFailed[i]);
                if (_FilterFailed[i] == _FillValue)
                {
                    cbxFilter.SelectedIndex = i;
                }
            }
            GetNeededDataToUpdateAsync();
        }

        private void InitEvents()
        {
            cbxFilter.DrawMode = DrawMode.OwnerDrawVariable;
            cbxFilter.Height = 40;
            cbxFilter.DropDownHeight = 150;
            cbxFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxFilter.DrawItem += ComboBoxCustom.MyComboBox_DrawItem;
            cbxFilter.MeasureItem += ComboBoxCustom.Cbo_MeasureItem;

            btnFirst.Click += ToolStripButtonClick;
            btnBack.Click += ToolStripButtonClick;
            Number1.Click += ToolStripButtonClick;
            Number2.Click += ToolStripButtonClick;
            Number3.Click += ToolStripButtonClick;
            Number4.Click += ToolStripButtonClick;
            Number5.Click += ToolStripButtonClick;
            btnNext.Click += ToolStripButtonClick;
            btnLast.Click += ToolStripButtonClick;

            cbxFilter.SelectedIndexChanged += CbxFilter_SelectedIndexChanged;
            btnSearch.Click += ActionResult;
            btnRePrint.Click += ActionResult;
            btnRefeshDatabase.Click += ActionResult;
            txtSearchDatabase.KeyDown += TxtSearchDatabase_KeyDown; ;

            FormClosing += FrmPreviewDatabase_FormClosing;
            Shared.OnLanguageChange += Shared_OnLanguageChange;

            //dgvCheckedResult.RowPostPaint += DataGridViewDatabase_RowPostPaint;
            Load += FrmCheckedResult_Load;
            comboBox1.SelectedValueChanged += (s, e) =>
            {
                _CurrentPage = (int)comboBox1.SelectedIndex + 1;
                dgvCheckedResult.Invalidate();
                RefreshPagination();
                dgvCheckedResult.FirstDisplayedScrollingRowIndex = 0;
            };
        }


        private void FrmCheckedResult_Load(object sender, EventArgs e)
        {
            btnRePrint.Visible = _IsAfterProduction && _IsRSeries;
        }

        private void CbxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetNeededDataToUpdateAsync();
        }

        public void UpdateLabel()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateLabel()));
                return;
            }
            lblNumberOfTotalCodeValue.Text = string.Format("{0:N0}", _TotalCode);
            lblNumberOfPrintedValue.Text = string.Format("{0:N0}", _NumberOfPrinted);
            lblNumberOfTotalCheckValue.Text = string.Format("{0:N0}", _TotalChecked);
            lblValidCodeValue.Text = string.Format("{0:N0}", _NumberOfCheckedPassed);
            lblNumberOfFailedCodeValue.Text = string.Format("{0:N0}", _NumberOfCheckedFailed);
        }

        private List<string> GetImageNameList()
        {
            try
            {
                string folderPath = Shared.Settings.ExportImagePath + "\\" + _JobName;
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var dir = new DirectoryInfo(folderPath);
                string strFileNameExtension = string.Format("*{0}", "bmp");
                FileInfo[] files = dir.GetFiles(strFileNameExtension); //Getting Text files
                var result = new List<string>();
                foreach (FileInfo file in files)
                {
                    result.Add(file.Name);
                }
                result.Sort((a, b) => b.CompareTo(a));
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void DataGridViewDatabase_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = sender as DataGridView;

            e.Graphics.DrawLine(SystemPens.ScrollBar, 0, 0, dgv.Width, 0);
            if (e.RowIndex != -1)
            {
                Rectangle rowRectangle = dgv.GetRowDisplayRectangle(e.RowIndex, true);
                e.Graphics.DrawLine(SystemPens.ScrollBar, rowRectangle.X, rowRectangle.Y, rowRectangle.Width, rowRectangle.Y);
            }
        }

        private void Shared_OnLanguageChange(object sender, EventArgs e)
        {
            SetLangguage();
        }

        private void TxtSearchDatabase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetNeededDataToUpdateAsync();
            }
        }

        private void ActionResult(object sender, EventArgs e)
        {
            if (sender == btnSearch)
            {
                GetNeededDataToUpdateAsync();
            }
            else if (sender == btnRefeshDatabase)
            {
                _CurrentPage = 1;
                _FillValue = "All";
                Reload();
            }
            else if (sender == btnRePrint)
            {
                if (_frmParent != null)
                    _frmParent.ReprintAsync();
            }
        }

        private void ToolStripButtonClick(object sender, EventArgs e)
        {
            var ToolStripButton = ((Button)sender);
            if (sender == btnBack)
            {
                _CurrentPage--;
            }
            else if (sender == btnNext)
            {
                _CurrentPage++;
            }
            else if (sender == btnFirst)
            {
                _CurrentPage = 1;
            }
            else if (sender == btnLast)
            {
                _CurrentPage = _PagesCount;
            }
            else
            {
                _CurrentPage = Convert.ToInt32(ToolStripButton.Text, CultureInfo.InvariantCulture);
            }

            if (_CurrentPage < 1)
            {
                _CurrentPage = 1;
            }
            else if (_CurrentPage > _PagesCount)
            {
                _CurrentPage = _PagesCount;
            }
            dgvCheckedResult.Invalidate();
            RefreshPagination();
            dgvCheckedResult.FirstDisplayedScrollingRowIndex = 0;
        }

        private void RefreshPagination()
        {
           var items = new Button[] { Number1, Number2, Number3, Number4, Number5 };
            //pageStartIndex contains the first button number of pagination.
            int pageStartIndex = 1;

            if (_PagesCount > 5 && _CurrentPage > 2)
                pageStartIndex = _CurrentPage - 2;

            if (_PagesCount > 5 && _CurrentPage > _PagesCount - 2)
                pageStartIndex = _PagesCount - 4;

            for (int i = pageStartIndex; i < pageStartIndex + 5; i++)
            {
                if (i > _PagesCount)
                {
                    items[i - pageStartIndex].Enabled = false;
                    items[i - pageStartIndex].BackColor = Color.White;
                    items[i - pageStartIndex].ForeColor = Color.Black;
                }
                else
                {
                    items[i - pageStartIndex].Enabled = true;
                    //Changing the page numbers
                    items[i - pageStartIndex].Text = i.ToString(CultureInfo.InvariantCulture);

                    //Setting the Appearance of the page number buttons
                    if (i == _CurrentPage)
                    {
                        items[i - pageStartIndex].BackColor = Color.Black;
                        items[i - pageStartIndex].ForeColor = Color.White;
                    }
                    else
                    {
                        items[i - pageStartIndex].BackColor = Color.White;
                        items[i - pageStartIndex].ForeColor = Color.Black;
                    }
                }

            }

            if (_PagesCount == 0)
            {
                btnBack.Enabled = btnFirst.Enabled = false;
                btnNext.Enabled = btnLast.Enabled = false;
            }
            else
            {
                //Enabling or Disalbing pagination first, last, previous , next buttons
                if (_CurrentPage == 1)
                    btnBack.Enabled = btnFirst.Enabled = false;
                else
                    btnBack.Enabled = btnFirst.Enabled = true;
                if (_CurrentPage == _PagesCount)
                    btnNext.Enabled = btnLast.Enabled = false;
                else
                    btnNext.Enabled = btnLast.Enabled = true;
            }

            Invoke(new Action(() =>
            {
                lblPagePerTotals.Text = $"{Lang.Page} {_CurrentPage} {Lang.Per} {_PagesCount} ({_SearchResultList.Count()} {Lang.Items})";
                lblGoToPage.Text = Lang.GoToPage;
                lblGo.Text = Lang.Go;
            }));
        }

        private void FrmPreviewDatabase_FormClosing(object sender, FormClosingEventArgs e)
        {
            KillThreadUpdateCheckedResult();
            _SearchResultList.Clear();
            _CheckedResult.Clear();
            _UpdateUICST?.Cancel();
            _UpdateUICST.Dispose();
            _CodeData.Clear();
        }

        private void EnabledWhenLoadDatabase(bool isEnable)
        {
            pnlPaging.Enabled = isEnable;
            pnlDrag.Enabled = isEnable;
            btnRePrint.Enabled = isEnable;
            btnSearch.Enabled = isEnable;
            btnRefeshDatabase.Enabled = isEnable;
        }

        private void KillThreadUpdateCheckedResult()
        {
            if (_ThreadUpdateCheckedResult != null && _ThreadUpdateCheckedResult.IsAlive)
            {
                _ThreadUpdateCheckedResult.Abort();
                _ThreadUpdateCheckedResult = null;
            }
        }

        private void AssignColumnNameToTable(List<string> values)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => AssignColumnNameToTable(values)));
                return;
            }
            string[] columns = values.ToArray();
            dgvCheckedResult.Columns.Clear();
            dgvCheckedResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            int selectedIndex = -1;
            dgvCheckedResult.CellClick += (obj, e) =>
            {
                if (e.RowIndex == -1) return;
                if (e.RowIndex == selectedIndex)
                {
                    dgvCheckedResult.Rows[e.RowIndex].Selected = false;
                    selectedIndex = -1;
                }
                else
                {
                    selectedIndex = e.RowIndex;
                }
            };

            dgvCheckedResult.CellValueNeeded += (obj, e) =>
            {
                try
                {
                    int searchResultLinesCount = _SearchResultList.Count();
                    if (e.RowIndex == -1) return;
                    string cell = "";
                    int lineIndex = e.RowIndex + (_CurrentPage - 1) * _PageRows;
                    if (searchResultLinesCount == 0 || searchResultLinesCount <= lineIndex) return;
                    int codeIndex = _SearchResultList[lineIndex];
                    if (cbxFilter.Text == "Unknown/Missed")
                    {
                        if (e.ColumnIndex == 0)
                        {
                            cell = "" + (codeIndex + 1);
                        }
                        else if (e.ColumnIndex == 1)
                        {
                            cell = _frmParent.GetCompareDataByPODFormat(_CodeData[codeIndex], _PODFormat);
                        }
                        else if (e.ColumnIndex == 2)
                        {
                            cell = ComparisonResult.Missed.ToString();
                        }
                        else if (e.ColumnIndex == 3)
                        {
                            cell = "Unknown";
                        }
                        else if (e.ColumnIndex == 4)
                        {
                            cell = "Unknown";
                        }
                    }
                    else
                    {
                        cell = lineIndex < _CheckedResult.Count() ? _CheckedResult[codeIndex][e.ColumnIndex] : "";
                    }

                    if (cell == "" && e.ColumnIndex != 1) return;
                    if (e.ColumnIndex != 2)
                    {
                        if (e.ColumnIndex == 1)
                        {
                            string dataValue = cell == "" ? Lang.CannotDetect : cell;
                            e.Value = dataValue;
                        }
                        else
                            e.Value = cell;
                    }
                    else
                    {
                        if (cell == ComparisonResult.Valid.ToString())
                        {
                            e.Value = Properties.Resources.icons8_done_24px_result;
                        }
                        else
                        {
                            if (cell == ComparisonResult.Duplicated.ToString())
                            {
                                e.Value = Properties.Resources.icon_Duplicated_Barcode;
                            }
                            else if (cell == ComparisonResult.Missed.ToString())
                            {
                                e.Value = Properties.Resources.icon_Missed_Barcode;
                            }
                            else if (cell == ComparisonResult.Null.ToString())
                            {
                                e.Value = Properties.Resources.icon_CantDetect_Barcode;
                            }
                            else
                            {
                                e.Value = Properties.Resources.icons8_multiply_20px;
                            }

                            if (dgvCheckedResult.Rows[e.RowIndex].Selected == true)
                            {
                                dgvCheckedResult.Rows[e.RowIndex].Height = 106;
                                string[] txt = string.Format("{0:D7}", dgvCheckedResult.Rows[e.RowIndex].Cells[0].Value.ToString()).Split(',');
                                string imgIndex = "";
                                foreach (string s in txt)
                                {
                                    imgIndex += s;
                                }
                                int imgLenght = imgIndex.Length;
                                for (int i = 0; i < 7 - imgLenght; i++)
                                {
                                    imgIndex = "0" + imgIndex;
                                }
                                string imgFileName = _ImageNameList.Find(x => x.Contains(imgIndex));
                                string path = Shared.Settings.ExportImagePath + "\\" + Shared.JobNameSelected.Split('.')[0] + "\\" + imgFileName;
                                if (imgFileName != null)
                                {
                                    try
                                    {
                                        var bmp = new Bitmap(Image.FromFile(path), 100, 100);
                                        e.Value = bmp;
                                        bmp = null;
                                    }
                                    catch
                                    {
                                        var bmp = new Bitmap(100, 100);
                                        e.Value = bmp;
                                        bmp = null;
                                    }
                                }
                                else
                                {
                                    e.Value = Properties.Resources.icon_NoImage;
                                }
                            }
                            else
                            {
                                dgvCheckedResult.Rows[e.RowIndex].Height = dgvCheckedResult.RowHeight;
                            }
                        }
                    }
                }
                catch (Exception)
                {

                }
            };

            int tableWidth = dgvCheckedResult.Width;
            var percentWidth = (float)1 / columns.Length;
            int tableCodeProductListWidth = dgvCheckedResult.Width - 39;
            for (int index = 0; index < columns.ToArray().Length; index++)
            {
                if (index == 2)
                {
                    var col = new DataGridViewImageColumn
                    {
                        HeaderText = columns[index],
                        Name = columns[index].Trim()
                    };
                    col.DefaultCellStyle.NullValue = null;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    if (index == 0)
                    {
                        col.Width = (int)(0.75 * tableCodeProductListWidth);
                    }
                    Size textSize = TextRenderer.MeasureText(col.HeaderText, dgvCheckedResult.Font);
                    col.Width = textSize.Width + 40;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvCheckedResult.Columns.Add(col);
                }
                else
                {
                    var col = new DataGridViewTextBoxColumn
                    {
                        HeaderText = columns[index],
                        Name = columns[index].Trim(),
                        SortMode = DataGridViewColumnSortMode.NotSortable
                    };
                    if (index == 0)
                    {
                        col.Width = (int)(0.75 * tableCodeProductListWidth);
                    }
                    Size textSize = TextRenderer.MeasureText(col.HeaderText, dgvCheckedResult.Font);
                    col.Width = textSize.Width + 40;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvCheckedResult.Columns.Add(col);
                }
            }

            FrmMain.AutoResizeColumnWith(dgvCheckedResult, defaultRecord, 2);
            dgvCheckedResult.RowCount = _PageRows;
            dgvCheckedResult.VirtualMode = true;
        }
        private readonly string[] defaultRecord = new string[] { "100000", "abcdefghijk123456789abcdefhgh", "Valid", "100", DateTime.Now.ToString() };

        private List<int> _SearchResultList = new List<int>();

        CancellationTokenSource _UpdateUICST;
        
        private async void GetNeededDataToUpdateAsync()
        {
            Invoke(new Action(() =>
            {
                lblPagePerTotals.Text = "";
            }));
            _UpdateUICST?.Cancel();
            _SearchResultList.Clear();
            EnabledWhenLoadDatabase(false);
            string filler = cbxFilter.Text == "All" ? "" : cbxFilter.Text == "Unknown/Missed" ? ComparisonResult.Missed.ToString() : cbxFilter.Text;
            filler = filler.ToLower();
            string keyWork = txtSearchDatabase.Text.ToLower();
            await Task.Run(() => { GetNeededDataToUpdate(filler, keyWork); });

            _PagesCount = Convert.ToInt32(Math.Ceiling(_SearchResultList.Count * 1.0 / _PageRows));
            RefreshPagination();
            EnabledWhenLoadDatabase(true);
            dgvCheckedResult.FirstDisplayedScrollingRowIndex = 0;
            dgvCheckedResult.ClearSelection();
            dgvCheckedResult.Invalidate();
            UpdateLabel();
        }

        private void GetNeededDataToUpdate(string filler, string keyWork)
        {
            try
            {
                _UpdateUICST = new CancellationTokenSource();
                var token = _UpdateUICST.Token;
                if (filler == "missed")
                {
                    _SearchResultList.Clear();
                    foreach (KeyValuePair<string, CompareStatus> item in _CheckedData)
                    {
                        if (!item.Value.Status)
                        {
                            string tmp = _frmParent.GetCompareDataByPODFormat(_CodeData[item.Value.Index], _PODFormat);
                            if (tmp.Contains(keyWork))
                            {
                                _SearchResultList.Add(item.Value.Index);
                            }
                        }
                        token.ThrowIfCancellationRequested();
                    }
                    _SearchResultList = _SearchResultList.OrderBy(x => x).ToList();
                }
                else
                {
                    for (int i = 0; i < _CheckedResult.Count(); i++)
                    {
                        string tmp = string.Join("", _CheckedResult[i]).ToLower();
                        string checkedResult = _CheckedResult[i][2].ToLower();
                        if (tmp.Contains(keyWork))
                        {
                            if (filler == "")
                            {
                                _SearchResultList.Add(i);
                            }
                            else if (filler == "failed")
                            {
                                if (!checkedResult.Equals("valid"))
                                {
                                    _SearchResultList.Add(i);
                                }
                            }
                            else
                            {
                                if (checkedResult.Equals(filler))
                                {
                                    _SearchResultList.Add(i);
                                }
                            }
                        }

                        token.ThrowIfCancellationRequested();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _SearchResultList.Clear();
                _PagesCount = Convert.ToInt32(Math.Ceiling(_SearchResultList.Count * 1.0 / _PageRows));
                Invoke(new Action(() =>
                {
                    RefreshPagination();
                    EnabledWhenLoadDatabase(true);
                    dgvCheckedResult.FirstDisplayedScrollingRowIndex = 0;
                    dgvCheckedResult.ClearSelection();
                    dgvCheckedResult.Invalidate();
                    UpdateLabel();
                }));
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
    }
}
