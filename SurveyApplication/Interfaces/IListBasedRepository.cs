using SurveyApplication.Dtos.ListBasedDtos;

namespace SurveyApplication.Interfaces;
public interface IListBasedRepository
{
    Task AddList(ListBasedQuestions list);
    Task AddListValue();
    Task SaveAnswer();
}
