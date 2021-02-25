using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP_Lab.Models
{
    class Request
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Url { get; set; }
        public string Body { get; set; }
    }
}
