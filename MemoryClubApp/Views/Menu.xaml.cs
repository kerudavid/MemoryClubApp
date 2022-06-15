using MemoryClubApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MemoryClubApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        // *esto evita que se genere un nullReference exception
        public static UsuarioLoginModel userInfo = new UsuarioLoginModel();
        public Menu(UsuarioLoginModel usuario, int? sucursal)
        {
            // *esto evita que se genere un nullReference exception
            userInfo = usuario;
            Preferences.Set("UserName", userInfo.usuario);
            Preferences.Set("IdUserName", userInfo.IdUsuario.ToString());
            Preferences.Set("UserSucursal", sucursal.ToString());
            
            //Para recuperar
            //int parentId = Convert.ToInt32(Preferences.Get("ParentId", "0"));

            /*Importante asignar valores a los elemntos de la pantalla despues de InitializeComponent()*/
            InitializeComponent();
            lblUsuario.Text = userInfo.usuario;
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }

        private void AsistenciaButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Asistencia());   
        }

        private void CateringButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Catering());
        }

        private void TransporteButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Transporte());
        }
    }
}