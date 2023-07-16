using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using WpfApp1.DbContexts;
using WpfApp1.HostBuilders;
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
        private IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddViewmodels()
                .ConfigureServices((hostContext, services) =>
            {
                string connectionString = hostContext.Configuration.GetConnectionString("Default");


                services.AddSingleton(new ReservroomDbContextFactory(connectionString));
                services.AddSingleton<IReservationProvider, DatabaseReservationProvider>();
                services.AddSingleton<IReservationCreator, DatabaseReservationCreator>();
                services.AddSingleton<IReservationConflictValidator, DatabaseReservationConflictValidator>();

                services.AddTransient<ReservationBook>();
                string hotelName = hostContext.Configuration.GetValue<string>("HotelName");
                services.AddSingleton((s) => new Hotel(hotelName, s.GetRequiredService<ReservationBook>()));

                services.AddSingleton<HotelStore>();
                services.AddSingleton<NavigationStore>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton(s => new MainWindow()
                {
                    DataContext = s.GetRequiredService<MainViewModel>()
                });

            }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            ReservroomDbContextFactory reservroomDbContextFactory = _host.Services.GetRequiredService<ReservroomDbContextFactory>();

            using (ReservRoomDbContext dbContext = reservroomDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            NavigationService<ReservationListingViewModel> navigationService = _host.Services.GetRequiredService<NavigationService<ReservationListingViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();

            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }

    }
}
