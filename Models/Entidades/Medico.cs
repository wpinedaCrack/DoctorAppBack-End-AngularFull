using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entidades
{
    public class Medico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Apellido es requerido")]
        [StringLength(60, ErrorMessage = "El Apellidos debe tener minimmo 1 y máximo 60 caracteres.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido")]
        [StringLength(60, ErrorMessage = "El Nombre debe tener minimmo 1 y máximo 60 caracteres.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "La Dirección es requerido")]
        [StringLength(100, ErrorMessage = "La Direccion debe tener minimmo 1 y máximo 100 caracteres.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El Telefono es requerido")]
        [StringLength(20, ErrorMessage = "El Telefono debe tener minimmo 1 y máximo 20 caracteres.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El Genero es requerido")]
        [StringLength(60, ErrorMessage = "El Genero debe tener al menos 1 caracter")]
        public char Genero { get; set; }

        [Required(ErrorMessage = "Especialidad es requerido")]
        public int EspecialidadId { get; set; }

        [ForeignKey("EspecialidadId")]
        public Especialidad Especialidad { get; set; }

        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
