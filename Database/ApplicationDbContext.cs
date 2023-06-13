using interview.Database.Entity;
using interview.Dtos.Seeder;
using Microsoft.EntityFrameworkCore;

namespace interview.Database;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options){

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserTableSeeder());
    }

    public DbSet<UsersEntity>UsersEntities{get;set;}
    public DbSet<NoteEntity>NoteEntities {get;set;}
}