using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Backup
{
    public int BackupId { get; set; }

    public string FilePath { get; set; } = null!;

    public DateTime? BackupDate { get; set; }

    public int PerformedBy { get; set; }

    public virtual User PerformedByNavigation { get; set; } = null!;
}
