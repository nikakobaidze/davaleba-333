using davaleba_333.Data;
using davaleba_333.Models;
using davaleba_333.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;


namespace davaleba_333.Controllers
{
    public class PhoneController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private List<Phone> _phones = new List<Phone>();
        private List<int> _cartItems = new List<int>();

        //private readonly List<Phone> _phones;

        //public PhoneController(/*ApplicationDbContext context*/)
        //{
        //    //_context = context;
        //    _phones = new List<Phone>();
        //    for (int i = 0; i < 50; i++)
        //    {
        //        _phones.Add(new Phone { ID = i, Name = $"phone {i}", Price = i * 100 }); ;
        //    }
        //}

        //
        public PhoneController()
        {
            _phones = new List<Phone>();

            for (int i = 0; i < 50; i++)
            {
                _phones.Add(new Phone { ID = i, Name = $"Phone {i}", Price = i * 100 });
            }
        }
        //[HttpGet]
        //public ActionResult<IEnumerable<Phone>> GetPhones()
        //{
        //    var phones = _context.Phones.ToList();
        //    return View(); 
        //}
        //[HttpPost]
        //public ActionResult<Phone> CreatePhone(Phone phone)
        //{
        //    _context.Phones.Add(phone);
        //    _context.SaveChanges();
        //    return View();
        //}
        //[HttpPut("{id}")]
        //public ActionResult<Phone> UpdatePhone(int id, Phone phone)
        //{
        //    var existingPhone = _context.Phones.Find(id);
        //    if (existingPhone == null)
        //    {
        //        return NotFound();
        //    }
        //    existingPhone.Name = phone.Name;
        //    existingPhone.Price = phone.Price;
        //    _context.SaveChanges();
        //    return View();
        //}
        //[HttpDelete("{id}")]
        //public ActionResult DeletePhone(int id)
        //{
        //    var phone = _context.Phones.Find(id);
        //    if (phone == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Phones.Remove(phone);
        //    _context.SaveChanges();
        //    return NoContent();
        //}
        //[HttpGet]
        //public ActionResult<IEnumerable<Phone>> GetPhones(string searchTerm)
        //{
        //    var phones = _context.Phones.ToList();

        //    // Perform the search if a search term is provided
        //    if (!string.IsNullOrEmpty(searchTerm))
        //    {
        //        phones = phones.Where(p => p.Name.Contains(searchTerm)).ToList();
        //    }

        //    return View(phones);
        //}


        //public IActionResult Index(int page = 1, int pagesize = 10)
        //{
        //    var totalItems=_phones.Count;
        //    var totalPages = (int)Math.Ceiling(totalItems / (double)pagesize);
        //    var phones=_phones.Skip((page- 1) * pagesize).Take(pagesize).ToList();
        //    var viewModel = new PhoneViewModel
        //    {
        //        Phones = phones,
        //        TotalPages = totalPages,
        //        CurrentPage = page,
        //    };
        //    viewModel.Phones = phones;

        //    return View("~/Views/Home/Phone.cshtml", viewModel);

        //    /*return View(viewModel)*/

        //}
        public IActionResult Index(int page = 1, int pageSize = 10, string search = "", string sortOrder = "")
        {
            var filterPhone = _phones;
            if (!string.IsNullOrEmpty(search))
            {
                filterPhone = _phones.Where(x => x.Name.Contains(search)).ToList();
            }
            switch (sortOrder)
            {
                case "price_asc":
                    filterPhone = filterPhone.OrderBy(p => p.Price).ToList();
                    break;
                case "price_desc":
                    filterPhone = filterPhone.OrderByDescending(p => p.Price).ToList();
                    break;
                default:
                    filterPhone = filterPhone.OrderBy(p => p.ID).ToList();
                    break;
            }
            var totalItems = filterPhone.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var phones = filterPhone.Skip((page - 1) * pageSize).Take(pageSize).ToList();


            var viewModel = new PhoneViewModel
            {
                Phones = phones,
                CurrentPage = page,
                TotalPages = totalPages,
                Search = search,
                SortOrder = sortOrder,
                //Phones = phones,
                //TotalPages = totalPages,
                //CurrentPage = page,
            };

            return View(viewModel);
        }
        public IActionResult Details(int id)
        {
            var phone = _phones.FirstOrDefault(p => p.ID == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }
        public IActionResult AddToCart(int id)
        {
            _cartItems.Add(id);
            return RedirectToAction("Cart");
        }
        public IActionResult Cart()
        {
            var cartPhones = _phones.Where(p => _cartItems.Contains(p.ID)).ToList();
            return View(cartPhones);
        }
        public IActionResult Congratulations()
        {
            return View();
        }



    }
}
