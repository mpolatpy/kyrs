using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetAdoption.ApplicationUtils;
using PetAdoption.Models;

namespace PetAdoption.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetAdoptionController : ControllerBase
    {
        private readonly ILogger<PetAdoptionController> _logger;
        private readonly DataContext _context;

        public PetAdoptionController(ILogger<PetAdoptionController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pet>>> ListPets([FromQuery] PetSearchParams searchParams)
        {
            var query = _context.Pets.AsQueryable();

            if(searchParams.Type != null)
            {
                query = query.Where(p => p.Type.ToLower() == searchParams.Type.ToLower());
            }

            if(searchParams.Gender != null)
            {
                query = query.Where(p => p.Gender.ToLower() == searchParams.Gender.ToLower());
            }

            if(searchParams.ZipCode != null)
            {
                query = query.Where(p => p.ZipCode == searchParams.ZipCode);
            }

            var pets = await query.ToListAsync();

            return this.Ok(pets);
        }
    }
}