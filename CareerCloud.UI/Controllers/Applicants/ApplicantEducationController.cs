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
    public class ApplicantEducationController : Controller
    {
        private readonly CareerCloudContext _context;
        private readonly ApplicantEducationLogic _logic;

        public ApplicantEducationController()
        {
             _logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>());
        }

        // GET: ApplicantEducation
        public IActionResult Index(Guid? Applicant)
        {
            var Educations = _logic.GetAll(a => a.ApplicantProfile, s => s.ApplicantProfile.SecurityLogin);

            if (Applicant != null)
                Educations = Educations.Where(a => a.Applicant == Applicant).ToList();
            else
                return NotFound();

            ViewData["Id"] = Applicant;
            return View(Educations.ToList());
        }

        // GET: ApplicantEducation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantEducationPoco = await _context.ApplicantEducations
                .Include(a => a.ApplicantProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantEducationPoco == null)
            {
                return NotFound();
            }

            return View(applicantEducationPoco);
        }

        // GET: ApplicantEducation/Create
        public IActionResult Create(Guid? Applicant)
        {
            PopulateApplicantList(Applicant);
            return View();
        }

        void PopulateApplicantList(Guid? Selected = null)
        {
            var logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
            ViewData["Applicant"] = new SelectList(
                logic.GetAll(i => i.SecurityLogin)
                .OrderByDescending(i => i.SecurityLogin.Created)
                .Select(i => new {
                    Id = i.Id,
                    Name = i.SecurityLogin.FullName
                })
                , "Id", "Name", Selected);
        }
        // POST: ApplicantEducation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Applicant,Major,CertificateDiploma,StartDate,CompletionDate,CompletionPercent")] ApplicantEducationPoco applicantEducationPoco)
        {
            if (ModelState.IsValid)
            {
                applicantEducationPoco.Id = Guid.NewGuid();
                _logic.Add(new ApplicantEducationPoco[] { applicantEducationPoco });

                return RedirectToAction(nameof(Details), "ApplicantProfile", new { Id = applicantEducationPoco.Applicant });

            }
            PopulateApplicantList(applicantEducationPoco.Applicant);
            return View(applicantEducationPoco);
        }

        // GET: ApplicantEducation/Edit/5
        public IActionResult Edit(Guid id)
        {

            var applicantEducationPoco = _logic.Get(id);

            if (applicantEducationPoco == null)
            {
                return NotFound();
            }
            PopulateApplicantList(applicantEducationPoco.Applicant);
            return View(applicantEducationPoco);
        }

        // POST: ApplicantEducation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Edit")]
        public async Task<IActionResult> EditSave(Guid id)
        {
            var poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await TryUpdateModelAsync(poco, "",
                            i=>i.CertificateDiploma,
                            i=> i.CompletionDate,
                            i=>i.CompletionPercent,
                            i=> i.Major,
                            i=>i.StartDate
                            )
                    )
                {

                    try
                    {
                        _logic.Update(new ApplicantEducationPoco[] { poco });
                        return RedirectToAction(nameof(Details), "ApplicantProfile", new { Id = poco.Applicant });
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ApplicantEducationPocoExists(poco.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                }
                
            }
            PopulateApplicantList(poco.Applicant);
            return View(poco);
        }

        // GET: ApplicantEducation/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {

            var poco = _logic.GetList(c => c.Id == id, a => a.ApplicantProfile, i=>i.ApplicantProfile.SecurityLogin).First();

            if (poco == null)
            {
                return NotFound();
            }

            return View(poco);
        }

        // POST: ApplicantEducation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var poco = _logic.Get(id);

            _logic.Delete(new ApplicantEducationPoco[] { poco });

            return RedirectToAction(nameof(Details), "ApplicantProfile", new { Id = poco.Applicant });
        }

        private bool ApplicantEducationPocoExists(Guid id)
        {
            return _logic.GetList(k => k.Id == id).Any();
        }
    }
}
