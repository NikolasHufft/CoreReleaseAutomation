using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreReleaseAutomation.Models
{
    [Table("t_log_version")]
    public class LogVersion
    {        
        [Column("no_version")]
        public string Version { get; set; }
        
        [Column("no_patch")]
        public string Patch { get; set; }
    }
}
