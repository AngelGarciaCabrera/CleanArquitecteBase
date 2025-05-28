using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public partial record Correo
    {
        // Expresión regular para validar el formato del correo
        private const string patterns = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        // Propiedad que almacena el valor del correo
        public string Value { get; init; }

        // Constructor privado que inicializa el valor del correo
        private Correo(string value) => Value = value;

        // Método estático para crear un objeto Correo, validando el formato
        public static Correo Create(string value)
        {
            // Eliminar espacios antes o después del correo
            value = value?.Trim();

            // Si el correo es nulo o no coincide con el patrón, lanzar una excepción
            if (string.IsNullOrEmpty(value) || !Regex.IsMatch(value, patterns))
            {
                throw new ArgumentException($"El correo '{value}' no tiene un formato válido.", nameof(value));
            }

            return new Correo(value);
        }

        public override string ToString() => Value;

    }
}
