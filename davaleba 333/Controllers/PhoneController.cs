using davaleba_333.Data;
using davaleba_333.Models;
using Microsoft.AspNetCore.Mvc;
using System;


namespace davaleba_333.Controllers
{
    public class PhoneController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PhoneController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Phone>> GetPhones()
        {
            var phones = _context.Phones.ToList();
            return Ok(phones);
        }
        [HttpPost]
        public ActionResult<Phone> CreatePhone(Phone phone)
        {
            _context.Phones.Add(phone);
            _context.SaveChanges();
            return Ok(phone);
        }
        [HttpPut("{id}")]
        public ActionResult<Phone> UpdatePhone(int id, Phone phone)
        {
            var existingPhone = _context.Phones.Find(id);
            if (existingPhone == null)
            {
                return NotFound();
            }
            existingPhone.Name = phone.Name;
            existingPhone.Price = phone.Price;
            _context.SaveChanges();
            return Ok(existingPhone);
        }
        [HttpDelete("{id}")]
        public ActionResult DeletePhone(int id)
        {
            var phone = _context.Phones.Find(id);
            if (phone == null)
            {
                return NotFound();
            }

            _context.Phones.Remove(phone);
            _context.SaveChanges();
            return NoContent();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
