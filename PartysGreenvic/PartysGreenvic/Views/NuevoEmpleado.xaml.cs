using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PartysGreenvic.Views
{
    using Helpers;
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NuevoEmpleado : ContentPage
	{
		public NuevoEmpleado ()
		{
			InitializeComponent ();
            btnAgregar.Clicked += BtnAgregar_Clicked;
            using (var datos = new DataAccess())
            {
                ListDatos.ItemsSource = datos.GetEmpleado();
            }
		}

        private async void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtRut.Text))
            {
                await DisplayAlert("Error", "Datos Vacíos", "Aceptar");
                return;
            }
        }
    }
}