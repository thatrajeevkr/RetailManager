using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    internal class ShellViewModel : Conductor<object>
    {
        private LoginViewModel _loginVM;
        public ShellViewModel(LoginViewModel loginVM) 
        { 
            //Instead of the shell, activate the login view model
            _loginVM = loginVM;
            ActivateItemAsync(_loginVM);
        }
    }
}
