using CommunitySite.Data.ViewModels;
using CommunitySite.Services.AdminViewServices;
using CommunitySite.Services.UserServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace CommunitySite.Components.Pages
{
    public partial class UserAdminPage
    {
        [Inject] private IAdminViewService AdminViewService { get; set; } = default!;
        [Inject] private IUserService UserService { get; set; } = default!;

        private List<UserViewModel> Users = new List<UserViewModel>();
        private UserViewModel selectedUser = default!;
        private string searchString = "";

        protected override async Task OnInitializedAsync()
        {
            await ListUsers();
        }

        private async Task ListUsers()
        {
            Users = await UserService.GetUsers();
        }

        private async Task EditSelectedUserDetails(TableRowClickEventArgs<UserViewModel> tableRowClickEventArgs)
        {
            await AdminViewService.CreateAdminDialog(selectedUser);
            await ListUsers();
        }
    }
}
