using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using static WpfTartaly.MainWindow;

namespace WpfTartaly
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Tartaly> tartalyok = new ObservableCollection<Tartaly>();
        public MainWindow()
        {
            InitializeComponent();
            rdoTeglatest.IsChecked = true;
            lbTartalyok.ItemsSource = tartalyok;
        }
        private void rdoTeglatest_Checked(object sender, RoutedEventArgs e)
        {
            txtAel.IsEnabled = true;
            txtAel.Clear();
            txtBel.IsEnabled = true;
            txtBel.Clear();
            txtCel.IsEnabled = true;
            txtCel.Clear();
        }

        private void rdoKocka_Checked(object sender, RoutedEventArgs e)
        {
            txtAel.IsEnabled = false;
            txtAel.Text = "10";
            txtBel.IsEnabled = false;
            txtBel.Text = "10";
            txtCel.IsEnabled = false;
            txtCel.Text = "10";
        }

        private void btnFelvesz_Click(object sender, RoutedEventArgs e)
        {
            Tartaly t;
            if (rdoKocka.IsChecked.Value)
            {
                t = new Tartaly(txtNev.Text);
            }
            else
            {
                int a = int.Parse(txtAel.Text);
                int b = int.Parse(txtBel.Text);
                int c = int.Parse(txtCel.Text);
                t = new Tartaly(txtNev.Text, a, b, c);
            }
            tartalyok.Add(t);
        }

        private void btnDuplaz_Click(object sender, RoutedEventArgs e)
        {
            if (lbTartalyok.SelectedItem != null)
            {
                Tartaly t = (Tartaly)lbTartalyok.SelectedItem;
                t.DuplazMeretet();
                lbTartalyok.Items.Refresh();
            }
        }

        private void btnLeenged_Click(object sender, RoutedEventArgs e)
        {
            if (lbTartalyok.SelectedItem != null)
            {
                Tartaly t = (Tartaly)lbTartalyok.SelectedItem;
                t.TeljesLeengedes();
                lbTartalyok.Items.Refresh();
            }
        }

        private void btntolt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double liter = Convert.ToDouble(txtMennyitTolt.Text);
                if (lbTartalyok.SelectedItem != null)
                {
                    Tartaly t = (Tartaly)lbTartalyok.SelectedItem;
                    t.Tolt(liter);
                    lbTartalyok.Items.Refresh();
                }
            }
            catch
            {
                txtMennyitTolt.Text = "";
            }
        }

        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            foreach (Tartaly t in lbTartalyok.Items)
            {
                StreamWriter sw = new StreamWriter("tartaly.txt");
                sw.WriteLine(t.Info());
                sw.Close();
            }
        }

        public class Tartaly

        {
            //todo OK 1.feladat (4p) Az osztály adattagjai a következők: (Minden adattag legyen rejtett!)
            //
            //	nev – karakterlánc(a tartály neve)
            //	a, b, c – int típusú(cm-ben a tartály élhosszai)
            //	aktLiter – double típusú(a tartályban lévő aktuális mennyiség literben)
            string nev;
            int a, b, c;
            double aktLiter;

            //todo 2. feladat (6p) Hozzon létre egy konstruktort, amely 4 paraméterrel rendelkezik(tartály neve és az élek hossza). Ezeket az értékeket adja át a fenti rejtett attribútumoknak.Az aktLiter pedig 0-ra állítja.

            public Tartaly(string nev, int a, int b, int c)
            {
                this.nev = nev;
                this.a = a;
                this.b = b;
                this.c = c;
                this.aktLiter = 0;
            }
            //todo 3. feladat (3p) Fejezze be az elkezdett konstruktort! Ez a konstruktor egy 10x10x10 cm3 kocka alakú üres tartályt hoz létre és a paraméterként kapott nevet adja neki.

            public Tartaly(String nev)
            {
                this.nev = nev;
                this.a = 10;
                this.b = 10;
                this.c = 10;
                this.aktLiter = 0;
            }


            //todo 4.feladat (4p) Fejezze be az elkezdett jellemző(property) készítését.Adja vissza a tartály cm3-ban mért térfogatát.

            public double Terfogat
            {
                get { return this.a * this.b * this.c / 1000d; }
            }

            //todo 5.feladat (2x3p) Készítsen visszatérési érték és paraméter nélküli metódusokat DuplazMeretet és TeljesLeengedes néven.Az DuplazMeretet duplázza a tartály térfogatát valamelyik él értékének változtatásával.A TeljesLeengedes pedig kiüríti a tartályt.

            public void DuplazMeretet()
            {
                this.a *= 2;
            }

            public void TeljesLeengedes()
            {
                this.aktLiter = 0;
            }

            //todo 6.feladat (4p) Készítsen Toltottseg néven double típusú tulajdonságot(property). A tulajdonság visszaadja, hogy %-osan milyen szinten van tele a tartály.

            public double Toltottseg
            {
                get => (this.aktLiter / Terfogat) * 100;
            }

            //todo 7.feladat (6p) Készítsen Tolt néven egyparaméteres visszatéréi érték nélküli metódust.A double paraméterben kapott literrel növeli a tartályban lévő mennyiséget.Amennyiben ez a mennyiség nem fér a tartályba, írjon ki hibaüzenetet és ne hajtsa végre a töltést!

            public void Tolt(double mennyit)
            {
                if (Terfogat < this.aktLiter + mennyit)
                {
                    //todo Ez nem szép! Helyette kivételt kellene dobni!
                    Console.WriteLine("Hiba! Nem lehet ennyit beletölteni!");
                    return;
                }
                this.aktLiter += mennyit;
            }


            public string Info()
            {
                return $"{this.nev}: {this.Terfogat * 1000:n1} cm3 = ({this.Terfogat:n2} liter)," +
                    $" töltöttsége: {this.Toltottseg:n2}%, ({this.aktLiter:n2} liter)," +
                    $" méretei: {this.a}x{this.b}x{this.c} cm";

                /*
                return string.Format("{0}: {1} cm3 = ({6} liter), töltöttsége: {2}%, ({7} liter), méretei: {3}x{4}x{5} cm"
                    , this.nev
                    , this.Terfogat
                    , this.Toltottseg
                    , this.a
                    , this.b
                    , this.c
                    , this.Terfogat / 1000
                    , this.aktLiter
                    );
                */

            }
            public override string ToString()
            {
                return Info();
            }
        }
    }
}
