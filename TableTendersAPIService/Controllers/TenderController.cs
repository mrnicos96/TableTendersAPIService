using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TableTendersAPIService.Models;
using System.Threading.Tasks;


namespace TableTendersAPIService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenderController : ControllerBase
    {
        TenderContext db;
        public TenderController(TenderContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tender>>> Get()
        {
            return await db.Tenders.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Tender>> Get(int id)
        {
            Tender tender = await db.Tenders.FirstOrDefaultAsync(x => x.Id == id);
            if (tender == null)
                return NotFound();
            return new ObjectResult(tender);
        }

        
        [HttpPost]
        public async Task<ActionResult<Tender>> Post(Tender tender)
        {
            if (tender == null)
            {
                return BadRequest();
            }

            db.Tenders.Add(tender);
            await db.SaveChangesAsync();
            return Ok(tender);
        }

        
        [HttpPut]
        public async Task<ActionResult<Tender>> Put(Tender tender)
        {
            if (tender == null)
            {
                return BadRequest();
            }
            if (!db.Tenders.Any(x => x.Id == tender.Id))
            {
                return NotFound();
            }

            db.Update(tender);
            await db.SaveChangesAsync();
            return Ok(tender);
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tender>> Delete(int id)
        {
            Tender tender = db.Tenders.FirstOrDefault(x => x.Id == id);
            if (tender == null)
            {
                return NotFound();
            }
            db.Tenders.Remove(tender);
            await db.SaveChangesAsync();
            return Ok(tender);
        }
    }
}
