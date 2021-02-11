using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Data.Entity;

namespace Projekt_Semestralny
{
    /// <summary>
    /// Logika interakcji dla klasy Reservation.xaml
    /// </summary>
    public partial class Reservation : Window
    {
        Dictionary<int, string> sits = new Dictionary<int, string>();

        public Reservation()
        {
            InitializeComponent();

            MiejscaList.SelectionMode = SelectionMode.Multiple;

            using (KinoRezerwacjeEntities context = new KinoRezerwacjeEntities())
            {
                FilmsCombo.ItemsSource = context.filmy.ToList();
                FilmsCombo.DisplayMemberPath = "tytul";
            }               
        }

        private void FilmsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SeanseList.Items.Clear();

            using (KinoRezerwacjeEntities context = new KinoRezerwacjeEntities())
            {
                var item = FilmsCombo.SelectedItem as filmy;
                
                var seanse = context.seanse.Where(s => s.id_filmu==item.id_filmu).ToList();

                foreach (var s in seanse)
                {
                    SeanseList.Items.Add(s.ToString());
                }
            }
        }

        private void SeanseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (KinoRezerwacjeEntities context = new KinoRezerwacjeEntities())
            {
                if (SeanseList.SelectedItem != null)
                {
                    MiejscaList.Items.Clear();
                    sits.Clear();

                    string curItem = SeanseList.SelectedItem.ToString();
                    
                    int id = Int32.Parse(curItem[24].ToString());
                    var miejsca = context.miejsca.Where(m => m.id_sali == id).ToList();                    

                    foreach (var m in miejsca)
                    {
                        sits.Add(m.id_miejsca, m.ToString());
                    }

                    foreach (var s in sits)
                    {
                        MiejscaList.Items.Add(s.Value);
                    }
                }
                else
                    MiejscaList.Items.Clear();
            }
           
        }

        private void MiejscaButton_Click(object sender, RoutedEventArgs e)
        {
            var list = MiejscaList.SelectedItems;
            var p = list[0].ToString().Split(' ').Last();
            Label1.Content = p;
        }

        private void DokonajRezerwacjiButton_Click(object sender, RoutedEventArgs e)
        {
            var list = MiejscaList.SelectedItems;

            string curItem = SeanseList.SelectedItem.ToString();

            using (KinoRezerwacjeEntities context = new KinoRezerwacjeEntities())
            {
                var reservation = new rezerwacje()
                {
                    id_seansu = Int32.Parse(curItem[11].ToString()),
                    typ_rezerwacji = "internetowa",
                    imie_klienta = ImieText.Text,
                    nazwisko_klienta = NazwiskoText.Text,
                    nr_telefonu = NrTelefonuText.Text,
                    czy_oplacone = true,
                    data_dokonania_rezerwacji = DateTime.Now
                };
                context.rezerwacje.Add(reservation);
                context.SaveChanges();

                var id_rezerwacji = context.rezerwacje.Max(r => r.id_rezerwacji);

                int i = 0;

                foreach (var l in list)
                {
                    var reservationSeats = new zarezerwowane_miejsca()
                    {
                        id_rezerwacji = id_rezerwacji,
                        id_seansu = Int32.Parse(curItem[11].ToString()),
                        id_miejsca = sits.FirstOrDefault(s => s.Value.Contains(l.ToString())).Key
                    };
                    context.zarezerwowane_miejsca.Add(reservationSeats);
                    i++;
                }
                
                context.SaveChanges();

            }
        }
    }
}
