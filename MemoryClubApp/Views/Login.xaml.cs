using MemoryClubApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MemoryClubApp.Services;

namespace MemoryClubApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            //Preferences.Set("ParentUsername", "Valor que quiero poner");


            //Para recuperar
            //int parentId = Convert.ToInt32(Preferences.Get("ParentId", "0"));
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Password.Text))
                {
                    await DisplayAlert("Advertencia", "Ingrese la contraseña","Ok");
                    return;
                }
                if (string.IsNullOrEmpty(Usuario.Text))
                {
                    await DisplayAlert("Advertencia","Ingrese el usuario","Ok");
                    return;
                }
                UsuarioLoginModel usuario = new UsuarioLoginModel();

                usuario.usuario = Usuario.Text;
                usuario.password = Password.Text;


                LoginResponseModel responseModel = new LoginResponseModel();
                responseModel = await new LoginService().Login(usuario);

                if (responseModel != null)
                {
                    if (responseModel.Success)
                    {
                        usuario.IdUsuario = responseModel.IdUsuario;
                        await Navigation.PushAsync(new Menu(usuario, responseModel.Sucursal));
                    }
                    else
                    {
                        _ = DisplayAlert("Error",responseModel.MessageError, "OK");
                    }
                }
                else
                {
                    _ = DisplayAlert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo.", "OK");
                    _ = Navigation.PushAsync(new Login());
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Ha ocurrido un error inesperado. Inténtelo de nuevo."+ex.Message, "Ok");
            }
            
        }
    }
}