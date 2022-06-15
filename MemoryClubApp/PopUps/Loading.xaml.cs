using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MemoryClubApp.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Loading : Rg.Plugins.Popup.Pages.PopupPage
    {
        public Loading(bool value)
        {
            InitializeComponent();

            if (value)
            {
                stlCharge.BackgroundColor = Color.White;
                lblCharge.IsVisible = true;
            }

            this.CloseWhenBackgroundIsClicked = false;
        }
    }
}