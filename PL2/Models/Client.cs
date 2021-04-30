using System;
using System.Collections.Generic;
using System.Text;

namespace PL.Models
{
    public class Client : IEquatable<Client>
    {
        public string ContactInformation { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Client);
        }

        public bool Equals(Client other)
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
