using FluentAssertions;
using Moq;
using SurveyApplication.Dtos.SurveyDtos;
using SurveyApplication.Entities.Survey;
using SurveyApplication.Features.Surveys.Command.CreateSurvey;
using SurveyApplication.Interfaces;
using SurveyApplication.Validations.ApplicationLayer.SurveyValidations;

namespace SurveyApplication.Tests.RepositoryTests;

public class SurveyRepositoryTests
{

    [Fact]
    public async Task CreateSurvey_ShouldThrowException_WhenSurveyAlreadyExists()
    {
        // Arrange - Mock nesnesi ve test verisi oluşturma
        var mockSurveyRepository = new Mock<ISurveyRepository>();

        var testSurvey = GetTestSurveys().First();

        mockSurveyRepository
            .Setup(repo => repo.CreateSurvey(It.IsAny<SurveyDto>()))
            .ThrowsAsync(new Exception("Survey already exists"));

        var validator = new CreateSurveyValidatorApp(mockSurveyRepository.Object);
        var handler = new CreateSurveyCommandHandle(mockSurveyRepository.Object, validator);

        var request = new CreateSurveyCommandRequest(testSurvey.Survey_Title, testSurvey.Finish_Time, testSurvey.Company_Name);

        // Act - Test edilecek metodun çağrılması
        Func<Task> resultAction = async () => await handler.Handle(request, CancellationToken.None);

        // Assert - Sonucun doğrulanması (FluentAssertions ile)
        await resultAction.Should()
                            .ThrowAsync<Exception>()
                            .WithMessage("Survey already exists")
                            .Where(ex => ex is Exception);
    }

    private List<Survey> GetTestSurveys()
    {
        var testSurveys = new List<Survey>
        {
            Survey.Create(1, "Survey1", DateTime.Now, DateTime.Now, 0, "Company1"),
            Survey.Create(2, "Survey2", DateTime.Now, DateTime.Now, 0, "Company2"),
            Survey.Create(3, "Survey3", DateTime.Now, DateTime.Now, 0, "Company3")
        };

        return testSurveys;
    }

}
