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
        public async Task<IActionResult> CreateModule([FromBody] CreateModuleVm module)
        {
            var moduleEntity = new Module
            {
                Name = module.Name
            };
            exploreWordsDbContext.Modules.Add(moduleEntity);
            await exploreWordsDbContext.SaveChangesAsync();

            return Ok(moduleEntity);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModuleById([FromRoute] Guid id)
        {
            var moduleEntity = await exploreWordsDbContext.Modules.Include(x => x.Words)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    Words = x.Words.Select(y => new
                    {
                        y.Id,
                        y.ModuleId,
                        y.Text,
                        y.Translate
                    })
                }).FirstOrDefaultAsync(x => x.Id == id);
            return Ok(moduleEntity);
        }
    }

    public class CreateModuleVm
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}