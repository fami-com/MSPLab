using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using MSP_Lab.Models;
using MSP_Lab.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSP_Lab.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        private List<Book> _books;
        private ObservableCollection<Book> _display;

        private const string _searchUrl = "https://api.itbook.store/1.0/search/{0}";
        private const string _detailsUrl = "https://api.itbook.store/1.0/books/{0}";

        private readonly HttpClient _client;

        public ListPage()
        {
            InitializeComponent();

            _client = new HttpClient();

            _books = new List<Book>();
            _display = new ObservableCollection<Book>();
            
            booksView.ItemsSource = _display;
        }

        private void AddBook(Book book)
        {
            _display.Add(book);
        }

        private void DeleteBook(Book book)
        {
            _display.Remove(book);
        }

        private async void OnTextChanged(object sender, EventArgs e)
        {
            var searchBar = (SearchBar)sender;
            var search = searchBar.Text;

            _display.Clear();

            if (search.Length < 3) return;

            var text = search.Replace(' ', '+');

            var response = await _client.GetAsync(string.Format(_searchUrl, text));

            if (!response.IsSuccessStatusCode)
            {
                await DisplayAlert("Error", "Error", "Ok");
                return;
            }

            using var data = await response.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(data);
            using var reader = new JsonTextReader(streamReader);

            var a = await JObject.LoadAsync(reader);

            var t = a["books"].Select(v => new Book
            {
                Title = (string)v["title"],
                Subtitle = (string)v["subtitle"],
                Isbn = (string)v["isbn13"],
                Price = decimal.TryParse((string)v["price"], NumberStyles.Currency, CultureInfo.CurrentCulture, out var t) ? (decimal?)t : null,
                Image = (string)v["image"]
            });

            foreach(var b in t)
            {
                AddBook(b);
            }
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var lv = (ListView)sender;
            var book = (Book)lv.SelectedItem;

            try
            {
                var response = await _client.GetAsync(string.Format(_detailsUrl, book.Isbn));

                response.EnsureSuccessStatusCode();

                using var data = await response.Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader(data);
                using var reader = new JsonTextReader(streamReader);
                
                var a = await JObject.LoadAsync(reader);

                Console.WriteLine(a);

                var details = new BookDetails
                {
                    Authors = ((string)a["authors"])?.Split(',').Select(x => x.Trim()).ToArray(),
                    Description = (string)a["desc"],
                    Image = (string)a["image"],
                    Isbn = (string)a["isbn13"],
                    Pages = int.Parse(((string)a["pages"])!),
                    Price = decimal.TryParse((string)a["price"], NumberStyles.Currency, CultureInfo.CurrentCulture, out var t) ? (decimal?)t : null,
                    Publisher = (string)a["publisher"],
                    Rating = int.Parse(((string)a["rating"])!) ,
                    Subtitle = (string)a["subtitle"],
                    Title = (string)a["title"],
                    Year = int.Parse(((string)a["year"])!)
                };
                
                await Navigation.PushAsync(new BookInfoPage(details));
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Error", $"Details for book {book.Isbn} not found", "OK");
            }
        }

        private void OnDelete(object sender, EventArgs e)
        {
            var book = (Book)((MenuItem)sender).CommandParameter;
            DeleteBook(book);
        }

        private async void OnAddItem(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BookAddPage(AddBook));
        }
    }
}