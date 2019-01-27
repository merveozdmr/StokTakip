namespace StokTakip.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model14")
        {
        }

        public virtual DbSet<AlinanUrunler> AlinanUrunler { get; set; }
        public virtual DbSet<AltKategori> AltKategori { get; set; }
        public virtual DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public virtual DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public virtual DbSet<aspnet_Paths> aspnet_Paths { get; set; }
        public virtual DbSet<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUsers { get; set; }
        public virtual DbSet<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }
        public virtual DbSet<aspnet_Profile> aspnet_Profile { get; set; }
        public virtual DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public virtual DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public virtual DbSet<aspnet_Users> aspnet_Users { get; set; }
        public virtual DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }
        public virtual DbSet<Birim> Birim { get; set; }
        public virtual DbSet<Depo> Depo { get; set; }
        public virtual DbSet<Firma> Firma { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Personel> Personel { get; set; }
        public virtual DbSet<Raf> Raf { get; set; }
        public virtual DbSet<SilinmisUrunler> SilinmisUrunler { get; set; }
        public virtual DbSet<Stok> Stok { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Urun> Urun { get; set; }
        public virtual DbSet<UrunDurum> UrunDurum { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AltKategori>()
                .HasMany(e => e.Urun)
                .WithOptional(e => e.AltKategori)
                .WillCascadeOnDelete();

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Membership)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Paths)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Roles)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Users)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Paths>()
                .HasOptional(e => e.aspnet_PersonalizationAllUsers)
                .WithRequired(e => e.aspnet_Paths);

            modelBuilder.Entity<aspnet_Roles>()
                .HasMany(e => e.aspnet_Users)
                .WithMany(e => e.aspnet_Roles)
                .Map(m => m.ToTable("aspnet_UsersInRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(e => e.aspnet_Membership)
                .WithRequired(e => e.aspnet_Users);

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(e => e.aspnet_Profile)
                .WithRequired(e => e.aspnet_Users);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventSequence)
                .HasPrecision(19, 0);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventOccurrence)
                .HasPrecision(19, 0);

            modelBuilder.Entity<Depo>()
                .HasMany(e => e.Raf)
                .WithOptional(e => e.Depo)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Firma>()
                .HasMany(e => e.Urun)
                .WithOptional(e => e.Firma)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Personel>()
                .Property(e => e.TcNo)
                .IsFixedLength();

            modelBuilder.Entity<Personel>()
                .Property(e => e.Cinsiyet)
                .IsFixedLength();

            modelBuilder.Entity<Personel>()
                .Property(e => e.PersonelTelefon)
                .IsFixedLength();

            modelBuilder.Entity<Raf>()
                .HasMany(e => e.Stok)
                .WithOptional(e => e.Raf)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Stok>()
                .HasMany(e => e.AlinanUrunler)
                .WithOptional(e => e.Stok)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Stok>()
                .HasMany(e => e.SilinmisUrunler)
                .WithOptional(e => e.Stok)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Urun>()
                .Property(e => e.Fiyat)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Urun>()
                .HasMany(e => e.UrunDurum)
                .WithOptional(e => e.Urun)
                .WillCascadeOnDelete();
        }
    }
}
