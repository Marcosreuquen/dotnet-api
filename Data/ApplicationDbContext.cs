
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using challenge.Models;

namespace challenge.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Character> DataCharacter { get; set; }

        public DbSet<Genre> DataGenre { get; set; }
        public DbSet<Movie> DataMovie { get; set; }
        public DbSet<User> DataUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().ToTable("Character");
            modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}