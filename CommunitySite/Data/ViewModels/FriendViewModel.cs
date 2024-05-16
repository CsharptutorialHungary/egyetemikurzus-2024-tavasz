#nullable enable

using CommunitySite.Data.Entity;

namespace CommunitySite.Data.ViewModels
{
    public class FriendViewModel
    {
        public decimal? Friendid1 { get; set; }

        public decimal? Friendid2 { get; set; }

        public string? FriendStartDate { get; set; }

        public decimal? IsFriend { get; set; }

        public decimal Friendrowid { get; set; }

        public Siteuser? Friendid1Navigation { get; set; }

        public Siteuser? Friendid2Navigation { get; set; }
    }
}
