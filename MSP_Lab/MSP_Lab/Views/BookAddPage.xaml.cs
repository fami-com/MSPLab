using MSP_Lab.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSP_Lab.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookAddPage : ContentPage
    {
        private readonly Action<Book> method;
        public BookAddPage(Action<Book> invokable)
        {
            method = invokable;
            InitializeComponent();
        }

        private async void OnSubmit(object sender, EventArgs e)
        {
            var title = titleEntry.Text;
            var subtitle = subtitleEntry.Text;
            var isbn = isbnEntry.Text;
            if (!isbn.All(x => x >= '0' && x <= '9') || isbn.Length != 13)
            {
                await DisplayAlert("Error", $"Invalid ISBN: {isbn}", "OK");
                return;
            }

            if(!decimal.TryParse(priceEntry.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out var price))
            {
                await DisplayAlert("Error", $"Invalid price: {priceEntry.Text}", "OK");
                return;
            }

            var book = new Book
            {
                Image = "",
                ISBN = isbn,
                Price = price,
                Subtitle = subtitle,
                Title = title
            };

            Console.WriteLine(book);

            method(book);
            await Navigation.PopAsync();
        }
    }
}