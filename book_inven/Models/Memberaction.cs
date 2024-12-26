using System;
using System.Collections.Generic;

namespace book_inven.Models;

public partial class Memberaction
{
    public int Aid { get; set; }

    public int Bid { get; set; }

    public int Mid { get; set; }

    public DateOnly BorrowDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public virtual Book BidNavigation { get; set; } = null!;

    public virtual Member MidNavigation { get; set; } = null!;
}
