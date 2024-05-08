using FluentAssertions;

namespace ArcDemo2024.Hotel.Shared.ResultPattern;

public class Test_ResultAggregate
{
    [Fact]
    public void Should_ResultAggregate_ReturnErrorsList()
    {
        // Arrange
        var r1 = Result<string, Error>.Success("Sucesso!!!");
        var r2 = Result<string, Error>.Failure(new Error("123"));

        // Act
        var resultAggregate = ResultAggregate<string, Error>.From(r1, r2);
        
        // Assert
        IEnumerable<Error> errors = null;
        if (resultAggregate.IsFailure)
        {
            errors = resultAggregate.Errors.Select(x => x.Error);
        }

        errors.Should().NotBeNull();
    }
}