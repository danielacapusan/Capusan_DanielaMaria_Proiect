using BloodBankM;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Capusan_DanielaMaria_Proiect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int idUser;
        BloodBankEntitiesModel em = new BloodBankEntitiesModel();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, RoutedEventArgs e)
        {
            try{
                  var user = (from u in em.Users
                            where u.username == txtuser.Text.Trim() && u.parola == txtparola.Password.Trim()
                            select u).First();
                idUser= user.id_user;
                Donatii donatie = new Donatii();
                donatie.Show();

            }catch(InvalidOperationException ex)
            {
                MessageBox.Show("Datele nu sunt introduse corect.");
            }
        }

        private void btnregister_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
        }
    }
}
