using FiyiStack.Areas.System.ParameterBack.Repositories;

namespace FiyiStack.Mailing
{
    public class Email : IEmail
    {
        public ParameterRepository _parameterRepository;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public Email(ParameterRepository parameterRepository, IWebHostEnvironment hostingEnvironment)
        {
            _parameterRepository = parameterRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        #region FiyiStack
        //public void MailFromContactForm(Contacto contact)
        //{
        //    try
        //    {
        //        //Crear un mensaje de correo electronico y un cliente de correo
        //        System.Net.Mail.MailMessage Mail = new();
        //        System.Net.Mail.SmtpClient Client = new();

        //        //Direcciones de correo electronico a enviarles el correo
        //        Mail.To.Add(contact.Email);
        //        Mail.To.Add("hello@fiyistack.com");

        //        string Asunto = "Tu mensaje fue recibido correctamente";
        //        Mail.Subject = Asunto;
        //        Mail.SubjectEncoding = Encoding.UTF8;

        //        #region Cuerpo
        //        string WebRootPath = _hostingEnvironment.WebRootPath;
        //        string TemplateContactPath = $@"{WebRootPath}/mailing-templates/new-contact-form-created.html";
        //        string HTMLContent = File.ReadAllText(TemplateContactPath);

        //        HTMLContent = HTMLContent.Replace("{*1*}", "Tu mensaje fue recibido correctamente");
        //        HTMLContent = HTMLContent.Replace("{*2*}", $@"{contact.FullName} - {contact.Email}");
        //        HTMLContent = HTMLContent.Replace("{*3*}", contact.DateTimeCreation.ToString("HH:mm dd/MM/yyyy"));
        //        HTMLContent = HTMLContent.Replace("{*4*}", $@"Mensaje: {contact.Requirement}");
        //        #endregion

        //        Mail.Body = HTMLContent;
        //        Mail.BodyEncoding = Encoding.UTF8;
        //        Mail.IsBodyHtml = true;

        //        string CorreoDelServidor = _parameterRepository.GetByName("CorreoDelServidor");
        //        string PasswordDelCorreo = _parameterRepository.GetByName("PasswordDelCorreo");
        //        string ServidorDelCorreo = _parameterRepository.GetByName("ServidorDeCorreos");

        //        //Correo de quien sale el correo y nombre de la aplicacion
        //        Mail.From = new System.Net.Mail.MailAddress(CorreoDelServidor, "FiyiStack");
        //        Client.Credentials = new NetworkCredential(CorreoDelServidor, PasswordDelCorreo);
        //        //Servidor del que se envia el correo.
        //        Client.Host = ServidorDelCorreo;
        //        //Enviar correo
        //        Client.Send(Mail);
        //    }
        //    catch (Exception) { throw; }
        //}
        #endregion
    }
}
