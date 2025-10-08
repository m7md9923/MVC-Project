using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "UserName can not be null")]
    [MaxLength(50)]
    public string UserName { get; set; } = null!;
    [Required(ErrorMessage = "FirstName can not be null")]
    [MaxLength(50)]  
    public string FirstName { get; set; } = null!;
    [Required(ErrorMessage = "LastName can not be null")]
    [MaxLength(50)]   
    public string LastName { get; set; } = null!;
    [Required(ErrorMessage = "Email can not be null")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = null!;
    public bool IsAgreed { get; set; }
}