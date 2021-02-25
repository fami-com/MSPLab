using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP_Lab.Models
{
    public class RemoteImage
    {
        [PrimaryKey]
        public string Url { get; set; }

        public string Search { get; set; }
    }
}
