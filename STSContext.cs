using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace STS.DAL.Entities;

public partial class STSContext : DbContext
{
    public STSContext()
    {
    }

    public STSContext(DbContextOptions<STSContext> options)
        : base(options)
    {
    }

   // public virtual DbSet<Applicant> Applicants { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Commentanswer> Commentanswers { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    //public virtual DbSet<Company1> Companies1 { get; set; }

    public virtual DbSet<CtAnswerComment> CtAnswerComments { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionComment> QuestionComments { get; set; }

    public virtual DbSet<QuestionQuestionanswer> QuestionQuestionanswers { get; set; }

    public virtual DbSet<Questiontype> Questiontypes { get; set; }

    public virtual DbSet<Qustionanswer> Qustionanswers { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

   // public virtual DbSet<Test1> Tests1 { get; set; }

    public virtual DbSet<TestComment> TestComments { get; set; }

    public virtual DbSet<TestQuestion> TestQuestions { get; set; }

    public virtual DbSet<TestTestgroup> TestTestgroups { get; set; }

    public virtual DbSet<Testgroup> Testgroups { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseNpgsql("Host=localhost;port=5432;Username=postgres;Password=postgres;Database=STS");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*modelBuilder.Entity<Applicant>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("applicants", "STS");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.ОСебе).HasColumnName("О_себе");
            entity.Property(e => e.Фио).HasColumnName("ФИО");
        });*/

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pk");

            entity.ToTable("category", "STS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comment_pk");

