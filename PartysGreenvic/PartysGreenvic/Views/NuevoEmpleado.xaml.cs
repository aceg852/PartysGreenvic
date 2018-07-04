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
        string RutGlobal;
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
        private async void ListDatos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new EdithPage((Empleado)e.SelectedItem));
            using (var datos = new DataAccess())
            {
                ListDatos.ItemsSource = datos.GetEmpleados();
            }
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
            try
            {
                if (this.txtRut.MaxLength < 7)
                    return;
                //Digito Verificador
                RutGlobal = this.txtRut.Text;
                int suma = 0;
                for (int x = RutGlobal.Length - 1; x >= 0; x--)
                    suma += int.Parse(char.IsDigit(RutGlobal[x]) ? RutGlobal[x].ToString() : "0") * (((RutGlobal.Length - (x + 1)) % 6) + 2);
                int numericDigito = (11 - suma % 11);
                string digito = numericDigito == 11 ? "0" : numericDigito == 10 ? "K" : numericDigito.ToString();
                RutGlobal = RutGlobal + digito;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "Aceptar");
                this.txtRut.Text = string.Empty;
                return;
            }
            //Existe Rut?
            int rutAux = int.Parse(RutGlobal.Substring(0, RutGlobal.Length - 1));

            char dv = char.Parse(RutGlobal.Substring(RutGlobal.Length - 1, 1));

            int m = 0, s = 1;
            for (; rutAux != 0; rutAux /= 10)
            {
                s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
            }
            if (dv == (char)(s != 0 ? s + 47 : 75))
            {
                //Armar Rut con puntos y guion
                string rut = RutGlobal;
                int cont = 0;
                if (rut.Length == 0)
                {
                    await DisplayAlert("Error", "Rut Incorrecto", "Aceptar");
                    this.txtRut.Text = string.Empty;
                    return;
                }
                else
                {
                    rut = rut.Replace(".", "");
                    rut = rut.Replace("-", "");
                    RutGlobal = "-" + rut.Substring(rut.Length - 1);
                    for (int i = rut.Length - 2; i >= 0; i--)
                    {
                        RutGlobal = rut.Substring(i, 1) + RutGlobal;
                        cont++;
                        if (cont == 3 && i != 0)
                        {
                            RutGlobal = "." + RutGlobal;
                            cont = 0;
                        }
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", "Rut Incorrecto", "Aceptar");
                RutGlobal = string.Empty;
                this.txtRut.Text = string.Empty;
                return;
            }
            Empleado empleado = new Empleado
            {
               
                Rut = RutGlobal,
                Nombre = this.txtNombre.Text
            };
            using (var datos = new DataAccess())
            {
                datos.InsertarEmpleado(empleado);
                ListDatos.ItemsSource = datos.GetEmpleados();
            }
        }
    }
}