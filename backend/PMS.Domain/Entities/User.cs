using PMS.Domain.Enums;

namespace PMS.Domain.Entities;

public class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    // For now we store hashed password string (later Auth service will generate it)
    public string PasswordHash { get; set; } = null!;

    public UserRole Role { get; set; } = UserRole.Member;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation: a user can create many projects
    public ICollection<Project> CreatedProjects { get; set; } = new List<Project>();

    // Navigation: a user can be in many projects (via ProjectMember)
    public ICollection<ProjectMember> ProjectMemberships { get; set; } = new List<ProjectMember>();

    // Navigation: tasks assigned to this user (optional but useful later)
    public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();

    // Navigation: tasks created by this user (optional but useful later)
    public ICollection<TaskItem> CreatedTasks { get; set; } = new List<TaskItem>();

    // Navigation: comments written by this user (optional)
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}