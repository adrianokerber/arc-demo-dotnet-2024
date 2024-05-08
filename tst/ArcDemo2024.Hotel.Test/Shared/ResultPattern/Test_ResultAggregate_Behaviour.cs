using ArcDemo2024.Hotel.Shared.ResultPattern;
using FluentAssertions;

namespace ArcDemo2024.Hotel.Test.Shared.ResultPattern;

public class Test_ResultAggregate_Behaviour
{
    [Fact]
    public void Should_ResultAggregate_ReturnFailureTrue()
    {
        // Arrange
        var r1 = Result<string, Error>.Success("Sucesso!!!");
        var r2 = Result<string, Error>.Failure(new Error("123"));

        // Act
        var resultAggregate = ResultAggregate<string, Error>.From(r1, r2);
        
        // Assert
        resultAggregate.IsFailure
                       .Should().BeTrue();
        resultAggregate.Errors
                       .Should().HaveCount(1)
                       .And.HaveElementAt(0, r2);
    }
}