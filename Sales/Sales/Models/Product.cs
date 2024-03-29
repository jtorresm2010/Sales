﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Models
{
    public class Product
    {
        public int albumId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string thumbnailUrl { get; set; }
        public override string ToString()
        {
            return this.title;
        }
    }
}
