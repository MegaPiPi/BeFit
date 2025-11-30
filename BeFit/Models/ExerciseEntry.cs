using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeFit.Models
{
    public class ExerciseEntry
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Exercise type")]
        public int ExerciseTypeId { get; set; }
        [ForeignKey("ExerciseTypeId")]
        public ExerciseType? ExerciseType { get; set; }

        [Required]
        [Display(Name = "Training session")]
        public int TrainingSessionId { get; set; }
        [ForeignKey("TrainingSessionId")]
        public TrainingSession? TrainingSession { get; set; }

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

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}