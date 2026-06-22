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
    public DbSet<Questionnaire> Questionnaires => Set<Questionnaire>();
    public DbSet<ClientAction> ClientActions => Set<ClientAction>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<QuestionnaireFile> QuestionnaireFiles => Set<QuestionnaireFile>();

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

        modelBuilder.Entity<Questionnaire>(entity =>
        {
            entity.ToTable("questionnaire");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactLeadId).HasColumnName("contact_id");
            entity.Property(e => e.Token).HasColumnName("token").IsRequired().HasMaxLength(64);
            entity.Property(e => e.TokenExpiresAt).HasColumnName("token_expires_at");
            entity.Property(e => e.Stage).HasColumnName("stage");
            entity.Property(e => e.Step1Answers).HasColumnName("step1_answers");
            entity.Property(e => e.Step2Answers).HasColumnName("step2_answers");
            entity.Property(e => e.Step3Answers).HasColumnName("step3_answers");
            entity.Property(e => e.CompletedAt).HasColumnName("completed_at");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.HasIndex(e => e.Token).IsUnique();
            entity.HasOne(e => e.ContactLead)
                  .WithMany()
                  .HasForeignKey(e => e.ContactLeadId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ClientAction>(entity =>
        {
            entity.ToTable("client_actions");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactLeadId).HasColumnName("contact_id");
            entity.Property(e => e.ActionType).HasColumnName("action_type").IsRequired().HasMaxLength(100);
            entity.Property(e => e.ExecutedAt).HasColumnName("executed_at");
            entity.HasOne(e => e.ContactLead)
                  .WithMany()
                  .HasForeignKey(e => e.ContactLeadId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<QuestionnaireFile>(entity =>
        {
            entity.ToTable("questionnaire_files");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.QuestionnaireId).HasColumnName("questionnaire_id");
            entity.Property(e => e.FileLabel).HasColumnName("file_label").IsRequired().HasMaxLength(50);
            entity.Property(e => e.OriginalFileName).HasColumnName("original_file_name").IsRequired().HasMaxLength(255);
            entity.Property(e => e.StoredFileName).HasColumnName("stored_file_name").IsRequired().HasMaxLength(100);
            entity.Property(e => e.ContentType).HasColumnName("content_type").IsRequired().HasMaxLength(100);
            entity.Property(e => e.SizeBytes).HasColumnName("size_bytes");
            entity.Property(e => e.UploadedAt).HasColumnName("uploaded_at");
            entity.HasOne(e => e.Questionnaire)
                  .WithMany()
                  .HasForeignKey(e => e.QuestionnaireId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.ToTable("documents");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FullName).HasColumnName("full_name").IsRequired().HasMaxLength(255);
            entity.Property(e => e.OriginalFileName).HasColumnName("original_file_name").IsRequired().HasMaxLength(255);
            entity.Property(e => e.StoredFileName).HasColumnName("stored_file_name").IsRequired().HasMaxLength(100);
            entity.Property(e => e.ContentType).HasColumnName("content_type").IsRequired().HasMaxLength(100);
            entity.Property(e => e.SizeBytes).HasColumnName("size_bytes");
            entity.Property(e => e.UploadedAt).HasColumnName("uploaded_at");
        });
    }
}
