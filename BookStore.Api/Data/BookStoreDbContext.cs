using System;
using BookStore.Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Data;

public class BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Author> Authors => Set<Author>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new { Id = 1, GenreName = "Fiction" },
            new { Id = 2, GenreName = "Non Fiction" },
            new { Id = 3, GenreName = "Fiction" },
            new { Id = 4, GenreName = "Fiction" },
            new { Id = 5, GenreName = "Fiction" }
        );

        modelBuilder.Entity<Author>().HasData(
            new { Id = 1, AuthorName = "Fyodor Dostoevsky", Age = 60 },
            new { Id = 2, AuthorName = "Alex Michaelides", Age = 47 },
            new { Id = 3, AuthorName = "Colleen Hoover", Age = 38 }
        );
    }
}
