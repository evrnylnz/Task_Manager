using System.ComponentModel.DataAnnotations;

namespace Task_Manager.Models
{
    public class Duty
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsArchived { get; set; }
    }
}
