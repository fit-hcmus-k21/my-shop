using ProjectMyShop.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMyShop.DTO;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.IO;

namespace ProjectMyShop.DAO
{
    internal class ProductDAO : DB
    {
        public int getTotalProduct()
        {
            var sql = "select count(*) as total from Product";
            var command = new SqlCommand(sql, DB.Instance.Connection);
            var reader = command.ExecuteReader();

            int result = 0;
            if (reader.Read())
            {
                result = (int)reader["total"];
            }
            reader.Close();
            return result;
        }

        public Product? getProductByID(int ProductID)
        {
            var sql = "select * from Product WHERE ID = @ProductID";
            var command = new SqlCommand(sql, DB.Instance.Connection);

            command.Parameters.AddWithValue("@ProductID", ProductID);

            var reader = command.ExecuteReader();

            Product? Product = null;
            if (reader.Read())
            {
                var ID = (int)reader["ID"];
                var Name = (String)reader["Name"];

                var SellingPrice = (int)(decimal)reader["SellingPrice"];
                //var SellingPrice = (int)reader["SellingPrice"];
                var Quantity = (int)reader["Quantity"];
                var ImagePath = (string)reader["ImagePath"];

                Product = new Product()
                {
                    ID = ID,
                    Name = Name,
                    SellingPrice = SellingPrice,
                    Quantity = Quantity,
                    ImagePath = ImagePath
                };

                
            }
            reader.Close();
            return Product;
        }

        public List<Product> GetTop5OutQuantity()
        {
            var sql = "select top(5) * from Product where Quantity < 5 order by Quantity ";
            var command = new SqlCommand(sql, DB.Instance.Connection);
            var reader = command.ExecuteReader();

            List<Product> list = new List<Product>();
            while (reader.Read())
            {
                var ID = (int)reader["ID"];
                var Name = (String)reader["Name"];

                var SellingPrice = (int)(decimal)reader["SellingPrice"];
                //var SellingPrice = (int)reader["SellingPrice"];
                var Quantity = (int)reader["Quantity"];

                Product Product = new Product()
                {
                    ID = ID,
                    Name = Name,
                    SellingPrice = SellingPrice,
                    Quantity = Quantity,
                };
                if (Product.Name != "")
                    list.Add(Product);
            }
            reader.Close();
            return list;
        }

        public List<Product> getProductsAccordingToSpecificCategory(int srcCategoryID)
        {
            var sql = "select * from Product where CatID = @CategoryID";

            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@CategoryID";
            sqlParameter.Value = srcCategoryID;

            var command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            List<Product> list = new List<Product>();
            while (reader.Read())
            {
                var ID = (int)reader["ID"];
                var Name = (String)reader["Name"];

                var SellingPrice = (int)(decimal)reader["SellingPrice"];
                var PurchasePrice = (int)(decimal)reader["PurchasePrice"];
                var Description = (String)reader["Description"];
                //var SellingPrice = (int)reader["SellingPrice"];
                var Quantity = (int)reader["Quantity"];
                var ImagePath = (string)reader["ImagePath"];


                Product Product = new Product()
                {
                    ID = ID,
                    Name = Name,
                    SellingPrice = SellingPrice,
                    Quantity = Quantity,
                    PurchasePrice = PurchasePrice,
                    Description = Description,
                    ImagePath = ImagePath
                };
                
                if (Product.Name != "")
                    list.Add(Product);
            }
            reader.Close();
            return list;
        }

        public void addProduct(Product Product)
        {
            // ID Auto Increment
            var sql = "";
            if (Product.ImagePath != null)
            {
                sql = "insert into Product(Name, Manufacturer, PurchasePrice, SellingPrice, Quantity, UploadDate, Description, CatID, ImagePath) " +
                    "values (@Name, @Manufacturer, @PurchasePrice, @SellingPrice, @Quantity, @UploadDate, @Description, @CatID, @ImagePath)"; //
            }
            else
            {
                sql = "insert into Product(Name, Manufacturer, PurchasePrice, SellingPrice, Quantity, UploadDate, Description, CatID) " +
                    "values (@Name, @Manufacturer, @PurchasePrice, @SellingPrice, @Quantity, @UploadDate, @Description, @CatID)"; //
            }
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);

