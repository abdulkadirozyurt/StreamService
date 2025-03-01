using StreamService.Core.Entities;
using StreamService.Entities.Enums;

namespace StreamService.Entities.Concrete
{
    public class Role : BaseEntity
    {
        public RoleType Name { get; set; }
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
