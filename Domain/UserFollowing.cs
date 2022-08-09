namespace Domain;

public class UserFollowing
{
    public string ObserverId { get; set; } = string.Empty;

    public AppUser Observer { get; set; } = new();

    public string TargetId { get; set; } = string.Empty;       

    public AppUser Target { get; set; } = new();
}
