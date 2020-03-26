using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CaseReviewApp.Models;

namespace CaseReviewApp.DAL
{
    public class CaseReviewDbContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<ReviewPanel> ReviewPanels { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>().MapToStoredProcedures();
            modelBuilder.Entity<Case>().MapToStoredProcedures();
            modelBuilder.Entity<ReviewPanel>().MapToStoredProcedures();

            modelBuilder.Entity<ReviewPanel>()
                .HasMany(rp => rp.Members)
                .WithMany(rp => rp.ReviewPanels)
                .Map(m =>
                {
                    m.ToTable("ReviewPanelMembers");
                    m.MapLeftKey("ReviewID");
                    m.MapRightKey("MemberID");
                });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}