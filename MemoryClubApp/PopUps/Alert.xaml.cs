using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;


namespace MemoryClubApp.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Alert : Rg.Plugins.Popup.Pages.PopupPage
    {
        private TaskCompletionSource<bool> _tcs = null;

        /// <summary>
        /// Alerta con solo titulo y descripción
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public Alert(string title, string description)
        {
            InitializeComponent();

            info.Text = title;
            info2.Text = description;
            info2.IsVisible = true;
            CloseWhenBackgroundIsClicked = false;
        }

        /// <summary>
        /// Alerta con titulo, descripción y nombre del icono
        /// </summary>
        /// <param name="title">El titulo de la alerta.</param>
        /// <param name="description">El cuerpo de la alerta (Opcional).</param>
        /// <param name="icon">Nombre del icono de la alerta.</param>
        public Alert(string title, string description, string icon)
        {
            InitializeComponent();

            info.Text = title;
            if (!string.IsNullOrEmpty(description))
            {
                info2.Text = description;
                info2.IsVisible = true;
            }
            CloseWhenBackgroundIsClicked = false;
            image.Source = icon;
        }

        /// <summary>
        /// Alerta solo con titulo
        /// </summary>
        /// <param name="title"></param>
        public Alert(string title)
        {
            InitializeComponent();
            info.Text = title;
            CloseWhenBackgroundIsClicked = false;
        }

        private async void Close_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            _tcs?.SetResult(true);
        }

        protected override bool OnBackButtonPressed()
        {
            _tcs?.SetResult(true);
            return base.OnBackButtonPressed();
        }

        public async Task<bool> Wait()
        {
            _tcs = new TaskCompletionSource<bool>();
            await Navigation.PushPopupAsync(this);

            return await _tcs.Task;
        }
    }
}