using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entidades;

namespace Data.Configuraciones
{
    public class EspecialidadConfiguraciones:IEntityTypeConfiguration<Especialidad>
    {
        public void Configure(EntityTypeBuilder<Especialidad> builder) {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.NombreEspecialidad)
                .IsRequired() 
                .HasMaxLength(60);

            builder.Property(e => e.Descripcion)
                .HasMaxLength(100);

            builder.Property(e => e.Estado)
                .IsRequired();

        }
    }
}
