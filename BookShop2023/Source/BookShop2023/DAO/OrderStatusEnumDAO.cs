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
    internal class OrderStatusEnumDAO : DB
    {
        private static List<OrderStatusEnum> Enums;
     
        public static List<OrderStatusEnum> loadAll()
        {
            if (Enums == null)
            {
                Enums = new List<OrderStatusEnum>();
                // load from db
                //...
                var sql = "select * from OrderStatusEnum;";

                var command = new SqlCommand(sql, DB.Instance.Connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    OrderStatusEnum statusEnum = new OrderStatusEnum()
                    {
                        Key = (string)reader["Key"],
                        Value = (int)reader["Value"],
                        Description = (string)reader["Description"],
                        DisplayText = (string)reader["DisplayText"],
                    };

                    Enums.Add(statusEnum);
                }


                reader.Close();
                

            }
            return Enums;
        }
    }
}
