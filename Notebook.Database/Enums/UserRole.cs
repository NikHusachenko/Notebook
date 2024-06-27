using System.ComponentModel.DataAnnotations;

namespace Notebook.Database.Enums;

public enum UserRole
{
    [Display(Name = "Administrator")]
    Administrator = 1,

    [Display(Name = "Author")]
    Author = 2,

    [Display(Name = "Visitor")]
    Visitor = 3,
}