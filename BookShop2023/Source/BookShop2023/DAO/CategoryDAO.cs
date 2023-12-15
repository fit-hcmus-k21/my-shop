using ProjectMyShop.DTO;
using ProjectMyShop.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ProjectMyShop.DAO
{
    internal class CategoryDAO : DB
    {
        public Category GetCategoryById(int id)
        {
            var sql = "select * from Category where ID=@CatId";
            var command = new SqlCommand(sql, DB.Instance.Connection );

            command.Parameters.Add("CatId", SqlDbType.Int).Value = id;

            var reader = command.ExecuteReader();

            Category? result = null;

            if (reader.Read()) // ORM - Object relational mapping
            {
                var catId = (int)reader["ID"];
                var catName = (string)reader["category_name"];

                result = new Category()
                {
                    ID = catId,
                    Name = catName,
                };
            }

            reader.Close();
            return result;
        }

        public List<Category> getCategoryList()
        {
            var sql = "select * from Category;";

            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();

            var resultList = new List<Category>();
            while (reader.Read())
            {
                Category category = new Category()
                {
                    ID = (int)reader["ID"],
                    Name = (string)reader["Name"],
                };

                resultList.Add(category);
            }
            reader.Close();
            return resultList;
        }
        public void AddCategory(Category cat)
        {
            var sql = "";
           
                sql = "insert into Category(Name) " +
                    "values (@Name)";
            
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);

            sqlCommand.Parameters.AddWithValue("@Name", cat.Name);

            
            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Inserted {cat.ID} OK");
                
            }
            catch (Exception ex)
            {
                
                System.Diagnostics.Debug.WriteLine($"Inserted {cat.ID} Fail: " + ex.Message);
            }

            
        }
        public int GetLastestInsertID()
        {
            string sql = "select ident_current('Category')";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);
            var result = sqlCommand.ExecuteScalar();
            System.Diagnostics.Debug.WriteLine(result);
            if (result == DBNull.Value)
            {
                return 0;
            }
            return System.Convert.ToInt32(result);
        }

        public int isExisted(Category cat)
        {
            string sql = "select ID from Category where Name = @Name";
            SqlCommand command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.AddWithValue("@Name", cat.Name);

            var reader = command.ExecuteReader();
            int ID = 0;
            while (reader.Read())
            {
                ID = (int)reader["ID"];
            }
            reader.Close();
            return ID;
        }

        public void updateCategory(int ID, Category category)
        {
            string sql;
           
                sql = "update Category set Name = @Name where ID = @ID";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);
            sqlCommand.Parameters.AddWithValue("@ID", ID);
            sqlCommand.Parameters.AddWithValue("@Name", category.Name);


            

            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Updated {category.ID} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Updated {category.ID} Fail: " + ex.Message);
            }
        }

        public void removeCategory(int ID)
        {
            string sql = "delete from Category where ID = @ID";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);
            sqlCommand.Parameters.AddWithValue("@ID", ID);
            try
            {
                sqlCommand.ExecuteNonQuery();
                sql = "delete from Product where CatID = @ID";
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
