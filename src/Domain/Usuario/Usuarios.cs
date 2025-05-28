using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Usuario
{
    public sealed class Usuario : AggregateRoot
    {
        public Usuario(int Id, string Nombre, Correo Correo,string Posicion, string Contraseña, int Departamento_id, int Preferencias_id, Rol Rol)
        {
            this.Id = Id;
            this.Nombre = Nombre;
            this.Correo = Correo ?? throw new ArgumentException("El correo proporcionado no es válido o esta vacio.");
            this.Posicion = Posicion;
            this.Contraseña = Contraseña;
            this.Departamento_id = Departamento_id;
            this.Preferencias_id = Preferencias_id;
            this.Rol = Rol ?? throw new ArgumentException("El Rol no es valido");
        }
        private Usuario() { }
        public int Id { get; private set; }

        public string Nombre { get; private set; } = string.Empty;
        public Correo? Correo { get; private set; }
        public string Posicion { get; private set; } = string.Empty;
        public string Contraseña { get; private set; } = string.Empty;
        public int Departamento_id { get; private set; }
        public int Preferencias_id { get; private set; } = 0;
        public Rol? Rol {get; private set;} 
      
        public void Update(string Nombre, Correo Correo, string Posicion, string Contraseña, int Departamento_id, int Preferencias_id, Rol Rol)
        {
            this.Nombre = Nombre;
            this.Correo = Correo ?? throw new ArgumentException("El correo proporcionado no es válido o esta vacio.");
            this.Posicion = Posicion;
            this.Contraseña = Contraseña;
            this.Departamento_id = Departamento_id;
            this.Preferencias_id = Preferencias_id;
            this.Rol = Rol ?? throw new ArgumentException("El Rol proporcionado no es válido o esta vacio.");

        }

    }
}