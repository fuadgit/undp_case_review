using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaseReviewApp.DAL;
using CaseReviewApp.Models;

namespace CaseReviewApp.Controllers
{
    public class MemberController : Controller
    {
        CaseReviewRepository repo = new CaseReviewRepository();
        // GET: Member
        public ActionResult Index()
        {
            List<Member> members = new List<Member>();
            try
            {
                members = repo.GetMembers().ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            return View(members);
        }

        [HttpPost]
        public ActionResult Insert(Member member)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    repo.InsertMember(member);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView("_MemberDetails", repo.GetMembers().ToList());
        }
    }
}