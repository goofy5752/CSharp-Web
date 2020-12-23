using System.Collections.Generic;
using System.Linq;
using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels.Cars;

namespace CarShop.Services
{
    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext _dbContext;

        public CarsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(CreateCarInputModel car)
        {
            var newCar = new Car
            {
                Model = car.Model,
                PlateNumber = car.PlateNumber,
                PictureUrl = car.Image,
                Year = car.Year,
                OwnerId = car.OwnerId
            };

            this._dbContext.Cars.Add(newCar);
            this._dbContext.SaveChanges();
        }

        public IEnumerable<GetCarViewModel> GetAll()
        {
            var cars = this._dbContext.Cars.Select(x => new GetCarViewModel
            {
                Id = x.Id,
                Image = x.PictureUrl,
                Model = x.Model,
                Year = x.Year,
                PlateNumber = x.PlateNumber,
                FixedIssues = x.Issues.Count(i => i.IsFixed),
                RemainingIssues = x.Issues.Count(i => i.IsFixed == false)
            }).ToList();
            return cars;
        }
    }
}
