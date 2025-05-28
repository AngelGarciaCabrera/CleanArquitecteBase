using ErrorOr;

namespace Domain.DomainErros;

public static partial class Errors
{
    public static class Correo
    {
        public static Error CorreoWithBadFormat =>
            Error.Validation("Correo.bad", "El Correo tiene mal formato");
    }
    public static class SendUserCorreo
    {
        public static Error UserCorreoWithBadFormat =>
            Error.Validation("request.Invalido", "El correo del usario tiene mal formato");
    }
    public static class SendInternalCorreo
    {
        public static Error InternalCorreoWithBadFormat =>
            Error.Validation("request.Invalido", "El correo del Interno tiene mal formato");
    }
    public static class Phone
    {
        public static Error PhoneNumberWithBadFormat =>
            Error.Validation("Teleofno.Invalido", "El Telefono del Interno tiene mal formato");
    }
    public static class Usuario
    {
        public static Error UsuarioNotFound =>
            Error.Validation("Usuario.Invalido", "El Usuario no fue encontrado");
        public static Error InvalidPassword =>
                Error.Validation("Usuario.PasswordInvalid", "La contraseña es incorrecta");
    }
    public static class Rol
    {
        public static Error RolNoIsValid =>
            Error.Validation("El Rol Invalido", "El Rol del usuario no es valido");

    }
     public static class Tickets
    {
        public static Error TicketsByDeparmentNotFound =>
            Error.Validation("El Departamento no cuenta con tickets", "El Departamento no cuenta con tickets");

    }

}