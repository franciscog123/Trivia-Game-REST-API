using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TriviaGame.DA.Entities
{
    public partial class TriviaGameDbContext : DbContext
    {
        public TriviaGameDbContext()
        {
        }

        public TriviaGameDbContext(DbContextOptions<TriviaGameDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Choice> Choice { get; set; }
        public virtual DbSet<GameMode> GameMode { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Quiz> Quiz { get; set; }
        public virtual DbSet<QuizQuestion> QuizQuestion { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Category1)
                    .HasColumnName("Category")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Choice>(entity =>
            {
                entity.Property(e => e.Choice1)
                    .HasColumnName("Choice")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Choice)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionId2");
            });

            modelBuilder.Entity<GameMode>(entity =>
            {
                entity.Property(e => e.GameMode1)
                    .HasColumnName("GameMode")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Question1)
                    .IsRequired()
                    .HasColumnName("Question")
                    .HasMaxLength(200);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryId2");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Quiz)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryId1");

                entity.HasOne(d => d.GameMode)
                    .WithMany(p => p.Quiz)
                    .HasForeignKey(d => d.GameModeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GameModeId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Quiz)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserId");
            });

            modelBuilder.Entity<QuizQuestion>(entity =>
            {
                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuizQuestion)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionId1");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.QuizQuestion)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuizId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20);
            });
        }
    }
}
