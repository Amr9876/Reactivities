
namespace Domain;

public class ActivityAttendee
{
    public string AppUserId { get; set; } = string.Empty; 
    public AppUser AppUser { get; set; } = new();

    public Guid ActivityId { get; set; }
    public Activity Activity { get; set; } = new();

    public bool IsHost { get; set; }

}