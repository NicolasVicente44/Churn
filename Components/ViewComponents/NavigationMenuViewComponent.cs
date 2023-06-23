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
                    new MenuItem {Controller = "Home", Action = "Privacy", Label = "Privacy"},

                    new MenuItem {Controller = "CreditCards", Action = "Index", Label = "Credit Cards", DropdownItems = new List<MenuItem> {
                    new MenuItem {Controller = "CreditCards", Action = "Index", Label = "List"},
                    new MenuItem {Controller = "CreditCards", Action = "Create", Label = "Create"},
                    } },
                    new MenuItem {Controller = "Mortgages", Action = "Index", Label = "Mortgages", DropdownItems = new List<MenuItem> {
                    new MenuItem {Controller = "Mortgages", Action = "Index", Label = "List"},
                    new MenuItem {Controller = "Mortgages", Action = "Create", Label = "Create"},
                    } },
                    new MenuItem {Controller = "Loans", Action = "Index", Label = "Loans", DropdownItems = new List<MenuItem> {
                    new MenuItem {Controller = "Loans", Action = "Index", Label = "List"},
                    new MenuItem {Controller = "Loans", Action = "Create", Label = "Create"},
                    } },
                    new MenuItem {Controller = "Accounts", Action = "Index", Label = "Accounts", DropdownItems = new List<MenuItem> {
                    new MenuItem {Controller = "Accounts", Action = "Index", Label = "List"},
                    new MenuItem {Controller = "Accounts", Action = "Create", Label = "Create"},
                    } }, 
                    new MenuItem {Controller = "Investments", Action = "Index", Label = "Investments", DropdownItems = new List<MenuItem> {
                    new MenuItem {Controller = "Investments", Action = "Index", Label = "List"},
                    new MenuItem {Controller = "Investments", Action = "Create", Label = "Create"},
                    } }, 


            };
            return View(menuItems); //becomes model in the view
        }
    }
}
