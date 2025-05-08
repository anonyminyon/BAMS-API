using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class Payment
{
    public string PaymentId { get; set; } = null!;

    public string? TeamFundId { get; set; }

    public string UserId { get; set; } = null!;

    public int Status { get; set; }

    public DateTime? PaidDate { get; set; }

    public string Note { get; set; } = null!;

    public DateTime? DueDate { get; set; }

    public int? PaymentMethod { get; set; }

    public virtual ICollection<PaymentItem> PaymentItems { get; set; } = new List<PaymentItem>();

    public virtual TeamFund? TeamFund { get; set; }

    public virtual Player User { get; set; } = null!;
}
