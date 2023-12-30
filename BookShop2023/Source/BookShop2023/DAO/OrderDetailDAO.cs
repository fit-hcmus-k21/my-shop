using BookShop2023.DTO;
using ProjectMyShop.DTO;
using ProjectMyShop.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop2023.DAO
{
    internal class OrderDetailDAO : DB
    {
        public void AddOrderDetail(OrderDetail detail)
        {
            var sql = "insert into OrderDetail(OrderID, ProductID, Quantity) " +
                "values (@OrderID, @ProductID, @Quantity)";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);

            sqlCommand.Parameters.AddWithValue("@OrderID", detail.OrderID);
            sqlCommand.Parameters.AddWithValue("@ProductID", detail.ProductID);
            sqlCommand.Parameters.AddWithValue("@Quantity", detail.Quantity);

            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Inserted {detail.OrderID} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Inserted {detail.OrderID} Fail: " + ex.Message);
            }
        }
        public void UpdateOrderDetail(int oldProductID, OrderDetail detail)
        {
            var sql = "update OrderDetail set Quantity = @Quantity, ProductID = @ProductID where OrderID = @OrderID and ProductID = @oldProductID";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);

            sqlCommand.Parameters.AddWithValue("@OrderID", detail.OrderID);
            sqlCommand.Parameters.AddWithValue("@ProductID", detail.ProductID);
            sqlCommand.Parameters.AddWithValue("@oldProductID", oldProductID);
            sqlCommand.Parameters.AddWithValue("@Quantity", detail.Quantity);

            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Updated {detail.OrderID} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Updated {detail.OrderID} Fail: " + ex.Message);
            }
        }
        public void DeleteOrderDetail(OrderDetail detail)
        {
            var sql = "delete from OrderDetail where OrderID = @OrderID and ProductID = @ProductID and Quantity = @Quantity";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);

            sqlCommand.Parameters.AddWithValue("@OrderID", detail.OrderID);
            sqlCommand.Parameters.AddWithValue("@ProductID", detail.ProductID);
            sqlCommand.Parameters.AddWithValue("@Quantity", detail.Quantity);

            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Deleted {detail.OrderID} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Deleted {detail.OrderID} Fail: " + ex.Message);
            }
        }



        public void InsertList(List<OrderDetail> list)
        {
            foreach (OrderDetail item in list)
            {
                var sql = "";

                sql = "insert into OrderDetail(OrderID, ProductID, Price, Quantity, Total) " +
                    "values (@OrderID, @ProductID, @Price, @Quantity, @Total)";

                SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);

                sqlCommand.Parameters.AddWithValue("@OrderID", item.OrderID);
                sqlCommand.Parameters.AddWithValue("@ProductID", item.ProductID);
                sqlCommand.Parameters.AddWithValue("@Price", item.Price);
                sqlCommand.Parameters.AddWithValue("@Quantity", item.Quantity);
                sqlCommand.Parameters.AddWithValue("@Total", item.Total);

                var queryProduct = "update Product set Quantity = Quantity - @Quantity where ID = @ID;";
                SqlCommand sqlCommandProduct = new SqlCommand(queryProduct, DB.Instance.Connection);

                sqlCommandProduct.Parameters.AddWithValue("@Quantity", item.Quantity);
                sqlCommandProduct.Parameters.AddWithValue("@ID", item.ProductID);


                try
                {
                    sqlCommand.ExecuteNonQuery();
                    sqlCommandProduct.ExecuteNonQuery();
                    System.Diagnostics.Debug.WriteLine($" Success");

                }
                catch (Exception ex)
                {

                    System.Diagnostics.Debug.WriteLine($"Fail: " + ex.Message);
                }
            }

        }
    }
}
