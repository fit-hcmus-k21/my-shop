using BookShop2023.DAO;
using ProjectMyShop.DAO;
using ProjectMyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop2023.BUS
{
    internal class OrderDetailBUS
    {
        private OrderDetailDAO _orderDetailDAO;

        public OrderDetailBUS()
        {
            _orderDetailDAO = new OrderDetailDAO();
        }

        public void InsertList(List<OrderDetail> list)
        {
            _orderDetailDAO.InsertList(list);
        }

        public void AddOrderDetail(OrderDetail detail)
        {
            _orderDetailDAO.AddOrderDetail(detail);
        }

        public void UpdateOrderDetail(int oldProductID, OrderDetail detail)
        {
            if (detail.Quantity >= 0)
            {
                _orderDetailDAO.UpdateOrderDetail(oldProductID, detail);
            }
            else
            {
                throw new Exception("Invalid Quantity");
            }
        }
        public void DeleteOrderDetail(OrderDetail detail)
        {
            _orderDetailDAO.DeleteOrderDetail(detail);
        }
    }
}
