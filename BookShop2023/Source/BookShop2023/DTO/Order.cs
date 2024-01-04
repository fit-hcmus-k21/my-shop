using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMyShop.DTO
{
    public class Order
    {

        public int ID { get; set; }
        public int Status { get; set; }
        public int CustomerID { get; set; }
      
        public int? VoucherID { get; set; }
        public int? FinalTotal { get; set; }
        public DateOnly CreatedAt { get; set; }

    }

}
