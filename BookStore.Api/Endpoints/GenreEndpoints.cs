using System;
using BookStore.Api.Data;
using BookStore.Api.Entity;
using BookStore.Api.Dtos.GenreDtos;
using BookStore.Api.Mappings;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Endpoints;

public static class GenreEndpoints
{
    public static RouteGroupBuilder MapGenreEndpoints(this WebApplication app)
    {
        var routeGroup = app.MapGroup("bookstore/genres").WithParameterValidation();

        // GET Endpoint
        routeGroup.MapGet("/", async (BookStoreDbContext dbContect) => await dbContect.Genres
        .Select(genre => genre.ToGenreDto())
        .AsNoTracking()
        .ToListAsync());

        // GET By Id Endpoint
        routeGroup.MapGet("/v1/{id}", async (int id, BookStoreDbContext dbContext) =>
        {
            Genre? genre = await dbContext.Genres.FindAsync(id);
            return genre is null ? Results.NoContent() : Results.Ok(genre.ToGenreDto());
        }).WithName("GenreGetEndpoint");

        // POST Endpoint
        routeGroup.MapPost("/v1", async (CreateGenreDto newGenre, BookStoreDbContext dbContext) =>
        {
            Genre genre = newGenre.ToGenreEntity();

            await dbContext.AddAsync(genre);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute("GenreGetEndpoint", new { Id = genre.Id }, genre.ToGenreDto());
        });

        // PUT Endpoint
        routeGroup.MapPut("/v1/{id}", async (int id, UpdateGenreDto updatedAuthor, BookStoreDbContext dbContext) =>
        {
            var existingGenre = await dbContext.Genres.FindAsync(id);
            if (existingGenre is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGenre).CurrentValues.SetValues(updatedAuthor.ToGenreEntity(id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE Endpoint
        routeGroup.MapDelete("/v1/{id}", async (int id, BookStoreDbContext dbContext) =>
        {
            await dbContext.Genres.Where(genre => genre.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });

        return routeGroup;
    }
}
