using System;
using System.Collections.Generic;
using System.Text;

namespace PL.Models
{
    public class Worker
    {
        public string PersonalData { get; set; }
        public int PassportNumber { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Worker worker &&
                   PersonalData == worker.PersonalData &&
                   PassportNumber == worker.PassportNumber;
        }
    }
}
