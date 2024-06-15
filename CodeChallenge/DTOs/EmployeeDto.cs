using System;
using System.Collections.Generic;

namespace CodeChallenge.Models
{
    public class EmployeeDto
    {
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public List<string> DirectReports { get; set; }
    }
}
