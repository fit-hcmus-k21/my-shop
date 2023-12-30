using ProjectMyShop.DAO;
using ProjectMyShop.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMyShop.BUS
{
    internal class ProductBUS
    {
        private ProductDAO _ProductDAO;

        public ProductBUS()
        {
            _ProductDAO = new ProductDAO();
            if (_ProductDAO.CanConnect())
            {
                _ProductDAO.Connect();
            }

        }

        public Dictionary<int, Product> ProductDictionary()
        {
            List<Product> products = loadAllProducts();
            Dictionary<int, Product> _ProductDictionary = new Dictionary<int, Product>();

            foreach (Product p in products)
            {
                _ProductDictionary.Add(p.ID, p);
            }
            return _ProductDictionary;
        }

        #region not updated yet
        public int GetTotalProduct()
        {
            return _ProductDAO.getTotalProduct();
        }
        public List<Product> Top5OutQuantity()
        {
            return _ProductDAO.GetTop5OutQuantity();
        }

        public List<Product> getProductsAccordingToSpecificCategory(int srcCategoryID)
        {
            return _ProductDAO.getProductsAccordingToSpecificCategory(srcCategoryID);
        }

        public void updateProduct(Product p)
        {
            _ProductDAO.updateProduct(p.ID, p);
        }

        public void addProduct(Product Product)
        {
            if (Product.Quantity < 0)
            {
                throw new Exception("Invalid Quantity");
            }
            else if (Product.PurchasePrice < 0 || Product.SellingPrice < 0)
            {
                throw new Exception("Invalid price");
            }
            else
            {
                Product.UploadDate = DateTime.Now.Date;
                _ProductDAO.addProduct(Product);
                Product.ID = _ProductDAO.GetLastestInsertID();
            }
        }
        public void removeProduct(Product Product)
        {
            _ProductDAO.deleteProduct(Product.ID);
        }
        public void updateProduct(int ID, Product Product)
        {
            Debug.WriteLine(Product.Quantity);
            if (Product.Quantity < 0)
            {
                throw new Exception("Invalid Quantity");
            }
            else if (Product.PurchasePrice < 0 || Product.SellingPrice < 0)
            {
                throw new Exception("Invalid price");
            }
            else
            {
                _ProductDAO.updateProduct(ID, Product);
            }
        }

        public List<BestSellingProduct> getBestSellingProductsInWeek(DateTime src)
        {
            return _ProductDAO.getBestSellingProductsInWeek(src);
        }

        public List<BestSellingProduct> getBestSellingProductsInMonth(DateTime src)
        {
            return _ProductDAO.getBestSellingProductsInMonth(src);
        }

        public List<BestSellingProduct> getBestSellingProductsInYear(DateTime src)
        {
            return _ProductDAO.getBestSellingProductsInYear(src);
        }
        public Product? getProductByID(int ProductID)
        {
            return _ProductDAO.getProductByID(ProductID);
        }
        #endregion

        #region Manage Product | support methods
        public List<Product> loadAllProducts()
        {
            return _ProductDAO.loadAllProducts();
        }

        public List<Product> loadAllProductsWithQuantity()
        {
            List<Product> list = new List<Product>(_ProductDAO.loadAllProducts());
            foreach (Product p in list)
            {
                if (p.Quantity == 0)
                {
                    list.Remove(p);
                }
            }
            return list;
        }

        public void setFilterCat(int id)
        {
            _ProductDAO.setFilterCat(id);
        }

        public void setSearchKeyword(string keyword)
        {
            _ProductDAO.setSearchKeyword(keyword);
        }

        public void setFilterPrice(int min, int max)
        {
            _ProductDAO.setFilterPrice(min, max);
        }

        public void removeFilterPrice()
        {
            _ProductDAO.removeFilterPrice();
        }

        public void setSortingCriteriaQuery(String query)
        {
            _ProductDAO.setSortingCriteriaQuery(query);
        }
        #endregion

    }

}
