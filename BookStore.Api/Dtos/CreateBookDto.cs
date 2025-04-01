using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.Dtos;

public record class CreateBookDto(
    [Required] string BookName,
    [Required][Range(1, 100)] decimal Price,
    int GenreId,
    int AuthorId,
    [Required] DateOnly ReleaseDate
);