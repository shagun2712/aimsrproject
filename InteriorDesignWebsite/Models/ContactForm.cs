using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InteriorDesignWebsite.Models
{
    public class ContactForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Space to Design")]
        public string SpaceToDesign { get; set; }

        [Required]
        [Range(1, 1000000, ErrorMessage = "Budget must be between 1 and 1,000,000")]
        [Display(Name = "Estimated Budget")]
        public decimal EstimatedBudget { get; set; }

        [Display(Name = "Design Style")]
        public string DesignStyle { get; set; }

        [Display(Name = "Project Timeline")]
        public string ProjectTimeline { get; set; }

        [Display(Name = "Special Requirements")]
        public string SpecialRequirements { get; set; }

        [Display(Name = "How Did You Hear About Us?")]
        public string HeardAboutUs { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}
