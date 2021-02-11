using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MSP_Lab.Models
{
    class Book
    {
        private string _image;

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ISBN { get; set; }
        public string Price { get; set; }
        public string Image { get => _image; set => _image = value == "" ? null : value; }

    }
}
