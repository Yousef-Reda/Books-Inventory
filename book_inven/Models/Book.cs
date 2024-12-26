using System;
using System.Collections.Generic;

namespace book_inven.Models;

public partial class Book
{
    public int Bid { get; set; }

    public string Btitle { get; set; } = null!;

    public int Bprice { get; set; }

    public string Btype { get; set; } = null!;

    public virtual ICollection<Memberaction> Memberactions { get; set; } = new List<Memberaction>();
}
