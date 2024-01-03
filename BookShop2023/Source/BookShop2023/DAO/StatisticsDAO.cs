using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMyShop.Helpers;
using System.Data.SqlClient;

namespace ProjectMyShop.DAO
{
    internal class StatisticsDAO : DB
    {
        public string getTotalRevenueUntilDate(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy-MM-dd");

            var sql = "select convert(varchar,cast(SUM(do.Quantity * p.SellingPrice) as int), 1) as Revenue from OrderDetail do join Product p on do.ProductID = p.ID join Orders o on do.OrderID = o.ID where CreatedAt <= @SelectedDate;";
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            string result = "0";
            if (reader.Read())
            {
                if (reader["Revenue"].GetType() != typeof(DBNull))
                    result = (string)reader["Revenue"];
            }
            reader.Close();
            return result.ToString();
        }

        public string getTotalProfitUntilDate(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy-MM-dd");

            var sql = "select convert(varchar,cast(SUM(do.Quantity *(p.SellingPrice - p.PurchasePrice)) as int), 1) as Profit from OrderDetail do join Product p on do.ProductID = p.ID join Orders o on do.OrderID = o.ID where CreatedAt <= @SelectedDate;";
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            string result = "0";
            if (reader.Read())
            {
                if (reader["Profit"].GetType() != typeof(DBNull))
                    result = (string)reader["Profit"];
            }
            reader.Close();
            return result.ToString();
        }

        public int getTotalOrdersUntilDate(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy-MM-dd");

            var sql = "select COUNT(ID) as Orders from Orders where CreatedAt <= @SelectedDate;";
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            int result = 0;
            if (reader.Read())
            {
                result = (int)reader["Orders"];
            }
            reader.Close();
            return result;
        }

        public List<Tuple<string, decimal>> getDailyRevenue(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy-MM-dd");

            //var sql = "select convert(varchar, o.CreatedAt) as CreatedAt, convert(varchar,cast(SUM(do.Quantity * p.SellingPrice) as money), 1) as Revenue from OrderDetail do join Product p on do.ProductID = p.ID join Orders o on do.OrderID = o.ID where CreatedAt < @SelectedDate group by o.CreatedAt order by o.CreatedAt asc;";
            var sql = "select convert(varchar, o.CreatedAt) as CreatedAt, cast(SUM(do.Quantity * p.SellingPrice) AS decimal(13,4)) as Revenue from OrderDetail do join Product p on do.ProductID = p.ID join Orders o on do.OrderID = o.ID where CreatedAt <= @SelectedDate group by o.CreatedAt order by o.CreatedAt asc;";
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, decimal>>();
            while (reader.Read())
            {
                var tuple = Tuple.Create((string)reader["CreatedAt"], (decimal)reader["Revenue"]);
                resultList.Add(tuple);
            }
            reader.Close();
            return resultList;
        }

        public List<Tuple<string, decimal>> getWeeklyRevenue(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy");

            var sql = "SELECT convert(varchar, DATEPART(iso_week, o.CreatedAt)) AS Week, cast(SUM(do.Quantity * p.SellingPrice) as decimal(13,4)) as Revenue FROM Product p join OrderDetail do on p.ID = do.ProductID join Orders o on o.ID = do.OrderID WHERE DATEPART(year, o.CreatedAt) = @SelectedYear GROUP BY DATEPART(iso_week, o.CreatedAt) ORDER BY DATEPART(iso_week, o.CreatedAt);";
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedYear";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, decimal>>();
            while (reader.Read())
            {
                var tuple = Tuple.Create((string)reader["Week"], (decimal)reader["Revenue"]);
                resultList.Add(tuple);
            }
            reader.Close();
            return resultList;
        }

