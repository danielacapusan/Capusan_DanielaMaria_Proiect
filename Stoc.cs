namespace BloodBankM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stoc")]
    public partial class Stoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_stoc { get; set; }

        [Required]
        [StringLength(50)]
        public string grupa { get; set; }

        public int cantitate { get; set; }

        public DateTime termen { get; set; }

        public int id_donator { get; set; }

        public virtual Donatie Donatie { get; set; }
    }
}
