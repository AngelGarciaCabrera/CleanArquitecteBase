using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public partial record Rol
    {
        // Expresión regular para validar el formato del correo
        private static readonly string[] ValoresPermitidos = new[] { "admin", "user" };

        public string Value { get; init; }


        public Rol(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El rol no puede estar vacío.");

            if (!ValoresPermitidos.Contains(value.ToLower()))
                throw new ArgumentException($"El rol '{value}' no es válido. Debe ser 'Admin' o 'Usuario'.");

            Value = value;
        }

        public static bool TryCreate(string value, out Rol rol)
        {
            try
            {
                rol = new Rol(value);
                return true;
            }
            catch
            {
                rol = null!;
                return false;
            }
        }

        public static Rol Create(string value)

        {


            return new Rol(value);

        }

        public override string ToString() => Value;

    }
}
