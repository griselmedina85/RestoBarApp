using Microsoft.EntityFrameworkCore;
using webapi.Dtos;
using webapi.Models;

namespace webapi.Context
{
    public class RestoAppContext : DbContext
    {
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<ReservationEntity> Reservations { get; set; }
        public DbSet<ReservationLogEntity> ReservationsLog { get; set; }
        public DbSet<RestaurantEntity> Restaurants { get; set; }
        public DbSet<TableEntity> Tables { get; set; }
        public DbSet<UserEntity> Employee { get; set; }


        public RestoAppContext(DbContextOptions<RestoAppContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aquí se pueden agregar configuraciones adicionales del modelo
            //Relacion entre mesa y ubicacion una table puede tener 1 location pero la
            //location puede tener varias table por eso la Fk esta en Table
            modelBuilder.Entity<TableEntity>()
                .HasOne<LocationEntity>(t => t.Location)
                .WithMany(l => l.Tables)
                .HasForeignKey(t => t.LocationId);

            modelBuilder.Entity<TableEntity>()
                .HasOne<RestaurantEntity>(t => t.Restaurant)
                .WithMany(l => l.Tables)
                .HasForeignKey(t => t.RestaurantId);

            //modelBuilder.Entity<PersonEntity>()
            //    .HasOne<ClientEntity>(p => p.Client)
            //    .WithOne(c => c.Person)
            //    .HasForeignKey<ClientEntity>(c => c.ClientId);
               
        }
    }
}
