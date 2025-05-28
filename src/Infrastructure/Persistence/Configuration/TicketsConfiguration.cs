using Domain.Tickets;

using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configuration
{
    public class TicketsConfiguration : IEntityTypeConfiguration<Tickets>
    {
        public void Configure(EntityTypeBuilder<Tickets> builder)
        {
            builder.ToTable("Tickets");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Descripcion)
                .IsRequired()
                .HasMaxLength(200);

            // Convertir Correo a string
            builder.Property(x => x.Estado)
                .IsRequired()
               .HasMaxLength(200);

            builder.Property(x => x.Departamento_Id).IsRequired()
                  .IsRequired();

            builder.Property(x => x.User_Id)
                .IsRequired();

            // Configurar PhoneNumber correctamente
            builder.Property(x => x.FechaDeCreacion)
                .IsRequired();

            builder.Property(x => x.Prioridad)
                .IsRequired()
                .HasMaxLength(50);
            builder.OwnsOne(x => x.CreadoPor, correo =>
            {
                correo.Property(c => c.Value)
                    .HasColumnName("CreadoPor") // así se guarda como string en la columna
                    .IsRequired(false);         // o true, si no permitís nulls
            });


        }
    }
}
