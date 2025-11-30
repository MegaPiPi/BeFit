using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExerciseStats
    {
        [Display(Name = "Exercise")]
        public string ExerciseName { get; set; }

        [Display(Name = "Number of complited sessions")]
        public int PerformedCount { get; set; }

        [Display(Name = "Number of repetitions")]
        public int TotalRepetitions { get; set; }

        [Display(Name = "Average weight (kg)")]
        public double AvgWeight { get; set; }

        [Display(Name = "Maximum weight (kg)")]
        public double MaxWeight { get; set; }
    }
}