using System;
using System.Collections.Generic;
using System.Text;

namespace PL.Models
{
    public class Componet : IEquatable<Componet>
    {
        public decimal Price { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Componet);
        }

        public bool Equals(Componet other)
        {
            return other != null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
