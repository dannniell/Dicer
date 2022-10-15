namespace Dicer.Interfaces
{
    public interface IEmailService
    {
        public void SendEmail(string to, string subject, string html);
    }
}
