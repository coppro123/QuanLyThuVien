using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;
using System.Threading.Tasks;

namespace QuanLyThuVien.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GPTController : ControllerBase
	{
		private readonly OpenAIAPI _openAIAPI;

		public GPTController()
		{
			// Đảm bảo bảo mật API Key, không nên để trực tiếp trong mã nguồn như thế này trong môi trường thực tế
			_openAIAPI = new OpenAIAPI("sk-proj-I44XpGDfbfIBfuEzVak3T3BlbkFJqxnKLCCK49V7oKRUI2YU");
		}

		[HttpGet("UseChatGPT")]
		public async Task<IActionResult> UseChatGPT(string query)
		{
			if (string.IsNullOrEmpty(query))
			{
				return BadRequest("Query cannot be empty.");
			}

			try
			{
				var completionRequest = new CompletionRequest
				{
					Prompt = query,
					Model = "gpt-3.5-turbo",
					MaxTokens = 150
				};

				var completions = await _openAIAPI.Completions.CreateCompletionAsync(completionRequest);

				string outputResult = string.Join("\n", completions.Completions.Select(c => c.Text));

				return Ok(outputResult);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
