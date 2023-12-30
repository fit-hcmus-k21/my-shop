using BookShop2023.DAO;
using BookShop2023.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookShop2023.BUS
{
    internal class OrderStatusEnumBUS
    {
        private static  List<OrderStatusEnum> _enums;

        public static List<OrderStatusEnum> Instance()
        {
            if (_enums== null)
            {
                _enums = OrderStatusEnumDAO.loadAll();
                //MessageBox.Show("All status enums: " + _enums.Count());
            }
            return _enums;
        }

        public Dictionary<int, string> StatusDictionary()
        {
            List<OrderStatusEnum> ListEnum = new List<OrderStatusEnum>();
            ListEnum = OrderStatusEnumDAO.loadAll();

            Dictionary<int, string> keyValuePairs= new Dictionary<int, string>();

            foreach (var status in ListEnum)
            {
                keyValuePairs[status.Value] = status.DisplayText;
            }
            return keyValuePairs;
        }

        public static int GetValueKeyNew()
        {
            if (_enums == null)
            {
                _enums = OrderStatusEnumDAO.loadAll();
            }

            foreach (OrderStatusEnum item in _enums)
            {
                if (item.Key.Equals("New"))
                {
                    return item.Value;
                }
            }
            return -1;
        }

        public static int GetValueKeyCompleted()
        {
            if (_enums == null)
            {
                _enums = OrderStatusEnumDAO.loadAll();
            }
            foreach (OrderStatusEnum item in _enums)
            {
                if (item.Key.Equals("Completed"))
                {
                    return item.Value;
                }
            }
            return -1;
        }

        public static int GetValueKeyCancelled()
        {
            if (_enums == null)
            {
                _enums = OrderStatusEnumDAO.loadAll();
            }

            foreach (OrderStatusEnum item in _enums)
            {
                if (item.Key.Equals("Cancelled"))
                {
                    return item.Value;
                }
            }
            return -1;
        }

        public static int GetValueKeyShipping()
        {
            if (_enums == null)
            {
                _enums = OrderStatusEnumDAO.loadAll();
            }

            foreach (OrderStatusEnum item in _enums)
            {
                if (item.Key.Equals("Shipping"))
                {
                    return item.Value;
                }
            }
            return -1;
        }
    }
}
