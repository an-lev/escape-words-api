using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExploreWords.WebApi.Controllers
{
	[ApiController]
	[Route("api/modules")]
	public class ModuleController : ControllerBase
	{
		private readonly ILogger<ModuleController> _logger;
		private readonly ExploreWordsDbContext exploreWordsDbContext;

		public ModuleController(ILogger<ModuleController> logger, ExploreWordsDbContext exploreWordsDbContext)
		{
			_logger = logger;
			this.exploreWordsDbContext = exploreWordsDbContext;
		}

		[HttpGet]
		public IEnumerable<Module> GetAllModules()
		{
			return exploreWordsDbContext.Modules.ToList();
		}

		[HttpPost]
		public async Task<IActionResult> GetAllWords([FromBody] Module module)
		{
			exploreWordsDbContext.Modules.Add(module);
			await exploreWordsDbContext.SaveChangesAsync();

			return Created($"/api/modules/{module.Id}", module);
		}
	}
}