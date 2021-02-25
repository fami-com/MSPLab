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
        private Label _titleLabel;
        private Label _subtitleLabel;
        private Label _priceLabel;
        private Label _isbnLabel;
        private Image _image;

        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(BookCell), "Title");
        public static readonly BindableProperty SubtitleProperty = BindableProperty.Create("Subtitle", typeof(string), typeof(BookCell), "Subtitle");
        public static readonly BindableProperty PriceProperty = BindableProperty.Create("Price", typeof(decimal?), typeof(BookCell), null);
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
        
        public decimal? Price
        {
            get => (decimal?)GetValue(PriceProperty);
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
            set => SetValue(ImageProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                _titleLabel.Text = Title;
                _subtitleLabel.Text = Subtitle;
                _priceLabel.Text = !(Price is null) ? $"${Price}" : "Invalid Price";
                _isbnLabel.Text = Isbn;
                _image.Source = new UriImageSource
                {
                    CachingEnabled = true,
                    Uri = new Uri(Image)
                };
            }
        }

        public BookCell()
        {
            _titleLabel = new Label() {HorizontalTextAlignment = TextAlignment.Start };
            _subtitleLabel = new Label() { HorizontalTextAlignment = TextAlignment.Start, FontSize = 12, TextColor = Color.Gray };
            _priceLabel = new Label() { HorizontalTextAlignment = TextAlignment.End, LineBreakMode= LineBreakMode.NoWrap };
            _isbnLabel = new Label() { HorizontalTextAlignment = TextAlignment.End, LineBreakMode = LineBreakMode.NoWrap, TextColor = Color.Gray };
            _image = new Image() { HorizontalOptions = LayoutOptions.Start };

            _titleLabel.SetBinding(Label.TextProperty, "title");
            _subtitleLabel.SetBinding(Label.TextProperty, "subtitle");
            _priceLabel.SetBinding(Label.TextProperty, "price");
            _isbnLabel.SetBinding(Label.TextProperty, "isbn");
            _image.SetBinding(Xamarin.Forms.Image.SourceProperty, "image");

            var dataLayout = new StackLayout() { HorizontalOptions=LayoutOptions.FillAndExpand, Margin = new Thickness(5, 1) };
            var metadataLayout = new StackLayout() { HorizontalOptions = LayoutOptions.EndAndExpand, Margin = new Thickness(5, 1) };
            var horizontal = new StackLayout() { Orientation = StackOrientation.Horizontal };
            var wrapper = new StackLayout() { Margin = new Thickness(5, 2) };

            dataLayout.Children.Add(_titleLabel);
            dataLayout.Children.Add(_subtitleLabel);

            metadataLayout.Children.Add(_priceLabel);
            metadataLayout.Children.Add(_isbnLabel);

            horizontal.Children.Add(_image);
            horizontal.Children.Add(dataLayout);
            horizontal.Children.Add(metadataLayout);

            metadataLayout.MinimumWidthRequest = 100;

            wrapper.Children.Add(horizontal);

            View = wrapper;
        }
    }
}
