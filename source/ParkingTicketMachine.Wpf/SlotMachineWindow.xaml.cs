using System;
using System.Windows;
using ParkingTicketMachine.Core;

namespace ParkingTicketMachine.Wpf
{
    /// <summary>
    /// Interaction logic for SlotMachineWindow.xaml
    /// </summary>
    public partial class SlotMachineWindow
    {
        private SlotMachine _slotMachine;
        public SlotMachineWindow(string name, EventHandler<Ticket> ticketReady)
        {
            InitializeComponent();
            Title = name;
            _slotMachine = new SlotMachine();
        }

        private void ButtonInsertCoin_Click(object sender, RoutedEventArgs e)
        {
            FastClock.Instance.IsRunning = false;

            if (ListBoxCoins.SelectedItem != null)
            {
                string currentCoin = ListBoxCoins.SelectedItem.ToString();
                int coin = Convert.ToInt32(currentCoin.Substring(37, 2).Replace(" ", string.Empty));
                int totalAmount = _slotMachine.InsertCoin(coin);
                if (totalAmount < 50)
                {
                    TextBoxTimeUntil.Text = "Minimum Amount is 50c";
                }
                else
                {
                    DateTime timeEndOfParking = _slotMachine.TimeEndOfParking();
                    TextBoxTimeUntil.Text = timeEndOfParking.ToShortTimeString();
                }
            }
            else
            {
                TextBoxTimeUntil.Text = "Inssert Coin";
            }
        }

        private void ButtonPrintTicket_Click(object sender, RoutedEventArgs e)
        {




            MessageBox.Show($"Sie dürfen bis {_slotMachine.EndOfParking} parken");
            
            _slotMachine.DeleteAllInfo();
            FastClock.Instance.IsRunning = true;
            TextBoxTimeUntil.Text = "";
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            _slotMachine.DeleteAllInfo();
            FastClock.Instance.IsRunning = true;
            TextBoxTimeUntil.Text = "";
        }

    }
}
