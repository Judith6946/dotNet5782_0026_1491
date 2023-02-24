using Simulator;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace PL.SimulatorWindows;

/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
public partial class SimulatorWindow : Window
{

    #region Prep stuff needed to remove close button on window
    private const int GWL_STYLE = -16;
    private const int WS_SYSMENU = 0x80000;
    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);




    #endregion

    static private BackgroundWorker bw;

    #region Simulator Events

    private event EventHandler<ReportStartEventArgs> OnStart = (e, args) => { bw.ReportProgress(2, args); };
    private event EventHandler<ReportEndEventArgs> OnEnd = (e, args) => { bw.ReportProgress(3, args); };
    private event EventHandler OnEndSim = (e, args) => { bw.CancelAsync(); };
    private event EventHandler<ReportProcessEventArgs> OnProcess = (e, args) => { bw.ReportProgress(4, args); };
    private event EventHandler<ReportErrorEventArgs> OnError = (e, args) => { bw.ReportProgress(5, args); };

    #endregion

    #region DP 
    public string MyTime
    {
        get { return (string)GetValue(MyTimeProperty); }
        set { SetValue(MyTimeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyTime.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyTimeProperty =
        DependencyProperty.Register("MyTime", typeof(string), typeof(SimulatorWindow), new PropertyMetadata(DateTime.Now.ToLongTimeString()));


    public BO.Order MyOrder
    {
        get { return (BO.Order)GetValue(MyOrderProperty); }
        set { SetValue(MyOrderProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyOrder.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyOrderProperty =
        DependencyProperty.Register("MyOrder", typeof(BO.Order), typeof(SimulatorWindow), null);


    public string StartTime
    {
        get { return (string)GetValue(StartTimeProperty); }
        set { SetValue(StartTimeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for dateTime.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty StartTimeProperty =
        DependencyProperty.Register("StartTime", typeof(string), typeof(SimulatorWindow), new PropertyMetadata(""));

    public string EndTime
    {
        get { return (string)GetValue(EndTimeProperty); }
        set { SetValue(EndTimeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for EndTime.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty EndTimeProperty =
        DependencyProperty.Register("EndTime", typeof(string), typeof(SimulatorWindow), new PropertyMetadata(""));

    #endregion

    public SimulatorWindow()
    {
        InitializeComponent();
        Loaded += SimulatorWindow_Loaded;
        bw = new();
        bw.WorkerReportsProgress = true;
        bw.WorkerSupportsCancellation = true;
        bw.DoWork += Bw_DoWork;
        bw.ProgressChanged += Bw_ProgressChanged;
        bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
        bw.RunWorkerAsync();
    }

    #region BW Methods

    private void Bw_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        Simulator.Simulator.UnregisterReportStart(OnStart);
        Simulator.Simulator.UnregisterReportEnd(OnEnd);
        Simulator.Simulator.UnregisterReportEndSim(OnEndSim);
        Simulator.Simulator.UnregisterReportProcess(OnProcess);
        Simulator.Simulator.UnregisterReportError(OnError);
        this.Close();
    }

    private void Bw_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        int updateType = e.ProgressPercentage;
        switch (updateType)
        {
            case 1:
                updateClock();
                break;
            case 2:
                startOrderUpdate((ReportStartEventArgs?)e.UserState);
                break;
            case 3:
                endOrderUpdate((ReportEndEventArgs?)e.UserState!);
                break;
            case 4:
                updateProcessBar(((ReportProcessEventArgs?)e.UserState!).ProcessValue);
                break;
            case 5:
                MessageBox.Show("Something went wrong, please try again later");
                break;
        }
    }

    private void Bw_DoWork(object? sender, DoWorkEventArgs e)
    {
        Simulator.Simulator.RegisterReportStart(OnStart);
        Simulator.Simulator.RegisterReportEnd(OnEnd);
        Simulator.Simulator.RegisterReportEndSim(OnEndSim);
        Simulator.Simulator.RegisterReportProcess(OnProcess);
        Simulator.Simulator.RegisterReportError(OnError);
        Simulator.Simulator.Activate();

        while (!bw.CancellationPending)
        {
            Thread.Sleep(1000);
            bw.ReportProgress(1); //for the clock
        }
    }

    #endregion


    #region WPF Methods

    /// <summary>
    /// Update the timer.
    /// </summary>
    private void updateClock()
    {
        MyTime = DateTime.Now.ToLongTimeString();

    }

    /// <summary>
    /// Handle end of order update.
    /// </summary>
    private void endOrderUpdate(ReportEndEventArgs args)
    {
        MyOrder = new() { ID = MyOrder.ID, Status = args.NewStatus };
        txtStatus.Background = Brushes.Red;
    }

    /// <summary>
    /// Handle start of order update - display order details.
    /// </summary>
    /// <param name="args">Update details (order and finish time)</param>
    private void startOrderUpdate(ReportStartEventArgs? args)
    {
        if (args != null)
        {
            MyOrder = args.Order;
            txtStatus.Background = Brushes.White;
            StartTime = DateTime.Now.ToLongTimeString();
            EndTime = args.EndTime.ToLongTimeString();
        }
    }

    /// <summary>
    /// Update value of process bar.
    /// </summary>
    /// <param name="v">New value</param>
    private void updateProcessBar(double v)
    {
        prbProcess.Value = v;
    }

    #endregion


    #region WPF events

    private void btnExit_Click(object sender, RoutedEventArgs e)
    {
        btnExit.Visibility = Visibility.Hidden;
        Simulator.Simulator.Deactivate();
    }

    private void SimulatorWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // Code to remove close box from window
        var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
        SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
    }

    #endregion


}