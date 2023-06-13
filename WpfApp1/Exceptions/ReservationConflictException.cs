﻿using System;
using WpfApp1.Models;

namespace WpfApp1.Exceptions
{
    internal class ReservationConflictException : Exception
    {
        public Reservation ExistingReservation { get; }

        public Reservation IncomingReservation { get;}

        public ReservationConflictException(Reservation existingReservation, Reservation incomingReservation)
        {
            ExistingReservation = existingReservation;
            IncomingReservation = incomingReservation;
        }

        public ReservationConflictException(string? message, Reservation existingReservation, Reservation incomingReservation) : base(message)
        {
            ExistingReservation = existingReservation;
            IncomingReservation = incomingReservation;
        }

        public ReservationConflictException(string? message, Exception? innerException, Reservation existingReservation = null, Reservation incomingReservation = null) : base(message, innerException)
        {
            ExistingReservation = existingReservation;
            IncomingReservation = incomingReservation;
        }

    }
}
