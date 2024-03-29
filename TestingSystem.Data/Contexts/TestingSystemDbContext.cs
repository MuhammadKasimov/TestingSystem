﻿using Microsoft.EntityFrameworkCore;
using TestingSystem.Domain.Entities.Attachments;
using TestingSystem.Domain.Entities.Courses;
using TestingSystem.Domain.Entities.Quizes;
using TestingSystem.Domain.Entities.Users;

namespace TestingSystem.Data.Contexts
{
    public class TestingSystemDbContext : DbContext
    {
        public TestingSystemDbContext(DbContextOptions<TestingSystemDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Quiz> Quizzes { get; set; }
        public virtual DbSet<QuizResult> QuizResults { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasIndex(c => c.Name)
                .IsUnique(true);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique(true);

            //modelBuilder.Entity<Course>()
            //    .HasMany(c => c.Quizes)
            //    .WithOne()
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Quiz>()
            //    .HasMany(c => c.Questions)
            //    .WithOne()
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Question>()
            //    .HasMany(c => c.Answers)
            //    .WithOne()
            //    .OnDelete(DeleteBehavior.Cascade);  

            //modelBuilder.Entity<Question>()
            //    .HasMany(c => c.Attachments)
            //    .WithOne()
            //    .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
