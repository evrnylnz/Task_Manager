using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Task_Manager.Models;

namespace Task_Manager.ViewModels
{
    public class DutyCreateModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public int StatusId { get; set; }
        public SelectList? Status { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsArchived { get; set; }


    }
}
