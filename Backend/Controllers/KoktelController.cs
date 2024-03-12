using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class KoktelController
    {
        // Dependency injection
        // Definiraš privatno svojstvo
        private readonly EdunovaContext _context;

        // Dependency injection
        // U konstruktoru primir instancu i dodjeliš privatnom svojstvu
        public KoktelController(EdunovaContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_context.Kokteli.ToList());
        }

        [HttpPost]
        public IActionResult Post(Koktel koktel)
        {
            _context.Kokteli.Add(koktel);
            _context.SaveChanges();
            return new JsonResult(koktel);
        }

        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, Koktel koktel)
        {
            var smjerIzBaze = _context.Kokteli.Find(sifra);
            // za sada ručno, kasnije će doći Mapper
            smjerIzBaze.Naziv = koktel.Naziv;
            smjerIzBaze.Opis= koktel.Opis;
            smjerIzBaze.Upute_za_pripremu= koktel.Upute_za_pripremu;
            

            _context.Kokteli.Update(smjerIzBaze);
            _context.SaveChanges();

            return new JsonResult(smjerIzBaze);
        }

        [HttpDelete]
        [Route("{sifra:int}")]
        [Produces("application/json")]
        public IActionResult Delete(int sifra)
        {
            var smjerIzBaze = _context.Kokteli.Find(sifra);
            _context.Kokteli.Remove(smjerIzBaze);
            _context.SaveChanges();
            return new JsonResult(new { poruka="Obrisano"});
        }

    }
}
