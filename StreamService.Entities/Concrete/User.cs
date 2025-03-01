﻿using System;
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
    [Required]
    public string Email { get; set; } = default!;

    [Required]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters.")]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character."
    )]
    public string Password { get; set; } = default!;
    public Membership Membership { get; set; }

    public List<StreamInformation> StreamInformations { get; set; }
}
