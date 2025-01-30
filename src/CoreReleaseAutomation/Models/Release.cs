using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreReleaseAutomation.Models
{
    [Table("t_client_di_release_automation")]
    public class Release : BaseModel
    {
        [Key]        
        [Column("id_release")]
        public string ReleaseId { get; set; }
                
        [Column("id_version")]        
        public string Version { get; set; }

        [Column("no_release_name")]        
        public string ReleaseName { get; set; }

        [Column("tx_path_origem")]
        public string PathOrigem { get; set; }

        [Column("tx_path_destiny")]
        public string PathDestiny { get; set; }

        [Column("tx_description")]
        public string Description { get; set; }

        [Column("id_release_type")]
        public string ReleaseType { get; set; }

        [Column("no_manager")]
        public string Manager { get; set; }

        [Column("tx_execution_log")]
        public string ExecutionLog { get; set; }

        [Column("id_status")]
        public string Status { get; set; }

        [Column("tx_job_tracker")]
        public string JobTracker { get; set; }

        public Release()
        {
            ReleaseId = Guid.NewGuid().ToString("N");
            Manager = "Ash";
        }
    }
}
