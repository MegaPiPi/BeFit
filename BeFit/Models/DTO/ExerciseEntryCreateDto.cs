using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.DTO
{
    public class ExerciseEntryCreateDto
    {
        [Required]
        [Display(Name = "Exercise type")]
        public int ExerciseTypeId { get; set; }

        [Required]
        [Display(Name = "Training session")]
        public int TrainingSessionId { get; set; }

        [Required]
        [Display(Name = "Weight (kg)")]
        [Range(0, 500, ErrorMessage = "Weight must be a positive number")]
        public double Weight { get; set; }

        [Required]
        [Display(Name = "Number of sets")]
        [Range(1, 10, ErrorMessage = "Number of sets should be between 1 to 10")]
        public int Sets { get; set; }

        [Required]
        [Display(Name = "Repetitions")]
        [Range(1, 50, ErrorMessage = "Number of repetitions should be between 1 to 50")]
        public int Repetitions { get; set; }
    }
}