namespace BloodBankM
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BloodBankEntitiesModel : DbContext
    {
        public BloodBankEntitiesModel()
            : base("name=BloodBankEntitiesModel")
        {
        }

        public virtual DbSet<Donatie> Donaties { get; set; }
        public virtual DbSet<Stoc> Stocs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donatie>()
                .HasMany(e => e.Stocs)
                .WithRequired(e => e.Donatie)
                .HasForeignKey(e => e.id_donator);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Donaties)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.id_medic);

         
        }
    }
}
