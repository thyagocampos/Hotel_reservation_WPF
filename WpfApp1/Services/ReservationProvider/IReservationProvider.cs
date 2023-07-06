﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services.ReservationProvider
{
    public interface IReservationProvider
   {
        Task<IEnumerable<Reservation>> GetAllReservations();
    }
}
