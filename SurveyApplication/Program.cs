using SurveyApplication.Data;
using SurveyApplication.Endpoints;
using SurveyApplication.Helpers;
using SurveyApplication.Interfaces;
using SurveyApplication.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISurveyRepository, SurveyRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<ISingleChoiceRepository, SingleChoiceRepository>();
builder.Services.AddScoped<IMultipleChoiceRepository, MultipleChoiceRepository>();
builder.Services.AddScoped<ITextBasedRepository, TextBasedRepository>();
builder.Services.AddSingleton<IDatabaseConnectionProvider, DatabaseConnectionProvider>();
builder.Services.AddApplicationServices();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.MapSurveyEndpoints();
app.MapQuestionEndpoints();
app.MapSingleChoiceEndpoints();
app.MapMultipleChoiceEndpoints();
app.MapTextBasedEndpoints();

app.Run();




