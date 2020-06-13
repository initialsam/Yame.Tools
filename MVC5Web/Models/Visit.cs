using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Web.Models
{
    public class Visit
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Pet Name")]
        public string PetName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Visit Date")]
        public DateTime VisitDate { get; set; }

        //Single checkbox
        [Display(Name = "Is this an emergency visit?")]
        public bool IsEmergencyVisit { get; set; }

        //Textarea
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        //Radio button
        //could also be string, bool, or double, depending on value type
        [Display(Name = "Veterinarian")]
        public int VeterinarianID { get; set; }

        //Number
        public int Cost { get; set; }

        //Range
        [Display(Name = "Pet Weight")]
        public double PetWeight { get; set; }

        //Tel
        [Phone]
        public string CustomerPhone { get; set; }

        [DisplayName("類別")]
        public List<string> Category { get; set; } = new List<string>();

        public MultiSelectList CategoryList { get; set; }
    }
}