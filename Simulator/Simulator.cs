

namespace Simulator;


public static class Simulator
{

    #region events

    private static event EventHandler? ReportStart;
    private static event EventHandler? ReportEnd;
    private static event EventHandler? ReportEndSim;
    private static event EventHandler? ReportProcess;
    private static event EventHandler? ReportError;

    #endregion

    volatile static bool flagActive;
    private static Random rn = new Random();
    private static BlApi.IBl bl = BlApi.Factory.Get();


    #region Deactivate/ Activate thread

    /// <summary>
    /// Deactivate the thread
    /// </summary>
    public static void Deactivate() => flagActive = false;

    /// <summary>
    /// Activate the simulator thread
    /// </summary>
    public static void Activate()
    {
        new Thread(() =>
        {
            flagActive = true;
            while (flagActive)
            {
                //get next order
                int? oldId = null;
                try { oldId = bl.Order.GetOldestOrder(); }
                catch (Exception e) { if (ReportError != null) ReportError(null, new ErrorEventArgs(e)); }

                if (oldId != null)
                {
                    try
                    {
                        BO.Order order = bl.Order.GetOrder((int)oldId);
                        int delay = rn.Next(15, 20);
                        DateTime end = DateTime.Now + new TimeSpan(0, 0, delay);

                        //report start
                        if (ReportStart != null) ReportStart(null, new ReportStartEventArgs(end, order!, DateTime.Now));

                        DateTime start = DateTime.Now;
                        int sec = (end - start).Seconds;
                        while (DateTime.Now < end)
                        {
                            Thread.Sleep(500);
                            int sec2 = (DateTime.Now - start).Seconds;
                            //repoer progress
                            if (ReportProcess != null) ReportProcess(null, new ReportProcessEventArgs((sec2 / (double)sec) * 100));
                        }

                        //report end
                        if (ReportEnd != null) ReportEnd(null, new ReportEndEventArgs(getNextStatus(order.Status)));
                        bl.Order.UpdateStatus(order.ID);
                    }
                    catch (Exception e) { if (ReportError != null) ReportError(null, new ErrorEventArgs(e)); }
                }
                Thread.Sleep(4000);
            }
            //report end simulator
            if (ReportEndSim != null) ReportEndSim(null, new());
        }).Start();
    }

    #endregion

    #region UTILS

    private static BO.Enums.OrderStatus getNextStatus(BO.Enums.OrderStatus? status) => status == BO.Enums.OrderStatus.approved ? BO.Enums.OrderStatus.sent : BO.Enums.OrderStatus.delivered;

    #endregion

    #region Register/ Unregister events
    public static void RegisterReportEndSim(EventHandler handler) => ReportEndSim += handler;
    public static void RegisterReportEnd(EventHandler handler) => ReportEnd += handler;
    public static void RegisterReportProcess(EventHandler handler) => ReportProcess += handler;
    public static void RegisterReportStart(EventHandler handler) => ReportStart += handler;
    public static void RegisterReportError(EventHandler handler) => ReportError += handler;


    public static void UnregisterReportEndSim(EventHandler handler) => ReportEndSim -= handler;
    public static void UnregisterReportEnd(EventHandler handler) => ReportEnd -= handler;
    public static void UnregisterReportStart(EventHandler handler) => ReportStart -= handler;
    public static void UnregisterReportProcess(EventHandler handler) => ReportProcess -= handler;
    public static void UNregisterReportError(EventHandler handler) => ReportError += handler;

    #endregion


}
