using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Exceptions;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private readonly Hotel _hotel;

        protected override void OnStartup(StartupEventArgs e)
        {            

            //test logic 
            /*    Hotel hotel = new Hotel("Thyago's Suites");

                try
                {
                    hotel.MakeReservation(new Reservation(
                                   new RoomID(1, 3),
                                   new DateTime(2023, 6, 1),
                                   new DateTime(2023, 6, 2),
                                   "Thyago"));

                    hotel.MakeReservation(new Reservation(
                        new RoomID(1, 3),
                        new DateTime(2023, 6, 3),
                        new DateTime(2023, 6, 4),
                        "Thyago"));
                }
                catch (ReservationConflictException ex)
                {

                    throw ex;
                }


            IEnumerable<Reservation> reservations = hotel.GetAllReservations();*/

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_hotel)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }

        public App()
        {
            _hotel = new Hotel("Thyago's Suites");
        }
    }
}
