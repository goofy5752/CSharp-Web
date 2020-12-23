using System.Text.RegularExpressions;
using CarShop.Services;
using CarShop.ViewModels.Cars;
using SUS.HTTP;
using SUS.MvcFramework;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsService _carsService;

        public CarsController(ICarsService carsService)
        {
            _carsService = carsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var cars = this._carsService.GetAll();
            return this.View(cars);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(CreateCarInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(input.Model) || input.Model.Length < 5 || input.Model.Length > 20)
            {
                return this.Error("Model is not valid.");
            }

            if (string.IsNullOrEmpty(input.Year.ToString()))
            {
                return this.Error("Year is required.");
            }

            if (string.IsNullOrEmpty(input.Image))
            {
                return this.Error("Image is required.");
            }

            if (string.IsNullOrEmpty(input.PlateNumber) || Regex.IsMatch(input.Model, @"^\w{2}\d{4}\w{2}$"))
            {
                return this.Error("Invalid license plate number;");
            }

            input.OwnerId = this.GetUserId();

            this._carsService.Create(input);
            return this.Redirect("/Cars/All");
        }
    }
}
