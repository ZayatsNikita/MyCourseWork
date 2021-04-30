using System;
using System.Collections.Generic;
using System.Text;

namespace PL.Models
{
    public class Worker : IEquatable<Worker>
    {
        public string PersonalData { get; set; }
        public int PassportNumber { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Worker);
        }

        public bool Equals(Worker other)
        {
            return other != null &&
                   PassportNumber == other.PassportNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PassportNumber);
        }
    }
}
