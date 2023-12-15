using ProjectMyShop.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMyShop.ViewModels
{
    internal class OrderDetailViewModel : INotifyPropertyChanged
    {
        public BindingList<OrderDetail> OrderDetails { get; set; } = new BindingList<OrderDetail>();

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
