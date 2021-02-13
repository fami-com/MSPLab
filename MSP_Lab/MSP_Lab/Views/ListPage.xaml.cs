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
using System.Collections.ObjectModel;

namespace MSP_Lab.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        private List<Book> books;
        private ObservableCollection<Book> display;

        private Book _book;

        public ListPage()
        {
            InitializeComponent();

            books = new List<Book>();
            display = new ObservableCollection<Book>();


            booksView.ItemsSource = display;

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Properties.Resources)).Assembly;

            var c = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            c.NumberFormat.CurrencySymbol = "$";
            CultureInfo.CurrentCulture = c;

            using (var stream = assembly.GetManifestResourceStream("MSP_Lab.Data.BooksList.json"))
            using (var streamReader = new StreamReader(stream))
            using (var reader = new JsonTextReader(streamReader))
            {
                var a = JObject.Load(reader);
                a["books"].Select(v => new Book
                {
                    Title = (string)v["title"],
                    Subtitle = (string)v["subtitle"],
                    ISBN = (string)v["isbn13"],
                    Price = decimal.TryParse((string)v["price"], NumberStyles.Currency, CultureInfo.CurrentCulture, out var t) ? (decimal?)t : null,
                    Image = (string)v["image"]
                }).OrderBy(x => x.Title).ToList().ForEach(AddBook);
            }
        }

        private void AddBook(Book book)
        {
            Console.WriteLine("!!!!!!!!!!!!!!! {0} {1}", books.Count, display.Count);

            _book = book;

            Console.WriteLine("!!!!!!!!!!!!!!! {0}", _book);

            books.Add(book);
            display.Add(book);


            Console.WriteLine("!!!!!!!!!!!!!!! {0} {1}", books.Count, display.Count);
        }

        private void DeleteBook(Book book)
        {
            books.Remove(book);
            display.Remove(book);

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Console.WriteLine("!!!!!!!!!!!!!!! {0} {1}", books.Count, display.Count);

        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            var searchBar = (SearchBar)sender;
            var search = searchBar.Text;

            var t = books.Where(b => b.Title.Contains(search, StringComparison.CurrentCultureIgnoreCase));
            if (t.Any())
            {
                display.Clear();
                t.ToList().ForEach(i => display.Add(i));
                booksEmpty.IsVisible = false;
                booksView.IsVisible = true;
            } else
            {
                booksView.IsVisible = false;
                booksEmpty.IsVisible = true;
            }
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var lv = (ListView)sender;
            var book = (Book)lv.SelectedItem;

            try
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Properties.Resources)).Assembly;

                BookDetails details;

                using (var stream = assembly.GetManifestResourceStream($"MSP_Lab.Data.Books.{book.ISBN}.json"))
                using (var streamReader = new StreamReader(stream))
                using (var reader = new JsonTextReader(streamReader))
                {
                    var a = await JObject.LoadAsync(reader);
                    details = new BookDetails
                    {
                        Authors = ((string)a["authors"]).Split(',').Select(x => x.Trim()).ToArray(),
                        Description = (string)a["desc"],
                        Image = (string)a["image"],
                        ISBN = (string)a["isbn13"],
                        Pages = int.Parse((string)a["pages"]),
                        Price = decimal.TryParse((string)a["price"], NumberStyles.Currency, CultureInfo.CurrentCulture, out var t) ? (decimal?)t : null,
                        Publisher = (string)a["publisher"],
                        Rating = int.Parse((string)a["rating"]),
                        Subtitle = (string)a["subtitle"],
                        Title = (string)a["title"],
                        Year = int.Parse((string)a["year"])
                    };
                }

                await Navigation.PushAsync(new BookInfoPage(details));
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Error", $"Details for book {book.ISBN} not found", "OK");
            }
        }

        private void OnDelete(object sender, EventArgs e)
        {
            var book = (Book)((MenuItem)sender).CommandParameter;
            DeleteBook(book);
        }

        private async void OnAddItem(object sender, EventArgs e)
        {
            Console.WriteLine("!!!!!!!!!!!!!!! {0} {1}", books.Count, display.Count);
            await Navigation.PushAsync(new BookAddPage(AddBook));
            Console.WriteLine("!!!!!!!!!!!!!!! {0}", _book);
            Console.WriteLine("!!!!!!!!!!!!!!! {0} {1}", books.Count, display.Count);
        }
    }
}