            entity.ToTable("Comment", "STS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Author).HasColumnName("author");
            entity.Property(e => e.Commentdate).HasColumnName("commentdate");
            entity.Property(e => e.Commenttime).HasColumnName("commenttime");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Author)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_fk");
        });

        modelBuilder.Entity<Commentanswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("newtable_pk");

            entity.ToTable("commentanswer", "STS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Answerdate).HasColumnName("answerdate");
            entity.Property(e => e.Answertime).HasColumnName("answertime");
            entity.Property(e => e.Author).HasColumnName("author");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Commentanswers)
                .HasForeignKey(d => d.Author)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("commentanswer_fk");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("company", "STS");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Owner).HasColumnName("owner");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.OwnerNavigation).WithMany()
                .HasForeignKey(d => d.Owner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("company_fk");
        });

       /* modelBuilder.Entity<Company1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("companies", "STS");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.ДатаСоздания).HasColumnName("Дата_создания");
            entity.Property(e => e.Название).HasMaxLength(100);
        });*/

        modelBuilder.Entity<CtAnswerComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ct_answer_comment_pk");

            entity.ToTable("ct_answer_comment", "STS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Commentanswerid).HasColumnName("commentanswerid");
            entity.Property(e => e.Commentid).HasColumnName("commentid");

            entity.HasOne(d => d.Commentanswer).WithMany(p => p.CtAnswerComments)
                .HasForeignKey(d => d.Commentanswerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ct_answer_comment_fk_1");

            entity.HasOne(d => d.Comment).WithMany(p => p.CtAnswerComments)
                .HasForeignKey(d => d.Commentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ct_answer_comment_fk");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employerfavorites_pk");

            entity.ToTable("favorites", "STS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Owner).HasColumnName("owner");
            entity.Property(e => e.Testid).HasColumnName("testid");

            entity.HasOne(d => d.OwnerNavigation).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.Owner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("favorites_fk");

            entity.HasOne(d => d.Test).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.Testid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fav_test");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("question_pk");

            entity.ToTable("question", "STS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Difficulty)
                .HasMaxLength(100)
                .HasDefaultValueSql("'Средний'::character varying")
                .HasColumnName("difficulty");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Topic)
                .HasMaxLength(200)
                .HasDefaultValueSql("'Без темы'::character varying")
                .HasColumnName("topic");
            entity.Property(e => e.Typeid).HasColumnName("typeid");

            entity.HasOne(d => d.Type).WithMany(p => p.Questions)
                .HasForeignKey(d => d.Typeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("question_fk");
        });

        modelBuilder.Entity<QuestionComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("question_comment_pk");

            entity.ToTable("question_comment", "STS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Commentid).HasColumnName("commentid");
            entity.Property(e => e.Questionid).HasColumnName("questionid");

            entity.HasOne(d => d.Comment).WithMany(p => p.QuestionComments)
                .HasForeignKey(d => d.Commentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("question_comment_fk");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionComments)
                .HasForeignKey(d => d.Questionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("question_comment_fk_1");
        });

        modelBuilder.Entity<QuestionQuestionanswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("question_questionanswer_pk");

            entity.ToTable("question_questionanswer", "STS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Questionanswerid).HasColumnName("questionanswerid");
            entity.Property(e => e.Questionid).HasColumnName("questionid");

            entity.HasOne(d => d.Questionanswer).WithMany(p => p.QuestionQuestionanswers)
                .HasForeignKey(d => d.Questionanswerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("question_questionanswer_fk");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionQuestionanswers)
                .HasForeignKey(d => d.Questionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("question_questionanswer_fk_1");
        });

        modelBuilder.Entity<Questiontype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("questiontype_pk");

            entity.ToTable("questiontype", "STS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<Qustionanswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("qustionanswer_pk");

            entity.ToTable("qustionanswer", "STS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Istrue).HasColumnName("istrue");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("result_pk");

            entity.ToTable("Result", "STS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.Testid).HasColumnName("testid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Test).WithMany(p => p.Results)
                .HasForeignKey(d => d.Testid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("result_fk_1");

            entity.HasOne(d => d.User).WithMany(p => p.Results)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("result_fk");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("test_pk");

            entity.ToTable("test", "STS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Author).HasColumnName("author");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Creationdate).HasColumnName("creationdate");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Difficulty)
                .HasDefaultValueSql("'Средний'::character varying")
                .HasColumnType("character varying")
                .HasColumnName("difficulty");
            entity.Property(e => e.Image)
                .HasDefaultValueSql("'https://papik.pro/uploads/posts/2022-01/1642329969_39-papik-pro-p-testirovanie-klipart-42.png'::text")
                .HasColumnName("image");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Tests)
                .HasForeignKey(d => d.Author)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("test_fk");

            entity.HasOne(d => d.Category).WithMany(p => p.Tests)
                .HasForeignKey(d => d.Categoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("test_category");
        });

       /* modelBuilder.Entity<Test1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("tests", "STS");

            entity.Property(e => e.ДатаСоздания).HasColumnName("Дата_создания");
            entity.Property(e => e.Категория).HasMaxLength(100);
            entity.Property(e => e.Название).HasMaxLength(100);
            entity.Property(e => e.Сложность).HasColumnType("character varying");
        });*/

        modelBuilder.Entity<TestComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("test_comment_pk");

            entity.ToTable("test_comment", "STS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Commentid).HasColumnName("commentid");
            entity.Property(e => e.Testid).HasColumnName("testid");

            entity.HasOne(d => d.Comment).WithMany(p => p.TestComments)
                .HasForeignKey(d => d.Commentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_test");

            entity.HasOne(d => d.Test).WithMany(p => p.TestComments)
                .HasForeignKey(d => d.Testid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("test_comment_fk");
        });

        modelBuilder.Entity<TestQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("test_question_pk");

            entity.ToTable("test_question", "STS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Questionid).HasColumnName("questionid");
            entity.Property(e => e.Testid).HasColumnName("testid");

            entity.HasOne(d => d.Question).WithMany(p => p.TestQuestions)
                .HasForeignKey(d => d.Questionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("test_question_fk_1");

            entity.HasOne(d => d.Test).WithMany(p => p.TestQuestions)
                .HasForeignKey(d => d.Testid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("test_question_fk");
        });

        modelBuilder.Entity<TestTestgroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("test_testgroup_pk");

            entity.ToTable("test_testgroup", "STS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Testgroupid).HasColumnName("testgroupid");
            entity.Property(e => e.Testid).HasColumnName("testid");

            entity.HasOne(d => d.Testgroup).WithMany(p => p.TestTestgroups)
                .HasForeignKey(d => d.Testgroupid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("test_testgroup_fk");

            entity.HasOne(d => d.Test).WithMany(p => p.TestTestgroups)
                .HasForeignKey(d => d.Testid)
                .HasConstraintName("test_testgroup_fk_1");
        });

        modelBuilder.Entity<Testgroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("testgroup_pk");

            entity.ToTable("testgroup", "STS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Creationdate).HasColumnName("creationdate");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.OwnerNavigation).WithMany(p => p.Testgroups)
                .HasForeignKey(d => d.Owner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("testgroup_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employer_pk");

            entity.ToTable("users", "STS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Aboutmyself).HasColumnName("aboutmyself");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(100)
                .HasColumnName("patronymic");
            entity.Property(e => e.Role)
                .HasComment("false - applicant, true - employer")
                .HasColumnName("role");

            entity.Property(e => e.pw)
                .HasMaxLength(100)
                .HasColumnName("pw")
                .HasComment("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
