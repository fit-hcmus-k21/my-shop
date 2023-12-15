using ProjectMyShop.Config;
using ProjectMyShop.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMyShop.BUS;

namespace ProjectMyShop.DAO
{
     public class AccountDAO : DB
    {

        public bool Authenticate(string username, string password)
        {
            // Table Account
            var sql = "SELECT * FROM Account WHERE Username = @Username AND Password = @Password";

            // Tạo một đối tượng SqlCommand
            var command = new SqlCommand(sql, DB.Instance.Connection);

            // Thêm tham số cho Username
            command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username;

            // Thêm tham số cho Password
            command.Parameters.Add("@Password", SqlDbType.NVarChar, 255).Value = password;

            // Mở DataReader để thực hiện truy vấn
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    #region Lấy dữ liệu User
                    while (reader.Read())
                    {
                        // Đọc dữ liệu từ mỗi dòng kết quả
                        var Name = reader["Name"].ToString();
                        var Role = reader["Role"].ToString();

                        AccountBUS.Instance().SetUserInfo(Name, Role);
                    }
                    #endregion
                    return true;
                }
                else
                {
                    // Không có dòng nào trong kết quả truy vấn
                    return false;
                }
            }

        }
    }
}
