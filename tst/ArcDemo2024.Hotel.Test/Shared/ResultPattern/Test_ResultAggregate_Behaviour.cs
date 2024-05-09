using ArcDemo2024.Hotel.Shared.ResultPattern;
using FluentAssertions;

namespace ArcDemo2024.Hotel.Test.Shared.ResultPattern;

public class Test_ResultAggregate_Behaviour
{
    [Fact]
    public void Should_ResultAggregate_ReturnFailureTrue()
    {
        // Arrange
        var r1 = Result<string, Error>.Success("Success");
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
    
    [Fact]
    public void Should_ResultAggregate_CombineAndRetunFailureTrue()
    {
        // Arrange
        var r1 = Result<string, Error>.Success("Success 1");
        var r2 = Result<string, Error>.Failure(new Error("123"));
        var resultAggregate1 = ResultAggregate<string, Error>.From(r1, r2);
        
        var r3 = Result<string, Error>.Success("Success 2");
        var r4 = Result<string, Error>.Success("Success 3");
        var r5 = Result<string, Error>.Success("Success 4");
        var resultAggregate2 = ResultAggregate<string, Error>.From(r3, r4, r5);

        // Act
        var finalResultAggregate = ResultAggregate<string, Error>.Combine(resultAggregate1, resultAggregate2);
        
        // Assert
        finalResultAggregate.IsFailure
                            .Should().BeTrue();
        finalResultAggregate.Errors
                            .Should().HaveCount(1)
                            .And.HaveElementAt(0, r2);
        finalResultAggregate.Successes
                            .Should().HaveCount(4);
    }
}