using BookShop2023.DAO;
using BookShop2023.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop2023.BUS
{
    internal class CustomerBUS
    {
        private CustomerDAO _customerDAO;
        private Dictionary<int, Customer> _customerDictionary = new Dictionary<int, Customer>();

        public CustomerBUS()
        {
            if (this._customerDAO == null)
            {
                this._customerDAO = new CustomerDAO();
            }
        }

        public List<Customer> loadAll()
        {
            return _customerDAO.loadAll();
        }

        public Dictionary<int, Customer> CustomerDictionary ()
        {
            List<Customer> list = loadAll();
            foreach (Customer customer in list)
            {
                _customerDictionary.Add(customer.ID, customer);
            }
            return _customerDictionary;

        }

        public int GetLatestInsertID()
        {
            return _customerDAO.GetLastestInsertID();
        }

        public void Insert(Customer customer)
        {
            _customerDAO.InsertCustomer(customer);
        }

        public void Update(Customer customer)
        {
            _customerDAO.UpdateCustomer(customer);
        }

        public void Remove(Customer customer)
        {
            _customerDAO.RemoveCustomer(customer.ID);
        }

        public Customer GetCustomerByID(int id)
        {
            return _customerDAO.GetCustomerByID(id);
        }
    }
}
