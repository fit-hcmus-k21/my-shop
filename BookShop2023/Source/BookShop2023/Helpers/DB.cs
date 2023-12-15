using ProjectMyShop.Config;
using ProjectMyShop.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static ProjectMyShop.Config.AppConfig;

namespace ProjectMyShop.Helpers
{
    public class DB
    {

        private static DB? _instance = null;
        private SqlConnection _connection = null;
        public string ConnectionString { get; set; } = "";

        public SqlConnection? Connection
        {
            get
            {
                if (_connection == null || _connection.State != ConnectionState.Open)
                {
;                   Connect();
                }



                return _connection;
            }
        }

        public static DB Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DB();
                }

                return _instance;
            }
        }


        /// Kiểm tra xem có thể kết nối hay không
        public bool CanConnect()
        {
            bool result = true;

            ConnectionString = AppConfig.ConnectionString();
            if (ConnectionString == null || ConnectionString.Equals(""))
            {
                MessageBox.Show("ConnectionString is empty ! Cannot connect to db.");
                return false;
            }

            _connection = new SqlConnection(ConnectionString);

            try
            {
                _connection.Open();
                _connection.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message); 
                result = false;
            }
            return result;
        }

        public void Connect()
        {
            ConnectionString = AppConfig.ConnectionString();
            if (ConnectionString == null || ConnectionString.Equals("")) 
            {
                MessageBox.Show("ConnectionString is empty ! Cannot connect to db.");
                return;
            }

            _connection = new SqlConnection(ConnectionString);
            _connection.Open();
        }

        public void Close()
        {
            _connection.Close();
        }


        #region Hàm bổ trợ cho chức năng test connection của settings
        public static bool TestConnection(string server, string db, int authType, string username, string password)
        {

            var builder = new SqlConnectionStringBuilder();

            builder.DataSource = server;
            builder.InitialCatalog = db;


            if ( authType == ((int)AuthTypeEnum.SqlServerAuthentication))
            {
                //MessageBox.Show("Auth type: sql server !, username, password : " + username + " " + password);
                builder.UserID = username;
                builder.Password = password;
            }
            else
            {
                builder.IntegratedSecurity = true;
            }

            builder.TrustServerCertificate = true;
            builder.ConnectTimeout = 3; // s
            builder.Pooling = false;


            bool result = true;

            using (SqlConnection connection = new SqlConnection(builder.ToString()))
            {
                try
                {
                    connection.Open();
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);

                    // Kiểm tra mã lỗi để xác định có phải là lỗi đăng nhập không thành công không
                    if (ex.Number == 18456) // Mã lỗi 18456 là lỗi đăng nhập không thành công
                    {
                        MessageBox.Show("Login failed. Please check your username and password.");
                    }

                    result = false;
                }
            }

            return result;
        }

        #endregion

    }
}
