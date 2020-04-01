using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CareerCloud.UI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;

namespace CareerCloud.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CompanyJobLogic _logic;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _logic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());
        }

        public IActionResult Index(string SearchValue)
        {
            var bitch = _logic.GetAll();
           var Model = new List<SearchVM>(); 
           SearchValue = string.IsNullOrWhiteSpace(SearchValue) ? null: SearchValue.Trim() ;
   
            if (!string.IsNullOrEmpty(SearchValue))
            {
                 Model = _logic.GetAll(i => i.CompanyJobDescription
                        , i => i.CompanyProfile
                        , i => i.CompanyProfile.CompanyDescriptions
                        , i => i.CompanyProfile.CompanyLocations).ToList()
                        .Where(c =>
                          c.CompanyJobDescription.JobName.Contains(SearchValue, StringComparison.OrdinalIgnoreCase) ||
                          c.CompanyProfile.CompanyDescriptions.First().CompanyName.Contains(SearchValue, StringComparison.OrdinalIgnoreCase))
                        
                        .Select(q => new SearchVM
                            {
                                JobDescId = q.CompanyJobDescription.Id,
                                Company = q.CompanyProfile.CompanyDescriptions.FirstOrDefault().CompanyName,
                                Job = q.CompanyJobDescription.JobName.Trim(),
                                JobCreatedDate = q.ProfileCreated.Date.ToShortDateString(),
                                Locations = q.CompanyProfile.CompanyLocations is null ? null : q.CompanyProfile.CompanyLocations.ToList()
                            } ).ToList();

            }
            else
            {
                Model = _logic.GetAll(i => i.CompanyJobDescription
                    , i => i.CompanyProfile
                    , i => i.CompanyProfile.CompanyDescriptions
                    , i => i.CompanyProfile.CompanyLocations).ToList()

                    .Select(q => new SearchVM
                    {
                        JobDescId = q.CompanyJobDescription.Id,
                        Company = q.CompanyProfile.CompanyDescriptions.FirstOrDefault().CompanyName,
                        Job = q.CompanyJobDescription.JobName.Trim(),
                        JobCreatedDate = q.ProfileCreated.Date.ToShortDateString(),
                        Locations = q.CompanyProfile.CompanyLocations is null ? null : q.CompanyProfile.CompanyLocations.ToList()
                    }).ToList();

            };

            return View(Model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
