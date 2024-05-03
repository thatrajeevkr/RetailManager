using Caliburn.Micro;
using RMDesktopUI.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    internal class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _events;
        private SalesViewModel _salesVM;
        private SimpleContainer _simpleContainer;
        public ShellViewModel(IEventAggregator events, SalesViewModel salesVM, SimpleContainer simpleContainer) 
        { 
            _events = events;
            _events.Subscribe(this);
            _salesVM = salesVM;
            _simpleContainer = simpleContainer;
            //Instead of the shell, activate the login view model
            ActivateItemAsync(_simpleContainer.GetInstance<LoginViewModel>());
        }


        Task IHandle<LogOnEvent>.HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            ActivateItemAsync(_salesVM);
            return Task.CompletedTask;
        }
    }
}
