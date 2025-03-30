using System;

namespace BookStore.Api.Entity;

public class Book
{
    public int Id { get; set; }
    public required string BookName { get; set; }
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }
    public int AuthorId { get; set; }
    public Author? Author { get; set; }
}
