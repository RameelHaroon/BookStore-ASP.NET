using System;
using BookStore.Api.Dtos;
using BookStore.Api.Entity;

namespace BookStore.Api.Mappings;

public static class BookMapping
{
    public static Book ToBookEntity(this CreateBookDto book)
    {
        return new Book()
        {
            BookName = book.BookName,
            Price = book.Price,
            ReleaseDate = book.ReleaseDate,
            GenreId = book.GenreId,
            AuthorId = book.AuthorId
        };
    }

    public static Book ToBookEntity(this UpdateBookDto book, int id)
    {
        return new Book()
        {
            Id = id,
            BookName = book.BookName,
            Price = book.Price,
            ReleaseDate = book.ReleaseDate,
            GenreId = book.GenreId,
            AuthorId = book.AuthorId
        };
    }

    public static BookSummaryDto ToBookSummaryDto(this Book book)
    {
        return new(
            book.Id,
            book.BookName,
            book.Genre!.GenreName,
            book.Author!.AuthorName,
            book.Price,
            book.ReleaseDate
        );
    }

    public static BookDetailsDto ToBookDetailsDto(this Book book)
    {
        return new(
            book.Id,
            book.BookName,
            book.GenreId,
            book.AuthorId,
            book.Price,
            book.ReleaseDate
        );
    }
}
