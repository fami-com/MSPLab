using MSP_Lab.ViewModels;
using MSP_Lab.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MSP_Lab
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
