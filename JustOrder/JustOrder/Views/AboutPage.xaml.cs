using JustOrder.Models;
using JustOrder.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JustOrder.Views
{
    public partial class AboutPage : ContentPage
    {
        MainPageViewModel mv;
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = mv = new MainPageViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            mv.CheckForBadge();
            mv.LoadList();
        }
    }
}