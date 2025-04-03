using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.Dtos.AuthorDtos;

public record class CreateAuthorDto(
    [Required] string name,
    [Required][Range(10, 100)] int age
);