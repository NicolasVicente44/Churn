using Churn.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;


namespace WorldDominion.Components.ViewComponents
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var menuItems = new List<MenuItem>
            {
                    new MenuItem {Controller = "Home", Action = "Index", Label = "Home"},
                    new MenuItem {Controller = "Shop", Action = "Index", Label = "Shop"},
                    new MenuItem {Controller = "Shop", Action = "ViewMyCart", Label = "Cart", Authorized = true },
                    new MenuItem {Controller = "Shop", Action = "Orders", Label = "Orders", Authorized = true},
                    new MenuItem {Controller = "Orders", Action = "Index", Label = "Admin", Authorized = true, AllowedRoles = new List<string> {"Administrator"},
                    DropdownItems = new List<MenuItem> {
                         new MenuItem {Controller = "Categories", Action = "Index", Label = "Categories"},
                         new MenuItem {Controller = "Products", Action = "Index", Label = "Products"},
                         new MenuItem {Controller = "Orders", Action = "Index", Label = "Orders"},
                         new MenuItem {Controller = "Carts", Action = "Index", Label = "Carts"},
                    } },
                    new MenuItem {Controller = "Home", Action = "About", Label = "About"},
                    new MenuItem {Controller = "Home", Action = "Privacy", Label = "Privacy"},

                    


            };
            return View(menuItems); //becomes model in the view
        }
    }
}
