using System;
using BookStore.Api.Data;
using BookStore.Api.Dtos.AuthorDtos;
using BookStore.Api.Entity;
using BookStore.Api.Mappings;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Endpoints;

public static class AuthorEndpoints
{
    public static RouteGroupBuilder MapAuthorEndpoints(this WebApplication app)
    {
        var routeGroup = app.MapGroup("bookstore/authors").WithParameterValidation();


        // GET Endpoints
        routeGroup.MapGet("/", async (BookStoreDbContext dbContext) => await dbContext.Authors
        .Select(author => author
        .ToAuthorDto())
        .AsNoTracking().ToListAsync());

        // GET By Id Endpoint
        routeGroup.MapGet("/v1/{id}", async (int id, BookStoreDbContext dbContext) =>
        {
            Author? author = await dbContext.Authors.FindAsync(id);
            return author is null ? Results.NoContent() : Results.Ok(author.ToAuthorDto());
        }).WithName("GenreGetEndpoint");

        // POST Endpoint
        routeGroup.MapPost("/v1", async (CreateAuthorDto newAuthor, BookStoreDbContext dbContext) =>
        {
            Author author = newAuthor.ToAuthorEntity();

            await dbContext.AddAsync(author);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute("GenreGetEndpoint", new { Id = author.Id }, author.ToAuthorDto());
        });

        // PUT Endpoint
        routeGroup.MapPut("/v1/{id}", async (int id, UpdateAuthorDto updatedAuthor, BookStoreDbContext dbContext) =>
        {
            var existingGenre = await dbContext.Authors.FindAsync(id);
            if (existingGenre is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGenre).CurrentValues.SetValues(updatedAuthor.ToAuthorEntity(id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE Endpoint
        routeGroup.MapDelete("/v1/{id}", async (int id, BookStoreDbContext dbContext) =>
        {
            await dbContext.Authors.Where(author => author.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });
        return routeGroup;
    }
}
