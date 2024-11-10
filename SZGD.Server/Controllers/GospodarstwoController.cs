using SZGD.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace SZGD.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GospodarstwoController : ControllerBase
    {
        // Lista przechowująca dane w pamięci
        private static List<Gospodarstwo> _gospodarstwa = new List<Gospodarstwo>();

        // GET: api/Gospodarstwo
        [HttpGet]
        public ActionResult<List<Gospodarstwo>> GetAll()
        {
            return Ok(_gospodarstwa);
        }

        // GET: api/Gospodarstwo/{id}
        [HttpGet("{id}")]
        public ActionResult<Gospodarstwo> GetById(int id)
        {
            var gospodarstwo = _gospodarstwa.FirstOrDefault(g => g.Id == id);
            if (gospodarstwo == null)
            {
                return NotFound();
            }
            return Ok(gospodarstwo);
        }

        // POST: api/Gospodarstwo
        [HttpPost]
        public ActionResult<Gospodarstwo> Create(Gospodarstwo gospodarstwo)
        {
            // Ustawiamy unikalne ID dla nowego gospodarstwa
            gospodarstwo.Id = _gospodarstwa.Any() ? _gospodarstwa.Max(g => g.Id) + 1 : 1;
            _gospodarstwa.Add(gospodarstwo);
            return CreatedAtAction(nameof(GetById), new { id = gospodarstwo.Id }, gospodarstwo);
        }

        // PUT: api/Gospodarstwo/{id}
        [HttpPut("{id}")]
        public ActionResult Update(int id, Gospodarstwo updatedGospodarstwo)
        {
            var gospodarstwo = _gospodarstwa.FirstOrDefault(g => g.Id == id);
            if (gospodarstwo == null)
            {
                return NotFound();
            }

            // Aktualizujemy właściwości gospodarstwa
            gospodarstwo.nazwa = updatedGospodarstwo.nazwa;

            return NoContent();
        }

        // DELETE: api/Gospodarstwo/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var gospodarstwo = _gospodarstwa.FirstOrDefault(g => g.Id == id);
            if (gospodarstwo == null)
            {
                return NotFound();
            }

            _gospodarstwa.Remove(gospodarstwo);
            return NoContent();
        }

        // POST: api/Gospodarstwo/{id}/Domownik
        [HttpPost("{id}/Domownik")]
        public ActionResult AddDomownik(int id, Domownik newDomownik)
        {
            // Znajdujemy gospodarstwo na podstawie id
            var gospodarstwo = _gospodarstwa.FirstOrDefault(g => g.Id == id);
            if (gospodarstwo == null)
            {
                return NotFound("Gospodarstwo o podanym ID nie istnieje.");
            }
            

            return CreatedAtAction(nameof(GetById), new { id = gospodarstwo.Id }, newDomownik);
        }
    }
}
