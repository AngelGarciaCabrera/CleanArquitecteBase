using Domain;
using Domain.Primitives;
using Domain.ValueObjects;
using MediatR;
using ErrorOr;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Usuario.Update;
using Domain.Usuario;
using BCrypt.Net;
using Domain.DomainErros;

namespace Application.Usuario.Update
{
    internal sealed class UpdateUsuarioCommandHandler : IRequestHandler<UpdateUsuarioCommand, ErrorOr<Unit>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateUsuarioCommand command, CancellationToken cancellationToken)
        {

            var usuario = await _usuarioRepository.GetByIdAsync(command.Id);

            if (usuario is null)
            {
                return Error.NotFound("usuario.NoEncontrado", "El usuario no existe");
            }

            if (Correo.Create(command.Correo) is not Correo correo)
            {
                return Error.Validation("Usuario.Correo", "El Correo no tiene el formato válido");
            }
            if (!Rol.TryCreate(command.Rol, out var rol))
            {
                return Errors.Rol.RolNoIsValid;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(command.Contraseña);
            // 🔹 Aquí actualizamos las propiedades del centro
            usuario.Update(
                command.Nombre,
                correo,
                command.Posicion,
                hashedPassword,
                command.Departamento_id,
                command.Preferencias_id,
                rol
            ); //usamos la funcion de la clase para actualizar


            await _usuarioRepository.UpdateAsync(usuario);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;


        }

    }
}
