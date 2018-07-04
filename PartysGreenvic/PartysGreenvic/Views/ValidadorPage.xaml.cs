using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PartysGreenvic.Views
{
    using Newtonsoft.Json;
    using PartysGreenvic.Helpers;
    using System.Data;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ValidadorPage : ContentPage
	{
        string RutGlobal;
		public ValidadorPage ()
		{
			InitializeComponent ();
            this.txtRut.MaxLength = 8;
            this.btnEmpleado.Clicked += BtnEmpleado_Clicked;
            this.btnCheck.Clicked += BtnCheck_Clicked;
		}


        private async void BtnCheck_Clicked(object sender, EventArgs e)
        {
            try
            {
                try
                {
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

                await DisplayAlert("", RutGlobal, "Aceptar");
                Label lbResults = new Label();
                
                //ListView lis = new ListView();
                //using (var datos = new DataAccess())
                //{
                //    DataTable tab = new DataTable();
                //    tab.ItemsSource = tab[0].datos.GetEmpleado(RutGlobal);
                //}

                using (var datos = new DataAccess())
                {
                    //datos.BuscarEmpleado(RutGlobal);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return;
            }
        }
        private async void BtnEmpleado_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NuevoEmpleado());
        }
        private void trasformar_rut()
        {

            string sTexto = this.txtRut.Text;
            string sRut;
            sTexto = sTexto.Replace("'", "-").ToString();
            sTexto = sTexto.Remove(sTexto.Length - 2);
            sRut = sTexto;
            sTexto = sTexto.Remove(sTexto.Length - 2);
            Convert.ToInt64(sTexto);
            if (Convert.ToInt64(sTexto) >= 10000000)
            {
                sRut = sRut.Substring(2, 10);
                txtRut.Text = sRut;
            }
            else
            {
                sRut = sRut.Substring(3, 9);
                txtRut.Text = sRut;
            }
        }

        private void txtRut_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}