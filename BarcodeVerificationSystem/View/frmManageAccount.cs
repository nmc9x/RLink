using BarcodeVerificationSystem.Controller;
using BarcodeVerificationSystem.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using UILanguage;

namespace BarcodeVerificationSystem.View
{
    public partial class frmManageAccount : Form
    {
        // Display and value list for role comboxox
        List<Role> listRole = new List<Role>() {
                new Role() { RoleName = "Administrator", Value = 0 },
                new Role() { RoleName = "Operator", Value = 1 }
            };

        private Thread _ThreadLoadData = null;
        private bool _IsCreate = false;
        private bool _IsEdit = false;

        // Columns of account datagridview
        string[] _ColumnNamesAccountList = new string[] { Lang.FullnameColum, Lang.UsernameColumn, Lang.PasswordColumn, Lang.RoleColumn};

        public frmManageAccount()
        {
            InitializeComponent();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            InitControlls();
            InitEvents();
            SetLanguage();
        }

        private void SetLanguage()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetLanguage()));
                return;
            }

            lbFullname.Text = Lang.Fullname;
            lbUsername.Text = Lang.Username;
            lbPassword.Text = Lang.Password;
            lbRetypePasword.Text = Lang.RetypePassword;
            lbRole.Text = Lang.Role;
            lblListTitle.Text = Lang.AccountList;
            lblInfoTitle.Text = Lang.Info;
            lblFormName.Text = Lang.Manage;
        }

        public void InitControlls()
        {
            // Set up role combobox
            cbxRole.DataSource = listRole;
            cbxRole.DisplayMember = "RoleName";
            cbxRole.ValueMember = "Value";

            btnSave.Enabled = false;

            // set up password char of password textbox
            // Initial account list table
            dgvAccount.VirtualMode = false;
            dgvAccount.AllowUserToAddRows = false;
            dgvAccount.AutoGenerateColumns = false;
            dgvAccount.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAccount.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular);

            // Assign column name to table
            int tableCodeProductListWidth = dgvAccount.Width - 39;//39 is width of column index
            for (int index = 0; index < _ColumnNamesAccountList.Length; index++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.HeaderText = _ColumnNamesAccountList[index];
                col.Name = _ColumnNamesAccountList[index].Trim();
                //// Set the DataPropertyName foreach column
                //if (index == 0)
                //    col.DataPropertyName = "FullName";
                //if (index == 1)
                //    col.DataPropertyName = "UserName";
                //if (index == 2)
                //    col.DataPropertyName = "Password";
                //if (index == 3)
                //    col.DataPropertyName = "RoleName";
                if (index == 0)
                {
                    col.Width = (int)(0.75 * tableCodeProductListWidth);
                }
                //END Set the DataPropertyName foreach column
                dgvAccount.Columns.Add(col);
            }
            dgvAccount.RowTemplate.Height = 35;

            dgvAccount.RowHeadersVisible = false;

            dgvAccount.EnableHeadersVisualStyles = false;

            dgvAccount.RowHeadersVisible = false;

            dgvAccount.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvAccount.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvAccount.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 255);
            dgvAccount.DefaultCellStyle.ForeColor = SystemColors.WindowFrame;

            dgvAccount.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvAccount.ColumnHeadersDefaultCellStyle.Padding = new Padding(0, 10, 0, 10);
            dgvAccount.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvAccount.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular);

            dgvAccount.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 232, 255);
            dgvAccount.RowsDefaultCellStyle.SelectionForeColor = SystemColors.WindowFrame;

            dgvAccount.AllowUserToResizeRows = false;
            dgvAccount.ReadOnly = true;
            dgvAccount.BorderStyle = BorderStyle.None;

            Graphics g = dgvAccount.CreateGraphics();

            Rectangle rect = new Rectangle(0, 40, dgvAccount.Width, 1);
            g.DrawRectangle(Pens.Red, rect);
            // Load data for account datagridview
            LoadAccount();
            EnableUITextbox();
        }

        public void InitEvents()
        {
            btnAdd.Click += ActionChanged;
            btnEdit.Click += ActionChanged;
            btnDelete.Click += ActionChanged;
            txtFind.TextChanged += TxtFind_TextChanged; ;
            btnSave.Click += ActionChanged;
            btnAccountRefresh.Click += ActionChanged;

            dgvAccount.SelectionChanged += ActionChanged;
            Shared.OnLanguageChange += Shared_OnLanguageChange;
            dgvAccount.RowPostPaint += DgvAccount_RowPostPaint; ;
        }

        private void DgvAccount_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, dgvAccount.Width, 1);
            e.Graphics.FillRectangle(SystemBrushes.ScrollBar, rect);
            if (e.RowIndex != -1)
            {
                RectangleF originalClip = e.Graphics.ClipBounds;
                Rectangle rowRectangle = this.dgvAccount.GetRowDisplayRectangle(e.RowIndex, true);
                e.Graphics.FillRectangle(SystemBrushes.ScrollBar, new Rectangle(rowRectangle.X, rowRectangle.Y, rowRectangle.Width, 1));
            }
        }

        private void TxtFind_TextChanged(object sender, EventArgs e)
        {
            dgvAccount.ClearSelection();
            string userName = txtFind.Text;
            LoadAccount(userName.ToLower());
        }

        private void Shared_OnLanguageChange(object sender,EventArgs e)
        {
            SetLanguage();
        }

        private void ActionChanged(object sender, EventArgs e)
        {
            if (sender == btnAdd)
            {
                _IsCreate = true;
                ClearTextBox();
                EnableUITextbox(true);
                EnableUIAddEdit();
            }
            else if (sender == btnEdit)
            {
                _IsEdit = true;
                EnableUITextbox(true);
                EnableUIAddEdit();
            }
            else if (sender == btnDelete)
            {
                if (_IsCreate || _IsEdit)
                {
                    _IsCreate = false;
                    _IsEdit = false;
                    EnableUITextbox();
                    EnableUIAddEdit();
                    return;
                }
                if (!CheckTextBox())
                {
                    return;
                }
                string message = Lang.AreYouSureYouWantToDeleteUser + txtFullName.Text + "?";
                DialogResult result = MessageBox.Show(message, Lang.Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DeleteUser();
                }
            }
            else if (sender == dgvAccount)
            {
                // fill select item on datagridview to text box
                if (dgvAccount.CurrentRow != null)
                {
                    txtFullName.Text = dgvAccount.CurrentRow.Cells[Lang.FullnameColum].Value.ToString();
                    txtUserName.Text = dgvAccount.CurrentRow.Cells[Lang.UsernameColumn].Value.ToString();
                    txtPassword.Text = dgvAccount.CurrentRow.Cells[Lang.PasswordColumn].Value.ToString();
                    txtRetypePassword.Text = txtPassword.Text;
                    string role = dgvAccount.CurrentRow.Cells[Lang.RoleColumn].Value.ToString();
                    cbxRole.SelectedItem = (role == "Administrator") ? listRole[0] : listRole[1];
                }
            }
            else if (sender == btnSave)
            {
                // Check text box
                if (!CheckTextBox())
                {
                    return;
                }
                if (_IsCreate)
                {
                    AddUser();
                }
                else if (_IsEdit)
                {
                    EditUser();
                }
            }
            else if (sender == btnAccountRefresh)
            {
                txtFind.Text = "";
            }
        }

        List<UserDataModel> _ListUser = new List<UserDataModel>();

        // Load data for account datagridview
        public void LoadAccount(string key = "")
        {
            dgvAccount.Rows.Clear();
            KillThreadLoadData();
            if (_ThreadLoadData == null || !_ThreadLoadData.IsAlive)
            {
                _ThreadLoadData = new Thread(() =>
                {
                    _ListUser.Clear();
                    _ListUser = UserController.LoadAccount(key);
                    int count = _ListUser.Count;
                    string[] user;
                    if (count > 0)
                    {
                        for (int index = 0; index < count; index++)
                        {
                            user = new string[] { String.Format(_ListUser[index].FullName), _ListUser[index].UserName,
                            _ListUser[index].Password, _ListUser[index].RoleName.ToString()};
                            dgvAccount.Invoke(new Action(() =>
                            {
                                dgvAccount.Rows.Add(user);
                            }));
                        }
                    }

                    Invoke(new Action(() =>
                    {
                        dgvAccount.ClearSelection();
                    }));
                    Thread.Sleep(20);
                });
                _ThreadLoadData.IsBackground = true;
                _ThreadLoadData.Start();
            }
        }
        // END Load data for account datagridview

        private void AddUser()
        {
            // Get selected value of role combobox
            int role = int.Parse(cbxRole.SelectedValue.ToString());
            // END
            // Check exist user
            if (UserController.CheckExistUserName(txtUserName.Text) != null)
            {
                MessageBox.Show(Lang.UsernameExist, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (txtPassword.Text.Length < 6)
                {
                    MessageBox.Show(Lang.PasswordAtLeast6Character, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Add account to database
                if (UserController.AddAccount(txtFullName.Text, txtUserName.Text, txtPassword.Text, role))
                {
                    MessageBox.Show(Lang.AddAccountSuccess, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAccount();
                    ClearTextBox();
                    _IsCreate = false;
                    dgvAccount.Enabled = true;
                    EnableUITextbox();
                    EnableUIAddEdit();
                    return;
                }
                else
                {
                    EnableUITextbox();
                    MessageBox.Show(Lang.AddAccountFailed);
                }
            }
        }

        private void DeleteUser()
        {
            // get exist user account
            UserDataModel userData = UserController.CheckExistUserName(txtUserName.Text);
            if (userData == null)
            {
                MessageBox.Show(Lang.UsernameExist, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string userName = txtUserName.Text;
                // prevent delete or edit default account
                if (userName == "Administrator" || userName == "Operator")
                {
                    MessageBox.Show(Lang.DefaultAccountEdited, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                // Delete account
                if (UserController.DeleteAccount(userData.UserName))
                {
                    MessageBox.Show(Lang.DeleteAccountSuccess, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAccount();
                    ClearTextBox();
                    EnableUITextbox();
                }
                else
                {
                    EnableUITextbox();
                    MessageBox.Show(Lang.DeleteAccountFailed, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void EditUser()
        {
            UserDataModel userData = UserController.CheckExistUserName(txtUserName.Text);
            if (userData == null)
            {
                MessageBox.Show(Lang.UsernameExist, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // get user name
                string userName = txtUserName.Text;
                // prevent delete or edit default account
                if (userName == "Administrator" || userName == "Operator")
                {
                    MessageBox.Show(Lang.DefaultAccountEdited, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (txtPassword.Text.Length < 0)
                {
                    MessageBox.Show(Lang.UsernameExist, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // get role value
                int role = int.Parse(cbxRole.SelectedValue.ToString());

                // Edit account
                if (UserController.EditAccount(txtFullName.Text, userData.UserName, txtPassword.Text, role))
                {
                    MessageBox.Show(Lang.EditAccountSuccess, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAccount();
                    ClearTextBox();
                    _IsEdit = false;
                    EnableUITextbox();
                    EnableUIAddEdit();
                }
                else
                {
                    EnableUITextbox();
                    MessageBox.Show(Lang.EditAccountFailed, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void KillThreadLoadData()
        {
            if (_ThreadLoadData != null && _ThreadLoadData.IsAlive)
            {
                _ThreadLoadData.Abort();
                _ThreadLoadData = null;
            }
        }

        // Clear text box
        public void ClearTextBox()
        {
            txtFind.Text = "";
            txtFullName.Text = "";
            txtPassword.Text = "";
            txtRetypePassword.Text = "";
            txtUserName.Text = "";
        }
        // END Clear text box

        // Check text box
        public bool CheckTextBox()
        {
            if (txtFullName.Text == "")
            {
                MessageBox.Show(lbFullname.Text + Lang.TextboxEmpty, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //lblMessage.Text = lbRetypePasword.Text + Lang.TextboxEmpty;
                return false;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show(lbPassword.Text + Lang.TextboxEmpty, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //lblMessage.Text = lbRetypePasword.Text + Lang.TextboxEmpty;
                return false;
            }
            if (txtRetypePassword.Text == "")
            {
                MessageBox.Show(lbRetypePasword.Text + Lang.TextboxEmpty, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //lblMessage.Text = lbRetypePasword.Text + Lang.TextboxEmpty;
                return false;
            }
            if (txtUserName.Text == "")
            {
                MessageBox.Show(lbUsername.Text + Lang.TextboxEmpty, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //lblMessage.Text = lbRetypePasword.Text + Lang.TextboxEmpty;
                return false;
            }
            if (txtPassword.Text != txtRetypePassword.Text)
            {
                MessageBox.Show(Lang.PasswordNotMatch, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //lblMessage.Text = Lang.PasswordNotMatch;
                return false;
            }
            return true;
        }
        // END Check text box

        private void EnableUITextbox(bool isEnable = false)
        {
            txtFullName.Enabled = isEnable;
            txtUserName.Enabled = isEnable;
            txtPassword.Enabled = isEnable;
            txtRetypePassword.Enabled = isEnable;
            cbxRole.Enabled = isEnable;
            if (_IsEdit)
            {
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                txtRetypePassword.Enabled = false;
            }
        }

        private void EnableUIAddEdit()
        {
            if (_IsCreate || _IsEdit)
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
                btnDelete.Image = BarcodeVerificationSystem.Properties.Resources.icon_failed_181;
                dgvAccount.Enabled = false;
            }
            else
            {
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnSave.Enabled = false;
                btnDelete.Image = BarcodeVerificationSystem.Properties.Resources.icon_delete_181;
                dgvAccount.Enabled = true;
            }
        }
    }

    public class Role
    {
        string _RoleName;
        int _Value;

        public string RoleName
        {
            get
            {
                return _RoleName;
            }

            set
            {
                _RoleName = value;
            }
        }

        public int Value
        {
            get
            {
                return _Value;
            }

            set
            {
                _Value = value;
            }
        }
    }
}
