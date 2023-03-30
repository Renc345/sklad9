using sklad.Classes;
using System;
using System.Collections.Generic;
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
using sklad.Pages;
using Excel = Microsoft.Office.Interop.Excel;

namespace sklad.Pages
{
    /// <summary>
    /// Логика взаимодействия для ViewingPage1.xaml
    /// </summary>
    public partial class ViewingPage1 : Page
    {
        public ViewingPage1()
        {
            InitializeComponent();
            var currentUser = SkladEntities.GetContext().Medicament.ToList();
            LviewMed.ItemsSource = currentUser;
            LviewMed.ItemsSource = SkladEntities.GetContext().Medicament.ToList();

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new Viewing());
        }
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddViewingPage((Medicament)LviewMed.SelectedItem));
        }
        private void BtnResetAll_Click(object sender, RoutedEventArgs e)
        {
            LviewMed.ItemsSource = SkladEntities.GetContext().Medicament.ToList();
        }
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LviewMed.ItemsSource = SkladEntities.GetContext().Medicament.Where(x => x.NazvanieMedicament.ToLower().Contains(TxtSearch.Text.ToLower())).ToList();
        }
        private void CmbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (CmbFiltr.SelectedIndex == 0)
            {
                LviewMed.ItemsSource = SkladEntities.GetContext().Medicament.Where(x => x.IDtype == 1).ToList();
            }
            else
                            if (CmbFiltr.SelectedIndex == 1)
            {
                LviewMed.ItemsSource = SkladEntities.GetContext().Medicament.Where(x => x.IDtype == 2).ToList();
            }
            else
                            if (CmbFiltr.SelectedIndex == 2)
            {
                LviewMed.ItemsSource = SkladEntities.GetContext().Medicament.Where(x => x.IDtype == 3).ToList();
            }
            else
                            if (CmbFiltr.SelectedIndex == 3)
            {
                LviewMed.ItemsSource = SkladEntities.GetContext().Medicament.Where(x => x.IDtype == 4).ToList();
            }


        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new AddViewingPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {// удаление нескольких пользователей
            var usersForRemoving = LviewMed.SelectedItems.Cast<Medicament>().ToList();
            if (MessageBox.Show($"Удалить {usersForRemoving.Count()} пользователей?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)

                try
                {
                    SkladEntities.GetContext().Medicament.RemoveRange(usersForRemoving);
                    SkladEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    LviewMed.ItemsSource = SkladEntities.GetContext().Medicament.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }


        }
        private void BtnSaveToExcel_Click(object sender, RoutedEventArgs e)
        {
            //объект Excel
            var app = new Excel.Application();

            //книга 
            Excel.Workbook wb = app.Workbooks.Add();
            //лист
            Excel.Worksheet worksheet = app.Worksheets.Item[1];
            int indexRows = 1;
            //ячейка
            worksheet.Cells[1][indexRows] = "Номер";
            worksheet.Cells[2][indexRows] = "Название лекарства";
            worksheet.Cells[3][indexRows] = "Цена";
            worksheet.Cells[4][indexRows] = "Тип";

            //список пользователей из таблицы после фильтрации и поиска
            var printItems = LviewMed.Items;
            //цикл по данным из списка для печати
            foreach (Medicament item in printItems)
            {
                worksheet.Cells[1][indexRows + 1] = indexRows;
                worksheet.Cells[2][indexRows + 1] = item.NazvanieMedicament;
                worksheet.Cells[3][indexRows + 1].Value = item.Price.ToString(); ;
                worksheet.Cells[4][indexRows + 1].Value = item.IDtype.ToString(); ;

                indexRows++;
            }
            Excel.Range range = worksheet.Range[worksheet.Cells[2][indexRows + 1],
                    worksheet.Cells[5][indexRows + 1]];
            range.ColumnWidth = 30; //ширина столбцов
            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;//выравнивание по левому краю

            //показать Excel
            app.Visible = true;
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        //Редактирование товаров
        {
            //    ClassFrame.frmObj.Navigate(new BasketPage((sender as Button).DataContext as Order));
        }


        private void buybtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pages.BasketPage((sender as Button).DataContext as Medicament));
        }
    }
}
