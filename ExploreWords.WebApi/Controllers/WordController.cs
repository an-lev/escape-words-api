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
        public async Task<Word> GetAllWords([FromBody] CreateWordVm word)
        {
            var wordEntity = new Word
            {
                ModuleId = word.ModuleId,
                Text = word.Text,
                Translate = word.Translate
            };

            exploreWordsDbContext.Words.Add(wordEntity);
            await exploreWordsDbContext.SaveChangesAsync();

            return wordEntity;
        }

        [HttpDelete("{wordId}")]
        public async Task<IActionResult> DeleteWord([FromRoute] Guid wordId)
        {
            Word wordToDelete = exploreWordsDbContext.Words.Find(wordId);
            if (wordToDelete == null)
                return NoContent();

            exploreWordsDbContext.Words.Remove(wordToDelete);
            exploreWordsDbContext.SaveChanges();

            return NoContent();
        }

    }

    public class CreateWordVm
    {
        public string Text { get; set; }
        public string Translate { get; set; }
        public Guid ModuleId { get; set; }
    }
}