using System.Text.RegularExpressions;

namespace SurveyApplication.Extensions;
public static class ResponseTypeControl
{
    public static bool ControlResponseType(string response, int textTypeId)
    {
        return textTypeId switch
        {
            1 => IsNumeric(response),
            2 => IsAlphaNumeric(response),
            3 => IsDate(response),
            _ => false,
        };
    }
    public static bool IsNumeric(string input)
    {
        return int.TryParse(input, out _);
    }
    public static bool IsAlphaNumeric(string input)
    {
        return Regex.IsMatch(input, @"^[a-zA-Z0-9]+$");
    }
    public static bool IsDate(string input)
    {
        return DateTime.TryParse(input, out _);
    }


}