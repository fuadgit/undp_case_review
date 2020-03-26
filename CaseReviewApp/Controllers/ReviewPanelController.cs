using CaseReviewApp.DAL;
using CaseReviewApp.Models;
using CaseReviewApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaseReviewApp.Controllers
{
    public class ReviewPanelController : Controller
    {
        CaseReviewRepository repo = new CaseReviewRepository();
        // GET: ReviewPanel
        public ActionResult Index()
        {
            ViewBag.Cases = repo.AvailableCasesForReview();
            ViewBag.Members = repo.GetMembers().ToList();
            return View(repo.GetReviewPanels().ToList());
        }

        [HttpPost]
        public ActionResult Insert(PanelFormData panelData)
        {
            repo.InsertReviewPanel(panelData);
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ReviewPanel");
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Delete(int ReviewID)
        {
            repo.DeleteReview(ReviewID);
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ReviewPanel");
            return Json(new { Url = redirectUrl });
        }

        public ActionResult EditModalData(int ReviewID)
        {
            ReviewPanel reviewPanel = repo.GetReviewPanel(ReviewID);
            ViewBag.AllMembers = repo.GetMembers().ToList();
            return PartialView("_EditModalBody", reviewPanel);
        }

        public ActionResult UpdateReviewPanel(PanelFormData panelFormData)
        {
            repo.UpdateReviewPanel(panelFormData);
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ReviewPanel");
            return Json(new { Url = redirectUrl });
        }

    }
}