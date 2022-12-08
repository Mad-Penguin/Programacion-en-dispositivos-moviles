using Listing.App.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Listing.App.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}