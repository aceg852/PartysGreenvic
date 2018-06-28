using System;
using System.Collections.Generic;
using System.Text;
namespace PartysGreenvic.ViewsModels
{
    using GalaSoft.MvvmLight.Command;
    using PartysGreenvic.Views;
    using System.Windows.Input;
    using Xamarin.Forms;
    using System.ComponentModel;
    class ValidadorViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #region Attributes
        private string rut;
        private string nombre;
        private bool isRunning;
        private bool isEnabled;
        #endregion
        #region Properties
        public string Rut
        {
            get
            {
                return this.rut;
            }
            set
            {
                if (this.rut != value)
                {
                    this.rut = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Rut)));
                }
            }
        }
        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
            set
            {
                if (this.isRunning != value)
                {
                    this.isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.IsRunning)));
                }
            }
        }
        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                if (this.isEnabled != value)
                {
                    this.isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.IsEnabled)));
                }
            }
        }
        #endregion
        #region Constructors
        public ValidadorViewModel()
        {
            // agregar funciones por defecto cuando inicie la aplicacion
            this.isEnabled = true;
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
            this.isRunning = true;
            this.isEnabled = false;
            await Application.Current.MainPage.DisplayAlert(
                "Invitación",
                "Persona Autorizada",
                "Aceptar");
            this.isRunning = false;
            this.isEnabled = true;
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
