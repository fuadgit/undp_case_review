using CaseReviewApp.Models;
using CaseReviewApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseReviewApp.DAL
{
    public class CaseReviewRepository
    {
        CaseReviewDbContext dbContext = new CaseReviewDbContext();
        //Members
        public List<Member> GetMembers()
        {
            return dbContext.Members.ToList();
        }

        public Member GetMember(int ID)
        {
            Member member = dbContext.Members.SingleOrDefault(m => m.MemberID == ID);
            return member;
        }
        public void InsertMember(Member member)
        {
            dbContext.Members.Add(member);
            dbContext.SaveChanges();
        }

        public void UpdateMember(Member member)
        {
            Member memberToBeUpdated = dbContext.Members.SingleOrDefault(m => m.MemberID == member.MemberID);
            memberToBeUpdated.Name = member.Name;
            memberToBeUpdated.Email = member.Email;
            dbContext.SaveChanges();
        }
        public void DeleteMember(Member member)
        {
            Member memberToBeDeleted = dbContext.Members.SingleOrDefault(m => m.MemberID == member.MemberID);
            dbContext.Members.Remove(memberToBeDeleted);
            dbContext.SaveChanges();
        }

        //Cases
        public List<Case> GetCases()
        {
            return dbContext.Cases.ToList();
        }
        public void InsertCase(Case @case)
        {
            dbContext.Cases.Add(@case);
            dbContext.SaveChanges();
        }

        public void UpdateCase(Case @case)
        {
            Case caseToBeUpdated = dbContext.Cases.SingleOrDefault(c => c.CaseID == @case.CaseID);
            caseToBeUpdated.CaseTitle = @case.CaseTitle;
            caseToBeUpdated.ShortDescription = @case.ShortDescription;
            caseToBeUpdated.CreatedBy = @case.CreatedBy;
            caseToBeUpdated.CreatedOn = @case.CreatedOn;
            dbContext.SaveChanges();
        }
        public void DeleteCase(Case @case)
        {
            Case caseTobeDeleted = dbContext.Cases.SingleOrDefault(c => c.CaseID == @case.CaseID);
            dbContext.Cases.Remove(caseTobeDeleted);
            dbContext.SaveChanges();
        }

        //Review Panels
        public List<ReviewPanelViewModel> GetReviewPanels()
        {
            List<ReviewPanel> reviews = dbContext.ReviewPanels.ToList();
            List<ReviewPanelViewModel> reviewPanels = reviews.Select(r => new ReviewPanelViewModel
            {
                ReviewID = r.ReviewID,
                CaseTitle = r.Case.CaseTitle,
                ReviewDetails = r.ReviewDetails,
                CreatedBy = r.CreatedBy,
                CreatedOn = r.CreatedOn,
                Members = r.Members.ToList()
            }).ToList();
            return reviewPanels;
        }

        public ReviewPanel GetReviewPanel(int ReviewID)
        {
            return dbContext.ReviewPanels.Include("Members").SingleOrDefault(r => r.ReviewID == ReviewID);
        }

        public void InsertReviewPanel(PanelFormData panelFormData)
        {
            CaseMember[] caseMembers = panelFormData.Reviews;
            if(caseMembers.Length > 0)
            {
                foreach (CaseMember caseMember in caseMembers)
                {
                    ReviewPanel reviewPanel = new ReviewPanel();
                    reviewPanel.CaseID = caseMember.CaseID;
                    reviewPanel.ReviewDetails = panelFormData.ReviewDetails;
                    reviewPanel.CreatedBy = panelFormData.CreatedBy;
                    reviewPanel.CreatedOn = panelFormData.CreatedOn;
                    List<Member> panelMembers = dbContext.Members.Where(m => caseMember.Members.Contains(m.MemberID)).ToList();
                    reviewPanel.Members = panelMembers;
                    reviewPanel.Case = dbContext.Cases.SingleOrDefault(c => c.CaseID == caseMember.CaseID);

                    dbContext.ReviewPanels.Add(reviewPanel);
                }
                dbContext.SaveChanges();
            }
        }

        public void UpdateReviewPanel(PanelFormData panelData)
        {
            ReviewPanel reviewPanel = dbContext.ReviewPanels.SingleOrDefault(r => r.ReviewID == panelData.ReviewID);
            if( reviewPanel != null)
            {
                CaseMember member = panelData.Reviews[0];
                List<Member> panelMembers = dbContext.Members.Where(m => member.Members.Contains(m.MemberID)).ToList();
                reviewPanel.Members.Clear();
                dbContext.SaveChanges();
                reviewPanel.ReviewDetails = panelData.ReviewDetails;
                reviewPanel.CreatedBy = panelData.CreatedBy;
                reviewPanel.CreatedOn = panelData.CreatedOn;
                reviewPanel.Members = panelMembers;

                dbContext.SaveChanges();
            }
        }

        public List<Case> AvailableCasesForReview()
        {
            int[] assignedCaseIds = dbContext.ReviewPanels.AsEnumerable().Select(r => r.CaseID).ToArray();
            return dbContext.Cases.Where(c => !assignedCaseIds.Contains(c.CaseID)).ToList();
        }

        public void DeleteReview(int ReviewID)
        {
            ReviewPanel reviewPanel = dbContext.ReviewPanels.SingleOrDefault(r => r.ReviewID == ReviewID);
            if (reviewPanel != null)
            {
                dbContext.ReviewPanels.Remove(reviewPanel);
            }
            dbContext.SaveChanges();
        }
    }
}