using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MSP_Lab.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Media;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSP_Lab.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GalleryPage : ContentPage
    {
        private const string _requestUrl = "https://pixabay.com/api/?key={0}&q={1}&image_type=photo&per_page={2}";
        private const string _apiKey = "19193969-87191e5db266905fe8936d565";
        private const string _request = "night+city";
        private const int _count = 27;

        private HttpClient _client;

        public GalleryPage()
        {
            InitializeComponent(); 

            _client = new HttpClient();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await OnRefresh();
        }

        private async void OnAddItem(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "Picking a photo is not supported on your device", "OK");
                return;
            }
            
            NavigationPage.SetHasNavigationBar(this, false);

            var img = await CrossMedia.Current.PickPhotoAsync();
            
            NavigationPage.SetHasNavigationBar(this, true);

            if (img is null) return;
            galleryLayout.Add(ImageSource.FromStream(() => img.GetStream()));
        }

        private async void OnRefresh(object sender, EventArgs e)
        {
            galleryLayout.Clear();
            await OnRefresh();
        }

        private async Task OnRefresh()
        {
            IEnumerable<RemoteImage> images = new List<RemoteImage>();

            try
            {
                var response = await _client.GetAsync(string.Format(_requestUrl, _apiKey, _request, _count));

                if (!response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Error", "Error", "Ok");
                    return;
                }

                using var data = await response.Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader(data);
                using var reader = new JsonTextReader(streamReader);

                var a = await JObject.LoadAsync(reader);

                images = a["hits"].Select(v => new RemoteImage { Url = (string)v["webformatURL"], Search = _request });

                await App.Db.InsertAllImagesAsync(images);
            }
            catch
            {
                images = await App.Db.GetImagesBySearchStringAsync(_request);
            }

            foreach (var i in images)
            {
                galleryLayout.Add(new UriImageSource
                {
                    CachingEnabled = true,
                    Uri = new Uri(i.Url)
                });
            }
        }
    }
}