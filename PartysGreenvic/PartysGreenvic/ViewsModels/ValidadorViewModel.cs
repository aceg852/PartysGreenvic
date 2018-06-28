using System;
using System.Collections.Generic;
using System.Text;
namespace PartysGreenvic.ViewsModels
{
    using GalaSoft.MvvmLight.Command;
    using PartysGreenvic.Views;
    using System.Windows.Input;
    using Xamarin.Forms;

    class ValidadorViewModel
    {
        #region Properties
        public string Rut
        {
            get;
            set;
        }
        public Boolean IsRunning
        {
            get;
            set;
        }
        #endregion
        #region Constructors
        public ValidadorViewModel()
        {
           // agregar funciones por defecto cuando inicie la aplicacion
        }
        #endregion
        #region Commands
        public ICommand CheckCommand
        {
            get
            {
                return new RelayCommand(Check);
            }
        }

        private async void Check()
        {
            if (string.IsNullOrEmpty(Rut))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Ingrese Rut",
                    "Aceptar");
                return;
            }

        }

        public ICommand NombreCommand
        {
            get
            {
                return new RelayCommand(NombreC);
            }
            set { }
        }
        private void NombreC()
        {
        }
        #endregion
    }
}
