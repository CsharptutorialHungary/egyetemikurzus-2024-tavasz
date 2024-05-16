#nullable enable

using CommunitySite.Data.Entity;

namespace CommunitySite.Data.ViewModels
{
    public class PostViewModel
    {
        public decimal Postid { get; set; }

        public decimal? Userid { get; set; }

        public decimal? Photoid { get; set; }

        public decimal? Groupid { get; set; }

        public string? PostText { get; set; }

        public string? PostDate { get; set; }

        public Sitegroup? Group { get; set; }

        public Photo? Photo { get; set; }

        public ICollection<Sitecomment> Sitecomments { get; set; } = new List<Sitecomment>();

        public Siteuser? User { get; set; }
    }
}
