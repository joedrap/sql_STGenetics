using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STGeneticsA.Models;
using STGeneticsA.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STGeneticsA.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AnimalController : ControllerBase
    {
        private readonly AnimalDbContext _context;

        public object GetAnimalById { get; private set; }

        public AnimalController(AnimalDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Animal>> CreateAnimal([FromBody] Animal animal)
        {
            try
            {
                _context.Animals.Add(animal);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAnimalById), new { id = animal.AnimalId }, animal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnimal(int id, [FromBody] Animal animal)
        {
            try
            {
                if (id != animal.AnimalId)
                {
                    return BadRequest("Animal ID mismatch");
                }

                _context.Entry(animal).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();
                var clientValues = (Animal)exceptionEntry.Entity;
                var databaseEntry = exceptionEntry.GetDatabaseValues();

                if (databaseEntry == null)
                {
                    return NotFound("Animal not found");
                }

                var databaseValues = (Animal)databaseEntry.ToObject();

                if (databaseValues.Name != clientValues.Name)
                    ModelState.AddModelError("Name", $"Current value: {databaseValues.Name}");

                if (databaseValues.Breed != clientValues.Breed)
                    ModelState.AddModelError("Breed", $"Current value: {databaseValues.Breed}");

                if (databaseValues.BirthDate != clientValues.BirthDate)
                    ModelState.AddModelError("BirthDate", $"Current value: {databaseValues.BirthDate}");

                if (databaseValues.Sex != clientValues.Sex)
                    ModelState.AddModelError("Sex", $"Current value: {databaseValues.Sex}");

                if (databaseValues.Price != clientValues.Price)
                    ModelState.AddModelError("Price", $"Current value: {databaseValues.Price}");

                if (databaseValues.Status != clientValues.Status)
                    ModelState.AddModelError("Status", $"Current value: {databaseValues.Status}");

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            try
            {
                var animal = await _context.Animals.FindAsync(id);
                if (animal == null)
                {
                    return NotFound("Animal not found");
                }

                _context.Animals.Remove(animal);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("filter")]
        public async Task<IActionResult> GetAnimals(int? id, string name, string sex, string status)
        {
            var query = _context.Animals.AsQueryable();

            if (id.HasValue)
            {
                query = query.Where(a => a.AnimalId == id.Value);
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(sex))
            {
                query = query.Where(a => a.Sex == sex);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(a => a.Status == status);
            }

            var animals = await query.ToListAsync();

            return Ok(animals);
        }
    }

}