using Microsoft.AspNetCore.Identity;

namespace Domain;

public class AppUser : IdentityUser
{
    public string DisplayName { get; set; } = string.Empty;

    public string Bio { get; set; } = string.Empty;

    public ICollection<ActivityAttendee>? Activities { get; set; }

    public ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public ICollection<UserFollowing> Followings { get; set; } = new List<UserFollowing>();

    public ICollection<UserFollowing> Followers { get; set; } = new List<UserFollowing>();
}