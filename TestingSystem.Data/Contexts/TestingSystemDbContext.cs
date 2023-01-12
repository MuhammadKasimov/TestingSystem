using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Domain.Entities.Answers;
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
        public virtual DbSet<QuizResults> QuizResults { get; set; }
        public virtual DbSet<Question>  Questions { get; set; }
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

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(true);
        }
    }
}
