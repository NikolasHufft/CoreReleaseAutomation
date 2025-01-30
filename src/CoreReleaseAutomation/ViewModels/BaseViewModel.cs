using System;
using System.ComponentModel.DataAnnotations;

namespace CoreReleaseAutomation.ViewModels
{
    public class BaseViewModel
    {
        [Display(Name = "Created When")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime dt_modify { get; set; }

        [Display(Name = "Created by")]
        [StringLength(64, ErrorMessage = "Version Id should be between 5 and 64 characters", MinimumLength = 5)]
        public string id_modify { get; set; }
    }
}
