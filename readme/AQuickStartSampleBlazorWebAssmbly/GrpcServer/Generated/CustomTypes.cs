#nullable enable
namespace CustomTypesGrpc // CustomTypes.tt Line: 9, called from GrpcGeneratorSettings.partial.cs Line: 208
{
    using System;
    using vPlugins;

    public partial class DecimalValue // CustomTypes.tt Line: 14
    {
        private const decimal BigFactor = 1_000_000_000_000_000_000m;
        private const decimal NanoFactor = 1_000_000_000;
        public DecimalValue(long units18, long units, int nanos)
        {
            Units18 = units18;
            Units = units;
            Nanos = nanos;
        }
        public static implicit operator decimal(DecimalValue grpcDecimal) // CustomTypes.tt Line: 24
        {
            return grpcDecimal.Units18 * BigFactor + grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor;
        }
        public static implicit operator DecimalValue(decimal value) // CustomTypes.tt Line: 28
        {
            var units18 = decimal.ToInt64(value / BigFactor);
            var val2 = value - units18 * BigFactor;
            var units = decimal.ToInt64(val2);
            var nanos = decimal.ToInt32((val2 - units) * NanoFactor);
            return new DecimalValue(units18, units, nanos);
        }
    }
    public partial class DecimalValueNullable // CustomTypes.tt Line: 37
    {
        public DecimalValueNullable(DecimalValue? grpcDecimal)
        {
            if (grpcDecimal != null)
            {
                this.HasValue = true;
                this.Value = grpcDecimal;
            }
        }
        public static implicit operator decimal?(DecimalValueNullable grpcDecimal) // CustomTypes.tt Line: 47
        {
            if (grpcDecimal.HasValue)
            {
                decimal d = grpcDecimal.Value;
                return d;
            }
            return null;
        }
        public static implicit operator DecimalValueNullable(decimal? value) // CustomTypes.tt Line: 56
        {
            if (value.HasValue)
            {
                DecimalValue dv = value.Value;
                return new DecimalValueNullable() { HasValue = true, Value = dv };
            }
            return new DecimalValueNullable();
        }
    }
    public partial class CharValue // CustomTypes.tt Line: 66
    {
        public CharValue(string str = " ")
        {
            this.Str = str;
        }
        public static implicit operator char(CharValue charValue) // CustomTypes.tt Line: 72
        {
            return charValue.Str[0];
        }
        public static implicit operator CharValue(char value) // CustomTypes.tt Line: 76
        {
            return new CharValue($"{value}");
        }
    }
    public partial class CharValueNullable // CustomTypes.tt Line: 81
    {
        public CharValueNullable(string? str = null)
        {
            this.Value = new CharValue();
            if (str != null)
            {
                this.HasValue = true;
                this.Value.Str = str;
            }
        }
        public static implicit operator char?(CharValueNullable charValue) // CustomTypes.tt Line: 92
        {
            if (charValue.HasValue)
                return charValue.Value.Str[0];
            return null;
        }
        public static implicit operator CharValueNullable(char? value) // CustomTypes.tt Line: 98
        {
            if (value.HasValue)
                return new CharValueNullable($"{value.Value}");
            return new CharValueNullable();
        }
    }
    public partial class DurationNullable // CustomTypes.tt Line: 105
    {
        partial void OnConstruction()
        {
            this.Value = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(new TimeSpan());
        }
        public static implicit operator TimeSpan(DurationNullable stamp) // CustomTypes.tt Line: 111
        {
            return stamp.Value.ToTimeSpan();
        }
        public static implicit operator DurationNullable(TimeSpan value) // CustomTypes.tt Line: 115
        {
            return new DurationNullable() { HasValue = true, Value = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(value) };
        }
        public static implicit operator TimeSpan?(DurationNullable stamp) // CustomTypes.tt Line: 119
        {
            if (stamp != null && stamp.HasValue)
                return stamp.Value.ToTimeSpan();
            return null;
        }
        public static implicit operator DurationNullable?(TimeSpan? value) // CustomTypes.tt Line: 125
        {
            if (value.HasValue)
                return new DurationNullable() { HasValue = true, Value = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(value.Value) };
            return null;
        }
        public static implicit operator TimeOnly?(DurationNullable stamp) // CustomTypes.tt Line: 131
        {
            if (!stamp.HasValue)
                return null;
            var dt = stamp.Value.ToTimeSpan();
            return new TimeOnly(dt.Hours, dt.Minutes, dt.Seconds, dt.Milliseconds);
        }
    }
    public partial class TimestampNullable // CustomTypes.tt Line: 139
    {
        partial void OnConstruction()
        {
            this.Value = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.MinValue.ToUniversalTime());
        }
        public static implicit operator DateTime(TimestampNullable stamp) // CustomTypes.tt Line: 145
        {
            return stamp.Value.ToDateTime();
        }
        public static implicit operator TimestampNullable(DateTime value) // CustomTypes.tt Line: 149
        {
            return new TimestampNullable() { HasValue = true, Value = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(value) };
        }
        public static implicit operator DateTime?(TimestampNullable stamp) // CustomTypes.tt Line: 153
        {
            if (stamp != null && stamp.HasValue)
                return stamp.Value.ToDateTime();
            return null;
        }
        public static implicit operator TimestampNullable?(DateTime? value) // CustomTypes.tt Line: 159
        {
            if (value.HasValue)
                return new TimestampNullable() { HasValue = true, Value = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(value.Value) };
            return null;
        }
        public static implicit operator DateOnly(TimestampNullable stamp) // CustomTypes.tt Line: 165
        {
            var dt = stamp.Value.ToDateTime();
            return new DateOnly(dt.Year, dt.Month, dt.Day);
        }
        public static implicit operator TimestampNullable(DateOnly value) // CustomTypes.tt Line: 170
        {
            var dt = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0, DateTimeKind.Utc);
            return new TimestampNullable() { HasValue = true, Value = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(dt) };
        }
        public static implicit operator DateOnly?(TimestampNullable stamp) // CustomTypes.tt Line: 175
        {
            if (stamp != null && stamp.HasValue)
            {
                var dt = stamp.Value.ToDateTime();
                return new DateOnly(dt.Year, dt.Month, dt.Day);
            }
            return null;
        }
        public static implicit operator TimestampNullable(DateOnly? value) // CustomTypes.tt Line: 184
        {
            if (value.HasValue)
            {
                var dt = new DateTime(value.Value.Year, value.Value.Month, value.Value.Day, 0, 0, 0, DateTimeKind.Utc);
                return new TimestampNullable() { HasValue = true, Value = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(dt) };
            }
            return new TimestampNullable();
        }
        public static implicit operator TimeOnly(TimestampNullable stamp) // CustomTypes.tt Line: 193
        {
            var dt = stamp.Value.ToDateTime();
            return new TimeOnly(dt.Hour, dt.Minute, dt.Second);
        }
        public static implicit operator TimestampNullable(TimeOnly value) // CustomTypes.tt Line: 198
        {
            var dt = new DateTime(1000, 1, 1, value.Hour, value.Minute, value.Second, DateTimeKind.Utc);
            return new TimestampNullable() { HasValue = true, Value = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(dt) };
        }
        public static implicit operator TimeOnly?(TimestampNullable stamp) // CustomTypes.tt Line: 203
        {
            if (stamp != null && stamp.HasValue)
            {
                var dt = stamp.Value.ToDateTime();
                return new TimeOnly(dt.Hour, dt.Minute, dt.Second);
            }
            return null;
        }
        public static implicit operator TimestampNullable(TimeOnly? value) // CustomTypes.tt Line: 212
        {
            if (value.HasValue)
            {
                var dt = new DateTime(1000, 1, 1, value.Value.Hour, value.Value.Minute, value.Value.Second, DateTimeKind.Utc);
                return new TimestampNullable() { HasValue = true, Value = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(dt) };
            }
            return new TimestampNullable();
        }
        public static implicit operator DateTimeOffset(TimestampNullable stamp) // CustomTypes.tt Line: 221
        {
            return stamp.Value.ToDateTimeOffset();
        }
        public static implicit operator TimestampNullable(DateTimeOffset value) // CustomTypes.tt Line: 225
        {
            return new TimestampNullable() { HasValue = true, Value = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(value) };
        }
        public static implicit operator DateTimeOffset?(TimestampNullable stamp) // CustomTypes.tt Line: 229
        {
            if (stamp != null && stamp.HasValue)
                return stamp.Value.ToDateTimeOffset();
            return null;
        }
        public static implicit operator TimestampNullable?(DateTimeOffset? value) // CustomTypes.tt Line: 235
        {
            if (value.HasValue)
                return new TimestampNullable() { HasValue = true, Value = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(value.Value) };
            return null;
        }
    }
    public partial class parameter
    {
    }
}
