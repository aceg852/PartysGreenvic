using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PartysGreenvic
{
    using Helpers;
    using Xamarin.Forms;
    using Views;
    using Xamarin.Forms.Xaml;   
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EdithPage : ContentPage
	{
        private Empleado empleado;
        private EdithPage EditPage;
		public EdithPage (Empleado empleado)
		{
			InitializeComponent();
            this.empleado = empleado;
            btnGuardar.Clicked += BtnGuardar_Clicked;
            btnBorrar.Clicked += BtnBorrar_Clicked;

            this.txtNombre.Text = empleado.Nombre;
            this.txtRut.Text = empleado.Rut;
		}

        public async void BtnBorrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var rta = await DisplayAlert("Confirmación", "Desea borrar aquel invitado?", "Sí", "No");
                if (!rta) return;
                using (var datos = new DataAccess())
                {
                    datos.BorrarEmpleado(empleado);
                }
                await DisplayAlert("Eliminado", "Invitado borraro correctamente", "Aceptar");
                this.btnBorrar.IsEnabled = false;
                this.btnGuardar.IsEnabled = false;
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "Aceptar");
                return;
            }
        }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            try
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
                    IDEmpleado = this.empleado.IDEmpleado,
                    Rut = txtRut.Text,
                    Nombre = txtNombre.Text
                };

                using (var datos = new DataAccess())
                {
                    datos.ActualizarEmpleado(empleado);
                }
                await DisplayAlert("Actualizado", "Invitado actualizado correctamente", "Aceptar");
                this.btnBorrar.IsEnabled = false;
                this.btnGuardar.IsEnabled = false;
                await Navigation.PopAsync();
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "Aceptar");
                return;
            }
        }
    }
}