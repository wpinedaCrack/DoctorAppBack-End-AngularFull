using System.ComponentModel.DataAnnotations;

namespace Models.Entidades
{
    public class Especialidad
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "El nombre de la especialidad no puede tener más de 60 caracteres.")]
        public string NombreEspecialidad { get; set; }

        [StringLength(100, ErrorMessage = "La descripción no puede tener más de 100 caracteres.")]
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
