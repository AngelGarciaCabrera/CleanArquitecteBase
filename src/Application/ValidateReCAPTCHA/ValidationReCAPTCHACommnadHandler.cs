using Application.ValidateReCAPTCHA;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ValidateReCAPTCHA 
{
    internal sealed class ValidateReCAPTCHACommandHandler : IRequestHandler<ValidateReCAPTCHACommnad, ErrorOr<Unit>>
    {
        private readonly string _googleSecretKey = "6LcjnfoqAAAAAJj6_ONJnBJToBk4PB1dK1XU3Bb5";  // Reemplaza con tu clave secreta de Google reCAPTCHA

        public async Task<ErrorOr<Unit>> Handle(ValidateReCAPTCHACommnad command, CancellationToken cancellationToken)
        {
            var recaptchaToken = command.RecaptchaToken;

            // Validamos el token de reCAPTCHA con la API de Google
            var isValid = await ValidateRecaptchaAsync(recaptchaToken);

            if (!isValid)
            {
                return Error.Validation("reCAPTCHA validation failed");
            }

            // Si la validación fue exitosa, continuamos con la lógica de la aplicación
            return Unit.Value;
        }

        private async Task<bool> ValidateRecaptchaAsync(string recaptchaToken)
        {
            if (string.IsNullOrEmpty(recaptchaToken))
            {
                return false;
            }

            var client = new HttpClient();
            var url = $"https://www.google.com/recaptcha/api/siteverify?secret={_googleSecretKey}&response={recaptchaToken}";

            var response = await client.PostAsync(url, null);
            var content = await response.Content.ReadAsStringAsync();

            var recaptchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(content);

            return recaptchaResponse?.Success ?? false;
        }
    }
}
