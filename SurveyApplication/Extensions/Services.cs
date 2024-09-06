using FluentValidation;
using Hangfire;
using Hangfire.PostgreSql;
using SurveyApplication.Caching;
using SurveyApplication.Data;
using SurveyApplication.Interfaces;
using SurveyApplication.Repository;
using SurveyApplication.Services.Email;
using SurveyApplication.Services.Email.Interfaces;
using SurveyApplication.Validations.MultipleChoiceValidations;
using SurveyApplication.Validations.QuestionValidations;
using SurveyApplication.Validations.SingleChoiceValidations;
using SurveyApplication.Validations.SurveyValidations;
using SurveyApplication.Validations.TextBasedValidations;
using System.Reflection;

namespace SurveyApplication.Extensions;
public static class Services
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ISurveyRepository, SurveyRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<ISingleChoiceRepository, SingleChoiceRepository>();
        services.AddScoped<IMultipleChoiceRepository, MultipleChoiceRepository>();
        services.AddScoped<ITextBasedRepository, TextBasedRepository>();
        services.AddScoped<IQuestionsAndAnswersRepository, QuestionsAndAnswersRepository>();
        services.AddSingleton<IDatabaseConnectionProvider, DatabaseConnectionProvider>();


        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(assembly));

        services.AddValidatorsFromAssemblyContaining<CreateSurveyValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateQuestionValidator>();
        services.AddValidatorsFromAssemblyContaining<AddSCQuestionValidator>();
        services.AddValidatorsFromAssemblyContaining<SaveSCAnswerValidator>();
        services.AddValidatorsFromAssemblyContaining<SetMaxChoiceAmountValidator>();
        services.AddValidatorsFromAssemblyContaining<AddChoicesMCQValidator>();
        services.AddValidatorsFromAssemblyContaining<SaveAnswerMCQValidator>();
        services.AddValidatorsFromAssemblyContaining<TextBasedQuestionsSetRelationValidator>();
        services.AddValidatorsFromAssemblyContaining<TextBasedQuestionsSaveAnswerValidator>();

        services.AddScoped<IGarnetClient, MyGarnetClient>();


        string connectionString = "User ID=postgres;Password=mysecretpassword;Host=localhost;Port=5432;Database=postgres;Pooling=true;";
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(options =>
            {
                options.UseNpgsqlConnection(connectionString);
            }));

        services.AddHangfireServer();
        services.AddTransient<IEmailService, EmailService>();



    }
}
