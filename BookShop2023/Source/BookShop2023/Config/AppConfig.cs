using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMyShop.Config
{
    internal class AppConfig
    {
        #region Khai báo 
        public static string  AuthType = "AuthType";
        public static string  Server = "Server";
        public static string  Database = "Database";
        public static string  Username = "Username";
        public static string  Password = "Password";
        public static string  Entropy = "Entropy";

        public static string  LoginStatus = "LoginStatus";
        public static string  LoginMode = "LoginMode";

        public static string  NumberProductPerPage = "NumberProductPerPage";
        public static string  OpenLastWindow = "OpenLastWindow";
        public static string  LastWindow = "LastWindow";
        public static string UserAccount = "UserAccount";
        #endregion

        #region Enums
        public enum AuthTypeEnum
        {
            WindowsAuthentication = 0,
            SqlServerAuthentication = 1,
        } 

        public enum LoginStatusEnum
        {
            LoggedOut = 0,
            LoggedIn = 1,
        }

        public enum LoginModeEnum
        {
            Admin = 1,
            Staff = 2
        }
        #endregion

        #region Getter and Setter for AppSettings
        public static string ? GetValue(string key)
        {
            ConfigurationManager.RefreshSection("appSettings");

            string? value = ConfigurationManager
                .AppSettings[key];
            return value;
        }

        public static void SetValue(string key, string value)
        {
            var configFile = ConfigurationManager
            .OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            settings[key].Value = value;

            configFile.Save(ConfigurationSaveMode.Minimal);
        }
        #endregion

        #region ConnectionString
        public static string ? ConnectionString()
        {
            string result = "";

            var builder = new SqlConnectionStringBuilder();
            string? server = AppConfig.GetValue(AppConfig.Server);
            string? database = AppConfig.GetValue(AppConfig.Database);
            string? username = AppConfig.GetValue(AppConfig.Username);
            string? password = AppConfig.GetValue(AppConfig.Password);
            string? lastWindow = AppConfig.GetValue(AppConfig.LastWindow);
            string? numProductPerPage = AppConfig.GetValue(AppConfig.NumberProductPerPage);
            string? authType = AppConfig.GetValue(AppConfig.AuthType);   // default is Windows Auhthentication

            builder.DataSource = server;
            builder.InitialCatalog = database;

            if (authType != null && authType.Equals(((int) AuthTypeEnum.SqlServerAuthentication).ToString()))
            {
                builder.UserID = Username;
                builder.Password = Password;
            } else
            {
                builder.IntegratedSecurity = true;
            }

            builder.TrustServerCertificate = true;
            builder.ConnectTimeout = 3; // s

            result = builder.ToString();
            return result;
        }
        #endregion


        #region Setter and Getter password
        public static String GetPassword()
        {
            var cypherText = AppConfig.GetValue(AppConfig.Password);
            var cypherTextInBytes = Convert.FromBase64String(cypherText!);

            var entropyText = AppConfig.GetValue(AppConfig.Entropy);
            var entropyTextInBytes = Convert.FromBase64String(entropyText);

            var passwordInBytes = ProtectedData.Unprotect(cypherTextInBytes,
                entropyTextInBytes, DataProtectionScope.CurrentUser);
            var password = Encoding.UTF8.GetString(passwordInBytes);

            return password;
        }

        public static void SetPassword(string password)
        {
            var passwordInBytes = Encoding.UTF8.GetBytes(password);

            var entropy = new byte[20];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(entropy);
            }
            var entropyBase64 = Convert.ToBase64String(entropy);

            var cypherText = ProtectedData.Protect(passwordInBytes, entropy,
                DataProtectionScope.CurrentUser);
            var cypherTextBase64 = Convert.ToBase64String(cypherText);

            AppConfig.SetValue(AppConfig.Password, cypherTextBase64);
            AppConfig.SetValue(AppConfig.Entropy, entropyBase64);
        }
        #endregion



    }
}
