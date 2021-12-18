using System;

namespace Pacco.Services.Availability.Core.ValueObjects
{
    public readonly struct Reservation : IEquatable<Reservation>
    {
        public Reservation(DateTime dateTime, int priority)
            => (DateTime, Priority) = (dateTime, priority);

        public DateTime DateTime { get; }
        public int Priority { get; }

        public override bool Equals(object obj) => obj is Reservation reservation && Equals(reservation);
        public bool Equals(Reservation other) => DateTime == other.DateTime && Priority == other.Priority;
        public override int GetHashCode() => HashCode.Combine(DateTime, Priority);
    }
}
