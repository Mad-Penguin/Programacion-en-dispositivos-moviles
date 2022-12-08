using Listing.App.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Listing.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrendingPage : ContentPage
    {
        public ObservableCollection<ListPartialDisplay> Items { get; set; }

        public TrendingPage()
        {
            InitializeComponent();

            var a = new ListPartialDisplay();
            a.Title = "Title 1";
            a.OwnerUsername = "owner 1";
            Items = new ObservableCollection<ListPartialDisplay>();
            Items.Add(a);

            /*Items = new ObservableCollection<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };*/

            TrendingList.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
