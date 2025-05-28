using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Application.EnviarCorreo.UserCorreo
{
    public class Email_User_Service
    {
        private readonly string _smtpServer = "mail.caribetours.com.do";
        private readonly int _port = 587;
        private readonly string _user = "sugerencia@caribetours.com.do";
        private readonly string _password = "Caribe2025*";

        public async Task EnviarCorreoUser(
        string recipientEmail,
        string userName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Caribe Tours", _user));
            message.To.Add(new MailboxAddress("", recipientEmail));
            message.Subject = "Nueva Queja o Sugerencia Recibida";

            message.Body = new TextPart("html")
            {
                Text = $@"
                <div style='font-family: Arial, sans-serif; text-align: center; padding: 20px; background-color: #f4f4f4;'>
                    <img src='https://caribetours.com.do/wp-content/uploads/2024/04/Asset-18-768x283.png' alt='Caribe Tours' style='max-width: 155px; margin-bottom: 20px;'>
                    <h2 style='color: #004aad;'>¡Hola, {userName}!</h2>
                    <p style='font-size: 16px; color: #333;'>Gracias por tomarte el tiempo de enviarnos tu sugerencia.</p>
                    <p style='font-size: 16px; color: #333;'>Valoramos mucho tu opinión y la tendremos en cuenta para mejorar nuestro servicio.</p>
                    <p style='font-size: 16px; color: #333;'>De parte de la familia Caribe Tours, ¡Gracias!.</p>
                    <p style='font-size: 14px; color: #777;'>Si necesitas más ayuda, no dudes en contactarnos.</p>
                    <p style='font-size: 14px; color: #777;'>sugerencia@caribetours.com.do</p>
                    <hr style='border: 1px solid #ddd;'>
                    <p style='font-size: 12px; color: #999;'>© 2025 Caribe Tours. Todos los derechos reservados.</p>
                </div>"
            };


            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_smtpServer, _port, false);
                await client.AuthenticateAsync(_user, _password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo al usuario: {ex.Message}");
            }
        }
    }
}
