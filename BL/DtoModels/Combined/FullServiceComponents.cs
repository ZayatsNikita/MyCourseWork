using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DtoModels.Combined
{
    public class FullServiceComponents
    {
        public int Id { get; set; }
        
        public Service Service {get;set;}
        
        public Component Componet { get; set; }

        public decimal ComonPrice 
        {
            get => Service.Price + Componet.Price;
        } 
    }
}
