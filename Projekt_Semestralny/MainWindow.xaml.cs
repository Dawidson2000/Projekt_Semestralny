using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekt_Semestralny
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Konstruktor głownego okno umozliwiającego wybór porządanej czynności. 
        /// Ustawia okno na środku ekranu oraz blokuje możliwośc zmiany jego wielkości.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.CanMinimize;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        //otwiera nowe okno rezerwacji
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reservation reservation = new Reservation();

            reservation.ShowDialog();
        }

        //otwiera nowe okno anulowania rezerwacji
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            CancelReservation cancelReservation = new CancelReservation();

            cancelReservation.ShowDialog();
        }
    }
}
