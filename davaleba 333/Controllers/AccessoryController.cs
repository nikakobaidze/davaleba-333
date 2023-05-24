using davaleba_333.Data;
using davaleba_333.Models;
using Microsoft.AspNetCore.Mvc;

namespace davaleba_333.Controllers
{
    public class AccessoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccessoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Accessory>> GetAccessories()
        {
            var accessories = _context.Accessories.ToList();
            return Ok(accessories);
        }
        [HttpPost]
        public ActionResult<Accessory> CreateAccessory(Accessory accessory)
        {
            _context.Accessories.Add(accessory);
            _context.SaveChanges();
            return Ok(accessory);
        }
        [HttpPut("{id}")]
        public ActionResult<Accessory> UpdateAccessory(int id, Accessory accessory)
        {
            var existingAccessory = _context.Accessories.Find(id);
            if (existingAccessory == null)
            {
                return NotFound();
            }
            existingAccessory.AccessoryName = accessory.AccessoryName;
            _context.SaveChanges();
            return Ok(existingAccessory);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteAccessory(int id)
        {
            var accessory = _context.Accessories.Find(id);
            if (accessory == null)
            {
                return NotFound();
            }
            _context.Accessories.Remove(accessory);
            _context.SaveChanges();
            return NoContent();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

