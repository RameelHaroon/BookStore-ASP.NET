using System;
using BookStore.Api.Dtos.AuthorDtos;
using BookStore.Api.Entity;

namespace BookStore.Api.Mappings;

public static class AuthorMapping
{
    public static Author ToAuthorEntity(this CreateAuthorDto author)
    {
        return new Author()
        {
            AuthorName = author.name,
            Age = author.age
        };
    }

    public static Author ToAuthorEntity(this UpdateAuthorDto author, int id)
    {
        return new Author()
        {
            Id = id,
            AuthorName = author.name,
            Age = author.age
        };
    }

    public static AuthorDto ToAuthorDto(this Author author)
    {
        return new(
            author.Id,
            author.AuthorName,
            author.Age
        );
    }
}
