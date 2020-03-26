using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CaseReviewApp.Models
{
    public class Case
    {
        [Key]
        public int CaseID { get; set; }
        [DisplayName("Case Title")]
        public String CaseTitle { get; set; }
        [DisplayName("Short Description")]
        public String ShortDescription { get; set; }
        [DisplayName("Created By")]
        public String CreatedBy { get; set; }
        [Column(TypeName = "datetime2")]
        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }
    }
}