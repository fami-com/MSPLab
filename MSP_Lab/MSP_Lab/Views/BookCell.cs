using MSP_Lab.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace MSP_Lab.Views
{
    class BookCell : ViewCell
    {
        private Label titleLabel;
        private Label subtitleLabel;
        private Label priceLabel;
        private Label isbnLabel;
        private Image image;

        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(BookCell), "Title");
        public static readonly BindableProperty SubtitleProperty = BindableProperty.Create("Subtitle", typeof(string), typeof(BookCell), "Subtitle");
        public static readonly BindableProperty PriceProperty = BindableProperty.Create("Price", typeof(string), typeof(BookCell), "Price");
        public static readonly BindableProperty IsbnProperty = BindableProperty.Create("Isbn", typeof(string), typeof(BookCell), "Isbn");
        public static readonly BindableProperty ImageProperty = BindableProperty.Create("Image", typeof(string), typeof(BookCell), "Image");

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Subtitle
        {
            get => (string)GetValue(SubtitleProperty);
            set => SetValue(SubtitleProperty, value);
        }
        
        public string Price
        {
            get => (string)GetValue(PriceProperty);
            set => SetValue(PriceProperty, value);
        }

        public string Isbn
        {
            get => (string)GetValue(IsbnProperty);
            set => SetValue(IsbnProperty, value);
        }

        public string Image
        {
            get => (string)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value == "" ? null : value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                titleLabel.Text = Title;
                subtitleLabel.Text = Subtitle;
                priceLabel.Text = Price;
                isbnLabel.Text = Isbn;
                image.Source = ImageSource.FromResource("MSP_Lab.Data.Books." + Image, typeof(BookCell).GetTypeInfo().Assembly);
            }
        }

        public BookCell()
        {
            titleLabel = new Label() {HorizontalTextAlignment = TextAlignment.Start };
            subtitleLabel = new Label() { HorizontalTextAlignment = TextAlignment.Start, FontSize = 12, TextColor = Color.Gray };
            priceLabel = new Label() { HorizontalTextAlignment = TextAlignment.End, LineBreakMode= LineBreakMode.NoWrap };
            isbnLabel = new Label() { HorizontalTextAlignment = TextAlignment.End, LineBreakMode = LineBreakMode.NoWrap, TextColor = Color.Gray };
            image = new Image() { HorizontalOptions = LayoutOptions.Start };

            titleLabel.SetBinding(Label.TextProperty, "title");
            subtitleLabel.SetBinding(Label.TextProperty, "subtitle");
            priceLabel.SetBinding(Label.TextProperty, "price");
            isbnLabel.SetBinding(Label.TextProperty, "isbn");
            image.SetBinding(Xamarin.Forms.Image.SourceProperty, "image");

            var dataLayout = new StackLayout() { HorizontalOptions=LayoutOptions.FillAndExpand, Margin = new Thickness(5, 1) };
            var metadataLayout = new StackLayout() { HorizontalOptions = LayoutOptions.EndAndExpand, Margin = new Thickness(5, 1) };
            var horizontal = new StackLayout() { Orientation = StackOrientation.Horizontal };
            var wrapper = new StackLayout() { Margin = new Thickness(5, 2) };

            dataLayout.Children.Add(titleLabel);
            dataLayout.Children.Add(subtitleLabel);

            metadataLayout.Children.Add(priceLabel);
            metadataLayout.Children.Add(isbnLabel);

            horizontal.Children.Add(image);
            horizontal.Children.Add(dataLayout);
            horizontal.Children.Add(metadataLayout);

            metadataLayout.MinimumWidthRequest = 100;

            wrapper.Children.Add(horizontal);

            View = wrapper;
        }
    }
}
