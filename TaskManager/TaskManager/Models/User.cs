namespace TaskManager.Models;
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public byte[] HashedPassword { get; set; }
    public byte[] SaltedPassword { get; set; }

    public ICollection<Task1> Tasks { get; set; } = null;
    public ICollection<Category> Categories { get; set; } = null;
    public ICollection<Status> Status { get; set; } = null;
}
