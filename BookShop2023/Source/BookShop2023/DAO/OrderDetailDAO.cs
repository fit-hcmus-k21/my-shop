using BookShop2023.DTO;
using ProjectMyShop.BUS;
using ProjectMyShop.DTO;
using ProjectMyShop.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
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
            var sql = "delete from OrderDetail where OrderID = @OrderID and ProductID = @ProductID ";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);

            sqlCommand.Parameters.AddWithValue("@OrderID", detail.OrderID);
            sqlCommand.Parameters.AddWithValue("@ProductID", detail.ProductID);

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

        public void DeleteOrderDetailList(int orderID)
        {
            var sql = "delete from OrderDetail where OrderID = @OrderID ";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);

            sqlCommand.Parameters.AddWithValue("@OrderID", orderID);

            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Deleted {orderID} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Deleted {orderID} Fail: " + ex.Message);
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

        public List<OrderDetail> GetListByOrderID(int orderID)
        {
            string sql = "select * from OrderDetail WHERE OrderID = @orderID";

            var command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.AddWithValue("@orderID", orderID);

            var reader = command.ExecuteReader();

            var result = new List<OrderDetail>();

            while (reader.Read())
            {
                var OrderID = reader.GetInt32("OrderID");
                var ProductID = reader.GetInt32("ProductID");
                var Quantity = reader.GetInt32("Quantity");
                var Price = Convert.ToInt32( reader["Price"]);
                var Total = Convert.ToInt32(reader["Total"]);




                OrderDetail _order = new OrderDetail()
                {
                    OrderID = OrderID,
                    ProductID = ProductID,
                    Quantity = Quantity,
                    Price = Price,
                    Total = Total
                };

                result.Add(_order);
            }

            reader.Close();
            return result;
        }
    }
}
