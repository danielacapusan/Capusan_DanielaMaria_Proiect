using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
using BloodBankM;
namespace Capusan_DanielaMaria_Proiect
{
    /// <summary>
    /// Interaction logic for Donatii.xaml
    /// </summary>
    enum ActionState
    {
        New, 
        Edit,
        Nothing
    }
    public partial class Donatii: Window
    {
        ActionState action = ActionState.Nothing;
        BloodBankEntitiesModel em = new BloodBankEntitiesModel();
        CollectionViewSource donatieViewSource,stocViewSource;
        Binding txtAdresaBinding = new Binding();
        Binding txtIdDonatie = new Binding();
        Binding txtIdMedic = new Binding();
        Binding txtDataRecoltariiBinding = new Binding();
        Binding txtGrupaSanguinaBinding = new Binding();
        Binding txtNumePrenumeBinding = new Binding();
        Binding txtTelefonBinding = new Binding();
        Binding txtCantitateBinding= new Binding();
        Binding txtGrupaSanguinaStocBinding = new Binding();
        Binding txtIdDonatorStocBinding = new Binding();
        Binding txtIdTermen = new Binding();
        public Donatii()
        {
            InitializeComponent();
            DataContext = this;
            txtAdresaBinding.Path = new PropertyPath("adresa");
            txtDataRecoltariiBinding.Path = new PropertyPath("data_recoltarii");
            txtGrupaSanguinaBinding.Path = new PropertyPath("grupa_sanguina");
            txtNumePrenumeBinding.Path = new PropertyPath("nume_prenume");
            txtTelefonBinding.Path = new PropertyPath("telefon");
            txtIdDonatie.Path = new PropertyPath("id_donatie");
            txtIdMedic.Path = new PropertyPath("id_medic");
            txtCantitateBinding.Path = new PropertyPath("cantitate");
            txtGrupaSanguinaBinding.Path = new PropertyPath("grupa");
            txtIdDonatorStocBinding.Path = new PropertyPath("id_donator");
            cantitateTextBox.SetBinding(TextBox.TextProperty, txtCantitateBinding);
            grupaTextBox.SetBinding(TextBox.TextProperty, txtGrupaSanguinaBinding);
            id_donatorTextBox.SetBinding(TextBox.TextProperty, txtGrupaSanguinaStocBinding);
            termenDatePicker.SetBinding(TextBox.TextProperty, txtIdTermen);
            adresaTextBox.SetBinding(TextBox.TextProperty, txtAdresaBinding);
            data_recoltariiDatePicker.SetBinding(TextBox.TextProperty, txtDataRecoltariiBinding);
            grupa_sanguinaTextBox.SetBinding(TextBox.TextProperty, txtGrupaSanguinaBinding);
            nume_prenumeTextBox.SetBinding(TextBox.TextProperty, txtNumePrenumeBinding);
            telefonTextBox.SetBinding(TextBox.TextProperty, txtTelefonBinding);
            id_donatieTextBox.SetBinding(TextBox.TextProperty, txtIdDonatie);
            id_medicTextBox.SetBinding(TextBox.TextProperty, txtIdMedic);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            donatieViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("donatieViewSource")));
            donatieViewSource.Source = em.Donaties.Local;
            em.Donaties.Load();
            stocViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stocViewSource")));
            stocViewSource.Source = em.Stocs.Local;
            em.Stocs.Load();  
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Donatie donatie = null;
            if (action == ActionState.New)
            {
                try
                {
                    donatie = new Donatie()
                    {
                        adresa = adresaTextBox.Text.Trim(),
                        grupa_sanguina = grupa_sanguinaTextBox.Text.Trim(),
                        nume_prenume = nume_prenumeTextBox.Text.Trim(),
                        telefon = telefonTextBox.Text.Trim(),
                        data_recoltarii = DateTime.Now,
                        id_medic = MainWindow.idUser
                    };
                    
                    em.Donaties.Add(donatie);
                    donatieViewSource.View.Refresh();
                    em.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Edit)
            {
                try
                {
                    donatie = (Donatie)donatieDataGrid.SelectedItem;
                    donatie.adresa = adresaTextBox.Text.Trim();
                    donatie.grupa_sanguina = grupa_sanguinaTextBox.Text.Trim();
                    donatie.nume_prenume = nume_prenumeTextBox.Text.Trim();
                    donatie.telefon = telefonTextBox.Text.Trim();
                    em.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                donatieViewSource.View.Refresh();
                donatieViewSource.View.MoveCurrentTo(donatie);
            }


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnAdd.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnCancel.IsEnabled = false;

            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            donatieDataGrid.IsEnabled = false;
            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = false;
            adresaTextBox.IsEnabled = true;
            data_recoltariiDatePicker.SelectedDate = DateTime.Now;
            grupa_sanguinaTextBox.IsEnabled = true;
            nume_prenumeTextBox.IsEnabled = true;
            telefonTextBox.IsEnabled = true;
            id_medicTextBox.Text = MainWindow.idUser.ToString();
            BindingOperations.ClearBinding(adresaTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(grupa_sanguinaTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(nume_prenumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(telefonTextBox, TextBox.TextProperty);
            adresaTextBox.Text = "";
            grupa_sanguinaTextBox.Text = "";
            nume_prenumeTextBox.Text = "";
            telefonTextBox.Text = "";
            Keyboard.Focus(adresaTextBox);
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            donatieViewSource.View.MoveCurrentToPrevious();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            donatieViewSource.View.MoveCurrentToNext();
        }

        private void btnRez_Click(object sender, RoutedEventArgs e)
        {
            Stoc stoc = new Stoc();
            try
            {
                stoc = (Stoc)stocDataGrid.SelectedItem;
                em.Stocs.Remove(stoc);
                em.SaveChanges();
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
            stocViewSource.View.Refresh();
        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            string tempAdresa = adresaTextBox.Text.ToString();
            string tempGrupaSanguina = grupa_sanguinaTextBox.Text.ToString();
            string tempNumePrenume = nume_prenumeTextBox.Text.ToString();
            string tempTelefon = telefonTextBox.Text.ToString();

            btnAdd.IsEnabled = false;
            btnEdit.IsEnabled = false;

            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            donatieDataGrid.IsEnabled = false;
            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = false;
            adresaTextBox.IsEnabled = true;
            grupa_sanguinaTextBox.IsEnabled = true;
            nume_prenumeTextBox.IsEnabled = true;
            telefonTextBox.IsEnabled = true;
            BindingOperations.ClearBinding(adresaTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(grupa_sanguinaTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(nume_prenumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(telefonTextBox, TextBox.TextProperty);
            adresaTextBox.Text = tempAdresa;
            grupa_sanguinaTextBox.Text = tempGrupaSanguina;
            nume_prenumeTextBox.Text = tempNumePrenume;
            telefonTextBox.Text = tempTelefon;
            Keyboard.Focus(adresaTextBox);
        }

        private void btnTAdd_Click(object sender, RoutedEventArgs e)
        {
            Stoc stoc = null;
            if (action == ActionState.New)
            {
                try
                {
                    stoc = new Stoc()
                    {
                        cantitate = int.Parse(cantitateTextBox.Text.Trim()),
                        grupa = grupa_sanguinaTextBox.Text.Trim(),
                        termen=DateTime.Now
                    };

                    em.Stocs.Add(stoc);
                    stocViewSource.View.Refresh();
                    em.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnAdd.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            donatieDataGrid.IsEnabled = true;
            btnPrev.IsEnabled = true;
            btnNext.IsEnabled = true;
            adresaTextBox.IsEnabled = false;
            grupa_sanguinaTextBox.IsEnabled = false;
            nume_prenumeTextBox.IsEnabled = false;
            telefonTextBox.IsEnabled = false;
            adresaTextBox.SetBinding(TextBox.TextProperty, txtAdresaBinding);
            grupa_sanguinaTextBox.SetBinding(TextBox.TextProperty, txtGrupaSanguinaBinding);
            nume_prenumeTextBox.SetBinding(TextBox.TextProperty, txtNumePrenumeBinding);
            telefonTextBox.SetBinding(TextBox.TextProperty, txtTelefonBinding);
        }
    }
}
