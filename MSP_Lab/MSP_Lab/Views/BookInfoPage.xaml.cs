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
        private BookDetails _details;

        //public BookInfoPage()
        //{
        //    details = new BookDetails();
        //    InitializeComponent();
        //}

        public BookInfoPage(BookDetails det)
        {
            _details = det;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            bookTitle.Text = _details.Title;
            bookSubtitle.Text = _details.Subtitle;
            bookYear.Text = _details.Year.ToString();
            bookAuthors.Text = "Authors: " + string.Join(", ", _details.Authors);
            bookIsbn.Text = _details.Isbn;
            bookPages.Text = $"Pages: {_details.Pages}";
            bookPrice.Text = !(_details.Price is null) ? $"${_details.Price}" : "Invalid Price";
            bookPublisherInfo.Text = $"Published by {_details.Publisher}";
            bookDesc.Text = _details.Description;
            bookRating.Text = $"{_details.Rating}/5";
            bookCover.Source = ImageSource.FromUri(new Uri(_details.Image));
        }
    }
}