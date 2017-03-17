using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EstiamteChart
{
    public int TotalEstimates { get; set; }

    public int? ApprovedEstimates { get; set; }

    public int? NonApprovedEstimates { get; set; }
}

public class SubmitBillChart
{
    public int TotalLocalPurchased { get; set; }

    public int? ApprovedBills { get; set; }

    public int? NonApprovedBills { get; set; }
}

public class DrawingChart
{
    public int TotalDrawings { get; set; }

    public int? ApprovedDrawings { get; set; }

    public int? NonApprovedDrawings { get; set; }
}
