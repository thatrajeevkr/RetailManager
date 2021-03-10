using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using TRMDesktopUI.EventModels;
using TRMDesktopUI.Library.API;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _eventAggregator;
        private SalesViewModel _salesViewModel;
        private ILoggedInUserModel _user;
        private IAPIHelper _apiHelper;

        public ShellViewModel(IEventAggregator eventAggregator, SalesViewModel salesViewModel, ILoggedInUserModel user, IAPIHelper apiHelper)
        {
            _eventAggregator = eventAggregator;
            _salesViewModel = salesViewModel;
            _user = user;
            _eventAggregator.SubscribeOnPublishedThread(this);
            ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
            _apiHelper = apiHelper;
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

        public async Task UserManagement()
        {
            await ActivateItemAsync(IoC.Get<UserDisplayViewModel>(), new CancellationToken());
        }


        public void ExitApplication()
        {
            TryCloseAsync();
        }

        public async Task LogOut()
        {
            _user.ResetUserModel();
            _apiHelper.LogOfUser();
            await ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }


        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(_salesViewModel, cancellationToken);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
    }
}
