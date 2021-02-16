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
        private Dictionary<int, string> sits = new Dictionary<int, string>();

        /// <summary>
        /// Konstruktor okna rezerwacji. Ustawia zawartość comboboxa z dostępnymi filmami.
        /// Ustawia okno na środku ekranu oraz blokuje możliwośc zmiany jego wielkości.
        /// </summary>
        public Reservation()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.CanMinimize;

            MiejscaList.SelectionMode = SelectionMode.Multiple;

            using (KinoRezerwacjeEntities context = new KinoRezerwacjeEntities())
            {
                FilmsCombo.ItemsSource = context.filmy.ToList();
                FilmsCombo.DisplayMemberPath = "tytul";
            }

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        //uzupełnia listę dostępnymi seansami po wybraniu danego filmu z listy rozwijanej
        private void FilmsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SeanseList.Items.Clear();

            using (KinoRezerwacjeEntities context = new KinoRezerwacjeEntities())
            {
                var item = FilmsCombo.SelectedItem as filmy;
                
                var seanse = context.seanse.Where(s => s.id_filmu==item.id_filmu).Where(s => s.czas_rozpoczecia > DateTime.Now).ToList();

                foreach (var s in seanse)
                {
                    SeanseList.Items.Add(s.ToString());
                }

                TextDirector.Visibility = Visibility.Visible;
                TextDuration.Visibility = Visibility.Visible;
                TextLanguage.Visibility = Visibility.Visible;
                DirectorLabel.Content = item.nazwisko_rezysera;
                DurationLabel.Content = item.czas_trwania_minuty + " min";
                LanguageLabel.Content = item.jezyk;
            }
        }

        //Uzupełnia listę dostepnych list po wybraniu danego seansu
        private void SeanseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (KinoRezerwacjeEntities context = new KinoRezerwacjeEntities())
            {
                if (SeanseList.SelectedItem != null)
                {
                    MiejscaList.Items.Clear();
                    sits.Clear();
                    NoSitsLabel.Content = "";

                    var curItem = SeanseList.SelectedItem.ToString().Split(' ');
                    
                    int id_sali = Int32.Parse(curItem[5].ToString());
                    int id_seansu = Int32.Parse(curItem[2].ToString());
                    
                    var miejsca = context.miejsca.Where(m => m.id_sali == id_sali).ToList();                    

                    foreach (var m in miejsca)
                    {
                        if(!context.zarezerwowane_miejsca.Where(o => o.id_seansu == id_seansu).Any(z => z.id_miejsca == m.id_miejsca))
                            sits.Add(m.id_miejsca, m.ToString());
                    }

                    foreach (var s in sits)
                    {
                        MiejscaList.Items.Add(s.Value);
                    }

                    if (MiejscaList.Items.Count == 0)
                    {
                        NoSitsLabel.Content = "Brak wolnych miejsc";
                    }

                }
                else
                    MiejscaList.Items.Clear();              
            }
           
        }

        //dodaje nowe rekordy do tabel "rezerwacje" i "zarezerwowane miejsca" 
        private void MakeReservation()
        {
            var list = MiejscaList.SelectedItems;

            var curItem = SeanseList.SelectedItem.ToString().Split(' ');

            using (KinoRezerwacjeEntities context = new KinoRezerwacjeEntities())
            {
                var reservation = new rezerwacje()
                {
                    id_seansu = Int32.Parse(curItem[2].ToString()),
                    typ_rezerwacji = "internetowa",
                    imie_klienta = NameText.Text,
                    nazwisko_klienta = SurnameText.Text,
                    nr_telefonu = NumberText.Text,
                    czy_oplacone = false,
                    data_dokonania_rezerwacji = DateTime.Now
                };
                context.rezerwacje.Add(reservation);
                context.SaveChanges();

                var id_reservation = context.rezerwacje.Max(r => r.id_rezerwacji);

                int i = 0;

                foreach (var l in list)
                {
                    var reservationSeats = new zarezerwowane_miejsca()
                    {
                        id_rezerwacji = id_reservation,
                        id_seansu = Int32.Parse(curItem[2].ToString()),
                        id_miejsca = sits.FirstOrDefault(s => s.Value.Contains(l.ToString())).Key
                    };
                    context.zarezerwowane_miejsca.Add(reservationSeats);
                    i++;
                }
                
                context.SaveChanges();

            }
        }
        
        //Wywołuje metodę anulowania rezerwacji
        private void MakeReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if (MiejscaList.SelectedItems.Count != 0 && NameText.Text.Length != 0 && SurnameText.Text.Length != 0 && NumberText.Text.Length == 9 && Int32.TryParse(NumberText.Text, out int num))
            {
                try
                {
                    MakeReservation();
                }
                catch (Exception)
                {
                    Info infobox = new Info("Błąd rezerwacji!");
                    infobox.ShowDialog();
                    this.Close();
                    return;
                }
                
                ErrorLabel.Content = "";
                using (KinoRezerwacjeEntities context = new KinoRezerwacjeEntities())
                {
                    Info infobox = new Info($"Sukces! Twój numer rezerwacji: {context.rezerwacje.Max(r => r.id_rezerwacji)}");
                    infobox.ShowDialog();
                }                   
                this.Close();
            }
            else
                ErrorLabel.Content = "Wypełnij poprawnie wszystkie pola!";
        }
    }
}
