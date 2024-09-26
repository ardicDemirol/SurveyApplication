namespace SurveyApplication.Interfaces;
public interface IJWTProvider
{
    string GenerateJWTToken(string email, string role);
}
