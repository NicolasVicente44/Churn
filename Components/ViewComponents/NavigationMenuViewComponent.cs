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
                    new MenuItem {Controller = "Home", Action = "About", Label = "About"},
                    new MenuItem {Controller = "Categories", Action = "Index", Label = "Categories", DropdownItems = new List<MenuItem> {
                    new MenuItem {Controller = "Categories", Action = "Index", Label = "List"},
                    new MenuItem {Controller = "Categories", Action = "Create", Label = "Create"},
                    } },
                    new MenuItem {Controller = "Products", Action = "Index", Label = "Products", DropdownItems = new List<MenuItem> {
                    new MenuItem {Controller = "Products", Action = "Index", Label = "List"},
                    new MenuItem {Controller = "Products", Action = "Create", Label = "Create"},
                    } },
                    new MenuItem {Controller = "Home", Action = "Privacy", Label = "Privacy"}

            };
            return View(menuItems); //becomes model in the view
        }
    }
}
