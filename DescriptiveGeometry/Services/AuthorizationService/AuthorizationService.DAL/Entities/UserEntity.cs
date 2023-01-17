using AuthorizationService.DAL.Entities.Enums;
using DG.Core.Entities;

namespace AuthorizationService.DAL.Entities;

public class UserEntity : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}