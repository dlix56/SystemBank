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


        public MainWindow()
        {
            InitializeComponent();
            var konto = new KontoOsobiste("1234567890", 1000);

            konto.OperacjaWykonana += Konto_OperacjaWykonana;

            bank.AddKonto(konto);


            var konto2 = new KontoOszczednosc("9876543210", 5000, 3.5m);
            konto2.OperacjaWykonana += Konto_OperacjaWykonana;
            bank.AddKonto(konto2);
            //bank.AddKonto(new KontoOsobiste("1234567890", 1000));
            //bank.AddKonto(new KontoOszczednosc("9876543210", 5000, 3.5m));

            //bank.WyswietlAll();
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
                var konto = bank.ZnajdzNumer("1234567890");
                konto!.Wyplac(5000);
            }
            catch (BrakFund ex)
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