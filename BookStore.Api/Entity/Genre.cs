using System;

namespace BookStore.Api.Entity;

public class Genre
{
    public int Id { get; set; }
    public required string GenreName { get; set; }
}
