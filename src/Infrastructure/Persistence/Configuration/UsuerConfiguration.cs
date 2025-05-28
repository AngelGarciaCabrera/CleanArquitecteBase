using Domain.Usuario;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configuration
{
    public class UsuerConfiguration : IEntityTypeConfiguration<Usuario>
    {
        //clase para manejar la conversion
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            // Configurar la clave primaria
            builder.HasKey(x => x.Id);

            
            // Configuración de las otras propiedades
            builder.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Correo)
                .IsRequired()
                .HasMaxLength(200)
                .HasConversion(
                    correo => correo.Value,  // Convertir a string para almacenar en DB
                    correo => Correo.Create(correo) // Convertir de string a Correo al leerlo
                );
            builder.Property(x => x.Posicion)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.Contraseña)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.Departamento_id)
                .IsRequired();
            builder.Property(x => x.Preferencias_id)
                .IsRequired();
             builder.Property(x => x.Rol)
                .IsRequired()
                .HasMaxLength(50)
                .HasConversion(
                    rol => rol.Value,  // Convertir a string para almacenar en DB
                    rol => Rol.Create(rol) // Convertir de string a Rol al leerlo
                );
                
        }
    }
}
