using AuthorizationService.DAL.Entities.Enums;

namespace AuthorizationService.DAL.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}
