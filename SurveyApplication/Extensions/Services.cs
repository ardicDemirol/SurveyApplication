using FluentValidation;
using Garnet;
using Hangfire;
using Hangfire.PostgreSql;
using SurveyApplication.Caching;
using SurveyApplication.Data;
using SurveyApplication.Interfaces;
using SurveyApplication.Repository;
using SurveyApplication.Services.Email;
using SurveyApplication.Services.Email.Interfaces;
using SurveyApplication.Validations.ApplicationLayer.AccountValidations;
using SurveyApplication.Validations.ApplicationLayer.MultipleChoiceValidations;
using SurveyApplication.Validations.ApplicationLayer.QuestionValidations;
using SurveyApplication.Validations.ApplicationLayer.SingleChoiceValidations;
using SurveyApplication.Validations.ApplicationLayer.SurveyValidations;
using SurveyApplication.Validations.ApplicationLayer.TextBasedValidations;
using SurveyApplication.Validations.PresantationLayer.AccountValidations;
using SurveyApplication.Validations.PresantationLayer.MultipleChoiceValidations;
using SurveyApplication.Validations.PresantationLayer.QuestionValidations;
using SurveyApplication.Validations.PresantationLayer.SingleChoiceValidations;
using SurveyApplication.Validations.PresantationLayer.SurveyValidations;
using SurveyApplication.Validations.PresantationLayer.TextBasedValidations;
using System.Reflection;

namespace SurveyApplication.Extensions;
public static class Services
{
    private static readonly string[] commandLineArgs = ["--config-import-path", "garnet.conf"];

    public static void AddApplicationServices(this IServiceCollection services)
    {
        Task.Run(() =>
        {
            using var server = new GarnetServer(commandLineArgs, cleanupDir: true);
            server.Start();
            Thread.Sleep(Timeout.Infinite);
        });


        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(assembly));


        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(options =>
            {
                //options.UseNpgsqlConnection(Environment.GetEnvironmentVariable("SurveyAppPostgresConnectionString"));
                options.UseNpgsqlConnection("Host=localhost; Username=postgres; Password=mysecretpassword; Database=postgres; Minimum Pool Size=2;Maximum Pool Size=5;Pooling=true; Application Name=survey; Include Error Detail=true; Timeout=1000; CommandTimeout=1000");
            }));


        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ISurveyRepository, SurveyRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<ISingleChoiceRepository, SingleChoiceRepository>();
        services.AddScoped<IMultipleChoiceRepository, MultipleChoiceRepository>();
        services.AddScoped<ITextBasedRepository, TextBasedRepository>();
        services.AddScoped<IQuestionsAndAnswersRepository, QuestionsAndAnswersRepository>();
        services.AddSingleton<IDatabaseConnectionProvider, DatabaseConnectionProvider>();
        services.AddScoped<IGarnetClient, MyGarnetClient>();
        services.AddScoped<IJWTProvider, JWTProvider>();

        services.AddValidatorsFromAssemblyContaining<RegisterValidatorPrs>();
        services.AddValidatorsFromAssemblyContaining<LoginValidatorPrs>();
        services.AddValidatorsFromAssemblyContaining<CreateSurveyValidatorPrs>();
        services.AddValidatorsFromAssemblyContaining<CreateQuestionValidatorPrs>();
        services.AddValidatorsFromAssemblyContaining<SCAddQuestionValidatorPrs>();
        services.AddValidatorsFromAssemblyContaining<SCSaveAnswerValidatorPrs>();
        services.AddValidatorsFromAssemblyContaining<MCQSetMaxChoiceAmountValidatorPrs>();
        services.AddValidatorsFromAssemblyContaining<MCQAddChoicesValidatorPrs>();
        services.AddValidatorsFromAssemblyContaining<MCQSaveAnswerValidatorPrs>();
        services.AddValidatorsFromAssemblyContaining<TBQSetRelationValidatorPrs>();
        services.AddValidatorsFromAssemblyContaining<TBQSaveAnswerValidatorPrs>();


        services.AddScoped<RegisterValidatorApp>();
        services.AddScoped<LoginValidatorApp>();
        services.AddScoped<CreateSurveyValidatorApp>();
        services.AddScoped<CreateQuestionValidatorApp>();
        services.AddScoped<MCQSetMaxAnswerAmountValidatiorApp>();
        services.AddScoped<MCQAddChoiceValidatorApp>();
        services.AddScoped<MCQSaveAnswerValidatorApp>();
        services.AddScoped<SCQAddChoiceValidatorApp>();
        services.AddScoped<SCQSaveAnswerValidatorApp>();
        services.AddScoped<TBSaveAnswerValidatorApp>();
        services.AddScoped<TBSetRelationValidatorApp>();

        services.AddTransient<IEmailService, EmailService>();

        services.AddHangfireServer();

    }
}
