using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StreamService.Core.Entities;

namespace StreamService.Entities.Concrete;

[Index(nameof(Email), IsUnique = true)]
public class User : BaseEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    [EmailAddress]
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public Membership Membership { get; set; }

    public List<StreamInformation> StreamInformations { get; set; }
}
