using Microsoft.EntityFrameworkCore;
using SyncWorkerService.Models;

namespace SyncWorkerService.Data;

public class SyncDbContext(DbContextOptions<SyncDbContext> options) : DbContext(options)
{
    public DbSet<ContactLead> ContactLeads => Set<ContactLead>();
    public DbSet<ClientAction> ClientActions => Set<ClientAction>();
    public DbSet<Questionnaire> Questionnaires => Set<Questionnaire>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<ContactLead>().ToTable("contact_leads").HasKey(e => e.Id);

        b.Entity<ClientAction>().ToTable("client_actions").HasKey(e => e.Id);
        b.Entity<ClientAction>().Property(e => e.Id).HasColumnName("id");
        b.Entity<ClientAction>().Property(e => e.ContactLeadId).HasColumnName("contact_id");
        b.Entity<ClientAction>().Property(e => e.ActionType).HasColumnName("action_type");
        b.Entity<ClientAction>().Property(e => e.ExecutedAt).HasColumnName("executed_at");
        b.Entity<ClientAction>()
            .HasOne(e => e.ContactLead)
            .WithMany()
            .HasForeignKey(e => e.ContactLeadId)
            .OnDelete(DeleteBehavior.Cascade);

        b.Entity<Questionnaire>().ToTable("questionnaire").HasKey(e => e.Id);
        b.Entity<Questionnaire>().Property(e => e.Id).HasColumnName("id");
        b.Entity<Questionnaire>().Property(e => e.ContactLeadId).HasColumnName("contact_id");
        b.Entity<Questionnaire>().Property(e => e.Token).HasColumnName("token");
        b.Entity<Questionnaire>().Property(e => e.TokenExpiresAt).HasColumnName("token_expires_at");
        b.Entity<Questionnaire>().Property(e => e.Stage).HasColumnName("stage");
        b.Entity<Questionnaire>().Property(e => e.Step1Answers).HasColumnName("step1_answers");
        b.Entity<Questionnaire>().Property(e => e.Step2Answers).HasColumnName("step2_answers");
        b.Entity<Questionnaire>().Property(e => e.Step3Answers).HasColumnName("step3_answers");
        b.Entity<Questionnaire>().Property(e => e.CompletedAt).HasColumnName("completed_at");
        b.Entity<Questionnaire>().Property(e => e.CreatedAt).HasColumnName("created_at");
        b.Entity<Questionnaire>()
            .HasOne(e => e.ContactLead)
            .WithMany()
            .HasForeignKey(e => e.ContactLeadId)
            .OnDelete(DeleteBehavior.Cascade);
        b.Entity<Questionnaire>().HasIndex(e => e.Token).IsUnique();
    }
}
