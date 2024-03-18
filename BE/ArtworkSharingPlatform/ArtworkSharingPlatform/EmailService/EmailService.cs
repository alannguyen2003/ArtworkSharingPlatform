using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System.Net.Http.Headers;

namespace ArtworkSharingHost.EmailService
{
	public class EmailService : IEmailService
	{

		public async Task<string> SendAsync(string from, string to, string subject, string body)
		{
			Configuration.Default.ApiKey["api-key"] = "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";
			string message;
			var apiInstance = new TransactionalEmailsApi();
			string SenderName = "Test Auto Email";
			string SenderEmail = from;
			SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
			string ToEmail = to;
			string ToName = "Guest";
			SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, ToName);
			List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
			To.Add(smtpEmailTo);
			string HtmlContent = $"<html><body><h1>{subject}</h1><p>{body}</p></body></html>";
			string TextContent = body;
			try
			{
				var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, HtmlContent, TextContent, subject);
				CreateSmtpEmail result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
				message = "Send Email Successfully. Please confirm your email address by click on the link we have sent you";
			}
			catch (Exception e)
			{
				message = "Send Email Failed";
			}
			return message;
		}
		public async Task<bool> ValidateEmail(string email)
		{
			using HttpClient client = new();
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.Add("User-Agent", "ASP");
			var json = await client.GetStringAsync(
				"https://emailvalidation.abstractapi.com/v1/?api_key=887d421a032a4a13b47dec623ae6ccee&email=" + email);

			var result = json.Contains("UNDELIVERABLE");
			return result;
		}
	}
}
