using Microsoft.EntityFrameworkCore;
using Portfolio_ZoranSimeunovic.Models;

namespace Portfolio_ZoranSimeunovic.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ContactLead> ContactLeads => Set<ContactLead>();
    public DbSet<ChecklistAnswer> ChecklistAnswers => Set<ChecklistAnswer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ContactLead>(entity =>
        {
            entity.ToTable("contact_leads");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(120);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Language).HasMaxLength(10);
            entity.Property(e => e.CreatedAt);
        });

        modelBuilder.Entity<ChecklistAnswer>(entity =>
        {
            entity.ToTable("checklist_answers");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactLeadId).HasColumnName("contact_lead_id");
            entity.Property(e => e.ListKey).HasColumnName("list_key").IsRequired().HasMaxLength(50);
            entity.Property(e => e.ItemText).HasColumnName("item_text").IsRequired().HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.HasOne(e => e.ContactLead)
                  .WithMany()
                  .HasForeignKey(e => e.ContactLeadId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
