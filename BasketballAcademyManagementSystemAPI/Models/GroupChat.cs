using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class GroupChat
{
    public string GroupChatId { get; set; } = null!;

    public string CreatedByUserId { get; set; } = null!;

    public string? TeamId { get; set; }

    public string ConversationName { get; set; } = null!;

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual ICollection<GroupChatMember> GroupChatMembers { get; set; } = new List<GroupChatMember>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual Team? Team { get; set; }
}
