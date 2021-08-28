using System.ComponentModel.DataAnnotations;

namespace Vehicules.API.Data.Entities
{
    public class VehicleType
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de vehiculo")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        public string Description { get; set; }
    }
}
