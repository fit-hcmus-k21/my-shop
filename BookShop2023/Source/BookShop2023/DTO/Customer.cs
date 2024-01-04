using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop2023.DTO
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class CustomerDataGrid : Customer
    {
        public int Rank { get; set; }
        public int TotalOrders { get; set; }
        public int TotalCost { get; set; }
        public int TotalProducts { get; set; }
}
}
