using JustOrder.ViewModels;
using JustOrder.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace JustOrder
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("AboutPage", typeof(AboutPage));
            Routing.RegisterRoute("CartPage", typeof(CartPage));
        }
    }
}
