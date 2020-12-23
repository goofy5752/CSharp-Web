using System.Collections.Generic;
using CarShop.Data.Models;

namespace CarShop.ViewModels.Issues
{
    public class GetIssuesViewModel
    {
        public string CarId { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public bool IsMechanic { get; set; }

        public ICollection<Issue> Issues { get; set; }
    }
}
