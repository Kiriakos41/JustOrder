using JustOrder.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JustOrder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        CartViewModel vm;
        public CartPage()
        {
            InitializeComponent();
            BindingContext = vm = new CartViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.OnAppearingAsync();
        }
    }
}