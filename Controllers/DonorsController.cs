using Microsoft.AspNetCore.Mvc;
using BloodBankApp.Models;

namespace BloodBankApp.Controllers
{
    public class DonorsController : Controller
    {
        private readonly BloodBankDBContext _context;

        public DonorsController(BloodBankDBContext context)
        {
            _context = context;
        }

        // GET: Donors
        public IActionResult Index()
        {
            var donors = _context.Donors.ToList();
            return View(donors);
        }

        // GET: Donors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Donors/Create
        [HttpPost]
        public IActionResult Create(Donor donor)
        {
            if (string.IsNullOrWhiteSpace(donor.FullName))
            {
                ModelState.AddModelError("FullName", "Full name is required.");
            }
            if (string.IsNullOrWhiteSpace(donor.BloodGroup))
            {
                ModelState.AddModelError("BloodGroup", "Blood group is required.");
            }
            if (string.IsNullOrWhiteSpace(donor.ContactNo))
            {
                ModelState.AddModelError("ContactNo", "Contact number is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(donor);
            }

            _context.Donors.Add(donor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Donors/Edit/5
        public IActionResult Edit(int id)
        {
            var donor = _context.Donors.Find(id);
            if (donor == null)
            {
                return NotFound();
            }
            return View(donor);
        }

        // POST: Donors/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Donor donor)
        {
            if (id != donor.DonorId)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(donor.FullName))
            {
                ModelState.AddModelError("FullName", "Full name is required.");
            }
            if (string.IsNullOrWhiteSpace(donor.BloodGroup))
            {
                ModelState.AddModelError("BloodGroup", "Blood group is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(donor);
            }

            _context.Donors.Update(donor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Donors/Delete/5
        public IActionResult Delete(int id)
        {
            var donor = _context.Donors.Find(id);
            if (donor == null)
            {
                return NotFound();
            }
            return View(donor);
        }

        // POST: Donors/Delete/5
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var donor = _context.Donors.Find(id);
            if (donor != null)
            {
                _context.Donors.Remove(donor);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}