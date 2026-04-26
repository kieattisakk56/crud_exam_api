using Microsoft.EntityFrameworkCore;
using ProjectApi.Domain.Common;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ApprovalDocument> ApprovalDocuments { get; set; }
    public DbSet<EmployeeProfile> EmployeeProfiles { get; set; }
    public DbSet<QueueTicket> QueueTickets { get; set; }
    public DbSet<ProductBarcode> ProductBarcodes { get; set; }
    public DbSet<ProductQrCode> ProductQrCodes { get; set; }
    public DbSet<ExamQuestion> ExamQuestions { get; set; }
    public DbSet<ExamQuestionChoice> ExamQuestionChoices { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<ExamResult> ExamResults { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // IT 06: ProductBarcode - unique constraint on Code
        modelBuilder.Entity<ProductBarcode>()
            .HasIndex(p => p.Code).IsUnique();

        // IT 07: ProductQrCode - unique constraint on Code
        modelBuilder.Entity<ProductQrCode>()
            .HasIndex(p => p.Code).IsUnique();

        // IT 02: User - unique constraint on Username
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username).IsUnique();

        // IT 08: ExamQuestion -> ExamQuestionChoice (1-to-many)
        modelBuilder.Entity<ExamQuestion>()
            .HasMany(q => q.Choices)
            .WithOne()
            .HasForeignKey(c => c.ExamQuestionId)
            .OnDelete(DeleteBehavior.Restrict);

        // IT 09: Post -> Comment (1-to-many)
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        // ============== Seed Data ==============

        modelBuilder.Entity<Person>().HasData(
            new Person { Id = 1, FirstName = "สมชาย", LastName = "ใจดี", DateOfBirth = new DateTime(1990, 5, 15), Address = "123 ถ.สุขุมวิท กรุงเทพฯ" },
            new Person { Id = 2, FirstName = "สมหญิง", LastName = "รักเรียน", DateOfBirth = new DateTime(1995, 8, 20), Address = "456 ถ.พหลโยธิน กรุงเทพฯ" },
            new Person { Id = 3, FirstName = "วิชัย", LastName = "สุขสันต์", DateOfBirth = new DateTime(1988, 1, 10), Address = "789 ถ.รัชดา กรุงเทพฯ" }
        );

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "admin", PasswordHash = "A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ=" },
            new User { Id = 2, Username = "user1", PasswordHash = "A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ=" }
        );

        modelBuilder.Entity<ApprovalDocument>().HasData(
            new ApprovalDocument { Id = 1, Title = "รายการที่ 1", Description = "ขออนุมัติจัดซื้ออุปกรณ์คอมพิวเตอร์", Status = 0 },
            new ApprovalDocument { Id = 2, Title = "รายการที่ 2", Description = "ขออนุมัติเดินทางไปสัมมนา", Status = 1, Reason = "อนุมัติตามที่เสนอ", ApprovedBy = "admin" },
            new ApprovalDocument { Id = 3, Title = "รายการที่ 3", Description = "ขออนุมัติงบประมาณโครงการ", Status = 2, Reason = "งบประมาณไม่เพียงพอ", ApprovedBy = "admin" },
            new ApprovalDocument { Id = 4, Title = "รายการที่ 4", Description = "ขออนุมัติจ้างพนักงานชั่วคราว", Status = 0 },
            new ApprovalDocument { Id = 5, Title = "รายการที่ 5", Description = "ขออนุมัติปรับปรุงสำนักงาน", Status = 0 }
        );

        modelBuilder.Entity<EmployeeProfile>().HasData(
            new EmployeeProfile { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", Phone = "0812345678", BirthDay = new DateTime(1990, 3, 15), Occupation = "Software Engineer", Sex = "Male" },
            new EmployeeProfile { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@example.com", Phone = "0898765432", BirthDay = new DateTime(1992, 7, 22), Occupation = "Designer", Sex = "Female" }
        );

        modelBuilder.Entity<ProductBarcode>().HasData(
            new ProductBarcode { Id = 1, Code = "ABCD-1234-EFGH-5678" },
            new ProductBarcode { Id = 2, Code = "WXYZ-9876-MNOP-5432" },
            new ProductBarcode { Id = 3, Code = "QRST-1111-UVWX-2222" }
        );

        modelBuilder.Entity<ProductQrCode>().HasData(
            new ProductQrCode { Id = 1, Code = "ABCDE-FGHIJ-KLMNO-PQRST-UVWXY-Z1234" },
            new ProductQrCode { Id = 2, Code = "11111-22222-33333-44444-55555-66666" }
        );

        // IT 08: Exam Questions (1-to-many choices)
        modelBuilder.Entity<ExamQuestion>().HasData(
            new ExamQuestion { Id = 1, QuestionNumber = 1, QuestionText = "ข้อใดต่างจากข้ออื่น", CorrectAnswer = 3 },
            new ExamQuestion { Id = 2, QuestionNumber = 2, QuestionText = "2 x X = 4 จะหาค่า X", CorrectAnswer = 2 },
            new ExamQuestion { Id = 3, QuestionNumber = 3, QuestionText = "เมืองหลวงของประเทศไทยคือ", CorrectAnswer = 2 }
        );
        modelBuilder.Entity<ExamQuestionChoice>().HasData(
            new ExamQuestionChoice { Id = 1,  ExamQuestionId = 1, ChoiceNumber = 1, ChoiceText = "3" },
            new ExamQuestionChoice { Id = 2,  ExamQuestionId = 1, ChoiceNumber = 2, ChoiceText = "5" },
            new ExamQuestionChoice { Id = 3,  ExamQuestionId = 1, ChoiceNumber = 3, ChoiceText = "9" },
            new ExamQuestionChoice { Id = 4,  ExamQuestionId = 1, ChoiceNumber = 4, ChoiceText = "11" },
            new ExamQuestionChoice { Id = 5,  ExamQuestionId = 2, ChoiceNumber = 1, ChoiceText = "1" },
            new ExamQuestionChoice { Id = 6,  ExamQuestionId = 2, ChoiceNumber = 2, ChoiceText = "2" },
            new ExamQuestionChoice { Id = 7,  ExamQuestionId = 2, ChoiceNumber = 3, ChoiceText = "3" },
            new ExamQuestionChoice { Id = 8,  ExamQuestionId = 2, ChoiceNumber = 4, ChoiceText = "4" },
            new ExamQuestionChoice { Id = 9,  ExamQuestionId = 3, ChoiceNumber = 1, ChoiceText = "เชียงใหม่" },
            new ExamQuestionChoice { Id = 10, ExamQuestionId = 3, ChoiceNumber = 2, ChoiceText = "กรุงเทพ" },
            new ExamQuestionChoice { Id = 11, ExamQuestionId = 3, ChoiceNumber = 3, ChoiceText = "ภูเก็ต" },
            new ExamQuestionChoice { Id = 12, ExamQuestionId = 3, ChoiceNumber = 4, ChoiceText = "ขอนแก่น" }
        );

        // IT 09: Posts and Comments
        modelBuilder.Entity<Post>().HasData(
            new Post 
            { 
                Id = 1, 
                Title = "Welcome to 63 Test", 
                Content = "This is the first post in our new system.", 
                Author = "Admin",
                ImageUrl = "https://picsum.photos/800/400"
            }
        );

        modelBuilder.Entity<Comment>().HasData(
            new Comment { Id = 1, PostId = 1, CommenterName = "Blend 285", CommentText = "have a good day" },
            new Comment { Id = 2, PostId = 1, CommenterName = "Kieattisakk", CommentText = "Great start!" }
        );
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Deleted:
                    entry.Entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
