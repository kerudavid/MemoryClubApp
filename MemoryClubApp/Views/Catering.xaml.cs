using MemoryClubApp.Models;
using MemoryClubApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MemoryClubApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Catering : ContentPage
    {
        public static ColaboradorResponseModel colaboradorResponse = new ColaboradorResponseModel();
        public static List<ColaboradorModel> colaboradorListModel = new List<ColaboradorModel>();
        public Catering()
        {
            InitializeComponent();
            GetColaboradores();
        }
        public async void GetColaboradores()
        {
            try
            {
                colaboradorResponse = await new ColaboradorService().GetColaborador();
                if (colaboradorResponse.Success)
                {
                    if (colaboradorResponse.ListaColab.Count > 0)
                    {
                        colaboradorListModel = colaboradorResponse.ListaColab;
                    }
                }
                else
                {
                    await DisplayAlert("Advertencia", "Ha ocurrido un error " + colaboradorResponse.MessageError, "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Advertencia", "Ha ocurrido un error al obtener la lista de colaboradores&#10;Intentelo más tarde.&#10" + ex.Message, "Ok");
            }
        }

        private void LunchButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Lunch(colaboradorListModel));
        }

        private void AlmuerzoButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Almuerzo(colaboradorListModel));
        }
    }
}