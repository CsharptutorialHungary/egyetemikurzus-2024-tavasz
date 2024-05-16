using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CommunitySite.Components.Pages
{
    public partial class Error
    {
        [Inject] private ISnackbar Snackbar { get; set; } = default!;
        [Parameter] public RenderFragment ChildContent { get; set; } = default!;

        public void ErrorHandler(Exception exception)
        {
            Snackbar.Add(exception.Message, Severity.Error);
        }
    }
}
