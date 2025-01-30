using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreReleaseAutomation.Models
{
    public class BaseModel
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
