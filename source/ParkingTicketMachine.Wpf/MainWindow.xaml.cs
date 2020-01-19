using ParkingTicketMachine.Core;
using System;
using System.Text;
using System.Windows;

namespace ParkingTicketMachine.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            FastClock clock = FastClock.Instance;
            clock.IsRunning = true;
            clock.OneMinuteIsOver += OnOneMinuteIsOver;
        }

        private void OnOneMinuteIsOver(object sender, DateTime e)
        {
            Title = $"Parkscheinzentrale,  {e.ToShortTimeString()}";
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            SlotMachineWindow newWindow = new SlotMachineWindow(TextBoxAddress.Text, LogTicket);
            newWindow.Show();
        }

        private void LogTicket(object sender, Ticket e)
        {
            new Ticket();
        }
    }
}
