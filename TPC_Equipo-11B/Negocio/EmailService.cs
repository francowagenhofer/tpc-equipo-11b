using System;
using System.Net;
using System.Net.Mail;

namespace Negocio {
    public class EmailService {
        private SmtpClient clienteSmtp;
        // Reemplaza con el correo de tu clínica y contraseña de aplicación (ej. de Gmail)
        private string emailEmisor = "tu-correo@gmail.com";
        private string passwordEmisor = "tu-contraseña-de-aplicacion";

        public EmailService()
        {
            // Configuración genérica para Gmail SMTP
            clienteSmtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(emailEmisor, passwordEmisor),
                EnableSsl = true
            };
        }

        public void EnviarCorreo(string emailDestino, string asunto, string cuerpo)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailEmisor, "Clínica Médica");
                mail.To.Add(emailDestino);
                mail.Subject = asunto;
                mail.Body = cuerpo;
                mail.IsBodyHtml = true;

                clienteSmtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar el correo: " + ex.Message);
            }
        }
    }
}
