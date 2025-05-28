
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;
namespace Application.EnviarCorreo;
public class Email_Interal_Service
{
    private readonly string _smtpServer = "mail.caribetours.com.do";
    private readonly int _port = 587;
    private readonly string _user = "sugerencia@caribetours.com.do";
    private readonly string _password = "Caribe2025*";

    public async Task EnviarCorreoInternal(string correo, string Usuario_Nombre,int CentroId,string Descripcion,
    string Fecha, string Seccion,
    string Telefono, string CentroNombre)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Caribe Tours", correo));
        message.To.Add(new MailboxAddress("", _user));
        message.Subject = "Nueva Queja o Sugerencia Recibida";

        message.Body = new TextPart("html")
        {
            Text = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; background-color: #f4f4f4; text-align: center;'>
                    <img src='https://caribetours.com.do/wp-content/uploads/2024/04/Asset-18-768x283.png' 
                         alt='Caribe Tours' style='max-width: 250px; margin-bottom: 20px;'>
                    <h3 style='color: #004aad; font-size: 24px; margin-bottom: 20px;'>Nueva Sugerencia Recibida</h3>
                    
                    <div style='text-align: left; display: inline-block; background-color: #fff; padding: 20px; border-radius: 8px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);'>
                        <h3 style='color: #004aad; font-size: 24px; margin-bottom: 20px;'>Información del Usuario:</h3>
                        <p style='font-size: 16px; color: #333; margin: 8px 0;'><strong>Nombre del Usuario:</strong> {Usuario_Nombre}</p>
                        <p style='font-size: 16px; color: #333; margin: 8px 0;'><strong>Teléfono:</strong> {Telefono}</p>
                        <p style='font-size: 16px; color: #333; margin: 8px 0;'><strong>Correo del Usuario:</strong> {correo}</p>

                        <h3 style='color: #004aad; font-size: 24px; margin-bottom: 20px;'>Información del Centro:</h3>
                        <p style='font-size: 16px; color: #333; margin: 8px 0;'><strong>ID del Centro:</strong> {CentroId}</p>
                        <p style='font-size: 16px; color: #333; margin: 8px 0;'><strong>Nombre del Centro:</strong> {CentroNombre}</p>
                        <p style='font-size: 16px; color: #333; margin: 8px 0;'><strong>Sección donde ocurrió:</strong> {Seccion}</p>

                        <h3 style='color: #004aad; font-size: 24px; margin-bottom: 20px;'>Razón u Opinión:</h3>
                        <p style='font-size: 16px; color: #333; margin: 8px 0;'><strong>Descripción:</strong> {Descripcion}</p>
                        <p style='font-size: 16px; color: #333; margin: 8px 0;'><strong>Fecha:</strong> {Fecha}</p>
                    </div>

                    <hr style='border: 1px solid #ddd; width: 80%; margin-top: 20px;'>

                    <p style='font-size: 14px; color: #777; margin-top: 20px;'>© 2025 Caribe Tours. Todos los derechos reservados.</p>
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
            Console.WriteLine($"Error al enviar el correo Interno: {ex.Message}");
        }
    }
}
