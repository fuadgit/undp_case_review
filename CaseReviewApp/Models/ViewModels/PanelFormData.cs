using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseReviewApp.Models.ViewModels
{
    public class PanelFormData
    {
        public int ReviewID { get; set; }
        public CaseMember[] Reviews { get; set; }
        public String ReviewDetails { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class CaseMember
    {
        public int CaseID { get; set; }
        public int[] Members { get; set; }
    }
}