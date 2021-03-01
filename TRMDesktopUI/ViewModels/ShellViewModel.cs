using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TRMDesktopUI.EventModels;

namespace TRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _eventAggregator;
        private SalesViewModel _salesViewModel;

        public ShellViewModel(IEventAggregator eventAggregator, SalesViewModel salesViewModel)
        {
            _eventAggregator = eventAggregator;
            _salesViewModel = salesViewModel;

            _eventAggregator.Subscribe(this);
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesViewModel);
        }
    }
}
