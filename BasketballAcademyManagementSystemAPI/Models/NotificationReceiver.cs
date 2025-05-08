using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class NotificationReceiver
{
    public string UserId { get; set; } = null!;

    public int NotificationId { get; set; }

    public bool IsRead { get; set; }

    public DateTime ReadTime { get; set; }

    public virtual Notification Notification { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
