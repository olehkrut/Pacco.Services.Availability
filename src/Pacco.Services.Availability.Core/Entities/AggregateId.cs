using Pacco.Services.Availability.Core.Exceptions;
using System;

namespace Pacco.Services.Availability.Core.Entities
{
    public class AggregateId : IEquatable<AggregateId>
    {
        public Guid Value { get; }

        public AggregateId() : this(Guid.NewGuid())
        {
        }

        public AggregateId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidAggregateException(value);
            }

            Value = value;
        }

        public override bool Equals(object obj) => Equals(obj as AggregateId);
        public bool Equals(AggregateId other) => other != null && Value.Equals(other.Value);
        public override int GetHashCode() => HashCode.Combine(Value);

        public static implicit operator Guid(AggregateId aggregateId) => aggregateId.Value;

        public static implicit operator AggregateId(Guid id) => new AggregateId(id);

        public override string ToString() => Value.ToString();
    }
}
