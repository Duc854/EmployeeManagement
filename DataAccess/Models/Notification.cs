using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public int SenderId { get; set; }

    public int? ReceiverId { get; set; }

    public int? DepartmentId { get; set; }

    public DateTime? SentDate { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Employee? Receiver { get; set; }

    public virtual User Sender { get; set; } = null!;
}
