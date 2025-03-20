using System;

namespace RestApiNUnitTests.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool Completed { get; set; }
    }
}
