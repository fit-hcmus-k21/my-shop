using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop2023.Models
{
    public class OrderDataGridItemSource
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public DateOnly CreatedAt { get; set; }
        public string Status { get; set; }

        public int FinalTotal { get; set; }
    }
}
