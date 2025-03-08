using FluentAssertions;
using Moq;
using SurveyApplication.Dtos.Account;
using SurveyApplication.Dtos.SurveyDtos;
using SurveyApplication.Entities.Survey;
using SurveyApplication.Features.Surveys.Command.CreateSurvey;
using SurveyApplication.Features.Surveys.Queries.GetSurveyById;
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
            .Throws<Exception>();

        var validator = new CreateSurveyValidatorApp(mockSurveyRepository.Object);
        var handler = new CreateSurveyCommandHandle(mockSurveyRepository.Object, validator);

        var request = new CreateSurveyCommandRequest(testSurvey.Survey_Title, testSurvey.Finish_Time, testSurvey.Company_Name);

        // Act - Test edilecek metodun çağrılması
        Func<Task> resultAction = async () => await handler.Handle(request, CancellationToken.None);

        // Assert - Sonucun doğrulanması (FluentAssertions ile)
        await resultAction.Should()
                            .ThrowAsync<Exception>()
                            .Where(ex => ex is Exception);
    }


    [Fact]
    public async Task GetSurveyById_ShouldGetResponse_WhenSurveyExists()
    {
        var mockSurveyRepository = new Mock<ISurveyRepository>();

        var testSurvey = GetTestSurveys().First();
        var response = new GetSurveyByIdQueryResponse("", DateTime.Now, DateTime.MaxValue, 0);

        mockSurveyRepository
            .Setup(repo => repo.GetSurveyById<GetSurveyByIdQueryResponse>(It.IsAny<int>()))
            .ReturnsAsync(response);


        var handler = new GetSurveyByIdQueryHandle(mockSurveyRepository.Object);
        var request = new GetSurveyByIdQueryRequest(testSurvey.Id);
        var result = await handler.Handle(request, CancellationToken.None);


        result.Should().NotBeNull();
        result.Should().BeOfType<GetSurveyByIdQueryResponse>();
    }


    [Fact]
    public async Task GetSurveyById_ShouldGetResponse_WhenSurveyDoesntExists()
    {
        var mockSurveyRepository = new Mock<ISurveyRepository>();

        var testSurvey = GetTestSurveys().First();
        var response = new GetSurveyByIdQueryResponse("", DateTime.Now, DateTime.MaxValue, 0);

        mockSurveyRepository
            .Setup(repo => repo.GetSurveyById<GetSurveyByIdQueryResponse>(It.IsAny<int>()))
            .ReturnsAsync(response);


        var handler = new GetSurveyByIdQueryHandle(mockSurveyRepository.Object);
        var request = new GetSurveyByIdQueryRequest(testSurvey.Id);
        Func<Task> result = async () => await handler.Handle(request, CancellationToken.None);

        await result.Should().ThrowAsync<Exception>();
        result.Should().NotBeNull();
    }



    #region Test Data
    private static List<Survey> GetTestSurveys()
    {
        var testSurveys = new List<Survey>
        {
            Survey.Create(1, "Survey1", DateTime.Now, DateTime.MaxValue, 0, "Company1"),
            Survey.Create(2, "Survey2", DateTime.Now, DateTime.MaxValue, 0, "Company2"),
            Survey.Create(3, "Survey3", DateTime.Now, DateTime.MaxValue, 0, "Company3")
        };

        return testSurveys;
    }

    #endregion

}