        public List<Tuple<string, decimal>> getMonthlyRevenue(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy");

            var sql = "WITH Months as (select month(GETDATE()) as Monthnumber, datename(month, GETDATE()) as NameOfMonth, 1 as number union all select month(dateadd(month, number, (GETDATE()))) Monthnumber, datename(month, dateadd(month, number, (GETDATE()))) as NameOfMonth, number + 1  from Months  where number < 12) select NameOfMonth, Revenue from Months left join (select datepart(month, o.CreatedAt) as OrderMonth, cast(SUM(do.Quantity * p.SellingPrice) AS decimal(13, 4)) as Revenue from OrderDetail do join Product p on do.ProductID = p.ID join Orders o on do.OrderID = o.ID where datepart(year, o.CreatedAt) = @SelectedYear group by datepart(month, o.CreatedAt)) MonthlyRevenue on Months.Monthnumber = MonthlyRevenue.OrderMonth order by Monthnumber;";
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedYear";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, decimal>>();
            while (reader.Read())
            {
                string monthName = (string)reader["NameOfMonth"];
                decimal monthValue = 0;
                
                if (reader["Revenue"].GetType() != typeof(DBNull))
                {
                    monthValue = (decimal)reader["Revenue"];
                }

                resultList.Add(Tuple.Create(monthName, (decimal)monthValue));
            }
            reader.Close();
            return resultList;
        }

        public List<Tuple<string, decimal>> getYearlyRevenue()
        {
            var sql = "select convert(varchar, datepart(year, o.CreatedAt)) as OrderYear, cast(SUM(do.Quantity * SellingPrice) AS decimal(13,4)) as Revenue from OrderDetail do join Product p on do.ProductID = p.ID join Orders o on do.OrderID = o.ID group by datepart(year, o.CreatedAt) order by datepart(year, o.CreatedAt) asc;";

            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, decimal>>();
            while (reader.Read())
            {
                var tuple = Tuple.Create((string)reader["OrderYear"], (decimal)reader["Revenue"]);
                resultList.Add(tuple);
            }
            reader.Close();
            return resultList;
        }

        public List<Tuple<string, decimal>> getDailyProfit(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy-MM-dd");

            var sql = "select convert(varchar, o.CreatedAt) as CreatedAt, cast(SUM(do.Quantity * (p.SellingPrice - p.PurchasePrice)) AS decimal(13,4)) as Profit from OrderDetail do join Product p on do.ProductID = p.ID join Orders o on do.OrderID = o.ID where CreatedAt <= @SelectedDate group by o.CreatedAt order by o.CreatedAt asc;";
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, decimal>>();
            while (reader.Read())
            {
                var tuple = Tuple.Create((string)reader["CreatedAt"], (decimal)reader["Profit"]);
                resultList.Add(tuple);
            }
            reader.Close();
            return resultList;
        }

        public List<Tuple<string, decimal>> getWeeklyProfit(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy");

            var sql = "SELECT convert(varchar, DATEPART(iso_week, o.CreatedAt)) AS Week, cast(SUM(do.Quantity * (p.SellingPrice - p.PurchasePrice)) as decimal(13,4)) as Profit FROM Product p join OrderDetail do on p.ID = do.ProductID join Orders o on o.ID = do.OrderID WHERE DATEPART(year, o.CreatedAt) = @SelectedYear GROUP BY DATEPART(iso_week, o.CreatedAt) ORDER BY DATEPART(iso_week, o.CreatedAt);";
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedYear";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, decimal>>();
            while (reader.Read())
            {
                var tuple = Tuple.Create((string)reader["Week"], (decimal)reader["Profit"]);
                resultList.Add(tuple);
            }
            reader.Close();
            return resultList;
        }

        public List<Tuple<string, decimal>> getMonthlyProfit(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy");

            var sql = "WITH Months as (select month(GETDATE()) as Monthnumber, datename(month, GETDATE()) as NameOfMonth, 1 as number union all select month(dateadd(month, number, (GETDATE()))) Monthnumber, datename(month, dateadd(month, number, (GETDATE()))) as NameOfMonth, number + 1  from Months  where number < 12) select NameOfMonth, Profit from Months left join (select datepart(month, o.CreatedAt) as OrderMonth, cast(SUM(do.Quantity * (p.SellingPrice - p.PurchasePrice)) AS decimal(13, 4)) as Profit from OrderDetail do join Product p on do.ProductID = p.ID join Orders o on do.OrderID = o.ID where datepart(year, o.CreatedAt) = @SelectedYear group by datepart(month, o.CreatedAt)) MonthlyProfit on Months.Monthnumber = MonthlyProfit.OrderMonth order by Monthnumber;";
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedYear";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, decimal>>();
            while (reader.Read())
            {
                string monthName = (string)reader["NameOfMonth"];
                decimal monthValue = 0;

                if (reader["Profit"].GetType() != typeof(DBNull))
                {
                    monthValue = (decimal)reader["Profit"];
                }

                resultList.Add(Tuple.Create(monthName, (decimal)monthValue));
            }
            reader.Close();
            return resultList;
        }

