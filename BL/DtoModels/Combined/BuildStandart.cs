using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DtoModels.Combined
{
    public class BuildStandart
    {
        public int Id { get; set; }
        public Service Service {get;set;}
        public Componet Componet { get; set; }

        public decimal GetComonPrice() => Service.Price + Componet.Price;
    }
}
