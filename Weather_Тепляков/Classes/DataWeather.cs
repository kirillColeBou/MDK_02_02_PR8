using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Weather_Тепляков.Classes
{
    public class DataWeather
    {
        [Key]
        public int Id { get; set; }
        public string City { get; set; }
        public string JsonData { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
