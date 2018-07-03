using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PartysGreenvic.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ValidadorPage : ContentPage
	{
		public ValidadorPage ()
		{
			InitializeComponent ();
            btnEmpleado.Clicked += BtnEmpleado_Clicked;
		}

        private async void BtnEmpleado_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NuevoEmpleado());
        }
    }
}