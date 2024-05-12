#nullable enable

using CommunitySite.Data.Entity;

namespace CommunitySite.Data.ViewModels
{
    public class GroupViewModel
    {
        public decimal Groupid { get; set; }

        public decimal? Ownerid { get; set; }

        public string? Name { get; set; }

        public Guid? GroupTechnicalName { get; set; }

        public Siteuser? Owner { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
