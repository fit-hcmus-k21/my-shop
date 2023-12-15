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
                if (_connection == null)
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


    }
}
