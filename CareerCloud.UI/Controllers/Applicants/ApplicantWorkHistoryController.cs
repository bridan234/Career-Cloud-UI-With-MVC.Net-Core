using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;

namespace CareerCloud.UI.Controllers.Applicants
{
    public class ApplicantWorkHistoryController : Controller
    {
        private readonly CareerCloudContext _context;
        private readonly ApplicantWorkHistoryLogic _logic;

        public ApplicantWorkHistoryController()
        {
            _logic = new ApplicantWorkHistoryLogic(new EFGenericRepository<ApplicantWorkHistoryPoco>());
        }

        // GET: ApplicantWorkHistory
        public async Task<IActionResult> Index(Guid? Applicant)
        {
            var WorkHistory = _logic.GetAll(a => a.ApplicantProfile, s => s.ApplicantProfile.SecurityLogin);

            if (Applicant != null)
                WorkHistory = WorkHistory.Where(a => a.Applicant == Applicant).ToList();
            else
                return NotFound();

            ViewData["Id"] = Applicant;
            return View(WorkHistory.ToList());
        }

        // GET: ApplicantWorkHistory/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantWorkHistoryPoco = await _context.ApplicantWorkHistory
                .Include(a => a.ApplicantProfile)
                .Include(a => a.SystemCountryCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantWorkHistoryPoco == null)
            {
                return NotFound();
            }

            return View(applicantWorkHistoryPoco);
        }

        // GET: ApplicantWorkHistory/Create
        public IActionResult Create(Guid? Applicant = null)
        {
            PopulateApplicantList(Applicant);
            PopulateCountryCodeList();
            return View();
        }

        // POST: ApplicantWorkHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Applicant,CompanyName,CountryCode,Location,JobTitle,JobDescription,StartMonth,StartYear,EndMonth,EndYear")] ApplicantWorkHistoryPoco applicantWorkHistoryPoco)
        {
            if (ModelState.IsValid)
            {
                applicantWorkHistoryPoco.Id = Guid.NewGuid();
                _logic.Add(new ApplicantWorkHistoryPoco[] { applicantWorkHistoryPoco });

                return RedirectToAction(nameof(Details), "ApplicantProfile", new { Id = applicantWorkHistoryPoco.Applicant });
            }
            PopulateApplicantList(applicantWorkHistoryPoco.Applicant);
            PopulateCountryCodeList(applicantWorkHistoryPoco.CountryCode);

            return View(applicantWorkHistoryPoco);
        }

        // GET: ApplicantWorkHistory/Edit/5
        public IActionResult Edit(Guid id)
        {

            var poco = _logic.Get(id);

            if (poco == null)
            {
                return NotFound();
            }
            PopulateApplicantList(id);
            PopulateCountryCodeList(poco.CountryCode);
           return View(poco);
        }

        // POST: ApplicantWorkHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Edit")]
        public async Task<IActionResult> EditSave(Guid id)
        {
            var poco = _logic.Get(id);

            if (ModelState.IsValid)
            {
                if (await TryUpdateModelAsync(poco, ""
                        , i => i.CompanyName
                        , i => i.CountryCode
                        , i => i.EndMonth
                        , i => i.EndYear
                        , i => i.JobDescription
                        , i => i.JobTitle
                        , i => i.Location
                        , i => i.StartMonth
                        , i => i.StartYear))
                {
                    try
                    {
                        _logic.Update(new ApplicantWorkHistoryPoco[] { poco });
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ApplicantWorkHistoryPocoExists(poco.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return RedirectToAction(nameof(Details), "ApplicantProfile", new { Id = poco.Applicant });
            }
            PopulateApplicantList(id);
            PopulateCountryCodeList(poco.CountryCode);
            return View(poco);
        }

        // GET: ApplicantWorkHistory/Delete/5
        public IActionResult Delete(Guid id)
        {

            var poco = _logic.GetList(c => c.Id == id, i => i.ApplicantProfile, i => i.SystemCountryCode, i=>i.ApplicantProfile.SecurityLogin).Single();
            if (poco == null)
            {
                return NotFound();
            }

            return View(poco);
        }

        // POST: ApplicantWorkHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var poco = _logic.Get(id);
            _logic.Delete(new ApplicantWorkHistoryPoco[] { _logic.Get(id) });
            return RedirectToAction(nameof(Details), "ApplicantProfile", new { Id = poco.Applicant });

        }
        void PopulateApplicantList(Guid? Selected = null)
        {
            var logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
            var Applicants = logic.GetAll(i => i.SecurityLogin).OrderByDescending(u => u.SecurityLogin.Created).Select(k => new { Id = k.Id, Name = k.SecurityLogin.FullName });
            ViewData["Applicant"] = new SelectList(Applicants, "Id", "Name", Selected);
        }

        void PopulateCountryCodeList(string Selected = null)
        {
            var logic = new SystemCountryCodeLogic(new EFGenericRepository<SystemCountryCodePoco>());
            var Countries = logic.GetAll().Where(i=>i.Code.Trim().Length<4).Select(k => new { Code = k.Code, Name = k.Name });
            ViewData["CountryCode"] = new SelectList(Countries, "Code", "Name", Selected);
        }
        private bool ApplicantWorkHistoryPocoExists(Guid id)
        {
            return _context.ApplicantWorkHistory.Any(e => e.Id == id);
        }
    }
}
