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

namespace CareerCloud.UI.Controllers.Companies
{
    public class CompanyDescriptionController : Controller
    {
        private readonly CareerCloudContext _context;
        private readonly CompanyDescriptionLogic _logic;

        public CompanyDescriptionController()
        {
            _logic = new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>());

        }

        // GET: CompanyDescription
        public async Task<IActionResult> Index()
        {
            var careerCloudContext = _context.CompanyDescriptions.Include(c => c.CompanyProfile).Include(c => c.SystemLanguageCode);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: CompanyDescription/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyDescriptionPoco = await _context.CompanyDescriptions
                .Include(c => c.CompanyProfile)
                .Include(c => c.SystemLanguageCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyDescriptionPoco == null)
            {
                return NotFound();
            }

            return View(companyDescriptionPoco);
        }


        // GET: CompanyDescription/Create
        public IActionResult Create(Guid? Company)
        {
            ViewData["emp"] = Company;
            PopulateCompanyList(Company);
            PopulateLanguageDropDownList();

            return View();
        }

        // POST: CompanyDescription/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Company,LanguageId,CompanyName,CompanyDescription")] CompanyDescriptionPoco companyDescriptionPoco)
        {
            if (ModelState.IsValid)
            {
                companyDescriptionPoco.Id = Guid.NewGuid();
                _logic.Add(new CompanyDescriptionPoco[] { companyDescriptionPoco });

                return RedirectToAction(nameof(Details),"companyprofile", new {Id = companyDescriptionPoco.Company });
            }
            PopulateCompanyList(companyDescriptionPoco.Id);
            PopulateLanguageDropDownList(companyDescriptionPoco.LanguageId);
            return View(companyDescriptionPoco);
        }

        // GET: CompanyDescription/Edit/5
        public async Task<IActionResult> Edit(Guid Id)
        {

            var companyDescriptionPoco = _logic.Get(Id);

            if (companyDescriptionPoco == null)
            {
                return NotFound();
            }
            PopulateLanguageDropDownList(companyDescriptionPoco.LanguageId);
            PopulateCompanyList(companyDescriptionPoco.Company);

            return View(companyDescriptionPoco);
        }

        // POST: CompanyDescription/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Edit")]
        public async Task<IActionResult> EditComfimed(Guid id)// CompanyDescriptionPoco companyDescriptionPoco)
        {
            var companyDescriptionPoco = _logic.Get(id);

            if (ModelState.IsValid)
            {
                try
                {
                    if (await TryUpdateModelAsync(companyDescriptionPoco, "", i => i.LanguageId, i => i.CompanyName, i => i.CompanyDescription))
                        _logic.Update(new CompanyDescriptionPoco[] { companyDescriptionPoco });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyDescriptionPocoExists(companyDescriptionPoco.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), "companyprofile", new { Id = companyDescriptionPoco.Company });
            }
            PopulateCompanyList(companyDescriptionPoco.Company);
            PopulateLanguageDropDownList(companyDescriptionPoco.LanguageId);
            return View(companyDescriptionPoco);
        }

        // GET: CompanyDescription/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyDescriptionPoco = await _context.CompanyDescriptions
                .Include(c => c.CompanyProfile)
                .Include(c => c.SystemLanguageCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyDescriptionPoco == null)
            {
                return NotFound();
            }

            return View(companyDescriptionPoco);
        }

        // POST: CompanyDescription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyDescriptionPoco = await _context.CompanyDescriptions.FindAsync(id);
            _context.CompanyDescriptions.Remove(companyDescriptionPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateCompanyList(Guid? Company = null)
        {
            var Companies = _logic.GetAll().Select(k => new { Id = k.Company, Name = k.CompanyName is "" ? "<empty>" : k.CompanyName });
            ViewData["Company"] = new SelectList(Companies, "Id", "Name", Company);
        }

        private void PopulateLanguageDropDownList(object selectedLanguage = null)
        {
            var SysLang = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>()).GetList(c => c.LanguageID.Length <= 3).OrderBy(l => l.Name);
            
            ViewBag.Language = new SelectList(SysLang, "LanguageID", "Name", selectedLanguage);
            
        }

        private bool CompanyDescriptionPocoExists(Guid id)
        {
            return _context.CompanyDescriptions.Any(e => e.Id == id);
        }
    }
}
