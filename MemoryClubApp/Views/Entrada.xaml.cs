using MemoryClubApp.Models;
using MemoryClubApp.Services;
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
    public partial class Entrada : ContentPage
    {
        public static Xamarin.Forms.Color exitosoColor = Xamarin.Forms.Color.FromRgb(71, 220, 1);
        public static Xamarin.Forms.Color errorColor = Xamarin.Forms.Color.FromRgb(183, 0, 0);
        public static Xamarin.Forms.Color advertenciaColor = Xamarin.Forms.Color.FromRgb(255, 135, 1);
        public static System.Drawing.Color sdColor = System.Drawing.Color.FromArgb(38, 127, 0);
        
        public static ClienteResponseModel clienteResponseModel = new ClienteResponseModel();
        public static List<ColaboradorModel> colabList = new List<ColaboradorModel>();
        public static List<ClienteModel> clienteModel = new List<ClienteModel>();
        
        public static int numVal = 0;
        public Entrada(List<ColaboradorModel> colaboradorListModel)
        {
            InitializeComponent();
            colabList = colaboradorListModel;
            GetClientes();
        }
        public async void GetClientes()
        {
            try
            {
                clienteResponseModel = await new ClienteService().Cliente();
                if (clienteResponseModel.success)
                {
                    if (clienteResponseModel.ListaCliente.Count > 0)
                    {
                        clienteModel = clienteResponseModel.ListaCliente;
                    }
                }
                else
                {
                    await DisplayAlert("Advertencia", "Ha ocurrido un error " + clienteResponseModel.MessageError, "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Advertencia", "Ha ocurrido un error al obtener la lista de clientes &#10;Intentelo más tarde.&#10" + ex.Message, "Ok");
            }
        }

        private async void PruebaButton_Clicked(object sender, EventArgs e)
        {

            imgInfo.IsVisible = false;
            lblEstado.IsVisible = false;

            try
            {
                //var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                scanner.TopText = "Entrada";

                //scanner.BottomText = "Holii";
                var result = await scanner.Scan();

                if (result != null)
                {
                    //Trata de convertir el resultado Text a Entero
                    try
                    {
                        numVal = Convert.ToInt32(result.Text);
                    }
                    catch
                    {
                        lblEstado.IsVisible = true;
                        lblEstado.Text = "Error, código incorrecto";

                        System.Drawing.Color adverC = advertenciaColor;

                        lblEstado.BackgroundColor = adverC;

                        imgInfo.IsVisible = true;
                        imgInfo.Source = "Alerta.png";
                        imgInfo.TintColor = adverC;

                        qrBtn.IsEnabled = true;

                        return;
                    }

                    //Si pudo transformar el texto a numerico valida si existe dentro del codigo de clientes
                    Constantes tipoPersona = new Constantes();
                    TransporteModel transporteModel = new TransporteModel();

                    transporteModel.CodigoPersona = clienteModel.Where(x => x.CI == numVal.ToString()).Select(x => x.IdCliente).FirstOrDefault();// Where(x=> x.IdCliente==numVal);
                    transporteModel.TipoPersona = tipoPersona.GetCliente();
                    
                    if (transporteModel.CodigoPersona <= 0)
                    {                   
                        lblEstado.IsVisible = true;
                        lblEstado.Text = "Código Incorrecto";

                        System.Drawing.Color adverC = advertenciaColor;

                        lblEstado.BackgroundColor = adverC;

                        imgInfo.IsVisible = true;
                        imgInfo.Source = "Alerta.png";
                        imgInfo.TintColor = adverC;

                        qrBtn.IsEnabled = true;

                        return;                     
                    }

                    transporteModel.IdTransportista = clienteModel.Where(x => x.CI == numVal.ToString()).Select(x => x.IdTransportista).FirstOrDefault();

                    //Asisgna nos valores del objeto asistecia
                    transporteModel.Usuario = Convert.ToInt32(Preferences.Get("IdUserName", "0"));
                    transporteModel.NombreUsuario = Preferences.Get("UserName", "0");
                    transporteModel.Sucursal = Convert.ToInt32(Preferences.Get("UserSucursal", "0"));
                    transporteModel.EntradaSalida = "ENTRADA";



                    string entrada = DateTime.Now.ToString("HH:mm");

                    transporteModel.Hora = DateTime.ParseExact(entrada, "HH:mm", null);
                    string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                    transporteModel.FechaMod = DateTime.Now.Date;
                    transporteModel.Fecha = DateTime.ParseExact(fecha, "dd/MM/yyyy", null);


                    TransporteResponseModel transporteResponseModel = new TransporteResponseModel();
                    transporteResponseModel = await new TransporteService().Entrada(transporteModel);
                    
                    if (transporteResponseModel.Success)
                    {
                        // Implicitly convert from a System.Drawing.Color to a Xamarin.Forms.Color
                        Xamarin.Forms.Color xfColor2 = sdColor;
                        // Implicity convert from a Xamarin.Forms.Color to a System.Drawing.Color
                        System.Drawing.Color exitosoC = exitosoColor;

                        lblEstado.IsVisible = true;
                        lblEstado.Text = "Registro Exitoso!";
                        lblEstado.BackgroundColor = exitosoC;

                        imgInfo.IsVisible = true;
                        imgInfo.Source = "Aprobado.png";
                        imgInfo.TintColor = exitosoC;

                        qrBtn.IsEnabled = true;
                    }
                    else
                    {
                        lblEstado.IsVisible = true;
                        lblEstado.Text = transporteResponseModel.MessageError;

                        System.Drawing.Color errorC = errorColor;

                        lblEstado.BackgroundColor = errorC;

                        imgInfo.IsVisible = true;
                        imgInfo.Source = "Rechazado.png";
                        imgInfo.TintColor = errorC;

                        qrBtn.IsEnabled = true;
                    }

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Advertencia", "Ha ocurrido un error &#10;" + ex.Message, "Ok");
                qrBtn.IsEnabled = true;
            }
        }
    }
}