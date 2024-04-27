using CommunitySite.Data.Entity;
using CommunitySite.Data.Context;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using System.Data.Common;

namespace CommunitySite.Components.Pages
{
    public partial class DatabaseConnectionPage
    {
        [Inject] IDbContextFactory<CommunitySiteContext> dbContextFactory { get; set; }

        private bool isConnected;

        protected override async Task OnInitializedAsync()
        {
            using (var dbcx = await dbContextFactory.CreateDbContextAsync())
            {
                isConnected = await dbcx.Database.CanConnectAsync();
            }
        }
    }
}
