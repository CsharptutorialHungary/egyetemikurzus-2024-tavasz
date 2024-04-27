using Microsoft.AspNetCore.Components;

namespace CommunitySite.Components.Pages
{
    public partial class Profile
    {
        [Parameter] public Guid UserTechnicalId { get; set; } = default!;
    }
}
