using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class OrderEntity
    {
        public int ClientId { get; set; }
        public int MasterId { get; set; }
        public int ManagerId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int Id { get; set; }
    }
}
