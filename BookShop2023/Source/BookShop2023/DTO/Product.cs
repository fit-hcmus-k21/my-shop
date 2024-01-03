using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ProjectMyShop.DTO
{
    public class Product: ICloneable
    {
        public int ID { get; set; }
        public string Name { get; set; } = "";
        public int CatID { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; } = "";
        public int PurchasePrice { get; set; }
        public int SellingPrice { get; set; }
        public DateTime UploadDate { get; set; }
        public string ImagePath { get; set; }
        public string Author { get; set; }
        public int PublishedYear { get; set; }


        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class BestSellingProduct : Product
    {
        public int SoldQuantity { get; set; }
    }
}
