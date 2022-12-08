using Listing.App.ViewModels;
using Listing.App.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Listing.App
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ListContentPage), typeof(ListContentPage));
            Routing.RegisterRoute(nameof(CreateListPage), typeof(CreateListPage));
            Routing.RegisterRoute(nameof(EditAccountPage), typeof(EditAccountPage));
            Routing.RegisterRoute(nameof(EditListPage), typeof(EditListPage));

        }

    }
}
