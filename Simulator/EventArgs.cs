

namespace Simulator;


public class ReportStartEventArgs : EventArgs
{
    public BO.Order Order { get; set; }
    public DateTime EndTime { get; set; }

    public DateTime StartTime { get; set; }

    public ReportStartEventArgs(DateTime endTime, BO.Order order, DateTime startTime)
    {
        Order = order;
        EndTime = endTime;
        StartTime = startTime;
    }
}

public class ReportEndEventArgs : EventArgs
{
    public BO.Enums.OrderStatus NewStatus { get; set; }

    public ReportEndEventArgs(BO.Enums.OrderStatus status)
    {
        NewStatus = status;
    }
}

public class ReportProcessEventArgs : EventArgs
{
    public double ProcessValue { get; set; }

    public ReportProcessEventArgs(double val)
    {
        ProcessValue = val;
    }
}

public class ReportErrorEventArgs : EventArgs
{
    public Exception Error { get; set; }

    public ReportErrorEventArgs(Exception e)
    {
        Error = e;
    }
}






