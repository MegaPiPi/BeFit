using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.DTO
{
    public class TrainingSessionCreateDto
    {
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }
}