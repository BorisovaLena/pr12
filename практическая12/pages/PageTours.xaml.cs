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
using практическая12;

namespace практическая12.pages
{
    /// <summary>
    /// Логика взаимодействия для PageTours.xaml
    /// </summary>
    public partial class PageTours : Page
    {
        List<Tour> listFilter = ClassBase.Base.Tour.ToList();
        public PageTours()
        {
            InitializeComponent();
            listTours.ItemsSource = ClassBase.Base.Tour.ToList();
            List<Type> type = ClassBase.Base.Type.ToList();
            cmbType.Items.Add("Все типы");
            for(int i = 0; i<type.Count; i++)
            {
                cmbType.Items.Add(type[i].Name);
            }
            cmbType.SelectedIndex = 0;
            cmbSort.SelectedIndex = 0; 
        }
        void Filter()
        {
            List<Tour> list = ClassBase.Base.Tour.ToList(); //фильтрация comboBox
            string t = cmbType.SelectedValue.ToString();
            int index = cmbType.SelectedIndex;
            List<TypeOfTour> types = ClassBase.Base.TypeOfTour.Where(z=> z.Type.Name==t).ToList();
            if (index != 0)
            {
                listFilter = new List<Tour>();
                foreach(TypeOfTour tot in types)
                {
                    foreach(Tour tour in list)
                    {
                        if(tour.Id==tot.TourId)
                        {
                            listFilter.Add(tour);
                        }
                    }
                }
            }
            else
            {
                listFilter = ClassBase.Base.Tour.ToList();
            }

            if (!string.IsNullOrWhiteSpace(tbSearch.Text)) //поиск
            {
                listFilter = listFilter.Where(z => z.Name.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }

            if (cbActual.IsChecked == true) //фильтрация CheckBox
            {
                listFilter = listFilter.Where(z => z.IsActual == true).ToList();
            }

            switch(cmbSort.SelectedIndex) //сотрировка по возрастанию и убыванию
            {
                case 1:
                    listFilter.Sort((x, y) => x.Price.CompareTo(y.Price));
                    break;
                case 2:
                    listFilter.Sort((x, y) => x.Price.CompareTo(y.Price));
                    listFilter.Reverse();
                    break;
            }

            listTours.ItemsSource = listFilter;
            int price = 0;
            foreach(Tour tour in listFilter)
            {
                price += (int)tour.Price * tour.TicketCount;
            }
            tbTotalCostOfTours.Text = string.Format("Общая стоимость туров: {0:C2}", price);
            if (listFilter.Count == 0)
            {
                MessageBox.Show("нет записей");
            }
        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cbActual_Checked(object sender, RoutedEventArgs e)
        {
            Filter();
        }
    }
}
