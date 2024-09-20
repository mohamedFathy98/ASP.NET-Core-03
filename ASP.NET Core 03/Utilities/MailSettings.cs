using System.Net.Mail;
using System.Net;

namespace ASP.NET_Core_03.Utilities
{
	public class Email
	{
		public string Subject { get; set; }
		public string Body { get; set; }
		public string Recipient { get; set; }
	}
	public static class MailSettings
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("mdx7700@gmail.com", "mderpufjqyupoija");
			client.Send("mdx7700@gmail.com", email.Recipient, email.Subject, email.Body);
		}
	}
}