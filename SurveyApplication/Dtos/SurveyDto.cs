﻿using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Dtos;

public class SurveyDto
{
    [Key]
    public int Survey_Id { get; set; }
    [Required]
    public string Survey_Title { get; set; } = string.Empty;
    //[Required]
    public DateTime Start_Time { get; set; }
    //[Required]
    public DateTime Finish_Time { get; set; }
    //[Required]
    public int Completed_Count { get; set; }
    [Required]
    public string Company_Name { get; set; } = string.Empty;

}
