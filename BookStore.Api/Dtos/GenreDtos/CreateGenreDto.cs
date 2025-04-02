using System.ComponentModel.DataAnnotations;
namespace BookStore.Api.Dtos.GenreDtos;

public record class CreateGenreDto(
    [Required] string name
);