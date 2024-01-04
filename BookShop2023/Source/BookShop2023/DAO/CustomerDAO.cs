using BookShop2023.DTO;
using ProjectMyShop.DTO;
using ProjectMyShop.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BookShop2023.DAO
{
    internal class CustomerDAO : DB
    {
        private List<Customer> _customerList;

        public List<Customer> loadAll()
        {
            _customerList = new List<Customer>();

            var sql = "select * from Customer;";

            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Customer customer = new Customer()
                {
                    ID = (int) reader["ID"],
                    Name = (string) reader["Name"],
                    Address = (string) reader["Address"],
                    PhoneNumber = (string) reader["PhoneNumber"],
                };

                _customerList.Add(customer);
            }


            reader.Close();
            return _customerList;
        }

        public List<CustomerDataGrid> loadAllCustomerDataGrid()
        {
            List<CustomerDataGrid> _customerList = new List<CustomerDataGrid>();

            var sql = "select cust.ID, cust.Name, cust.Address, cust.PhoneNumber, count(distinct o.ID) as TotalOrders, SUM(ISNULL(od.Total, 0)) AS TotalCost, count(distinct od.ProductID) as TotalProducts " +
                "FROM Customer AS cust\r\n" +
                "LEFT JOIN Orders AS o ON cust.ID = o.CustomerID\r\n" +
                "LEFT JOIN OrderDetail AS od ON od.OrderID = o.ID " +
                "group by cust.ID, cust.Name, cust.Address, cust.PhoneNumber " +
                "order by TotalCost DESC";

            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();
            int rank = 0;

            while (reader.Read())
            {
                rank++;
                CustomerDataGrid customer = new CustomerDataGrid()
                {
                    Rank = rank,
                    ID = (int)reader["ID"],
                    Name = (string)reader["Name"],
                    Address = (string)reader["Address"],
                    PhoneNumber = (string)reader["PhoneNumber"],
                    TotalCost = Convert.ToInt32(reader["TotalCost"]),
                    TotalOrders = (int)reader["TotalOrders"],
                    TotalProducts = (int)reader["TotalProducts"]
                };

                _customerList.Add(customer);
            }


            reader.Close();
            return _customerList;
        }

        public void InsertCustomer(Customer customer)
        {
            var sql = "";

            sql = "insert into Customer(Name, Address, PhoneNumber) " +
                "values (@Name, @Address, @PhoneNumber)";

            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);

            sqlCommand.Parameters.AddWithValue("@Name", customer.Name);
            sqlCommand.Parameters.AddWithValue("@Address", customer.Address);
            sqlCommand.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);



            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Inserted {customer.Name} OK");

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine($"Inserted {customer.Name} Fail: " + ex.Message);
            }

            

        }

        public int GetLastestInsertID()
        {
            string sql = "select ident_current('Customer')";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);
            var result = sqlCommand.ExecuteScalar();
            System.Diagnostics.Debug.WriteLine(result);
            if (result == DBNull.Value)
            {
                return 0;
            }
            return System.Convert.ToInt32(result);
        }

        public void UpdateCustomer(Customer customer)
        {
            string sql;

            sql = "update Customer set Name = @Name, Address = @Address, PhoneNumber = @PhoneNumber" +
                " where ID = @ID";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);
            sqlCommand.Parameters.AddWithValue("@ID", customer.ID);
            sqlCommand.Parameters.AddWithValue("@Name", customer.Name);
            sqlCommand.Parameters.AddWithValue("@Address", customer.Address);
            sqlCommand.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);


            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Updated {customer.Name} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Updated {customer.Name} Fail: " + ex.Message);
            }
        }

        public void RemoveCustomer(int ID)
        {
            string sql = "delete from Customer where ID = @ID";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);
            sqlCommand.Parameters.AddWithValue("@ID", ID);

            try
            {
                sqlCommand.ExecuteNonQuery();

                System.Diagnostics.Debug.WriteLine($"Deleted {ID} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Deleted {ID} Fail: " + ex.Message);
            }
        }

        public Customer GetCustomerByID(int id)
        {
            string sql = "select * from Customer where ID = @ID";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);
            sqlCommand.Parameters.AddWithValue("@ID", id);

            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Customer customer = new Customer()
                {
                    ID = (int)reader["ID"],
                    Name = (string)reader["Name"],
                    Address = (string)reader["Address"],
                    PhoneNumber = (string)reader["PhoneNumber"],
                };

                reader.Close();
                return customer;
            }

            reader.Close();

            return new Customer() 
            { 
                ID = id
            };

        }
    }
}
