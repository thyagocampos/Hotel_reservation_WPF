using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Stores;
using WpfApp1.ViewModels;

namespace WpfApp1.Services
{
    public class NavigationService
    {

        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createVieModel;

        public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createVieModel)
        {
            _navigationStore = navigationStore;
            _createVieModel = createVieModel;
        }
        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createVieModel();
        }

    }
}
