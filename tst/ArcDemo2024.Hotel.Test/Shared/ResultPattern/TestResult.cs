using FluentAssertions;

namespace ArcDemo2024.Hotel.Shared.ResultPattern;

public class TestResult
{
    [Fact]
    public void Should_Result_ReturnAsSuccess()
    {
        // Arrange
        var expectedValue = "Sucesso!!!";

        // Act
        var result = Result<string, Error>.Success(expectedValue);
        
        // Assert
        result.IsSuccess
              .Should().BeTrue();
        result.Value
              .Should().NotBeNull()
              .And.Be(expectedValue);
        result.Error
              .Should().BeNull();
    }
    
    [Fact]
    public void Should_Result_ReturnAsFailure()
    {
        // Arrange
        var expectedError = new Error("S3X", "Expected failure!");

        // Act
        var result = Result<string, Error>.Failure(expectedError);
        
        // Assert
        result.IsFailure
              .Should().BeTrue();
        result.Value
              .Should().BeNull();
        result.Error
              .Should().NotBeNull()
              .And.Be(expectedError);
    }
}