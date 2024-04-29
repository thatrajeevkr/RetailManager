using Caliburn.Micro;
using RMDesktopUI.Helpers;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    internal class LoginViewModel : Screen
    {
		private string _userName;
        private string _password;
        private string errorMessage;
        private IAPIHelper _apiHelper;

        public LoginViewModel(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public string UserName
		{
			get { return _userName; }
			set 
			{
				_userName = value;
				NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
		}

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool IsErrorVisible
        {
            get { return ErrorMessage?.Length > 0; }
        }


        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);
            }
        }

        public bool CanLogIn
        {
            get
            {
                bool output = false;
                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public async Task LogIn()
        {
            try
            {
                var result = await _apiHelper.Authenticate(UserName, Password);
                await _apiHelper.GetLoggedInUserInfo(result.Access_Token);
                ErrorMessage = "";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

    }
}
