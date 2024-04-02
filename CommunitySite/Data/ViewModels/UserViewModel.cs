using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CommunitySite.Data.ViewModels
{
    public class UserViewModel
    {
        public int Userid { get; set; }

        public int? PermissionId { get; set; }

        public string? SurName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Passwords { get; set; }

        public string? Workplace { get; set; }

        public string? School { get; set; }

        public int? BirthYear { get; set; }

        public int? BirthMonth { get; set; }

        public int? BirthDay { get; set; }
    }
}
