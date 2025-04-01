namespace BookStore.Api.Dtos;

public record class BookSummaryDto(
    int Id,
    string BookName,
    string Genre,
    string Author,
    decimal Price,
    DateOnly ReleaseData
);