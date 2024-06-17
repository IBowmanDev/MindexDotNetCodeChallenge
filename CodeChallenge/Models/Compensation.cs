using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        [Required]
        public String EmployeeId { get; set; }

        public String CompensationId { get; set; }
        public int Salary { get; set; }
        public DateTime EffectiveDate { get; set; }

        [JsonIgnore]
        public Employee Employee { get; set; }
    }
}
