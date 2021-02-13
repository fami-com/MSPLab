using MSP_Lab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSP_Lab.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookInfoPage : ContentPage
    {
        private BookDetails details;

        //public BookInfoPage()
        //{
        //    details = new BookDetails();
        //    InitializeComponent();
        //}

        public BookInfoPage(BookDetails det)
        {
            details = det;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            bookTitle.Text = details.Title;
            bookSubtitle.Text = details.Subtitle;
            bookYear.Text = details.Year.ToString();
            bookAuthors.Text = "Authors: " + string.Join(", ", details.Authors);
            bookIsbn.Text = details.ISBN;
            bookPages.Text = $"Pages: {details.Pages}";
            bookPrice.Text = !(details.Price is null) ? $"${details.Price}" : "Invalid Price";
            bookPublisherInfo.Text = $"Published by {details.Publisher}";
            bookDesc.Text = details.Description;
            bookRating.Text = $"{details.Rating}/5";
            bookCover.Source = ImageSource.FromResource("MSP_Lab.Data.Books." + details.Image, typeof(BookInfoPage).GetTypeInfo().Assembly);
        }
    }
}