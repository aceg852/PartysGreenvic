using System;
using System.Collections.Generic;
using System.Text;

namespace PartysGreenvic.ViewsModels
{
    class MainViewModel
    {
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
    }
}
