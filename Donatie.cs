namespace BloodBankM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Donatie")]
    public partial class Donatie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Donatie()
        {
            Stocs = new HashSet<Stoc>();
        }

        [Key]
        public int id_donatie { get; set; }

        [Required]
        [StringLength(500)]
        public string nume_prenume { get; set; }

        [Required]
        [StringLength(50)]
        public string grupa_sanguina { get; set; }

        [Required]
        [StringLength(100)]
        public string adresa { get; set; }

        [Required]
        [StringLength(10)]
        public string telefon { get; set; }

        public DateTime data_recoltarii { get; set; }

        public int id_medic { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stoc> Stocs { get; set; }
    }
}
