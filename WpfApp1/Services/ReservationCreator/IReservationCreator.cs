using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services.ReservationCreator
{
    public interface IReservationCreator
    {
        Task CreateReservation(Reservation reservation);
    }
}
