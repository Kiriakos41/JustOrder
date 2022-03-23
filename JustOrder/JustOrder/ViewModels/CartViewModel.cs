using JustOrder.Models;
using JustOrder.Repositories;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JustOrder.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        public Command GoBack { get; }
        public Command<Cart> AddItem { get; }
        public Command<Cart> RemoveItem { get; }
        public Command<Cart> DelItem { get; }
        public ObservableCollection<Cart> Items { get; set; } = new ObservableCollection<Cart>();
        public Command LoadItemsCommand { get; }
        public CartViewModel()
        {
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            GoBack = new Command(() => Back());
            AddItem = new Command<Cart>(PlusItem);
            RemoveItem = new Command<Cart>(MinusItem);
            DelItem = new Command<Cart>(DeleteItem);
        }
        public void Back()
        {
            Shell.Current.GoToAsync("..");
        }
        async Task ExecuteLoadItemsCommand()
        {
            Items.Clear();
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var refills = await unitOfwork.CartTable.Get<Cart>();
                foreach (var item in refills)
                {
                    Items.Add(item);
                }
            }
            IsBusy = false;
        }

        public async void DeleteItem(Cart item)
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                if (item == null)
                    return;

                await unitOfwork.CartTable.Delete(item);
                await ExecuteLoadItemsCommand();
            }
        }

        public async void PlusItem(Cart item)
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                if (item == null)
                    return;
                item.Quantity++;
                if (item.Quantity != 1)
                {
                    item.IsZero = true;
                }
                    
                if (item.Quantity == 1)
                {
                    item.IsZero = false;
                }
                    


                await unitOfwork.CartTable.Update(item);
                await ExecuteLoadItemsCommand();
            }
        }

        public async void MinusItem(Cart item)
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                if (item == null)
                    return;

                if (item.Quantity > 1)
                {
                    item.Quantity--;
                    item.IsZero = true;
                    if(item.Quantity == 1)
                        item.IsZero= false;
                }
                else if(item.Quantity == 1)
                {
                    item.IsZero = false;
                }


                await unitOfwork.CartTable.Update(item);
                await ExecuteLoadItemsCommand();
            }
        }

        public void OnAppearingAsync()
        {
            IsBusy = true;
        }
    }
}
