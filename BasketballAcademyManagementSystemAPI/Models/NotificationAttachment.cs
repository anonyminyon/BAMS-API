using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class NotificationAttachment
{
    public string AttachmentId { get; set; } = null!;

    public int NotificationId { get; set; }

    public string Url { get; set; } = null!;

    public virtual Notification Notification { get; set; } = null!;
}
