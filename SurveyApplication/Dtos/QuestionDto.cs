﻿using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Dtos;
public class QuestionDto
{
    [Key]
    public int Question_Id { get; set; }
    [Required]
    public string Question_Text { get; set; } = string.Empty;
    [Required]
    public int Question_Order { get; set; }
    [Required]
    public char Question_Answer_Required { get; set; }
    [Required]
    public int Survey_Id { get; set; }
    [Required]
    public int Question_Type_Id { get; set; }
}


