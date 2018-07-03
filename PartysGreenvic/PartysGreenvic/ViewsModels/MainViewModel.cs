namespace PartysGreenvic.ViewsModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Models;
    using Helpers;
    class MainViewModel
    {
        #region Propierties
        public TokenResponse Token
        {
            get;
            set;
        }
        public UserLocal User
        {
            get;
            set;
        }
        #endregion

        #region ViewModels

        public ValidadorViewModel Validador
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            this.Validador = new ValidadorViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
