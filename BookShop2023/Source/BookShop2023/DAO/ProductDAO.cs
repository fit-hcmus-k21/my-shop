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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;

namespace ProjectMyShop.DAO
{
    internal class ProductDAO : DB
    {
        #region not updated yet
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

                sql = "insert into Product(Name, CatID, PurchasePrice, SellingPrice, Quantity, UploadDate, Description, Author, ImagePath, PublishedYear) " +
                    "values (@Name, @CatID, @PurchasePrice, @SellingPrice, @Quantity, @UploadDate, @Description, @Author, @ImagePath, @PublishedYear)"; //

            SqlCommand sqlCommand = new SqlCommand(sql, DB.Instance.Connection);

            sqlCommand.Parameters.AddWithValue("@Name", Product.Name);
            sqlCommand.Parameters.AddWithValue("@Author", Product.Author);

            sqlCommand.Parameters.AddWithValue("@PurchasePrice", Product.PurchasePrice);
            sqlCommand.Parameters.AddWithValue("@SellingPrice", Product.SellingPrice);
            sqlCommand.Parameters.AddWithValue("@Quantity", Product.Quantity);
            sqlCommand.Parameters.AddWithValue("@UploadDate", Product.UploadDate);
            sqlCommand.Parameters.AddWithValue("@Description", Product.Description);
            sqlCommand.Parameters.AddWithValue("@CatID", Product.CatID);

