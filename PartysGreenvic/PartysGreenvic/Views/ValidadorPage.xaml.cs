




namespace PartysGreenvic.Views
{
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using PartysGreenvic.Helpers;
    using SQLite.Net;
    using System.IO;

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
                    #region ValidarRut

                    
                    if (string.IsNullOrEmpty(this.txtRut.Text))
                        return;
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
                #endregion
                //await DisplayAlert("", RutGlobal, "Aceptar");
                using (var conexion2 = new DataAccess())
                {

                    string bdpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "PartysGreenvic.db3");
                    var conexion = new DataAccess();
                    //var datos = conexion.GetEmpleado(RutGlobal);
                    var datoss = conexion.GetEmpleado(RutGlobal);
                    var nombre = datoss.Nombre;
                    //var invitado = datos.Rut.Where(x => x.Rut == RutGlobal);
                    if (datoss == null)
                    {
                        await DisplayAlert("Acceso Denegado", "Esta persona no tiene Invitación vigente", "Aceptar");
                        return;
                    }
                    await DisplayAlert("Acceso Permitido", nombre+" con acceso Autorizado", "Aceptar");
                    return;
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

        
    } 
}