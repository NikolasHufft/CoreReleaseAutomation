using CoreReleaseAutomation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreReleaseAutomation.ViewModels
{
    public class HomeViewModel : BaseModel
    {
        [Key]
        [MaxLength(32)]        
        public string ReleaseAutomationId { get; set; }

        //[Required]
        [Display(Name = "Version Id")]        
        [StringLength(64, ErrorMessage = "Version Id should be between 5 and 64 characters", MinimumLength = 5)]
        public string Version { get; set; }

        [Required]
        [Display(Name = "Version Name")]        
        [StringLength(256, ErrorMessage = "Version Name should be between 5 and 64 characters", MinimumLength = 5)]
        public string ReleaseName { get; set; }        

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        [StringLength(1024, ErrorMessage = "Description should be between 5 and 64 characters", MinimumLength = 5)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Release Type")]
        [MaxLength(32)]
        public string ReleaseType { get; set; }

        [Required]
        [Display(Name = "Manger")]
        [MaxLength(64)]
        public string Manager { get; set; }

        [NotMapped]
        public IEnumerable<string> Managers { get; set; }
        
        [Display(Name = "Execution Log")]
        [DataType(DataType.MultilineText)]
        [MaxLength]
        public string ExecutionLog { get; set; }
        
        [Display(Name = "Status")]
        [MaxLength(32)]
        public string Status { get; set; }

        [NotMapped]
        public IEnumerable<string> StatusList { get; set; }

        [Required]
        [Display(Name = "Copy Files to Patch Folder?")]
        public string CopyFiles { get; set; }


        public HomeViewModel()
        {
            ReleaseAutomationId = Guid.NewGuid().ToString("N");
            Managers = new List<string>();
            StatusList = new List<string>();
            ReleaseType = "Hotfix";
            CopyFiles = "Yes";
        }
    }
}
