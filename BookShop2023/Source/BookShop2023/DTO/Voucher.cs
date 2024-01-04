using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMyShop.DTO
{
    public class Voucher
    {
        public int ID { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int PercentOff { get; set; }
        public string DisplayText { get; set; }
        public int MinCost { get; set; }
        public string Description { get; set; }

        public string ImagePath = "Assets/Icons/voucher.jpg";
    }
}
