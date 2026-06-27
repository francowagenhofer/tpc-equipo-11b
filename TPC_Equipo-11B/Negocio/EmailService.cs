using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Negocio {
    public class EmailService {
        private SmtpClient clienteSmtp;
        private string emailEmisor;
        private string nombreVisible;

        public EmailService()
        {
            emailEmisor = ConfigurationManager.AppSettings["EmailEmisor"];
            string passwordEmisor = ConfigurationManager.AppSettings["EmailPassword"];
            nombreVisible = ConfigurationManager.AppSettings["EmailNombreVisible"] ?? "Clínica Médica";

            if (string.IsNullOrWhiteSpace(emailEmisor) || string.IsNullOrWhiteSpace(passwordEmisor))
            {
                throw new Exception("La configuración de correo (EmailEmisor / EmailPassword) no está definida en Web.config.");
            }

            clienteSmtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(emailEmisor, passwordEmisor),
                EnableSsl = true
            };
        }

        public void EnviarCorreo(string emailDestino, string asunto, string cuerpo)
        {
            if (string.IsNullOrWhiteSpace(emailDestino))
            {
                throw new Exception("No se puede enviar el correo: el destinatario está vacío.");
            }

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailEmisor, nombreVisible);
                mail.To.Add(emailDestino);
                mail.Subject = asunto;
                mail.Body = cuerpo;
                mail.IsBodyHtml = true;
                clienteSmtp.Send(mail);
            }
            catch (SmtpException smtpEx)
            {
                throw new Exception("Error SMTP al enviar el correo (revisar credenciales/configuración): " + smtpEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar el correo: " + ex.Message);
            }
        }
    }
}