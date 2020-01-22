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
            new SlotMachineWindow("Limesstrasse" ,PrintTicket).Show();
            new SlotMachineWindow("Landstrasse", PrintTicket).Show();
        }

        private void OnOneMinuteIsOver(object sender, DateTime e)
        {
            Title = $"Parkscheinzentrale,  {e.ToShortTimeString()}";
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            SlotMachineWindow newWindow = new SlotMachineWindow(TextBoxAddress.Text, PrintTicket);
            newWindow.Owner = this;
            newWindow.Show();
        }

        private void PrintTicket(object sender, Ticket ticket)
        {
            TextBlockLog.Text += $"\n  {ticket.TimePrinted.ToShortDateString()} {ticket.TimePrinted.ToShortTimeString()} {ticket.Location}; Parking until: {ticket.EndOfParking}, Paid: {ticket.PaidPrice}c";
        }
    }
}
