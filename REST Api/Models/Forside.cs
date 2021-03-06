using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace REST_Api.Models
{
    [Table("Forside")]
    public partial class Forside
    {
        public int ID { get; set; }

        public int? FK_Kolonne { get; set; }

        public int? FærdigvareNr { get; set; }

        [StringLength(265)]
        public string FærdigvareNavn { get; set; }

        public int? ProcessordreNr { get; set; }

        [StringLength(20)]
        public string Produktionsinitialer { get; set; }

        public DateTime? Dato { get; set; }
    }
}
