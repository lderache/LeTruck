using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TheTruck.Entities
{
    public enum BuyFrequency
    {
        [Display(Name = "Every week")]
        Weekly,
        [Display(Name = "Every 2 weeks")]
        BiMonthly,
        [Display(Name = "Every month")]
        Monthly
    }

    public enum StayPeriod
    {
        [Display(Name = "1 Month")]
        OneMonth,
        [Display(Name = "3 Months")]
        ThreeMonths,
        [Display(Name = "6 Months")]
        SixMonths,
        [Display(Name = "1 Year")]
        OneYear
    }

    public class Survey
    {
        public int Id { get; set; }
        public String Username { get; set; }

        [DisplayName("How often would you like to order ?")]
        public BuyFrequency OrderFrequency { get; set; }

        [DisplayName("How long time will you stay in Taishan ?")]
        public StayPeriod StayPeriod { get; set; }

        [DisplayName("We would love to hear your feedback, please let us know your thought")]
        public String Comment { get; set; }
    }
}
