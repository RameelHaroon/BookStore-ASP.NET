using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.Dtos.GenreDtos;

public record class UpdateGenreDto(
    [Required] string name
);