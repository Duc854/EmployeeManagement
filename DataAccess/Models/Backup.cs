using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Backup
{
    public int BackupId { get; set; }

    public string FilePath { get; set; } = null!;

    public DateTime? BackupDate { get; set; }

    public int PerformedBy { get; set; }

    public virtual User PerformedByNavigation { get; set; } = null!;
}
