using System;
using WpfApp1.Stores;
using WpfApp1.ViewModels;

namespace WpfApp1.Commands
{
    internal class NavigateCommand : CommandBase
    {

        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createVieModel;

        public NavigateCommand(NavigationStore navigationStore, Func<ViewModelBase> createVieModel)
        {
            _navigationStore = navigationStore;
            _createVieModel = createVieModel;
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = _createVieModel();                
        }
    }
}
