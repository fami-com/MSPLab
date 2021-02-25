using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SQLitePCL;
using SQLite;

namespace MSP_Lab.Models
{
    public class Book
    {
        [PrimaryKey]
        public string Isbn { get; set; }

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
    }
}
