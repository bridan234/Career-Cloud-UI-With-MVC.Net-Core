using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Profiles")]
    public class ApplicantProfilePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name="Applicant")]
        public Guid Login { get; set; }

        [Column("Current_Salary"), Display(Name ="Current Salary")]
        public Decimal? CurrentSalary { get; set; }

        [Column("Current_Rate"), Display(Name = "Current Rate")]
        public Decimal? CurrentRate { get; set; }
        public string Currency  { get; set; }

        [Column("Country_Code"), Display(Name = "Country")]
        public string Country { get; set; }

        [Column("State_Province_Code"), Display(Name = "Province")]
        public string Province { get; set; }

        [Column("Street_Address"), Display(Name = "Street")]
        public string Street { get; set; }

        [Column("City_Town"), Display(Name = "City")]
        public string City { get; set; }

        [Column("Zip_Postal_Code"), Display(Name = "Zip Code")]
        public string PostalCode { get; set; }

        [Column("Time_Stamp"), Display(Name = "Time Stamp")]
        public Byte[] TimeStamp { get; set; }
        public virtual SecurityLoginPoco SecurityLogin { get; set; }
        public virtual SystemCountryCodePoco SystemCountryCode { get; set; }

        #region Navigational-Pointer ...
        public virtual ICollection<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public virtual ICollection<ApplicantJobApplicationPoco> ApplicantJobApplications {get; set;}
        public virtual ApplicantResumePoco ApplicantResume {get; set;}
        public virtual ICollection<ApplicantSkillPoco> ApplicantSkills {get; set;}
        public virtual ICollection<ApplicantWorkHistoryPoco> ApplicantWorkHistories{get; set;}
        #endregion
    }
}
