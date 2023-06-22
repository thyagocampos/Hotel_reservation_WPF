using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services.ReservationConflictValidation
{
    public interface IReservationConflictValidator
    {
        Task<Reservation?> GetConflictingReservation(Reservation reservation);
    }
}
