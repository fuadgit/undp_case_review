using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseReviewApp.Models
{
    public class Member
    {
        public int MemberID { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public virtual ICollection<ReviewPanel> ReviewPanels { get; set; }
    }
}