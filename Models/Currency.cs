using System;
using System.Diagnostics.CodeAnalysis;

namespace TcmbImplementationStudy.Models
{
    public class Currency : IEquatable<Currency>, IComparable<Currency>
    {
        public int CurrencyId { get; set; }

        public string Kod { get; set; }

        public decimal ForexBuying { get; set; }

        public string Tarih { get; set; }

        public int CompareTo([AllowNull] Currency other)
        {
            return other == null ? 1 : this.Tarih.CompareTo(other.Tarih);
        }

        public bool Equals([AllowNull] Currency other)
        {
            return other != null && this.Tarih.Equals(other.Tarih);
        }
    }
}
