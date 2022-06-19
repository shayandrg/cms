namespace Cms.Data.Entities.Users;

public class Admin
{
    public int Id { get; set; }
    
    public string EmailAddress { get; set; }
    
    public string PasswordHash { get; set; }

    public DateTime RegistredAt { get; set; }

    public bool IsActive { get; set; }
}