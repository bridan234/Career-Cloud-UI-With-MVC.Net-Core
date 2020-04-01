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
    public class ApplicantSkillController : Controller
    {
        private readonly CareerCloudContext _context;
        private readonly ApplicantSkillLogic _logic;

        public ApplicantSkillController()
        {
            _logic = new ApplicantSkillLogic(new EFGenericRepository<ApplicantSkillPoco>());

        }

        // GET: ApplicantSkill
        public IActionResult Index(Guid? Applicant)
        {
            var Skills = _logic.GetAll(a => a.ApplicantProfile, s=>s.ApplicantProfile.SecurityLogin);

            if (Applicant != null)
                Skills = Skills.Where(a => a.Applicant == Applicant).ToList();
            else
                return NotFound();

            ViewData["Id"] = Applicant;
            return View(Skills.ToList());
        }

        // GET: ApplicantSkill/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantSkillPoco = await _context.ApplicantSkills
                .Include(a => a.ApplicantProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantSkillPoco == null)
            {
                return NotFound();
            }

            return View(applicantSkillPoco);
        }

        // GET: ApplicantSkill/Create
        public IActionResult Create(Guid? Applicant)
        {
            PopulateApplicantList(Applicant);
            return View();
        }

        // POST: ApplicantSkill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Applicant,Skill,SkillLevel,StartMonth,StartYear,EndMonth,EndYear")] ApplicantSkillPoco applicantSkillPoco)
        {
            if (ModelState.IsValid)
            {
                applicantSkillPoco.Id = Guid.NewGuid();
                _logic.Add(new ApplicantSkillPoco[] { applicantSkillPoco });

                return RedirectToAction(nameof(Details), "ApplicantProfile", new { Id = applicantSkillPoco.Applicant });
            }
            PopulateApplicantList(applicantSkillPoco.Applicant);
            return View(applicantSkillPoco);
        }

        // GET: ApplicantSkill/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            
            var poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound();
            }

            PopulateApplicantList(poco.Applicant);
            return View(poco);
        }

        // POST: ApplicantSkill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Edit")]
        public async Task<IActionResult> EditSave(Guid id)
        {
            var poco = _logic.Get(id);
            if (ModelState.IsValid)
            {
                if (await TryUpdateModelAsync(poco,"",
                                i=>i.Skill,
                                i=>i.SkillLevel,
                                i=> i.StartMonth,
                                i=>i.StartYear,
                                i=>i.EndMonth,
                                i=>i.EndYear))
                {
                    try
                    {
                        _logic.Update(new ApplicantSkillPoco[] { poco });
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ApplicantSkillPocoExists(poco.Id))
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

            PopulateApplicantList(poco.Applicant);
            return View(poco);
        }

        // GET: ApplicantSkill/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {

            var applicantSkillPoco = _logic.GetList(c=>c.Id==id, i=>i.ApplicantProfile, i=>i.ApplicantProfile.SecurityLogin).FirstOrDefault();
                
            if (applicantSkillPoco == null)
            {
                return NotFound();
            }

            return View(applicantSkillPoco);
        }

        // POST: ApplicantSkill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var applicantSkillPoco = _logic.Get(id);
            _logic.Delete(new ApplicantSkillPoco[] { applicantSkillPoco });

            return RedirectToAction(nameof(Details), "ApplicantProfile", new { Id = applicantSkillPoco.Applicant });
        }

        void PopulateApplicantList(Guid? Selected = null)
        {
            var logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
            var Applicants = logic.GetAll(i => i.SecurityLogin).OrderByDescending(u => u.SecurityLogin.Created).Select(k => new { Id = k.Id, Name = k.SecurityLogin.FullName });
            ViewData["Applicant"] = new SelectList(Applicants, "Id", "Name", Selected);
        }

        private bool ApplicantSkillPocoExists(Guid id)
        {
            return _logic.GetList(e => e.Id == id).Any();
        }
    }
}
