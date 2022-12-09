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
    /// Логика взаимодействия для PageHotels.xaml
    /// </summary>
    public partial class PageHotels : Page
    {
        ClassPagin pc = new ClassPagin();
        List<Hotel> listHotel = ClassBase.Base.Hotel.ToList();
        public PageHotels()
        {
            InitializeComponent();
            dgHotels.ItemsSource = ClassBase.Base.Hotel.ToList();
            pc.Countlist = listHotel.Count;
            pc.CountPage = 10;
            dgHotels.ItemsSource = listHotel.Skip(0).Take(pc.CountPage).ToList();  // отображаем первые записи в том количестве, которое равно CountPage
            DataContext = pc;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Hotel hotel = ClassBase.Base.Hotel.FirstOrDefault(z => z.Id == index);
            ClassFrame.mainFrame.Navigate(new PageUpdateHotel(hotel));
        }

        private void btnPageTours_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.mainFrame.Navigate(new PageTours());
        }

        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;

            switch (tb.Uid)  // определяем, куда конкретно было сделано нажатие
            {
                case "prev":
                    pc.CurrentPage--;
                    break;
                case "next":
                    pc.CurrentPage++;
                    break;
                case "firstPage":
                    pc.CurrentPage = 1;
                    break;
                case "lastPage":
                    pc.CurrentPage = pc.CountPages;
                    break;
                default:
                    pc.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            dgHotels.ItemsSource = listHotel.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();  // оображение записей постранично с определенным количеством на каждой странице
        }

        private void txtPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                    pc.CountPage = Convert.ToInt32(txtPageCount.Text); // если в текстовом поле есnь значение, присваиваем его свойству объекта, которое хранит количество записей на странице
            }
            catch
            {
                pc.CountPage = 10; // если в текстовом поле значения нет, присваиваем свойству объекта, которое хранит количество записей на странице количество элементов в списке
            }
            pc.Countlist = listHotel.Count;  // присваиваем новое значение свойству, которое в объекте отвечает за общее количество записей
            dgHotels.ItemsSource = listHotel.Skip(0).Take(pc.CountPage).ToList();  // отображаем первые записи в том количестве, которое равно CountPage
            pc.CurrentPage = 1; // текущая страница - это страница 1
        }

        private void btnAddHotel_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.mainFrame.Navigate(new PageUpdateHotel());
        }

        private void btnDeleteHotel_Click(object sender, RoutedEventArgs e)
        {
            if (dgHotels.SelectedItems.Count != 0)
            {
                foreach (Hotel hotel in dgHotels.SelectedItems)
                {
                    List<HotelOfTour> hot = ClassBase.Base.HotelOfTour.Where(z => z.HotelId == hotel.Id).ToList();
                    foreach(HotelOfTour h in hot)
                    {
                        if (h.Tour.IsActual == false)
                        {
                            if(MessageBox.Show("Вы хотите удалить {0}", Name, MessageBoxButton.YesNo)==MessageBoxResult.Yes)
                            {
                                ClassBase.Base.Hotel.Remove(hotel);
                                ClassBase.Base.SaveChanges();
                                MessageBox.Show("Успешное удаление!!!");
                                ClassFrame.mainFrame.Navigate(new PageHotels());
                            }
                        }
                        else
                        {
                            MessageBox.Show("Данный отель не может быть удален!!!");
                        }
                    } 
                }
            }
            else
            {
                MessageBox.Show("Выберите отель для удаления!!!");
            }
        }
    }
}
