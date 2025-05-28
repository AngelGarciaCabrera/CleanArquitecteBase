using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Application.EnviarCorreo.NewTIcketEmail
{
    public class NewTIcketEmailServices
    {
        private readonly string _smtpServer = "mail.caribetours.com.do";
        private readonly int _port = 587;
        private readonly string _user = "sugerencia@caribetours.com.do";
        private readonly string _password = "Caribe2025*";

        public async Task EnviarCorreoUser(
            string recipientEmail,
            int id,
            string nombre,
            string estado,
            string fechaDeCreacion,
            string prioridad,
            string creadoPor)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Caribe Tours", _user));
            message.To.Add(new MailboxAddress("", recipientEmail));
            message.Subject = "Nuevo Ticket recibido!";

            message.Body = new TextPart("html")
            {
                Text = $@"
                <div style='font-family: Arial, sans-serif; background-color: #f9f9f9; padding: 30px; text-align: center;'>
                    <img src='https://caribetours.com.do/wp-content/uploads/2024/04/Asset-18-768x283.png' alt='Caribe Tours' style='max-width: 180px; margin-bottom: 20px;'>
                    <h2 style='color: #004aad;'>¡Hola!</h2>
                    <p style='font-size: 16px; color: #333;'>Has recibido un nuevo ticket. A continuación te compartimos el resumen de tu ticket:</p>

                    <table style='margin: 20px auto; border-collapse: collapse; width: 80%; max-width: 500px;'>
                        <tr style='background-color: #004aad; color: #ffffff;'>
                            <th style='padding: 10px; border: 1px solid #ddd;'>Campo</th>
                            <th style='padding: 10px; border: 1px solid #ddd;'>Detalle</th>
                        </tr>
                        <tr>
                            <td style='padding: 10px; border: 1px solid #ddd;'>Número de Ticket</td>
                            <td style='padding: 10px; border: 1px solid #ddd;'>{id}</td>
                        </tr>
                        <tr style='background-color: #f2f2f2;'>
                            <td style='padding: 10px; border: 1px solid #ddd;'>Nombre</td>
                            <td style='padding: 10px; border: 1px solid #ddd;'>{nombre}</td>
                        </tr>
                        <tr>
                            <td style='padding: 10px; border: 1px solid #ddd;'>Estado</td>
                            <td style='padding: 10px; border: 1px solid #ddd;'>{estado}</td>
                        </tr>
                        <tr style='background-color: #f2f2f2;'>
                            <td style='padding: 10px; border: 1px solid #ddd;'>Fecha de Creación</td>
                            <td style='padding: 10px; border: 1px solid #ddd;'>{fechaDeCreacion}</td>
                        </tr>
                        <tr>
                            <td style='padding: 10px; border: 1px solid #ddd;'>Prioridad</td>
                            <td style='padding: 10px; border: 1px solid #ddd;'>{prioridad}</td>
                        </tr>
                        <tr style='background-color: #f2f2f2;'>
                            <td style='padding: 10px; border: 1px solid #ddd;'>Creado Por</td>
                            <td style='padding: 10px; border: 1px solid #ddd;'>{creadoPor}</td>
                        </tr>
                    </table>

                    <p style='font-size: 14px; color: #555;'>Favor trabajarlo lo antes posible.</p>
                    <p style='font-size: 14px; color: #555;'>Para cualquier consulta adicional, puedes escribir al creador para mas informacion al: <br>{creadoPor}</strong></p>

                    <hr style='margin: 30px 0; border: none; border-top: 1px solid #ccc;'>

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
