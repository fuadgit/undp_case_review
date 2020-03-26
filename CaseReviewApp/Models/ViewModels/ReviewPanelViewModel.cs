using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CaseReviewApp.Models.ViewModels
{
    public class ReviewPanelViewModel
    {
        public int ReviewID { get; set; }
        [DisplayName("Case Title")]
        public String CaseTitle { get; set; }
        [DisplayName("Review Details")]
        public String ReviewDetails { get; set; }
        [DisplayName("Created By")]
        public String CreatedBy { get; set; }
        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("Panel Members")]
        public List<Member> Members { get; set; }
    }
}