using MongoDB.Bson;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kztek_Model.Models
{
    [Table("Time_ServerLog")]
    public class Time_ServerLog
    {
        [Key]
        public ObjectId Id { get; set; }

        public string OS { get; set; }

        public string Database { get; set; }

        public int NumberRecord { get; set; }

        public int TimeFinished { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
