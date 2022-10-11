using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExploreWords.WebApi.Controllers
{
	[ApiController]
	[Route("api/words")]
	public class WordController : ControllerBase
	{
		private readonly ILogger<WordController> _logger;
		private readonly ExploreWordsDbContext exploreWordsDbContext;

		public WordController(ILogger<WordController> logger, ExploreWordsDbContext exploreWordsDbContext)
		{
			_logger = logger;
			this.exploreWordsDbContext = exploreWordsDbContext;
		}

		[HttpGet]
		public IEnumerable<Word> GetAllWords()
		{
			return exploreWordsDbContext.Words.ToList();
		}

		[HttpPost]
		public IEnumerable<object> GetAllWords([FromBody] Word word)
		{
			return exploreWordsDbContext.Words.Select(x => new { x.Id, x.Text, x.Translate }).ToList();
		}
	}
}