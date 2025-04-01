namespace BookStore.Api.Dtos;

public record class BookDetailsDto(
    int Id,
    string BookName,
    int GenreId,
    int AuthorId,
    decimal Price,
    DateOnly ReleaseData
);
