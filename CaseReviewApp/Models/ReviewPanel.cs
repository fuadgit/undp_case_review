using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CaseReviewApp.Models
{
    public class ReviewPanel
    {
        [Key]
        public int ReviewID { get; set; }
        [DisplayName("Review Details")]
        public String ReviewDetails { get; set; }
        [DisplayName("Created By")]
        public String CreatedBy { get; set; }
        [Column(TypeName = "datetime2")]
        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public int CaseID { get; set; }
        public virtual Case Case { get; set; }
    }
}