using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class PaymentItem
{
    public int PaymentItemId { get; set; }

    public string PaymentId { get; set; } = null!;

    public string PaidItemName { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Note { get; set; } = null!;

    public virtual Payment Payment { get; set; } = null!;
}
