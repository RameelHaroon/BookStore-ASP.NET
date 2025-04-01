using System;
using BookStore.Api.Data;
using BookStore.Api.Dtos;
using BookStore.Api.Mappings;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Entity;

public static class BookEndpoints
{
    public static RouteGroupBuilder MapBookEndpoints(this WebApplication app)
    {
        var routeGroup = app.MapGroup("bookstore/books").WithParameterValidation();

        // GET All Endpoint
        routeGroup.MapGet("/", async (BookStoreDbContext dbContext) => await dbContext.Books
        .Include(book => book.Genre)
        .Include(book => book.Author)
        .Select(book => book.ToBookSummaryDto())
        .AsNoTracking().ToListAsync());

        // GET By ID Endpoint
        routeGroup.MapGet("/v1/{id}", async (int id, BookStoreDbContext dbContext) =>
        {
            Book? book = await dbContext.Books.FindAsync(id);
            return book is null ? Results.NotFound() : Results.Ok(book.ToBookDetailsDto());
        }).WithName("GetGameEndpoint");

        // POST Endpoint
        routeGroup.MapPost("/v1", async (CreateBookDto newBook, BookStoreDbContext dbContext) =>
        {
            var book = newBook.ToBookEntity();

            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute("GetGameEndpoint", new { Id = book.Id }, book.ToBookDetailsDto());
        });

        // PUT Endpoint
        routeGroup.MapPut("/v1", async (int id, UpdateBookDto book, BookStoreDbContext dbContext) =>
        {
            var existingBook = await dbContext.Books.FindAsync(id);
            if (existingBook is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingBook).CurrentValues.SetValues(book.ToBookEntity(id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE Endpoint
        routeGroup.MapDelete("/v1/{id}", async (int id, BookStoreDbContext dbContext) =>
        {
            await dbContext.Books.Where(book => book.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });

        return routeGroup;
    }
}
