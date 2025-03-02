using System;
using StreamService.Core.Entities;
using StreamService.Entities.Enums;

namespace StreamService.Entities.Concrete;

public class Port : BaseEntity
{
    public int Number { get; set; } // Port numarası (4000, 4001, vb.)
    public int MaxUserLimit { get; set; } // Bu port için kullanıcı limiti
    public bool IsActive { get; set; } // Port aktif mi?
    public PortType Type { get; set; } // Port türü (Ingest veya Watch)
    public List<User> Users { get; set; }
}
