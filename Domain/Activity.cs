
namespace Domain;

public class Activity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public string City { get; set; } = string.Empty;

    public string Venue { get; set; } = string.Empty;

    public bool IsCancelled { get; set; }

    public ICollection<ActivityAttendee> Attendees { get; set; } = new List<ActivityAttendee>();

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}