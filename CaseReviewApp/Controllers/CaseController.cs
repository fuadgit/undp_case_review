using CaseReviewApp.DAL;
using CaseReviewApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaseReviewApp.Controllers
{
    public class CaseController : Controller
    {
        CaseReviewRepository repo = new CaseReviewRepository();
        // GET: Case
        public ActionResult Index()
        {
            List<Case> cases = new List<Case>();
            try
            {
                cases = repo.GetCases();
            }
            catch (Exception)
            {
                throw new Exception("Can not get cases");
            }
            
            return View(cases);
        }
        [HttpPost]
        public ActionResult Insert(Case @case)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    repo.InsertCase(@case);
                }
            }
            catch (Exception)
            {
                throw new Exception("Failed to insert new case");
            }
            
            return PartialView("_CaseDetails", repo.GetCases().ToList());
        }
    }
}