        public List<Tuple<string, decimal>> getYearlyProfit()
        {
            var sql = "select convert(varchar, datepart(year, o.CreatedAt)) as OrderYear, cast(SUM(do.Quantity * (p.SellingPrice - p.PurchasePrice)) AS decimal(13,4)) as Profit from OrderDetail do join Product p on do.ProductID = p.ID join Orders o on do.OrderID = o.ID group by datepart(year, o.CreatedAt) order by datepart(year, o.CreatedAt) asc;";

            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, decimal>>();
            while (reader.Read())
            {
                var tuple = Tuple.Create((string)reader["OrderYear"], (decimal)reader["Profit"]);
                resultList.Add(tuple);
            }
            reader.Close();
            return resultList;
        }

        public List<Tuple<string, int>> getDailyQuantityOfSpecificProduct(int srcProductID, int srcCategoryID, DateTime srcDate)
        {
            var sql = "select convert(varchar, o.CreatedAt) as CreatedAt, SUM(do.Quantity) as Quantity from Product p left join OrderDetail do on p.ID = do.ProductID join Orders o on do.OrderID = o.ID where p.ID = @ProductID  and p.CatID = @CategoryID and o.CreatedAt <= @SelectedDate group by o.CreatedAt order by o.CreatedAt asc";

            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@ProductID";
            sqlParameter.Value = srcProductID;

            var sqlParameter1 = new SqlParameter();
            sqlParameter1.ParameterName = "@CategoryID";
            sqlParameter1.Value = srcCategoryID;

            string sqlFormattedDate = srcDate.ToString("yyyy-MM-dd");
            var sqlParameter2 = new SqlParameter();
            sqlParameter2.ParameterName = "@SelectedDate";
            sqlParameter2.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, DB.Instance.Connection);

            command.Parameters.Add(sqlParameter);
            command.Parameters.Add(sqlParameter1);
            command.Parameters.Add(sqlParameter2);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, int>>();
            while (reader.Read())
            {
                var tuple = Tuple.Create((string)reader["CreatedAt"], (int)reader["Quantity"]);
                resultList.Add(tuple);
            }
            reader.Close();
            return resultList;
        }

        public List<Tuple<string, int>> getWeeklyQuantityOfSpecificProduct(int srcProductID, int srcCategoryID, DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy");

            var sql = "SELECT DATEPART(iso_week, o.CreatedAt) AS Week, SUM(do.Quantity) as Quantity, " +
                "convert(varchar,cast(SUM(do.Quantity * (p.SellingPrice - p.PurchasePrice)) as money), 1) as Profit" +
                " FROM Product p join OrderDetail do on p.ID = do.ProductID join Orders o on o.ID = do.OrderID WHERE DATEPART(year, o.CreatedAt) = @SelectedYear and p.ID = @ProductID  and p.CatID = @CategoryID GROUP BY DATEPART(iso_week, o.CreatedAt) ORDER BY DATEPART(iso_week, o.CreatedAt);";
            
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedYear";
            sqlParameter.Value = sqlFormattedDate;
            
            var sqlParameter1 = new SqlParameter();
            sqlParameter1.ParameterName = "@ProductID";
            sqlParameter1.Value = srcProductID;

            var sqlParameter2 = new SqlParameter();
            sqlParameter2.ParameterName = "@CategoryID";
            sqlParameter2.Value = srcCategoryID;

            var command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.Add(sqlParameter);
            command.Parameters.Add(sqlParameter1);
            command.Parameters.Add(sqlParameter2);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, int>>();
            while (reader.Read())
            {
                var week = (string)reader["Week"].ToString();
                var quantity = (int)reader["Quantity"];
                var tuple = Tuple.Create(week, quantity);
                resultList.Add(tuple);
            }
            reader.Close();
            return resultList;
        }

