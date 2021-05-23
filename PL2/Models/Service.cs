using System;

namespace PL.Models
{
    public class Service : IEquatable<Service>
    {
        public int InfoAboutComponentId {get;set; }

        public decimal Price { get; set; }
    
        public string Description { get; set; }
        
        public string Title { get; set; }
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Service);
        }

        public bool Equals(Service other)
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
