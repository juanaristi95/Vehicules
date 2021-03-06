using System;
using System.ComponentModel.DataAnnotations;

namespace Vehicules.API.Data.Entities
{
    public class VehiclePhoto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Vehicle Vehicle { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://vehiculesjdaitm2021.azurewebsites.net/images/noimage.png"
            : $"https://vehiclesjda.blob.core.windows.net/vehiclephotos/{ImageId}";
    }
}