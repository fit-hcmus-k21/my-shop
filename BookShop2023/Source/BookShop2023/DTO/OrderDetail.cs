using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMyShop.DTO
{
    public class OrderDetail : ICloneable
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
