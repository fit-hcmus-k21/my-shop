using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop2023.DTO
{
    public class OrderStatusEnum : INotifyPropertyChanged
    {
        public string Key { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
        public string DisplayText { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
