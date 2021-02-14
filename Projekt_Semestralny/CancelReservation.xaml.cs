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
using System.Windows.Shapes;

namespace Projekt_Semestralny
{
    /// <summary>
    /// Logika interakcji dla klasy CancelReservation.xaml
    /// </summary>
    public partial class CancelReservation : Window
    {
        public CancelReservation()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void DeleteReservation(int iD, int number)
        {
            using (KinoRezerwacjeEntities context = new  KinoRezerwacjeEntities())
            {
                var reservation = context.rezerwacje.Where(r => r.id_rezerwacji == iD).Single();

                if (Int32.Parse(reservation.nr_telefonu) == number)
                {
                    context.zarezerwowane_miejsca.RemoveRange(context.zarezerwowane_miejsca.Where(z => z.id_rezerwacji == iD));

                    context.rezerwacje.Remove(reservation);

                    context.SaveChanges();
                }
                else
                    throw new Exception();

            }
            
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int number;

            if (IdBox.Text.Length != 0 && NrBox.Text.Length == 9 && Int32.TryParse(IdBox.Text, out id) && Int32.TryParse(NrBox.Text, out number))
            {
                try
                {
                    DeleteReservation(id, number);
                }
                catch (Exception)
                {
                    Info infoBox = new Info("Błędne dane!");
                    infoBox.ShowDialog();
                    //MessageBox.Show("Nie udało się!");
                    this.Close();
                    return;
                }

                MessageBox.Show("Anulowano!");
                this.Close();
            }
        }
    }
}