            sqlCommand.Parameters.AddWithValue("@ImagePath", Product.ImagePath);
            sqlCommand.Parameters.AddWithValue("@PublishedYear", Product.PublishedYear);



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
            var result = sqlCommand.ExecuteScalar();
            System.Diagnostics.Debug.WriteLine(result);
            if (result == DBNull.Value)
            {
                return 0;
            }
            return System.Convert.ToInt32(result);
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
                sql = "update Product set Name = @Name, Author = @Author, Description = @Description, " +
                "PurchasePrice = @PurchasePrice, Quantity = @Quantity, SellingPrice = @SellingPrice, ImagePath = @ImagePath where ID = @ID";
            }
            else
            {
                sql = "update Product set Name = @Name, Author = @Author, Description = @Description, " +
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

            var sql = "select TOP(5) p.ID, p.Name, p.Author, p.Quantity, p.Description, p.PurchasePrice, p.SellingPrice, p.CatID, p.UploadDate, p.ImagePath, sum(do.Quantity) as Quantity from Orders o join OrderDetail do on o.ID = do.OrderID join Product p on p.ID = do.ProductID where CreatedAt between DATEADD(DAY, -7, @SelectedDate) and DATEADD(DAY, 1, @SelectedDate) group by p.ID, p.Name, p.Author, p.Quantity, p.Description, p.PurchasePrice, p.SellingPrice, p.CatID, p.UploadDate, p.ImagePath order by sum(do.Quantity) desc;";

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
                var Author = (String)reader["Author"];

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

            var sql = "select TOP(5) p.ID, p.Name, p.Author, p.Quantity, p.Description, p.PurchasePrice, p.SellingPrice, p.CatID, p.UploadDate, p.ImagePath, sum(do.Quantity) as Quantity from Orders o join OrderDetail do on o.ID = do.OrderID join Product p on p.ID = do.ProductID where datepart(year, CreatedAt) = datepart(year, @SelectedDate) and datepart(month, CreatedAt) = datepart(month, @SelectedDate) group by p.ID, p.Name, p.Author, p.Quantity, p.Description, p.PurchasePrice, p.SellingPrice, p.CatID, p.UploadDate, p.ImagePath order by sum(do.Quantity) desc;";

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
                var Author = (String)reader["Author"];
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

            var sql = "select TOP(5) p.ID, p.Name, p.Author, p.Quantity, p.Description, p.PurchasePrice, p.SellingPrice, p.CatID, p.UploadDate, p.ImagePath, sum(do.Quantity) as Quantity from Orders o join OrderDetail do on o.ID = do.OrderID join Product p on p.ID = do.ProductID where datepart(year, CreatedAt) = datepart(year, @SelectedDate) group by p.ID, p.Name, p.Author, p.Quantity, p.Description, p.PurchasePrice, p.SellingPrice, p.CatID, p.UploadDate, p.ImagePath order by sum(do.Quantity) desc;";

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
                var Author = (String)reader["Author"];
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

        #endregion

        #region declare variables
        bool _hasCategoryFilter = false;
        bool _hasPriceFilter = false;
        bool _hasPublishedYearFilter = false;

        int categoryFilter = -1;
        int yearFilter = -1;
        int minPrice = -1;
        int maxPrice = -1;
        string _sortingCriteriaQuery = "";
        string _keyword = "";
        #endregion

        #region setter  methods
        public void setFilterCat(int id)
        {
            if (id != -1)
            {
                _hasCategoryFilter = true;
                categoryFilter = id;
            } else
            {
                _hasCategoryFilter = false;
            }
        }

        public void setSearchKeyword(string keyword)
        {
            _keyword = keyword;
        }

        public void setFilterPrice(int min, int max)
        {
            _hasPriceFilter = true;
            minPrice = min;
            maxPrice = max;
        }

        public void removeFilterPrice()
        {
            _hasPriceFilter = false;
        }
        #endregion

        public List<Product> loadAllProducts () {
            List<Product> list = new List<Product>();

            var sql = @"
                select *, count(*) over() as Total from Product
                where (LOWER(CONVERT(VARCHAR(100), Name)) LIKE LOWER(CONVERT(VARCHAR(100), @Keyword))
                        OR LOWER(CONVERT(NVARCHAR(100), Author)) LIKE LOWER(CONVERT(NVARCHAR(100), @Keyword))) 
                "
                + (_hasCategoryFilter ? " AND CatID = @CatID " : " ")
                + (_hasPublishedYearFilter ? " AND PublishedYear = @PublishedYear " : " ")
                + (_hasPriceFilter ? " AND (SellingPrice BETWEEN @MinPrice AND @MaxPrice) " : " ")
                + _sortingCriteriaQuery
                ;
        
            // MessageBox.Show("Command: " + sql);
            var command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.Add("@Keyword", SqlDbType.NVarChar).Value = $"%{_keyword}%";

            if (_hasCategoryFilter)
            {
                command.Parameters.Add("@CatID", SqlDbType.Int).Value = categoryFilter;
            }
            if (_hasPublishedYearFilter)
            {
                command.Parameters.Add("@PublishedYear", SqlDbType.Int).Value = yearFilter;
            }
            if (_hasPriceFilter)
            {
                command.Parameters.Add("@MinPrice", SqlDbType.Int).Value = minPrice;
                command.Parameters.Add("@MaxPrice", SqlDbType.Int).Value = maxPrice;

            }

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Create Product instances and populate properties
                    int id = (int)reader["ID"];
                    string name = (string)reader["Name"];
                    int categoryId = (int)reader["CatID"];
                    int quantity = (int)reader["Quantity"];
                    string imagePath = (string)reader["ImagePath"];
                    string author = (string)reader["Author"];
                    int publishedYear = (int)reader["PublishedYear"];
                    int sellingPrice =  Convert.ToInt32((decimal) reader["SellingPrice"]);
                    int purchasePrice = Convert.ToInt32((decimal)reader["PurchasePrice"]);


                    Product p = new Product
                    {
                        ID = id,
                        Name = name,
                        CatID = categoryId,
                        Author = author,
                        PublishedYear = publishedYear,
                        SellingPrice = sellingPrice,
                        PurchasePrice = purchasePrice,
                        ImagePath = imagePath,
                        Quantity = quantity

                    };

                    list.Add(p);

                }
            }
            return list;
        }
    }
}
