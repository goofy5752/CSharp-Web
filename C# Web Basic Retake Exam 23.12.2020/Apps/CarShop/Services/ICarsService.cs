using System.Collections.Generic;
using CarShop.ViewModels.Cars;

namespace CarShop.Services
{
    public interface ICarsService
    {
        void Create(CreateCarInputModel car);

        IEnumerable<GetCarViewModel> GetAll();
    }
}
