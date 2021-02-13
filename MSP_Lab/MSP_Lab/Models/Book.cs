using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MSP_Lab.Models
{
    public class Book : IEquatable<Book>
    {
        private string _image;

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ISBN { get; set; }
        public decimal? Price { get; set; }
        public string Image { get => _image; set => _image = value == "" ? null : value; }

        public bool Equals(Book other)
        {
            return Title == other.Title && Subtitle == other.Subtitle && ISBN == other.ISBN && Price == other.Price;
        }

        public override bool Equals(object obj)
        {
            return obj is Book b && Equals(b);
        }

        public static bool operator==(Book lhs, Book rhs)
        {
            return lhs is Book b1 && rhs is Book b2 && b1.Equals(b2);
        }

        public static bool operator !=(Book lhs, Book rhs)
        {
            return lhs is Book b1 && rhs is Book b2 && !b1.Equals(b2);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Subtitle, ISBN, Price);
        }

        public override string ToString()
        {
            return $"Book {{Title = {Title}, Subtitle = {Subtitle}, Price = {Price}, ISBN = {ISBN}}}";
        }
    }
}
