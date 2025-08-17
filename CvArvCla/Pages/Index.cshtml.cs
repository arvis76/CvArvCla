using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Mail;

namespace CvArvCla.Pages;

public class IndexModel : PageModel
{
	private readonly string LikeFilePath = "App_Data/likes.txt";

	[BindProperty]
	public bool MessageSent { get; set; } = false;

	public int LikeCount { get; set; }

	public void OnGet()
	{
		LikeCount = GetCurrentLikes();
	}

	public IActionResult OnPost()
	{
		// 1. Update counter
		LikeCount = GetCurrentLikes() + 1;
		SaveLikes(LikeCount);

		// 2. Send email
		try
		{
			var smtpClient = new SmtpClient("smtp.gmail.com")
			{
				Port = 587,
				Credentials = new NetworkCredential("arvisclas@gmail.com", "qdow iofo frvs gflr"),
				EnableSsl = true,
			};

			var mailMessage = new MailMessage
			{
				From = new MailAddress("arvisclas@gmail.com"),
				Subject = "New Like!",
				Body = $"Someone liked your site. Total likes: {LikeCount}.",
				IsBodyHtml = false,
			};
			mailMessage.To.Add("arvisclas@gmail.com");

			smtpClient.Send(mailMessage);

			MessageSent = true;
		}
		catch (Exception ex)
		{
			Console.WriteLine("Email error: " + ex.Message);
		}

		return Page();
	}

	private int GetCurrentLikes()
	{
		try
		{
			if (!Directory.Exists("App_Data"))
				Directory.CreateDirectory("App_Data");

			if (!System.IO.File.Exists(LikeFilePath))
				System.IO.File.WriteAllText(LikeFilePath, "0");

			string text = System.IO.File.ReadAllText(LikeFilePath);
			return int.TryParse(text, out int result) ? result : 0;
		}
		catch
		{
			return 0;
		}
	}

	private void SaveLikes(int count)
	{
		try
		{
			System.IO.File.WriteAllText(LikeFilePath, count.ToString());
		}
		catch
		{
			// Ignore errors silently or log
		}
	}
}
