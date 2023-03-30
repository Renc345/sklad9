using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using sklad.Classes;
using sklad.Pages;


namespace sklad.Pages
{
    /// <summary>
    /// Логика взаимодействия для ViewingPage.xaml
    /// </summary>
    public partial class ViewingPage : Page
    {
        public ViewingPage()
        {
            InitializeComponent();
            DGridMedicament.ItemsSource = SkladEntities.GetContext().Medicament.ToList();

        }
        private void BtnResetAll_Click(object sender, RoutedEventArgs e)
        {
            DGridMedicament.ItemsSource = SkladEntities.GetContext().Medicament.ToList();
        }
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGridMedicament.ItemsSource = SkladEntities.GetContext().Medicament.Where(x => x.NazvanieMedicament.ToLower().Contains(TxtSearch.Text.ToLower())).ToList();
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new Viewing());
        }

        private void CmbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (CmbFiltr.SelectedIndex == 0)
            {
                DGridMedicament.ItemsSource = SkladEntities.GetContext().Medicament.Where(x => x.IDtype == 1).ToList();
            }
            else
                            if (CmbFiltr.SelectedIndex == 1)
            {
                DGridMedicament.ItemsSource = SkladEntities.GetContext().Medicament.Where(x => x.IDtype == 2).ToList();
            }
            else
                            if (CmbFiltr.SelectedIndex == 2)
            {
                DGridMedicament.ItemsSource = SkladEntities.GetContext().Medicament.Where(x => x.IDtype == 3).ToList();
            }
            else
                            if (CmbFiltr.SelectedIndex == 3)
            {
                DGridMedicament.ItemsSource = SkladEntities.GetContext().Medicament.Where(x => x.IDtype == 4).ToList();
            }
            
        }
    }
    
}
