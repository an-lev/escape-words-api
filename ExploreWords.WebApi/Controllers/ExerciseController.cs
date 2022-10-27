using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExploreWords.WebApi.Controllers
{
    [ApiController]
    [Route("api/exercises")]
    public class ExerciseController : ControllerBase
    {
        private IEnumerable<Exercise> exercises;
        private ExploreWordsDbContext context;

        public ExerciseController(ExploreWordsDbContext context)
        {
            exercises = new List<Exercise> {
                new Exercise(ExerciseType.ChooseCorrectTranslate, "Choose the correct word", "The exersice displays word and 4 translations. Your tasks is choose correct one.")

            };
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllExercieses()
        {
            return Ok(exercises);
        }

        [HttpGet("{id}")]
        public IActionResult GetExerciseData([FromRoute] ExerciseType id)
        {
            var words = context.Words.Take(50).OrderBy(x => Guid.NewGuid()).ToList();

            var wordsToChoose = words.Take(10).ToList();
            var wordsBox = words.Skip(10).ToList();

            var res = wordsToChoose.Select(x => new
            {
                Word = x,
                Choices = wordsBox.OrderBy(x=>Guid.NewGuid()).Take(3).Select(x=>x.Translate)
            });

            return Ok(res);
        }
    }

    public enum ExerciseType
    {
        ChooseCorrectTranslate = 1
    }

    public class Exercise
    {
        public ExerciseType Id { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public Exercise(ExerciseType id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
    }
}

