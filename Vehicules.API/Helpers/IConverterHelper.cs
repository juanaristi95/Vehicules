using System;
using System.Threading.Tasks;
using Vehicules.API.Data.Entities;
using Vehicules.API.Models;

namespace Vehicules.API.Helpers
{
    public interface IConverterHelper
    {
        Task<User> ToUserAsync(UserViewModel model, Guid imageId, bool isNew);

        UserViewModel ToUserViewModel(User user);

        Task<Vehicle> ToVehicleAsync(VehicleViewModel model, bool isNew);

        VehicleViewModel ToVehicleViewModel(Vehicle vehicle);

        Task<Detail> ToDetailAsync(DetailViewModel model, bool isNew);

        DetailViewModel ToDetailViewModel(Detail detail);
    }
}
