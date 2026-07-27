using Microsoft.AspNetCore.Mvc;
using BloodBankApp.Models;

namespace BloodBankApp.Controllers
{
    public class ReportsController : Controller
    {
        private readonly BloodBankDBContext _context;

        public ReportsController(BloodBankDBContext context)
        {
            _context = context;
        }

        public IActionResult FilterByBloodGroup(string bloodGroup)
        {
            var donors = _context.Donors.ToList();

            if (!string.IsNullOrEmpty(bloodGroup))
            {
                donors = donors.Where(d => d.BloodGroup == bloodGroup).ToList();
            }

            return View(donors);
        }

        public IActionResult RecentDonors()
        {
            var donors = _context.Donors.OrderByDescending(d => d.LastDonationDate).ToList();
            return View(donors);
        }

        public IActionResult DonationCounts()
        {
            var result = _context.Donors.Select(d => new DonorDonationCount
            {
                FullName = d.FullName,
                TotalDonations = d.Donations.Count()
            }).ToList();

            return View(result);
        }

        public IActionResult TotalVolume()
        {
            var total = _context.Donations.Sum(d => d.VolumeMl);
            ViewBag.TotalVolume = total;
            return View();
        }
    }
}