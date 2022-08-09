
namespace Domain;

public class Comment
{ 
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Body { get; set; } = string.Empty;
    public AppUser Author { get; set; } = new();
    public Activity Activity { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
