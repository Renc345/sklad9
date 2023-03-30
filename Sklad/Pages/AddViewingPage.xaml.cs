using Microsoft.Win32;
using sklad.Classes;
using System;
using System.Collections.Generic;
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
using static System.Net.Mime.MediaTypeNames;

namespace sklad.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddViewingPage.xaml
    /// </summary>
    public partial class AddViewingPage : Page
    {
        private Medicament _currentMedicament = new Medicament();
        string imgLoc = "пусто";
        public AddViewingPage(Medicament selectedMedicament)
        {
            InitializeComponent();
            CMBtype.ItemsSource = SkladEntities.GetContext().Type.ToList();
            CMBtype.SelectedValuePath = "IDtype";
            CMBtype.DisplayMemberPath = "NazvanieType";

            if (selectedMedicament != null)
            {
                _currentMedicament = selectedMedicament;
                //txtTitle.Text = "Изменение лекарства";
                //BtnAddOrEdit.Content = "Изменить";
            }
            DataContext = _currentMedicament;
            
        }
        private void BtnAddOrEdit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentMedicament.NazvanieMedicament)) error.AppendLine("Укажите название лекарства");
            if (string.IsNullOrWhiteSpace(_currentMedicament.Price.ToString())) error.AppendLine("Укажите цену лекарства");
            if (string.IsNullOrWhiteSpace(_currentMedicament.IDtype.ToString())) error.AppendLine("Укажите вид лекарства");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (_currentMedicament.IDmedicament == 0)
            {
                SkladEntities.GetContext().Medicament.Add(_currentMedicament);
                try
                {
                    if (imgLoc != null && imgLoc != "пусто")
                    {
                        byte[] img = null;
                        FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        img = br.ReadBytes((int)fs.Length);
                        string filename = imgLoc.Substring(imgLoc.LastIndexOf('\\')+1);
                        _currentMedicament.Image = String.Concat("/Photo/foto/", filename);
                    }
                    if (imgLoc == "пусто") _currentMedicament.Image = null;

                    SkladEntities.GetContext().SaveChanges();
                    Classes.ClassFrame.frmObj.Navigate(new ViewingPage1());
                    MessageBox.Show("Новое лекарство успешно добавлено!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                try
                {
                    if (imgLoc != null && imgLoc != "пусто")
                    {
                        byte[] img = null;
                        FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        img = br.ReadBytes((int)fs.Length);
                        _currentMedicament.Image = imgLoc;
                    }
                    if (imgLoc == "пусто") _currentMedicament.Image = null;

                    SkladEntities.GetContext().SaveChanges();
                    Classes.ClassFrame.frmObj.Navigate(new ViewingPage1());
                    MessageBox.Show("Лекарство успешно изменено!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new ViewingPage1());
        }
        private void ImageLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog
                {
                    Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif|All Files (*.*)|*.*",
                    Title = "Выберите фото/изображение лекарства"
                };
                if (dlg.ShowDialog() == true)
                {
                    imgLoc = dlg.FileName.ToString();
                    imageMedicament.Source = new BitmapImage(new Uri(imgLoc));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ImageDel_Click(object sender, RoutedEventArgs e)
        {
            imageMedicament.Source = new BitmapImage(new Uri(".\\Photo\\unnamed.jpg"));
            imgLoc = "пусто";
        }

        private void TxtNazvanieMedicament_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
