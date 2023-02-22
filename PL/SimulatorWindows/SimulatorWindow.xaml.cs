using Simulator;
using System;
using System.ComponentModel;
using System.Printing.IndexedProperties;
using System.Threading;
using System.Windows;

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

    BackgroundWorker bw;


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
        throw new NotImplementedException();
    }

    private void Bw_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        int updateType = e.ProgressPercentage;
        switch(updateType)
        {
            case 1:updateClock();
                break;
            case 2:displayOrder((ReportStartEventArgs?)e.UserState);
                break;
            case 3:endOrderUpdate();
                break;
        }
    }

    

    private void Bw_DoWork(object? sender, DoWorkEventArgs e)
    {
        Simulator.Simulator.RegisterReportStart((e,args) => { bw.ReportProgress(2, args); });
        Simulator.Simulator.RegisterReportEnd((e, args) => { bw.ReportProgress(3); });
       // Simulator.Simulator.RegisterReportEndSim();
        Simulator.Simulator.Activate();

        while (!bw.CancellationPending)
        {
            Thread.Sleep(1000);
            bw.ReportProgress(1); //for the clock
        }
    }

    #endregion


    #region WPF Methods
    private void updateClock()
    {
        MyTime = DateTime.Now.ToLongTimeString();

    }

    private void endOrderUpdate()
    {
        MessageBox.Show($"finish updating order {MyOrder.ID}");
    }

    private void displayOrder(ReportStartEventArgs? userState)
    {
        if(userState != null)
        {
            MyOrder = userState.Order;
            StartTime = DateTime.Now.ToLongTimeString();
            EndTime = userState.EndTime.ToLongTimeString();
        }
    }

    #endregion


    #region WPF events

    private void btnExit_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void SimulatorWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // Code to remove close box from window
        var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
        SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
    }

    #endregion


}