            sqlCommand.Parameters.AddWithValue("@Name", Product.Name);
            sqlCommand.Parameters.AddWithValue("@PurchasePrice", Product.PurchasePrice);
            sqlCommand.Parameters.AddWithValue("@SellingPrice", Product.SellingPrice);
            sqlCommand.Parameters.AddWithValue("@Quantity", Product.Quantity);
            sqlCommand.Parameters.AddWithValue("@UploadDate", Product.UploadDate);
            sqlCommand.Parameters.AddWithValue("@Description", Product.Description);
            sqlCommand.Parameters.AddWithValue("@CatID", Product.CatID);


            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Inserted {Product.ID} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Inserted {Product.ID} Fail: " + ex.Message);
            }
        }

        public int GetLastestInsertID()
        {
            string sql = "select ident_current('Product')";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);
            var resutl = sqlCommand.ExecuteScalar();
            System.Diagnostics.Debug.WriteLine(resutl);
            return System.Convert.ToInt32(sqlCommand.ExecuteScalar());
        }
        public void deleteProduct(int Productid)
        {
            string sql = "delete from Product where ID = @ID";
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);
            sqlCommand.Parameters.AddWithValue("@ID", Productid);
            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Deleted {Productid} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Deleted {Productid} Fail: " + ex.Message);
            }
        }
        public void updateProduct(int id, Product Product)
        {
            string sql;
            if (Product.ImagePath != null)
            {
                sql = "update Product set Name = @Name, Manufacturer = @Manufacturer, Description = @Description, " +
                "PurchasePrice = @PurchasePrice, Quantity = @Quantity, SellingPrice = @SellingPrice, ImagePath = @ImagePath where ID = @ID";
            }
            else
            {
                sql = "update Product set Name = @Name, Manufacturer = @Manufacturer, Description = @Description, " +
                "PurchasePrice = @PurchasePrice, Quantity = @Quantity, SellingPrice = @SellingPrice where ID = @ID";
            }
            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);
            sqlCommand.Parameters.AddWithValue("@ID", id);
            sqlCommand.Parameters.AddWithValue("@Name", Product.Name);
            sqlCommand.Parameters.AddWithValue("@PurchasePrice", Product.PurchasePrice);
            sqlCommand.Parameters.AddWithValue("@SellingPrice", Product.SellingPrice);
            sqlCommand.Parameters.AddWithValue("@Quantity", Product.Quantity);
            sqlCommand.Parameters.AddWithValue("@Description", Product.Description);

            

            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Updated {Product.ID} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Updated {Product.ID} Fail: " + ex.Message);
            }
        }

        public List<BestSellingProduct> getBestSellingProductsInWeek(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy-MM-dd");

            var sql = "select TOP(5) p.ID, p.Name, p.Manufacturer, p.Quantity, p.Description, p.PurchasePrice, p.SellingPrice, p.CatID, p.UploadDate, p.ImagePath, sum(do.Quantity) as Quantity from Orders o join OrderDetail do on o.ID = do.OrderID join Product p on p.ID = do.ProductID where CreatedAt between DATEADD(DAY, -7, @SelectedDate) and DATEADD(DAY, 1, @SelectedDate) group by p.ID, p.Name, p.Manufacturer, p.Quantity, p.Description, p.PurchasePrice, p.SellingPrice, p.CatID, p.UploadDate, p.ImagePath order by sum(do.Quantity) desc;";

            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            List<BestSellingProduct> list = new List<BestSellingProduct>();
            while (reader.Read())
            {
                var ID = (int)reader["ID"];
                var Name = (String)reader["Name"];
                var Manufacturer = (String)reader["Manufacturer"];

                var SellingPrice = (int)(decimal)reader["SellingPrice"];
                var PurchasePrice = (int)(decimal)reader["PurchasePrice"];
                var Description = (String)reader["Description"];
                var Quantity = (int)reader["Quantity"];
                var ImagePath = (string)reader["ImagePath"];

                BestSellingProduct Product = new BestSellingProduct()
                {
                    ID = ID,
                    Name = Name,
                    SellingPrice = SellingPrice,
                    Quantity = Quantity,
                    PurchasePrice = PurchasePrice,
                    Description = Description,
                    ImagePath = ImagePath
                };
                if (!reader["ImagePath"].Equals(DBNull.Value))
                {
                    var byteImagePath = (byte[])reader["ImagePath"];
                    using (MemoryStream ms = new MemoryStream(byteImagePath))
                    {
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.UriSource = null;
                        image.StreamSource = ms;
                        image.EndInit();
                        image.Freeze();
                        Product.ImagePath = ImagePath;
                    }
                }
                if (Product.Name != "")
                    list.Add(Product);
            }
            reader.Close();
            return list;
        }

        public List<BestSellingProduct> getBestSellingProductsInMonth(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy-MM-dd");

            var sql = "select TOP(5) p.ID, p.Name, p.Manufacturer, p.Quantity, p.Description, p.PurchasePrice, p.SellingPrice, p.CatID, p.UploadDate, p.ImagePath, sum(do.Quantity) as Quantity from Orders o join OrderDetail do on o.ID = do.OrderID join Product p on p.ID = do.ProductID where datepart(year, CreatedAt) = datepart(year, @SelectedDate) and datepart(month, CreatedAt) = datepart(month, @SelectedDate) group by p.ID, p.Name, p.Manufacturer, p.Quantity, p.Description, p.PurchasePrice, p.SellingPrice, p.CatID, p.UploadDate, p.ImagePath order by sum(do.Quantity) desc;";

            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            List<BestSellingProduct> list = new List<BestSellingProduct>();

            while (reader.Read())
            {
                var ID = (int)reader["ID"];
                var Name = (String)reader["Name"];
                var Manufacturer = (String)reader["Manufacturer"];
                var SellingPrice = (int)(decimal)reader["SellingPrice"];
                var PurchasePrice = (int)(decimal)reader["PurchasePrice"];
                var Description = (String)reader["Description"];
                var Quantity = (int)reader["Quantity"];
                var ImagePath = (string)reader["ImagePath"];

                BestSellingProduct Product = new BestSellingProduct()
                {
                    ID = ID,
                    Name = Name,
                    SellingPrice = SellingPrice,
                    Quantity = Quantity,
                    PurchasePrice = PurchasePrice,
                    Description = Description,
                    ImagePath = ImagePath
                };
   
                if (Product.Name != "")
                    list.Add(Product);
            }
            reader.Close();
            return list;
        }

        public List<BestSellingProduct> getBestSellingProductsInYear(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy-MM-dd");

            var sql = "select TOP(5) p.ID, p.Name, p.Manufacturer, p.Quantity, p.Description, p.PurchasePrice, p.SellingPrice, p.CatID, p.UploadDate, p.ImagePath, sum(do.Quantity) as Quantity from Orders o join OrderDetail do on o.ID = do.OrderID join Product p on p.ID = do.ProductID where datepart(year, CreatedAt) = datepart(year, @SelectedDate) group by p.ID, p.Name, p.Manufacturer, p.Quantity, p.Description, p.PurchasePrice, p.SellingPrice, p.CatID, p.UploadDate, p.ImagePath order by sum(do.Quantity) desc;";

            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            List<BestSellingProduct> list = new List<BestSellingProduct>();
            while (reader.Read())
            {
                var ID = (int)reader["ID"];
                var Name = (String)reader["Name"];
                var Manufacturer = (String)reader["Manufacturer"];
                var SellingPrice = (int)(decimal)reader["SellingPrice"];
                var PurchasePrice = (int)(decimal)reader["PurchasePrice"];
                var Description = (String)reader["Description"];
                var Quantity = (int)reader["Quantity"];
                var ImagePath = (string)reader["ImagePath"];

                BestSellingProduct Product = new BestSellingProduct()
                {
                    ID = ID,
                    Name = Name,
                    SellingPrice = SellingPrice,
                    Quantity = Quantity,
                    PurchasePrice = PurchasePrice,
                    Description = Description,
                    ImagePath = ImagePath
                };
                if (!reader["ImagePath"].Equals(DBNull.Value))
                {
                    var byteImagePath = (byte[])reader["ImagePath"];
                    using (MemoryStream ms = new MemoryStream(byteImagePath))
                    {
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.UriSource = null;
                        image.StreamSource = ms;
                        image.EndInit();
                        image.Freeze();
                        Product.ImagePath = ImagePath;
                    }
                }
                if (Product.Name != "")
                    list.Add(Product);
            }
            reader.Close();
            return list;
        }
    }
}
