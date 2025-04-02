using System;
using BookStore.Api.Dtos.GenreDtos;
using BookStore.Api.Entity;

namespace BookStore.Api.Mappings;

public static class GenreMapping
{
    public static Genre ToGenreEntity(this CreateGenreDto genre)
    {
        return new Genre()
        {
            GenreName = genre.name
        };
    }

    public static Genre ToGenreEntity(this UpdateGenreDto genre, int id)
    {
        return new Genre()
        {
            Id = id,
            GenreName = genre.name
        };
    }

    public static GenreDto ToGenreDto(this Genre genre)
    {
        return new(
            genre.Id,
            genre.GenreName
        );
    }
}
