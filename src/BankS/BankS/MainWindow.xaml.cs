using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BankS.Bank;
using BankS.Bank.Wyjatki;

namespace BankS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BankS.Bank.Bank bank = new BankS.Bank.Bank();


        private void DodajKonto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nr = NrKontaBox.Text;
                decimal saldo = decimal.Parse(SaldoBox.Text);
                string typ = (TypKontaBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                Konto konto;

                if (typ == "Osobiste")
                {
                    konto = new KontoOsobiste(nr, saldo);
                }
                else
                {
                    konto = new KontoOszczednosc(nr, saldo, 3.5m);
                }

                konto.OperacjaWykonana += Konto_OperacjaWykonana;

                bank.AddKonto(konto);

                MessageBox.Show("Dodano konto");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            var konto = new KontoOsobiste("1234567890", 1000);

            konto.OperacjaWykonana += Konto_OperacjaWykonana;

            bank.AddKonto(konto);
            var konto3 = new KontoOsobiste("123545450", 1655000);

            konto3.OperacjaWykonana += Konto_OperacjaWykonana;

            bank.AddKonto(konto3);

            var konto2 = new KontoOszczednosc("9876543210", 5000, 3.5m);
            konto2.OperacjaWykonana += Konto_OperacjaWykonana;
            bank.AddKonto(konto2);


            var konto4 = new KontoOszczednosc("9875656650", 6000, 4m);
            konto4.OperacjaWykonana += Konto_OperacjaWykonana;
            bank.AddKonto(konto4);
        }
        private void Konto_OperacjaWykonana(Konto konto, decimal kwota)
        {
            string typ = kwota > 0 ? "Wpłata" : "Wypłata";
            MessageBox.Show($"{typ}: {Math.Abs(kwota)} zł\nSaldo: {konto.Saldo} zł");
        }

        private void PokazKonta_Click(object sender, RoutedEventArgs e)
        {
            OutputBox.Text = "";

            foreach (var konto in bank.Konta)
            {
                OutputBox.Text += $"{konto.NrKonta} | {konto.Saldo} zł\n";
            }
        }

        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            bank.ZapiszDoPliku("bank.xml");
            MessageBox.Show("Zapisano do pliku XML");
        }

        private void Wczytaj_Click(object sender, RoutedEventArgs e)
        {
            bank.WczytajZPliku("bank.xml");
            MessageBox.Show("Wczytano z pliku XML");
        }

        private void Wyplac_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var konto = bank.ZnajdzNumer(NrKontaBox.Text);

                if (konto == null)
                {
                    MessageBox.Show("Nie znaleziono konta");
                    return;
                }

                decimal kwota = decimal.Parse(KwotaBox.Text);
                konto.Wyplac(kwota);

                PokazKonta_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Wplac_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var konto = bank.ZnajdzNumer(NrKontaBox.Text);

                if (konto == null)
                {
                    MessageBox.Show("Nie znaleziono konta");
                    return;
                }

                decimal kwota = decimal.Parse(KwotaBox.Text);
                konto.Wplac(kwota);

                PokazKonta_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Sortuj_Click(object sender, RoutedEventArgs e)
        {
            OutputBox.Text = "";

            foreach (var konto in bank.PobierzSortowane())
            {
                OutputBox.Text += $"{konto.NrKonta} | {konto.Saldo} zł\n";
            }
        }

    }
}