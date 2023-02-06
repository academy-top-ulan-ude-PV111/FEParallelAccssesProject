using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace FEParallelAccssesProject
{
    public class Country
    {
        public int Id { set; get; }
        
        //[ConcurrencyCheck]
        public string Title { set; get; } = null!;

        [Timestamp]
        public byte[]? Timestamp { set; get; }

    }
    /*
    public class Company
    {
        public int Id { set; get; }
        public string Title { set; get; } = null!;
        public Country? Country { set; get; }
        public int CountryId { set; get; }
        public List<Employe> Employes { set; get; } = new();
    }
    public class Position
    {
        public int Id { set; get; }
        public string Title { set; get; } = null!;
        List<Employe> Employes { set; get; } = new();
    }
    public class Employe
    {
        public int Id { set; get; }
        public string Name { set; get; } = null!;
        public Company? Company { set; get; }
        public int CompanyId { set; get; }
        public Position? Position { set; get; }
        public int PositionId { set; get; }
    }
    public class AppContext : DbContext
    {
        public DbSet<Employe> Employes { set; get; }
        public DbSet<Company> Companies { set; get; }
        public DbSet<Country> Countries { set; get; }
        public DbSet<Position> Positions { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CompanyDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>()
                        .Property(c => c.Title)
                        .IsConcurrencyToken();
        }
    }
    */

    public class TempContext : DbContext
    {
        public TempContext() 
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public DbSet<Country> Countries { set; get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WorkDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(new Country[]{ 
                new Country(){ Id = 1, Title = "China"},
                new Country(){ Id = 2, Title = "Korea"},
                new Country(){ Id = 3, Title = "Germany"},
            });
        }

    }
    internal class Program
    {
        /*
        static void ConcurrensyTokenExample()
        {
            using (AppContext context = new())
            {
                try
                {
                    Country? country = context.Countries.FirstOrDefault();
                    if (country is not null)
                    {
                        country.Title = "Korea";
                        context.SaveChanges();
                        Console.WriteLine("Update is correct");
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {

                }
            }
        }
        */
        static void Main(string[] args)
        {
            using(TempContext context = new())
            {
                try
                {
                    Country? country = context.Countries.FirstOrDefault();
                    country!.Title = "Bulgaria";
                    context.SaveChanges();
                }
                catch(DbUpdateConcurrencyException ex)
                {

                }
                
            }
        }
    }
}