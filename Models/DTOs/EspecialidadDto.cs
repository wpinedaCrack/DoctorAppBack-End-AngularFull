using System.ComponentModel.DataAnnotations;

namespace Models.DTOs
{
    public class EspecialidadDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La Especialidad es Requerida")]
        [StringLength(60, ErrorMessage = "El nombre de la especialidad no puede tener más de 60 caracteres.")]
        public string NombreEspecialidad { get; set; }

        [Required(ErrorMessage = "La Descripción es Requerida")]
        [StringLength(100, ErrorMessage = "La descripción no puede tener más de 100 caracteres.")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El Estado es Requerida")]
        public int Estado { get; set; }
    }
}
