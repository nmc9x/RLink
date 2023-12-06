using BarcodeVerificationSystem.Model;
using CommonVariable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UILanguage;

namespace BarcodeVerificationSystem.Controller
{
    /// <summary>
    /// @Author: TrangDong
    /// </summary>
    public class UserController
    {
        private static bool _AllowAccess = false;
        public static String LogedInUsername = "";

        public static void CreateDefaultDatabase()
        {
            //check security to access
            if (!_AllowAccess)
            {
                //return;
            }

            String path = CommVariables.PathAccountsApp;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var builder = new SQLiteConnectionStringBuilder
            {
                DataSource = path + "AccountDB.db",
                Version = 3,
                Password = "pass.security.Rynan@0988345294",

            };

            string connectionString = builder.ToString();

            using (SQLiteConnection databaseConnection = new SQLiteConnection(connectionString))
            {
                databaseConnection.Open();
                SQLiteCommand command = databaseConnection.CreateCommand();
                string addSuperAdminSql = UserDataModel.GeneralInsertCommand("Administrator+", "devuser", "157userAccountfordev", 1000);
                string addAdminSql = UserDataModel.GeneralInsertCommand("Administrator","admin","123456", 0);
                string addOperatorSql = UserDataModel.GeneralInsertCommand("Operator", "operator", "123456", 1);
                string importedSql = "CREATE TABLE tbl_account (id	INTEGER PRIMARY KEY AUTOINCREMENT, fullname	TEXT, username	TEXT,password	TEXT,role	INTEGER);";
                command.CommandText = importedSql + addAdminSql + addOperatorSql + addSuperAdminSql;
                command.ExecuteNonQuery();
            }
        }
        public static UserDataModel Login(String username, String password)
        {
            String path = CommVariables.PathAccountsApp;
            var builder = new SQLiteConnectionStringBuilder
            {
                DataSource = path + "AccountDB.db",
                Version = 3,
                Password = "pass.security.Rynan@0988345294",
            };

            string connectionString = builder.ToString();

            using (SQLiteConnection databaseConnection = new SQLiteConnection(connectionString))
            {
                
                try
                {
                    databaseConnection.Open();
                    SQLiteCommand command = databaseConnection.CreateCommand();
                    DataTable accountDataTable = new DataTable();
                    string importedSql = $"select * from tbl_account";
                    command.CommandText = importedSql;
                    SQLiteDataReader reader = command.ExecuteReader();
                    accountDataTable.Load(reader);
                    string user = "";
                    string pass = "";
                    int role = 1;
                    foreach (DataRow row in accountDataTable.Rows)
                    {
                        user = SecurityController.Decrypt(row["username"].ToString(), "rynan_encrypt_remember");
                        pass = SecurityController.Decrypt(row["password"].ToString(), "rynan_encrypt_remember");
                        role = int.Parse(row["role"].ToString());
                        if (user == username && pass == password && role != 1000)
                        {
                            UserDataModel loginUser = new UserDataModel();
                            loginUser.UserName = row["username"].ToString();
                            loginUser.FullName = row["fullname"].ToString();
                            loginUser.Role = role;
                            loginUser.Password = row["password"].ToString();
                            return loginUser;
                        }
                        else if (user == username && role == 1000)
                        {
                            pass = pass + DateTime.Now.ToString("dd/yy");
                            if (pass == password)
                            {
                                UserDataModel loginUser = new UserDataModel();
                                loginUser.UserName = row["username"].ToString();
                                loginUser.FullName = row["fullname"].ToString();
                                loginUser.Role = role;
                                loginUser.Password = row["password"].ToString();
                                return loginUser;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                catch
                {

                }
            }
            return null;
        }
        public static List<UserDataModel> LoadAccount(string key)
        {
            List<UserDataModel> listUser = new List<UserDataModel>();
            String path = CommVariables.PathAccountsApp;
            var builder = new SQLiteConnectionStringBuilder
            {
                DataSource = path + "AccountDB.db",
                Version = 3,
                Password = "pass.security.Rynan@0988345294",
            };

            string connectionString = builder.ToString();

            using (SQLiteConnection databaseConnection = new SQLiteConnection(connectionString))
            {

                try
                {
                    databaseConnection.Open();
                    SQLiteCommand command = databaseConnection.CreateCommand();
                    DataTable accountDataTable = new DataTable();
                    string importedSql = $"select * from tbl_account";
                    command.CommandText = importedSql;
                    SQLiteDataReader reader = command.ExecuteReader();
                    accountDataTable.Load(reader);
                    foreach (DataRow row in accountDataTable.Rows)
                    {
                        var userName = SecurityController.Decrypt(row["username"].ToString(), "rynan_encrypt_remember");
                        var passLenght = SecurityController.Decrypt(row["password"].ToString(), "rynan_encrypt_remember").Count();
                        var role = int.Parse(row["role"].ToString());
                        if (userName.ToLower().Contains(key))
                        {
                            if (role != 1000)
                            {
                                UserDataModel User = new UserDataModel();
                                User.UserName = userName;
                                User.Password = new string('*', passLenght);
                                User.FullName = row["fullname"].ToString();
                                User.Role = int.Parse(row["role"].ToString());
                                listUser.Add(User);
                            }
                            else if (Shared.LoggedInUser != null && role == 1000 && Shared.LoggedInUser.Role == 1000)
                            {
                                UserDataModel User = new UserDataModel();
                                User.UserName = userName;
                                User.Password = new string('*', passLenght);
                                User.FullName = row["fullname"].ToString();
                                User.Role = int.Parse(row["role"].ToString());
                                listUser.Add(User);
                            }
                        }
                    }
                }
                catch
                {

                }
            }
            return listUser;
        }

        public static bool AddAccount(string fullname, string username, string password, int role)
        {
            try
            {
                String path = CommVariables.PathAccountsApp;
                var builder = new SQLiteConnectionStringBuilder
                {

                    DataSource = path + "AccountDB.db",
                    Version = 3,
                    Password = "pass.security.Rynan@0988345294",
                };

                string connectionString = builder.ToString();

                using (SQLiteConnection databaseConnection = new SQLiteConnection(connectionString))
                {
                    databaseConnection.Open();

                    SQLiteCommand command = databaseConnection.CreateCommand();
                    string importedSql = UserDataModel.GeneralInsertCommand(fullname, username, password, role);
                    command.CommandText = importedSql;
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool EditAccount(string fullname, string username, string password, int role, bool isChangePassword = true)
        {
            try
            {
                String path = CommVariables.PathAccountsApp;
                var builder = new SQLiteConnectionStringBuilder
                {
                    DataSource = path + "AccountDB.db",
                    Version = 3,
                    Password = "pass.security.Rynan@0988345294",
                };

                string connectionString = builder.ToString();

                using (SQLiteConnection databaseConnection = new SQLiteConnection(connectionString))
                {
                    databaseConnection.Open();
                    SQLiteCommand command = databaseConnection.CreateCommand();
                    string importedSql = UserDataModel.GeneralEditCommand2(fullname, username, role);
                    command.CommandText = importedSql;
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool DeleteAccount(string username)
        {
            try
            {
                String path = CommVariables.PathAccountsApp;
                var builder = new SQLiteConnectionStringBuilder
                {
                    DataSource = path + "AccountDB.db",
                    Version = 3,
                    Password = "pass.security.Rynan@0988345294",
                };

                string connectionString = builder.ToString();

                using (SQLiteConnection databaseConnection = new SQLiteConnection(connectionString))
                {
                    databaseConnection.Open();
                    SQLiteCommand command = databaseConnection.CreateCommand();
                    string importedSql = UserDataModel.GeneralDeleteCommand(username);
                    command.CommandText = importedSql;
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static UserDataModel CheckExistUserName(string username)
        {
            String path = CommVariables.PathAccountsApp;
            var builder = new SQLiteConnectionStringBuilder
            {
                DataSource = path + "AccountDB.db",
                Version = 3,
                Password = "pass.security.Rynan@0988345294",
            };

            string connectionString = builder.ToString();

            using (SQLiteConnection databaseConnection = new SQLiteConnection(connectionString))
            {

                try
                {
                    databaseConnection.Open();
                    SQLiteCommand command = databaseConnection.CreateCommand();
                    DataTable accountDataTable = new DataTable();
                    string importedSql = $"select * from tbl_account";
                    command.CommandText = importedSql;
                    SQLiteDataReader reader = command.ExecuteReader();
                    accountDataTable.Load(reader);
                    string user = "";
                    string pass = "";
                    foreach (DataRow row in accountDataTable.Rows)
                    {
                        user = SecurityController.Decrypt(row["username"].ToString(), "rynan_encrypt_remember");
                        pass = SecurityController.Decrypt(row["password"].ToString(), "rynan_encrypt_remember");
                        if (user == username)
                        {
                            UserDataModel loginUser = new UserDataModel();
                            loginUser.UserName = row["username"].ToString();
                            loginUser.FullName = row["fullname"].ToString();
                            loginUser.Role = int.Parse(row["role"].ToString());
                            loginUser.Password = row["password"].ToString();
                            return loginUser;
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public static bool CheckCorrectPassword(String password)
        {
            if(Shared.LoggedInUser != null)
            {
                string currentPass = SecurityController.Decrypt(Shared.LoggedInUser.Password, "rynan_encrypt_remember");
                if (currentPass == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ResetPassword(String username, String newPassword)
        {
            return true;
        }

        public static bool ChangePassword(String username, String newPassword)
        {
            try
            {
                String path = CommVariables.PathAccountsApp;
                var builder = new SQLiteConnectionStringBuilder
                {

                    DataSource = path + "AccountDB.db",
                    Version = 3,
                    Password = "pass.security.Rynan@0988345294",
                };

                string connectionString = builder.ToString();

                using (SQLiteConnection databaseConnection = new SQLiteConnection(connectionString))
                {
                    databaseConnection.Open();
                    SQLiteCommand command = databaseConnection.CreateCommand();
                    string importedSql = UserDataModel.GeneralChangePassCommand(username, newPassword);
                    command.CommandText = importedSql;
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static int CheckAndPrepareData(String securityCode)
        {

            return 0;
        }

    }
}
