using SurveyApplication.Endpoints;
using SurveyApplication.Helpers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.MapQuestionsAndAnswersEndpoints();

app.Run();




