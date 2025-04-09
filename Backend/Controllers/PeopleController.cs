using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleService _peopleService;

        public PeopleController([FromKeyedServices("peopleService")] IPeopleService peopleService)
        { 
            _peopleService = peopleService;
        }

        [HttpGet("all")]
        public List<People> GetPeople() => Repository.People;
        [HttpGet("{id}")]
        public  ActionResult<People> Get(int id) {
            var people =   Repository.People.FirstOrDefault(p => p.Id == id);
            if(people == null)
            {
                return NotFound();
            }
            return Ok(people);
        }
        [HttpGet("search/{search}")]
        public List<People> Get(string search) =>
            Repository.People.Where(p => p.Name.ToUpper().Contains(search.ToUpper())).ToList();

        [HttpPost]
        public IActionResult Add(People people)
        {
            // if(string.IsNullOrEmpty(people.Name))
            if (!_peopleService.Validate(people))
            {
                return BadRequest();
            }
            Repository.People.Add(people);

            return NoContent();
        }
    }

    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class Repository
    {
        public static List<People> People = new List<People>{
            new People()
            {
                Id = 1,
                Name = "Luis",
                BirthDate = new DateTime(1982,12,5)
            },
            new People()
            {
                Id = 2,
                Name = "Juan",
                BirthDate = new DateTime(1982,11,9)
            },
            new People()
            {
                Id = 3,
                Name = "Pedro",
                BirthDate = new DateTime(1982,9,11)
            },
            new People()
            {
                Id = 4,
                Name = "Jose",
                BirthDate = new DateTime(1982,7,16)
            }
        };
    }
}
