using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystem.Models.Data
{
    public class VotingSystemDbContext : DbContext
    {
        public VotingSystemDbContext(DbContextOptions<VotingSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<EditionDraft> EditionDrafts { get; set; }
        public DbSet<ActiveEdition> ActiveEditions { get; set; }
        public DbSet<ConcludedEdition> ConcludedEditions { get; set; }
        public DbSet<Edition> Editions { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<District> Districts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var priceConverter = new ValueConverter<Price, decimal>(
                price => price.PLN,
                decPln => new Price(decPln));

            var durationConverter = new ValueConverter<Duration, long>(
                duration => duration.Value.Ticks,
                ticks => new Duration(TimeSpan.FromTicks(ticks)));

            modelBuilder.Entity<Project>()
                .Property(p => p.Price)
                .HasConversion(priceConverter);

            modelBuilder.Entity<Project>()
                .Property(p => p.EstimatedRealizationTime)
                .HasConversion(durationConverter);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.District)
                .WithMany()
                .HasForeignKey(p => p.DistrictId)
                .OnDelete(DeleteBehavior.NoAction);

            

            modelBuilder.Entity<District>().Property(i => i.Id).ValueGeneratedNever();
            modelBuilder.Entity<District>().HasData(new District[] {
                new District(1, "Repty"),
                new District(2, "Lasowice"),
                new District(3, "Bobrowniki Śląskie"),
                new District(4, "Opatowice"),
                new District(5, "Rybna"),
                new District(6, "Pniowiec"),
                new District(7, "Sowice"),
                new District(8, "Puferki"),
                new District(9, "Repecko"),
                new District(10, "Siwcowe"),
                new District(11, "Tłuczykąt"),
            });

            modelBuilder.Entity<Subscriber>()
                .Property<int>("Id");
            
            modelBuilder.Entity<Subscriber>()
                .HasKey("Id");

            modelBuilder.Entity<Vote>()
                .Property<int>("Id");

            modelBuilder.Entity<Vote>()
                .Property<int>("EditionId");

            modelBuilder.Entity<Vote>()
                .HasKey("Id");

            modelBuilder.Entity<Vote>()
                .HasIndex("EditionId", "ProjectId", "VoterPesel")
                .IsUnique();

            modelBuilder.Entity<Edition>();

            modelBuilder.Entity<Edition>()
                .HasMany(ae => ae.Participants).WithOne();

            

            modelBuilder.Entity<ActiveEdition>()
                .HasBaseType<Edition>();

            modelBuilder.Entity<ConcludedEdition>()
                .HasBaseType<Edition>();



            modelBuilder.Entity<EditionParticipant>()
                .HasKey("EditionId", "ProjectId");

            modelBuilder.Entity<EditionParticipant>()
                .HasOne(aep => aep.Project)
                .WithMany();

            modelBuilder.Entity<EditionParticipant>()
                .HasMany(aep => aep.Votes)
                .WithOne()
                .HasForeignKey("EditionId", "ProjectId");


            modelBuilder.Entity<ActiveEditionParticipant>()
                .HasBaseType<EditionParticipant>();

            modelBuilder.Entity<ConcludedEditionParticipant>()
                .HasBaseType<EditionParticipant>();

        }

    }
}
