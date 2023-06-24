﻿namespace Churn.Models
{
    public class MenuItem
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Label { get; set; }

        public object RouteValues { get; set; } 

        public List<MenuItem> DropdownItems { get; set; }
    }
}
