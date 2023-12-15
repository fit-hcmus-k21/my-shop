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
        public string CustomerName { get; set; }
        public DateOnly CreatedAt { get; set; }
        public int Status { get; set; }
        public string CustomerAddress { get; set; }
        public int? VoucherID { get; set; }
        public float? FinalTotal { get; set; }

        //public List<OrderDetail> OrderDetailList { get; set; }


    }

}
