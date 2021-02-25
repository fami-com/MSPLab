using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP_Lab.Models
{
    public class BookDetails
    {
        [PrimaryKey]
        public string Isbn { get; set; }

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Authors { get; set; }
        public string Publisher { get; set; }
        public int Pages { get; set; }
        public int Year { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
    }
}
