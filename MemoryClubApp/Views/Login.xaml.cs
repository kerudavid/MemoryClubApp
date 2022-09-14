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
using MemoryClubApp.Interfaces;
using MemoryClubApp.PopUps;
using Rg.Plugins.Popup.Services;

namespace MemoryClubApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();    
        }

        public async void SinInternet()
        {
            try
            {
                /*await PopupNavigation.Instance.PushAsync(new Alert("Alerta", "El dispositivo no tiene conexión a Internet este momento. \nAlgunas funcionalidades no estaran disponibles"));
                await Navigation.PopToRootAsync();*/
                await PopupNavigation.Instance.PopAsync();
                await PopupNavigation.Instance.PushAsync(new Alert("Aviso", "Su equipo no cuenta con Internet en este momento"));
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.PopAsync();
                await PopupNavigation.Instance.PushAsync(new Alert("Aviso", "Cominíquese con soporte. " + ex.Message));

            }
        }


        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new Loading(false));

            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                int signaLevel = DependencyService.Get<IWifiSignal>().GetStrenght();

                if (signaLevel <= 2.5 && signaLevel!=2022)
                {
                    await PopupNavigation.Instance.PopAsync();
                    await new Alert("Aviso", "La velocidad de conexión es muy baja " ).Wait();
                    await PopupNavigation.Instance.PushAsync(new Loading(false));
                }

                try
                {
                    if (string.IsNullOrEmpty(Password.Text) && string.IsNullOrEmpty(Usuario.Text))
                    {

                        await PopupNavigation.Instance.PopAsync();
                        await PopupNavigation.Instance.PushAsync(new Alert("Aviso", "Ingrese todos los campos"));

                        return;
                    }
                    if (string.IsNullOrEmpty(Password.Text))
                    {
                        await PopupNavigation.Instance.PopAsync();
                        await PopupNavigation.Instance.PushAsync(new Alert("Aviso", "Ingrese la contraseña"));
                       
                        return;
                    }
                    if (string.IsNullOrEmpty(Usuario.Text))
                    {
                        await PopupNavigation.Instance.PopAsync();
                        await PopupNavigation.Instance.PushAsync(new Alert("Aviso", "Ingrese el usuario"));
                        
                        return;
                    }                    

                    btnIngresar.IsEnabled = false;

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
                            await PopupNavigation.Instance.PopAsync();
                            await Navigation.PushAsync(new Menu(usuario, responseModel.Sucursal));
                        }
                        else
                        {
                            await PopupNavigation.Instance.PopAsync();
                            await PopupNavigation.Instance.PushAsync(new Alert("Aviso", responseModel.MessageError));
                            
                        }
                    }
                    else
                    {
                        await PopupNavigation.Instance.PopAsync();
                        await PopupNavigation.Instance.PushAsync(new Alert("Error", "Ocurrió un error inesperado. Inténtelo de nuevo."));
                       
                        _ = Navigation.PushAsync(new Login());
                    }
                    btnIngresar.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    btnIngresar.IsEnabled = true;
                    await PopupNavigation.Instance.PopAsync();
                    await PopupNavigation.Instance.PushAsync(new Alert("Error", "Ha ocurrido un error inesperado. Inténtelo de nuevo. " + ex.Message));
                    
                }
            }
            else
            {
                //var mainPage = Application.Current.MainPage;
                SinInternet();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            return true;
        }
    }
}