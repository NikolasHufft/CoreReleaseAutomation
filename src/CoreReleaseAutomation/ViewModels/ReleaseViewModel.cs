using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreReleaseAutomation.ViewModels
{
    public class ReleaseViewModel : BaseViewModel
    {
        [Key]
        [MaxLength(32)]
        public string ReleaseAutomationId { get; set; }

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

        [Display(Name = "Job Tracker")]
        [MaxLength]
        public string JobTracker { get; set; }


        [Display(Name = "Path Origem")]
        [MaxLength(512)]
        public string PathOrigem { get; set; }

        [Display(Name = "Path Destiny")]
        [MaxLength(512)]
        public string PathDestiny { get; set; }

        public ReleaseViewModel()
        {
            ReleaseAutomationId = Guid.NewGuid().ToString("N");
            Managers = new List<string>();
            StatusList = new List<string>();
            ReleaseType = "Hotfix";
            CopyFiles = "Yes";
        }
    }
}
