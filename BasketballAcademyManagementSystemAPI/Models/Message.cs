using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class Message
{
    public string MessageId { get; set; } = null!;

    public string GroupChatId { get; set; } = null!;

    public string CreatedByUserId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string MessageType { get; set; } = null!;

    public DateTime SentDate { get; set; }

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual GroupChat GroupChat { get; set; } = null!;
}
