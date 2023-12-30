using BookShop2023.BUS;
using BookShop2023.DTO;
using ProjectMyShop.BUS;
using ProjectMyShop.DTO;
using ProjectMyShop.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMyShop.DAO
{
    internal class OrderDAO: DB
    {
  

    List<OrderDetail> GetOrderDetail(int orderID)
        {
            string sql = "select * from OrderDetail WHERE OrderID = @orderID";

            var command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.AddWithValue("@orderID", orderID);
            
            var reader = command.ExecuteReader();

            var result = new List<OrderDetail>();

            var _ProductBUS = new ProductBUS();
            while (reader.Read())
            {
                var OrderID = reader.GetInt32("OrderID");
                var ProductID = reader.GetInt32("ProductID");
                var Quantity = reader.GetInt32("Quantity");

                var Product = _ProductBUS.getProductByID(ProductID);

                OrderDetail _order = new OrderDetail()
                {
                    OrderID = OrderID,
                    ProductID = ProductID,
                    Quantity = Quantity
                };

                result.Add(_order);
            }

            reader.Close();
            return result;
        }

        Order ORMapping(SqlDataReader reader)
        {
            var ID = (int)reader["ID"];
            var CreatedAt = DateOnly.Parse(DateTime.Parse(reader["CreatedAt"].ToString()).Date.ToShortDateString());
            var Status = (System.Int32)reader["Status"];
            // var VoucherID = (Voucher)reader["VoucherID"];


            Order order = new Order()
            {
                ID = ID,
                CreatedAt = CreatedAt,
                Status = Status,
            };
            return order;
        }

        List<Order> Select(SqlCommand command)
        {
            var reader = command.ExecuteReader();

            var result = new List<Order>();

            while (reader.Read())
            {
                result.Add(ORMapping(reader));
            }
            reader.Close();

            

            return result;
        } 

        public List<Order> GetOrders(int offset, int size)
        {
            string sql = "select * from Orders " +
                "Order by CreatedAt DESC, ID ASC " +
                "offset @Off rows " +
                "fetch first @Size rows only";

            var command = new SqlCommand(sql, DB.Instance.Connection);

            command.Parameters.AddWithValue("@Off", offset);
            command.Parameters.AddWithValue("@Size", size);

            return Select(command);
        }

        public List<Order> GetAllOrders()
        {
            string sql = "select * from Orders" +
                "Order by CreatedAt DESC, ID ASC ";

            var command = new SqlCommand(sql, DB.Instance.Connection);
            return Select(command);
        }

        internal List<Order> GetAllOrdersByDate(DateTime FromDate, DateTime ToDate)
        {
            string sql = "select * from Orders " +
                "Where CreatedAt >= @fromDate AND CreatedAt <= @toDate";
            var command = new SqlCommand(sql, DB.Instance.Connection);

            command.Parameters.AddWithValue("@fromDate", DateTime.Parse(FromDate.ToString()));
            command.Parameters.AddWithValue("@toDate", DateTime.Parse(ToDate.ToString()));

            return Select(command);
        }


        public List<Order> GetOrdersByDate(int offset, int size, DateTime fromDate, DateTime toDate)
        {
            string sql = "select * from Orders " +
                "Where CreatedAt >= @fromDate AND CreatedAt <= @toDate" +
                "Order by CreatedAt DESC, ID ASC " +
                "offset @Off rows " +
                "fetch first @Size rows only";

            var command = new SqlCommand(sql, DB.Instance.Connection);

            command.Parameters.AddWithValue("@fromDate", DateTime.Parse(fromDate.ToString()));
            command.Parameters.AddWithValue("@toDate", DateTime.Parse(toDate.ToString()));
            command.Parameters.AddWithValue("@Off", offset);
            command.Parameters.AddWithValue("@Size", size);

            return Select(command);
        }

 
        public void AddOrder(Order order)
        {

            // ID Auto Increment
            var sql = "insert into Orders(CustomerID, CreatedAt, Status, FinalTotal) " +
                "values (@CustomerID, @CreatedAt, @Status, @FinalTotal)"; 
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);

            sqlCommand.Parameters.AddWithValue("@CustomerID", order.CustomerID);
            sqlCommand.Parameters.AddWithValue("@CreatedAt", DateTime.Parse(order.CreatedAt.ToString()));
            sqlCommand.Parameters.AddWithValue("@Status", OrderStatusEnumBUS.GetValueKeyNew());
            //sqlCommand.Parameters.AddWithValue("@Voucher", order.VoucherID);
            sqlCommand.Parameters.AddWithValue("@FinalTotal", order.FinalTotal);


            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Inserted {order.ID} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Inserted {order.ID} Fail: " + ex.Message);
            }
        }

        public int GetLastestInsertID()
        {
            string sql = "select ident_current('Orders')";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);
            var result = sqlCommand.ExecuteScalar();
            System.Diagnostics.Debug.WriteLine(result);
            if (result == DBNull.Value)
            {
                return 0;
            }
            return System.Convert.ToInt32(result);
        }
        public void UpdateOrder(int orderID,Order order)
        {
            var sql = "update Orders " +
                "SET CustomerID = @CustomerID, CreatedAt = @CreatedAt, Status =  @Status, VoucherID = @VoucherID, FinalTotal = @FinalTotal " +
                "where ID = @OrderID";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);

            sqlCommand.Parameters.AddWithValue("@OrderID", orderID);
            sqlCommand.Parameters.AddWithValue("@CustomerID", order.CustomerID);
            sqlCommand.Parameters.AddWithValue("@CreatedAt", DateTime.Parse(order.CreatedAt.ToString()));
            sqlCommand.Parameters.AddWithValue("@Status", order.Status);
            sqlCommand.Parameters.AddWithValue("@VoucherID", order.VoucherID);
            sqlCommand.Parameters.AddWithValue("@FinalTotal", order.FinalTotal);


            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Updated {orderID} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Updated {orderID} Fail: " + ex.Message);
            }
        }

        public void DeleteOrder(int orderID)
        {
            var sql = "delete from Orders where ID = @OrderID";
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

        public int CountOrders()
        {
            string sql = "select count(*) as c from Orders";
            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();

            int result = 0;

            if (reader.Read())
            {
                result = (int)reader["c"];
            }
            reader.Close();
            return result;
        }

        public int CountOrderByWeek()
        {
            string sql = "select count(*) as week from Orders where datediff(day, CreatedAt, GETDATE()) < 7";
            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();

            int result = 0;

            if (reader.Read())
            {
                result = (int)reader["week"];
            }
            reader.Close();
            return result;
        }
        public int CountOrderByMonth()
        {
            string sql = "select count(*) as month from Orders where datediff(day, CreatedAt, GETDATE()) < 30";
            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();

            int result = 0;

            if (reader.Read())
            {
                result = (int)reader["month"];
            }
            reader.Close();
            return result;
        }
    }
}
