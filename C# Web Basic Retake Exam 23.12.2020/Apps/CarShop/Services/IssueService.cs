using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels.Issues;

namespace CarShop.Services
{
    public class IssueService : IIssueService
    {
        private readonly ApplicationDbContext _dbContext;

        public IssueService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public GetIssuesViewModel GetIssues(string carId, string userId)
        {
            var car = this._dbContext.Cars.FirstOrDefault(x => x.Id == carId);
            var isMechanic = this._dbContext.Users.FirstOrDefault(x => x.Id == userId).IsMechanic;
            var carIssues = this._dbContext.Issues.Where(x => x.CarId == carId).ToList();
            var issueViewModel = new GetIssuesViewModel
            {
                CarId = car.Id,
                Model = car.Model,
                Year = car.Year,
                Issues = carIssues,
                IsMechanic = isMechanic
            };

            return issueViewModel;
        }

        public void Create(string carId, string description)
        {
            var newIssue = new Issue
            {
                Description = description,
                CarId = carId,
                IsFixed = false
            };

            this._dbContext.Cars.FirstOrDefault(x => x.Id == carId)?.Issues.Add(newIssue);
            this._dbContext.Issues.Add(newIssue);
            this._dbContext.SaveChanges();
        }

        public void Delete(string issueId)
        {
            var issueToRemove = this._dbContext.Issues.FirstOrDefault(x => x.Id == issueId);

            this._dbContext.Issues.Remove(issueToRemove);
            this._dbContext.SaveChanges();
        }

        public void Fix(string issueId)
        {
            var issueToRemove = this._dbContext.Issues.FirstOrDefault(x => x.Id == issueId).IsFixed = true;
            this._dbContext.SaveChanges();
        }
    }
}
