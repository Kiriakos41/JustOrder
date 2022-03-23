using JustOrder.Models;
using JustOrder.Repositories;
using JustOrder.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace JustOrder.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private string search;
        private string count;
        public string Count
        {
            get => count;
            set => SetProperty(ref count, value);
        }
        public string Search
        {
            get => search;
            set => SetProperty(ref search, value);
        }
        public Command Cart { get; }
        public Command<Burger> ItemTapped { get; }
        public ObservableCollection<Burger> burgers { get; set; } = new ObservableCollection<Burger>();
        public MainPageViewModel()
        {
            OpenShell = new Command(OpenSideShell);
            ItemTapped = new Command<Burger>(OnItemSelected);
            Cart = new Command(GoToCart);
        }
        public async void LoadList()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var items = await unitOfwork.BurgerTable.Get<Burger>();
                burgers.Clear();
                if (items.Count == 0)
                {
                    burgers.Add(new Burger { Code = "1", Description = "669 Cal", Name = "Whooper", Price = 5, Image = "BurgerOne" });
                    burgers.Add(new Burger { Code = "2", Description = "493 Cal", Name = "Big King", Price = 8, Image = "BurgerTwo" });
                    burgers.Add(new Burger { Code = "3", Description = "1,360 Cal", Name = "Bacon King", Price = 12, Image = "BurgerThree" });
                    burgers.Add(new Burger { Code = "4", Description = "919 Cal", Name = "Double Whopper", Price = 9, Image = "BurgerForth" });
                    burgers.Add(new Burger { Code = "5", Description = "336 Cal", Name = "Rodeo Burger", Price = 6, Image = "burger1" });
                    burgers.Add(new Burger { Code = "6", Description = "298 Cal", Name = "Cheeseburger", Price = 7.5M, Image = "burger2" });
                    burgers.Add(new Burger { Code = "7", Description = "560 Cal", Name = "Big Fish", Price = 4.5M, Image = "burger3" });
                    burgers.Add(new Burger { Code = "8", Description = "1,245 Cal", Name = "Ch'King Sandwich", Price = 6.5M, Image = "burger4" });
                    foreach (var m in burgers)
                    {
                        await unitOfwork.BurgerTable.Insert(m);
                    }
                }
                else
                {
                    foreach (var m in items)
                    {
                        burgers.Add(m);
                    }
                }
            }
        }
        public Command OpenShell { get; set; }
        public void OpenSideShell()
        {
            Shell.Current.FlyoutIsPresented = true;
        }
        async void OnItemSelected(Burger item)
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                if (item == null)
                    return;
               var cart = new Cart
                {
                    Description = item.Description,
                    Image = item.Image,
                    Name = item.Name,
                    Price = item.Price,
                    Code = item.Code,
                    Quantity = 1
                };
                var items = await unitOfwork.CartTable.Get<Cart>();

                if (items.Count == 0)
                {
                    await unitOfwork.CartTable.Insert(cart);
                }

                if (!items.Any(c => c.Code == cart.Code) && items.Count != 0)
                {
                    await unitOfwork.CartTable.Insert(cart);
                }
                else if(items.Any(c => c.Code == cart.Code) && items.Count != 0)
                {

                    var t = items
                        .Where(x => x.Code == cart.Code)
                        .Select(x => x.Code)
                        .FirstOrDefault();

                    foreach (var m in items)
                    {
                        if(t == m.Code)
                        {
                            m.Quantity++;
                            await unitOfwork.CartTable.Update(m);
                        }
                    }
                }
                var tm = await unitOfwork.CartTable.Get<Cart>();
                int count1 = tm.Count;
                if (count1 == 0)
                    Count = "";
                else
                    Count = count1.ToString();
            }
        }
        public async void CheckForBadge()
        {
            using (var unitOfwork = new UnitOfWork(App.DbPath))
            {
                var items = await unitOfwork.CartTable.Get<Cart>();

                foreach (var item in items)
                {
                    await unitOfwork.CartTable.Delete(item);
                }

                int count1 = items.Count;
                if (count1 == 0)
                    Count = "";
                else
                    Count = count1.ToString();
            }
        }
        public async void GoToCart()
        {
            await Shell.Current.GoToAsync(nameof(CartPage));
        }
    }
}