        public List<Tuple<string, int>> getMonthlyQuantityOfSpecificProduct(int srcProductID, int srcCategoryID, DateTime srcDate)
        {
            var sql = "WITH Months as (select month(GETDATE()) as Monthnumber, datename(month, GETDATE()) as NameOfMonth, 1 as number " +
                "union all " +
                "select month(dateadd(month, number, (GETDATE()))) Monthnumber, datename(month, dateadd(month, number, (GETDATE()))) as NameOfMonth, number + 1  from Months  where number < 12) select NameOfMonth, Quantity from Months left join (select datepart(month, o.CreatedAt) as OrderMonth, SUM(do.Quantity) as Quantity " +
                "from OrderDetail do join Product p on do.ProductID = p.ID join Orders o on do.OrderID = o.ID " +
                "where datepart(year, o.CreatedAt) = @SelectedYear and p.ID = @ProductID and p.CatID = @CategoryID " +
                "group by datepart(month, o.CreatedAt)) MonthlyQuantity on Months.Monthnumber = MonthlyQuantity .OrderMonth order by Monthnumber;";

            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@ProductID";
            sqlParameter.Value = srcProductID;

            var sqlParameter1 = new SqlParameter();
            sqlParameter1.ParameterName = "@CategoryID";
            sqlParameter1.Value = srcCategoryID;

            string sqlFormattedDate = srcDate.ToString("yyyy");
            var sqlParameter2 = new SqlParameter();
            sqlParameter2.ParameterName = "@SelectedYear";
            sqlParameter2.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, DB.Instance.Connection);

            command.Parameters.Add(sqlParameter2);
            command.Parameters.Add(sqlParameter);
            command.Parameters.Add(sqlParameter1);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, int>>();
            while (reader.Read())
            {
                string monthName = (string)reader["NameOfMonth"];
                int quantity = 0;

                if (reader["Quantity"].GetType() != typeof(DBNull))
                {
                    quantity = (int)reader["Quantity"];
                }
                resultList.Add(Tuple.Create(monthName, quantity));
            }
            reader.Close();
            return resultList;
        }

        public List<Tuple<string, int>> getYearlyQuantityOfSpecificProduct(int srcProductID, int srcCategoryID)
        {
            var sql = "select convert(varchar, datepart(year, o.CreatedAt)) as OrderYear, SUM(do.Quantity) as Quantity from OrderDetail do join Product p on do.ProductID = p.ID join Orders o on do.OrderID = o.ID join Category cat on p.CatID = cat.ID where p.ID = @ProductID and cat.ID = @CategoryID group by convert(varchar, datepart(year, o.CreatedAt)) order by convert(varchar, datepart(year, o.CreatedAt)) asc;";

            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@ProductID";
            sqlParameter.Value = srcProductID;

            var sqlParameter1 = new SqlParameter();
            sqlParameter1.ParameterName = "@CategoryID";
            sqlParameter1.Value = srcCategoryID;

            var command = new SqlCommand(sql, DB.Instance.Connection);

            command.Parameters.Add(sqlParameter);
            command.Parameters.Add(sqlParameter1);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, int>>();
            while (reader.Read())
            {
                resultList.Add(Tuple.Create((string)reader["OrderYear"], (int)reader["Quantity"]));
            }
            reader.Close();
            return resultList;
        }

        public List<Tuple<string, int>> getProductQuantityInCategory(int srcCategoryID)
        {
            var sql = "select p.Name, sum(do.Quantity) as Quantity from Product p join OrderDetail do on p.ID = do.ProductID join Orders o on o.ID = do.OrderID where p.CatID = @SelectedCategory group by p.Name;";

            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedCategory";
            sqlParameter.Value = srcCategoryID;

            var command = new SqlCommand(sql, DB.Instance.Connection);

            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, int>>();
            while (reader.Read())
            {
                string Name = (string)reader["Name"];
                int quantity = 0;

                if (reader["Quantity"].GetType() != typeof(DBNull))
                {
                    quantity = (int)reader["Quantity"];
                }
                resultList.Add(Tuple.Create(Name, quantity));
            }
            reader.Close();
            return resultList;
        }
    }
}
