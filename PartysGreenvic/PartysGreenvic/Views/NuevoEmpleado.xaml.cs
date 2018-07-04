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
            this.txtRut.MaxLength = 8;
            this.txtNombre.MaxLength = 40;
            btnAgregar.Clicked += BtnAgregar_Clicked;
            ListDatos.ItemSelected += ListDatos_ItemSelected;
            using (var datos = new DataAccess())
            {
                ListDatos.ItemsSource = datos.GetEmpleados();
            }
        }

        private void ListDatos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new EdithPage((Empleado)e.SelectedItem));   
        }

        private async void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                await DisplayAlert("Error", "Debe Ingresar Nombre", "Aceptar");
                this.txtNombre.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtRut.Text))
            {
                await DisplayAlert("Error", "Debe Ingresar Rut", "Aceptar");
                this.txtRut.Focus();
                return;
            }
            Empleado empleado = new Empleado
            {
                Rut = txtRut.Text,
                Nombre = txtNombre.Text
            };
            using (var datos = new DataAccess())
            {
                datos.InsertarEmpleado(empleado);
                ListDatos.ItemsSource = datos.GetEmpleados();
            }
        }
    }
}