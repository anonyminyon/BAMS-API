using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class GroupChatMember
{
    public string GroupChatId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime JoinDate { get; set; }

    public virtual GroupChat GroupChat { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
