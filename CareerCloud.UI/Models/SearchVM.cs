using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CareerCloud.UI.Models
{
    public class SearchVM
    {
        public Guid JobDescId { get; set; }

        [Display(Name = "Date Posted")]
        public string JobCreatedDate { get; set; }

        [Display(Name = "Company")]
        public string Company {get; set;}

		[Display(Name = "Job Title")]
		public string Job { get; set; }

		[Display(Name = "Job Locations")]
		public ICollection<CompanyLocationPoco> Locations { get; set; }

        public string SearchValue { get; set; }
        public string FilterType { get; set; }
    }
}
