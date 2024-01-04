using ProjectMyShop.DTO;
using ProjectMyShop.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop2023.DAO
{
    internal class VoucherDAO : DB
    {
        public Voucher GetVoucherById(int id)
        {
            var sql = "select * from Voucher where ID = @ID";
            var command = new SqlCommand(sql, DB.Instance.Connection);

            command.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            var reader = command.ExecuteReader();

            Voucher? result = null;

            if (reader.Read()) // ORM - Object relational mapping
            {
                var voucherId = (int)reader["ID"];
                var DisplayText = (string)reader["DisplayText"];
                var DateStart =  DateOnly.Parse(DateTime.Parse(reader["StartDate"].ToString()).Date.ToShortDateString());
                var DateEnd = DateOnly.Parse(DateTime.Parse(reader["EndDate"].ToString()).Date.ToShortDateString());
                var PercentOff = (int)reader["PercentOff"];
                var MinCost = (int)reader["MinCost"];
                var Description = (string)reader["Description"];

                result = new Voucher()
                {
                    ID = voucherId,
                    DisplayText = DisplayText,
                    StartDate = DateStart,
                    EndDate = DateEnd,
                    PercentOff = PercentOff,
                    MinCost = MinCost,
                    Description = Description,
                };
            }

            reader.Close();
            return result;
        }

        public List<Voucher> getVoucherList()
        {
            var sql = "select * from Voucher;";

            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();

            var resultList = new List<Voucher>();
            while (reader.Read())
            {
                var voucherId = (int)reader["ID"];
                var DisplayText = (string)reader["DisplayText"];
                var DateStart = DateOnly.Parse(DateTime.Parse(reader["StartDate"].ToString()).Date.ToShortDateString());
                var DateEnd = DateOnly.Parse(DateTime.Parse(reader["EndDate"].ToString()).Date.ToShortDateString());
                var PercentOff = (int)reader["PercentOff"];
                var MinCost = (int)reader["MinCost"];
                var Description = (string)reader["Description"];



                Voucher v = new Voucher()
                {
                    ID = voucherId,
                    DisplayText = DisplayText,
                    StartDate = DateStart,
                    EndDate = DateEnd,
                    PercentOff = PercentOff,
                    MinCost = MinCost,
                    Description = Description,
                };

                resultList.Add(v);
            }


            reader.Close();
            return resultList;
        }
        public void AddVoucher(Voucher voucher)
        {
            var sql = "";

            sql = "insert into Voucher(StartDate, EndDate, PercentOff, DisplayText, MinCost, Description) " +
                "values (@StartDate, @EndDate, @PercentOff, @DisplayText, @MinCost, @Description)";

            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);

            sqlCommand.Parameters.AddWithValue("@StartDate", DateTime.Parse(voucher.StartDate.ToString()));
            sqlCommand.Parameters.AddWithValue("@EndDate", DateTime.Parse(voucher.EndDate.ToString()));
            sqlCommand.Parameters.AddWithValue("@PercentOff", voucher.PercentOff);
            sqlCommand.Parameters.AddWithValue("@DisplayText", voucher.DisplayText);
            sqlCommand.Parameters.AddWithValue("@MinCost", voucher.MinCost);
            sqlCommand.Parameters.AddWithValue("@Description", voucher.Description);



            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Inserted {voucher.ID} OK");

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine($"Inserted {voucher.ID} Fail: " + ex.Message);
            }


        }
        public int GetLastestInsertID()
        {
            string sql = "select ident_current('Voucher')";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);
            var result = sqlCommand.ExecuteScalar();
            System.Diagnostics.Debug.WriteLine(result);
            if (result == DBNull.Value)
            {
                return 0;
            }
            return System.Convert.ToInt32(result);
        }



        public void updateVoucher(int ID, Voucher voucher)
        {
            string sql;

            sql = "update Voucher set StartDate = @StartDate, EndDate = @EndDate, PercentOff = @PercentOff, DisplayText = @DisplayText, MinCost=@MinCost, Description=@Description  where ID = @ID";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);
            sqlCommand.Parameters.AddWithValue("@ID", ID);
            sqlCommand.Parameters.AddWithValue("@StartDate", DateTime.Parse(voucher.StartDate.ToString()));
            sqlCommand.Parameters.AddWithValue("@EndDate", DateTime.Parse(voucher.EndDate.ToString()));
            sqlCommand.Parameters.AddWithValue("@PercentOff", voucher.PercentOff);
            sqlCommand.Parameters.AddWithValue("@DisplayText", voucher.DisplayText);
            sqlCommand.Parameters.AddWithValue("@MinCost", voucher.MinCost);
            sqlCommand.Parameters.AddWithValue("@Description", voucher.Description);






            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Updated {voucher.ID} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Updated {voucher.ID} Fail: " + ex.Message);
            }
        }

        public void removeVoucher(int ID)
        {
            string sql = "delete from Voucher where ID = @ID";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);
            sqlCommand.Parameters.AddWithValue("@ID", ID);
            try
            {
                sqlCommand.ExecuteNonQuery();
                sql = "delete from Orders where VoucherID = @ID";
                sqlCommand.Parameters.AddWithValue("@ID", ID);
                sqlCommand.ExecuteNonQuery();

                System.Diagnostics.Debug.WriteLine($"Deleted {ID} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Deleted {ID} Fail: " + ex.Message);
            }
        }
    }
}
