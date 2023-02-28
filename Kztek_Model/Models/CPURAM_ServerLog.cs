using MongoDB.Bson;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kztek_Model.Models
{
    [Table("CPURAM_ServerLog")]
    public class CPURAM_ServerLog
    {
        [Key]
        public ObjectId Id { get; set; }

        public string OS { get; set; }

        public string Database { get; set; }

        public double CPU_Usage { get; set; }

        public double RAM_Availability { get; set; }

        public DateTime DateCreated { get; set; }
    }

    public class CPURAM_ServerLog_Custom
    {
        public string Id { get; set; }

        public string OS { get; set; }

        public string Database { get; set; }

        public double CPU_Usage { get; set; }

        public double RAM_Availability { get; set; }

        public DateTime DateCreated { get; set; }
        public string Type { get; set; }
        public double AVGCPU { get; set; }

        public double AVGRAM { get; set; }
    }
}
