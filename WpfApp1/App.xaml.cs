using Microsoft.EntityFrameworkCore;
using System.Windows;
using WpfApp1.DbContexts;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Services.ReservationConflictValidation;
using WpfApp1.Services.ReservationCreator;
using WpfApp1.Services.ReservationProvider;
using WpfApp1.Stores;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=reservoom.db";

        private readonly Hotel _hotel;
        private readonly HotelStore _hotelStore;
        private readonly NavigationStore _navigationStore;
        
        private ReservroomDbContextFactory _reservroomDbContextFactory;
        public App()
        {
             _reservroomDbContextFactory = new ReservroomDbContextFactory(CONNECTION_STRING);

            IReservationProvider reservationProvider = new DatabaseReservationProvider(_reservroomDbContextFactory);
            IReservationCreator reservationCreator = new DatabaseReservationCreator(_reservroomDbContextFactory);
            IReservationConflictValidator reservationConflictValidator = new DatabaseReservationConflictValidator(_reservroomDbContextFactory);
            
            ReservationBook reservationBook = new ReservationBook(reservationProvider, reservationCreator, reservationConflictValidator);

            _hotel = new Hotel("Thyago's Suites", reservationBook);
            _hotelStore = new HotelStore(_hotel);
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {                        
            using (ReservRoomDbContext dbContext = _reservroomDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            _navigationStore.CurrentViewModel = CreateReservationViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }

        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotelStore, new NavigationService(_navigationStore, CreateReservationViewModel));
        }
        
        private ReservationListingViewModel CreateReservationViewModel()
        {
            return ReservationListingViewModel.LoadViewModel(_hotelStore, new NavigationService(_navigationStore, CreateMakeReservationViewModel));
        }
    }
}
