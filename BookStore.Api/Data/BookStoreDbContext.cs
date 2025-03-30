using System;
using BookStore.Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Data;

public class BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Author> Authors => Set<Author>();
}
