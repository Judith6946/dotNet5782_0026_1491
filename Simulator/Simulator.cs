
using BO;
using DO;

namespace Simulator;


public class ReportStartEventArgs : EventArgs
{
    public BO.Order Order { get; set; }
    public DateTime EndTime { get; set; }

    public ReportStartEventArgs(DateTime endTime,BO.Order order)
    {
        Order = order;
        EndTime = endTime;
    }
}


public static class Simulator
{

    private static event EventHandler? ReportStart;
    private static event EventHandler? ReportEnd;
    private static event EventHandler? ReportEndSim;

    volatile static bool flagActive;
    private static Random rn=new Random();
    private static BlApi.IBl bl = BlApi.Factory.Get();

    public static void Deactivate() => flagActive = false;

    public static void Activate()
    {
        new Thread(() =>
        {
            flagActive = true;
            while (flagActive)
            {
                int? oldId = bl.Order.GetOldestOrder();
                if (oldId != null)
                {
                    BO.Order order = bl.Order.GetOrder((int)oldId);
                    int delay = rn.Next(15, 20);
                    DateTime tm = DateTime.Now + new TimeSpan(0,0,0,0,delay * 1000);
                    if(ReportStart!=null)
                        ReportStart(order,new ReportStartEventArgs( tm, order)); ///come back!!!!!!!!
                    Thread.Sleep(delay * 1000);
                    bl.Order.UpdateStatus(order.ID);
                    if (ReportEnd != null)
                        ReportEnd(order,new());
                }
                Thread.Sleep(8000);
            }
            if (ReportEndSim != null)
                ReportEndSim(new(), new());
        }).Start();
    }


    #region Register/ Unregister events
    public static void RegisterReportEndSim(EventHandler handler)=>ReportEndSim+=handler;
    public static void RegisterReportEnd(EventHandler handler) => ReportEnd += handler;
    public static void RegisterReportStart(EventHandler handler) => ReportStart += handler;

    public static void UnregisterReportEndSim(EventHandler handler) => ReportEndSim -= handler;
    public static void UnregisterReportEnd(EventHandler handler) => ReportEnd -= handler;
    public static void UnregisterReportStart(EventHandler handler) => ReportStart -= handler;

    #endregion


}
