using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonVariable
{
    /// <summary>
    /// @Author: TrangDong
    /// Variables use for all project
    /// </summary>
    public class CommVariables
    {
        private static string PathProgramData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        public static string PathProgramDataApp
        {
            get
            {
                return PathProgramData + "\\R-Link\\";
            }
        }

        public static string PathSettingsApp
        {
            get
            {
                return PathProgramDataApp + "Settings\\";
            }
        }

        public static string PathHistoriesApp
        {
            get
            {
                return PathProgramDataApp + "Histories\\";
            }
        }

        public static string PathAccountsApp
        {
            get
            {
                return PathProgramDataApp + "Accounts\\";
            }
        }

        public static string PathJobsApp
        {
            get
            {
                return PathProgramDataApp + "Jobs\\";
            }
        }

        public static string PathWarehouseInput
        {
            get
            {
                return PathProgramDataApp + "WarehouseInput\\";
            }
        }
        public static string PathCheckedResult
        {
            get
            {
                return PathProgramDataApp + "CheckedResult\\";
            }
        }
        public static string PathPrintedResponse
        {
            get
            {
                return PathProgramDataApp + "PrintedResponse\\";
            }
        }
        public static string PathAllowPC
        {
            get
            {
                return PathProgramDataApp + "RConfig\\";
            }
        }

    }
}
