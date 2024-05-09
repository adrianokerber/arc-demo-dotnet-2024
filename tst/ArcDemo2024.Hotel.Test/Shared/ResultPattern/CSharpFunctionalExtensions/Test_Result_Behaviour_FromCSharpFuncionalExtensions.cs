using CSharpFunctionalExtensions;
using FluentAssertions;
using Error = ArcDemo2024.Hotel.Shared.ResultPattern.Error;

namespace ArcDemo2024.Hotel.Test.Shared.ResultPattern.CSharpFunctionalExtensions;

public class Test_Result_Behaviour_FromCSharpFuncionalExtensions
{
    [Fact]
    public void Should_Result_ReturnAsSuccess()
    {
        // Arrange
        var expectedValue = "Sucesso!!!";

        // Act
        var result = Result.Success<string, Error>(expectedValue);
        
        // Assert
        result.IsSuccess
              .Should().BeTrue();
        result.Value
              .Should().NotBeNull()
              .And.Be(expectedValue);
    }
    
    [Fact]
    public void Should_Result_ReturnAsFailure()
    {
        // Arrange
        var expectedError = new Error("S3X", "Expected failure!");

        // Act
        var result = Result.Failure<string, Error>(expectedError);
        
        // Assert
        result.IsFailure
              .Should().BeTrue();
        result.Error
              .Should().NotBeNull()
              .And.Be(expectedError);
    }
}