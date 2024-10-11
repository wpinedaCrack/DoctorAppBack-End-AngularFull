using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entidades;

namespace Data.Configuraciones
{
    public class MedicoConfiguraciones : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Apellidos).IsRequired().HasMaxLength(60);

            builder.Property(e => e.Nombres).IsRequired().HasMaxLength(60);

            builder.Property(e => e.Direccion).IsRequired().HasMaxLength(100);

            builder.Property(e => e.Telefono).IsRequired(false).HasMaxLength(20);

            builder.Property(e => e.Genero).IsRequired().HasColumnType("char").HasMaxLength(1);

            builder.Property(e => e.Estado).IsRequired();

            builder.Property(e => e.EspecialidadId).IsRequired().HasMaxLength(100);

            /* Relaciones 1 a Muchos*/
            builder.HasOne(x => x.Especialidad).WithMany()
                .HasForeignKey(x => x.EspecialidadId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
