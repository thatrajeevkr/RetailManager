using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TRMDesktopUI.EventModels;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _eventAggregator;
        private SalesViewModel _salesViewModel;
        private ILoggedInUserModel _user;

        public ShellViewModel(IEventAggregator eventAggregator, SalesViewModel salesViewModel, ILoggedInUserModel user)
        {
            _eventAggregator = eventAggregator;
            _salesViewModel = salesViewModel;
            _user = user;
            _eventAggregator.Subscribe(this);
            ActivateItem(IoC.Get<LoginViewModel>());
        }
        public bool IsLoggedIn
        {
            get {
                bool output = false;

                if(String.IsNullOrEmpty(_user.Token) == false)
                {
                    output = true;
                }
                return output;
            }
            
        }


        public void ExitApplication()
        {
            TryClose();
        }

        public void LogOut()
        {
            _user.LogOfUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesViewModel);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
    }
}
