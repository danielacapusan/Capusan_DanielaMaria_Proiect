using BloodBankM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
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

namespace Capusan_DanielaMaria_Proiect
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        BloodBankEntitiesModel em = new BloodBankEntitiesModel();

        public Register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            User user = null;
            try
            {
                user = new User()
                {
                    nume_prenume = txtNume.Text.Trim(),
                    username = txtUser.Text.Trim(),
                    parola = txtParola.Password.Trim()
                };
                em.Users.Add(user);
                em.SaveChanges();
                MessageBox.Show("Contul a fost creat.");
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
