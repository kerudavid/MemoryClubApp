using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MemoryClubApp.Models;
using MemoryClubApp.Services;
using System.Drawing; 
using ZXing;
using Xamarin.Essentials;

namespace MemoryClubApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Asistencia : ContentPage
    {
        public static Xamarin.Forms.Color exitosoColor = Xamarin.Forms.Color.FromRgb(71, 220, 1);
        public static Xamarin.Forms.Color errorColor = Xamarin.Forms.Color.FromRgb(183, 0, 0);
        public static Xamarin.Forms.Color advertenciaColor = Xamarin.Forms.Color.FromRgb(255, 135, 1);
        public static System.Drawing.Color sdColor = System.Drawing.Color.FromArgb(38, 127, 0);
        public static ClienteResponseModel clienteResponseModel = new ClienteResponseModel();
        public static List<ClienteModel> clienteModel = new List<ClienteModel>();
        public static int numVal = 0;
        public Asistencia()
        {
            InitializeComponent();
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
                    await DisplayAlert("Advertencia","Ha ocurrido un error "+ clienteResponseModel.MessageError, "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Advertencia", "Ha ocurrido un error al obtener la lista de clientes &#10;Intentelo más tarde.&#10" + ex.Message,"Ok");
            }
        }

        private async void PruebaButton_Clicked(object sender, EventArgs e)
        {
            imgInfo.IsVisible=false;
            lblEstado.IsVisible = false;
            try
            {
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();          
                scanner.TopText = "Asistencia";
                
                //scanner.BottomText = "Holii";
                var result = await scanner.Scan();
                if (result!=null)
                {
                    qrBtn.IsEnabled=false;
                    
                    //Trata de convertir el resultado Text a Entero
                    try
                    {
                        numVal = Convert.ToInt32(result.Text);
                    }
                    catch
                    {
                        lblEstado.IsVisible = true;
                        lblEstado.Text = "Aviso, código incorrecto";
                        System.Drawing.Color adverC = advertenciaColor;

                        imgInfo.IsVisible=true;
                        imgInfo.Source = "Alerta.png";
                        imgInfo.TintColor = adverC;

                        qrBtn.IsEnabled = true;

                        lblEstado.BackgroundColor = adverC;
                     
                        return;
                    }
                    //Si pudo transformar el texto a numerico valida si existe dentro del codigo de clientes

                    AsistenciaModel asistencia = new AsistenciaModel();
                    asistencia.CodigoCliente = clienteModel.Where(x => x.CI.Equals(numVal.ToString())).Select(x => x.IdCliente).FirstOrDefault();// Where(x=> x.IdCliente==numVal);
                    
                    if (asistencia.CodigoCliente <= 0)
                    {
                        lblEstado.IsVisible = true;

                        lblEstado.Text = "Código Incorrecto";

                        System.Drawing.Color adverC = advertenciaColor;

                        imgInfo.IsVisible = true;
                        imgInfo.Source = "Alerta.png";
                        imgInfo.TintColor = adverC;

                        lblEstado.BackgroundColor= adverC;

                        qrBtn.IsEnabled = true;

                        return;
                    }

                    //Asisgna nos valores del objeto asistecia
                    asistencia.Usuario = Convert.ToInt32(Preferences.Get("IdUserName","0"));
                    asistencia.NombreUsuario = Preferences.Get("UserName", "0");
                    asistencia.Sucursal = Convert.ToInt32(Preferences.Get("UserSucursal", "0"));

                    string entrada = DateTime.Now.ToString("HH:mm");

                    asistencia.HoraEntrada = DateTime.ParseExact(entrada,"HH:mm",null);
                    string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                    asistencia.FechaMod = DateTime.Now.Date;
                    asistencia.Fecha = DateTime.ParseExact(fecha, "dd/MM/yyyy", null);


                    AsistenciaResponseModel asistenciaResponseModel = new AsistenciaResponseModel();
                    asistenciaResponseModel = await new AsistenciaService().Asistencia(asistencia);
                    if (asistenciaResponseModel.Success)
                    {                       
                        // Implicitly convert from a System.Drawing.Color to a Xamarin.Forms.Color
                        Xamarin.Forms.Color xfColor2 = sdColor;
                        // Implicity convert from a Xamarin.Forms.Color to a System.Drawing.Color
                        System.Drawing.Color exitosoC = exitosoColor;

                        lblEstado.IsVisible = true;

                        lblEstado.Text = "Registro Exitoso!";
                        lblEstado.BackgroundColor= exitosoC;

                        imgInfo.IsVisible = true;
                        imgInfo.Source = "Aprobado.png";
                        imgInfo.TintColor = exitosoC;

                        qrBtn.IsEnabled=true;
                    }
                    else
                    {
                        lblEstado.IsVisible = true;

                        lblEstado.Text = asistenciaResponseModel.MessageError;
                        
                        System.Drawing.Color errorC = errorColor;
                        lblEstado.BackgroundColor = errorC;

                        imgInfo.IsVisible=true;
                        imgInfo.Source = "Rechazado.png";
                        imgInfo.TintColor = errorC;

                        qrBtn.IsEnabled = true;

                    }

                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Advertencia", "Ha ocurrido un error &#10;"+ex.Message, "Ok");

                qrBtn.IsEnabled = true;
            }
        }
    }
}