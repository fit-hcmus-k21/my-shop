﻿using ProjectMyShop.DAO;
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
    }
}