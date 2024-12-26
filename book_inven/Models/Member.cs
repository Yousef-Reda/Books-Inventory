using System;
using System.Collections.Generic;

namespace book_inven.Models;

public partial class Member
{
    public int Mid { get; set; }

    public string Mname { get; set; } = null!;

    public string? Maddress { get; set; }

    public string? Mphone { get; set; }

    public virtual ICollection<Memberaction> Memberactions { get; set; } = new List<Memberaction>();
}
