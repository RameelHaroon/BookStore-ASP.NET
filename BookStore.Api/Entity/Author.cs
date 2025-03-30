using System;

namespace BookStore.Api.Entity;

public class Author
{
    public int Id { get; set; }
    public required string AuthorName { get; set; }
    public int Age { get; set; }
}
