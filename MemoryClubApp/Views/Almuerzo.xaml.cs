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
    public partial class Almuerzo : ContentPage
    {
        public static Xamarin.Forms.Color exitosoColor = Xamarin.Forms.Color.FromRgb(71, 220, 1);
        public static Xamarin.Forms.Color errorColor = Xamarin.Forms.Color.FromRgb(183, 0, 0);
        public static Xamarin.Forms.Color advertenciaColor = Xamarin.Forms.Color.FromRgb(255, 135, 1);
        public static System.Drawing.Color sdColor = System.Drawing.Color.FromArgb(38, 127, 0);
        
        public static ClienteResponseModel clienteResponseModel = new ClienteResponseModel();
        public static List<ColaboradorModel> colabList = new List<ColaboradorModel>();
        public static List<ClienteModel> clienteModel = new List<ClienteModel>();
        
        public static int numVal = 0;
        public Almuerzo(List<ColaboradorModel> colaboradorListModel)
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
                scanner.TopText = "Almuerzo";

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
                        lblEstado.Text = "Aviso, código incorrecto";

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
                    CateringModel almuerzoModel = new CateringModel();

                    almuerzoModel.CodigoPersona = clienteModel.Where(x => x.CI == numVal.ToString()).Select(x => x.IdCliente).FirstOrDefault();// Where(x=> x.IdCliente==numVal);
                    almuerzoModel.TipoPersona = tipoPersona.GetCliente();

                    if (almuerzoModel.CodigoPersona <= 0)
                    {
                        almuerzoModel.CodigoPersona = colabList.Where(p => p.CIColaborador == numVal.ToString()).Select(p => p.IdColaborador).FirstOrDefault();
                        almuerzoModel.TipoPersona = tipoPersona.GetColaborador();

                        if (almuerzoModel.CodigoPersona <= 0)
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

                    }

                    //Asisgna nos valores del objeto asistecia
                    almuerzoModel.Usuario = Convert.ToInt32(Preferences.Get("IdUserName", "0"));
                    almuerzoModel.NombreUsuario = Preferences.Get("UserName", "0");
                    almuerzoModel.Sucursal = Convert.ToInt32(Preferences.Get("UserSucursal", "0"));
                    almuerzoModel.TipoMenu = "Almuerzo";

                    string entrada = DateTime.Now.ToString("HH:mm");

                    almuerzoModel.Hora = DateTime.ParseExact(entrada, "HH:mm", null);
                    string fecha = DateTime.Now.ToString("MM-dd-yyyy");
                    almuerzoModel.FechaMod = DateTime.Now.Date;
                    almuerzoModel.Fecha = DateTime.ParseExact(fecha, "MM-dd-yyyy", null);


                    CateringResponseModel cateringResponseModel = new CateringResponseModel();
                    cateringResponseModel = await new AlmuerzoService().Almuerzo(almuerzoModel);
                    
                    if (cateringResponseModel.Success)
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
                        lblEstado.Text = cateringResponseModel.MessageError;
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