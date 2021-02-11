using MSP_Lab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json.Linq;
using System.Resources;
using System.Globalization;
using System.Collections;

namespace MSP_Lab.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        private List<Book> books;

        public ListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Properties.Resources)).Assembly;

            using (var stream = assembly.GetManifestResourceStream("MSP_Lab.Data.BooksList.json"))
            using (var reader = new StreamReader(stream))
            {
                var str = reader.ReadToEnd();
                JObject a = JObject.Parse(str);
                books = a["books"].Select(v => new Book { Title = (string)v["title"], Subtitle = (string)v["subtitle"], ISBN = (string)v["isbn13"], Price = (string)v["price"], Image = (string)v["image"] }).ToList();
            }

            booksView.ItemsSource = books;
        }
    }
}