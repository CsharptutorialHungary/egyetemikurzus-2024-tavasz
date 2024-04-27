using CommunitySite.Data.Entity;

namespace CommunitySite.Data.ViewModels
{
    public class UserViewModel
    {
        public int Userid { get; set; }

        public Guid Usertechnicalname { get; set; }

        public int PermissionId { get; set; }

        public string? Username { get; set; }

        public string? ShortName { get; set; }

        public string? SurName { get; set; }

        public string? LastName { get; set; }

        public string? Workplace { get; set; }

        public string? School { get; set; }

        public DateTime? BirthDate { get; set; }

        public ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

        public ICollection<Message> MessageSenders { get; set; } = new List<Message>();

        public Permission? Permission { get; set; }

        public ICollection<Photo> Photos { get; set; } = new List<Photo>();

        public ICollection<Post> Posts { get; set; } = new List<Post>();

        public ICollection<Sitecomment> Sitecomments { get; set; } = new List<Sitecomment>();

        public ICollection<Sitegroup> Sitegroups { get; set; } = new List<Sitegroup>();
    }
}
