﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ProjectMyShop.DTO
{
    public class Category: ICloneable
    {
        public int ID { get; set; }
        public string Name { get; set; }


        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
