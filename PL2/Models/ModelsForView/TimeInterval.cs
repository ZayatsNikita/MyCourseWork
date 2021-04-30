using System;
using System.ComponentModel.DataAnnotations;


namespace PL.Models.ModelsForView
{
    public class TimeInterval
    {
        [Display(Name = "Start of term")]
        public DateTime? From { get; set; }

        [Display(Name = "End of term")]
        public DateTime? To { get; set; }
    }
}
