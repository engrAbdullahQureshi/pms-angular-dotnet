using Microsoft.EntityFrameworkCore;
using PMS.Domain.Entities;

namespace PMS.Infrastructure.Persistence;

public class PmsDbContext : DbContext
{
    // Constructor for dependency injection 
    public PmsDbContext(DbContextOptions<PmsDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectMember> ProjectMembers => Set<ProjectMember>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // -------------------------
        // User
        // -------------------------
        modelBuilder.Entity<User>(entity =>
        {
            // Unique Email
            entity.HasIndex(u => u.Email).IsUnique();

            // Optional: limit sizes (good practice)
            entity.Property(u => u.FullName).HasMaxLength(200);
            entity.Property(u => u.Email).HasMaxLength(320);
            entity.Property(u => u.PasswordHash).HasMaxLength(500);
        });

        // -------------------------
        // Project
        // -------------------------
        modelBuilder.Entity<Project>(entity =>
        {
            entity.Property(p => p.Name).HasMaxLength(200);

            // Project.CreatedById -> User (one user can create many projects)
            entity.HasOne(p => p.CreatedBy)
                  .WithMany(u => u.CreatedProjects)
                  .HasForeignKey(p => p.CreatedById)
                  .OnDelete(DeleteBehavior.Restrict); // don't allow deleting user if projects exist
        });

        // -------------------------
        // ProjectMember (Join Table)
        // -------------------------
        modelBuilder.Entity<ProjectMember>(entity =>
        {
            // Prevent duplicate membership: one user cannot be added twice to same project
            entity.HasIndex(pm => new { pm.ProjectId, pm.UserId }).IsUnique();

            entity.HasOne(pm => pm.Project)
                  .WithMany(p => p.Members)
                  .HasForeignKey(pm => pm.ProjectId)
                  .OnDelete(DeleteBehavior.Cascade); // delete memberships if project is deleted

            entity.HasOne(pm => pm.User)
                  .WithMany(u => u.ProjectMemberships)
                  .HasForeignKey(pm => pm.UserId)
                  .OnDelete(DeleteBehavior.Restrict); // don't delete user if membership exists
        });

        // -------------------------
        // TaskItem
        // -------------------------
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.Property(t => t.Title).HasMaxLength(300);

            // Indexes for performance
            entity.HasIndex(t => t.ProjectId);
            entity.HasIndex(t => t.AssignedToUserId);

            // TaskItem.ProjectId -> Project (one project has many tasks)
            entity.HasOne(t => t.Project)
                  .WithMany(p => p.Tasks)
                  .HasForeignKey(t => t.ProjectId)
                  .OnDelete(DeleteBehavior.Cascade); // delete tasks if project deleted

            // TaskItem.AssignedToUserId -> User (optional assignment)
            entity.HasOne(t => t.AssignedTo)
                  .WithMany(u => u.AssignedTasks)
                  .HasForeignKey(t => t.AssignedToUserId)
                  .OnDelete(DeleteBehavior.SetNull); // if assigned user deleted (rare), unassign task

            // TaskItem.CreatedById -> User (required)
            entity.HasOne(t => t.CreatedBy)
                  .WithMany(u => u.CreatedTasks)
                  .HasForeignKey(t => t.CreatedById)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // -------------------------
        // Comment
        // -------------------------
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(c => c.Content).HasMaxLength(2000);

            // Index
            entity.HasIndex(c => c.TaskId);

            // Comment.TaskId -> TaskItem (one task has many comments)
            entity.HasOne(c => c.Task)
                  .WithMany(t => t.Comments)
                  .HasForeignKey(c => c.TaskId)
                  .OnDelete(DeleteBehavior.Cascade); // delete comments if task deleted

            // Comment.UserId -> User (one user has many comments)
            entity.HasOne(c => c.User)
                  .WithMany(u => u.Comments)
                  .HasForeignKey(c => c.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}