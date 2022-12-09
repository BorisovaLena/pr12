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

namespace практическая12.pages
{
    /// <summary>
    /// Логика взаимодействия для PageUpdateHotel.xaml
    /// </summary>
    public partial class PageUpdateHotel : Page
    {
        Hotel hotel;
        bool add;
        public PageUpdateHotel(Hotel hotel)
        {
            InitializeComponent();
            add = false;
            cmbCount();
            this.hotel = hotel;
            tbName.Text = hotel.Name;
            tbCountOfStars.Text = Convert.ToString(hotel.CountOfStars);
            cmbCountry.SelectedValue = hotel.CountryCode;
            tbDescription.Text = hotel.Description;
        }
        public PageUpdateHotel()
        {
            InitializeComponent();
            add = true;
            cmbCount();
        }

        public void cmbCount()
        {
            cmbCountry.ItemsSource = ClassBase.Base.Country.ToList();
            cmbCountry.SelectedValuePath = "Code";
            cmbCountry.DisplayMemberPath = "Name";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (add==true)
            {
                hotel = new Hotel();
            }
            hotel.Name = tbName.Text;
            if(Convert.ToInt32(tbCountOfStars.Text)>-1 && Convert.ToInt32(tbCountOfStars.Text)<6)
            {

            }
            hotel.CountOfStars = Convert.ToInt32(tbCountOfStars.Text);
            hotel.CountryCode = Convert.ToString(cmbCountry.SelectedValue);
            hotel.Description = tbDescription.Text;
            if (add == true)
            {
                ClassBase.Base.Hotel.Add(hotel);
            }
            ClassBase.Base.SaveChanges();
            if (add == true)
            {
                MessageBox.Show("Успешное добавление записи!!!");
            }
            else
            {
                MessageBox.Show("Успешное изменение записи!!!");
            }
            ClassFrame.mainFrame.Navigate(new PageHotels());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.mainFrame.Navigate(new PageHotels());
        }
    }
}
