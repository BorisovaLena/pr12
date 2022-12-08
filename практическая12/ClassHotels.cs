using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace практическая12
{
    public partial class Hotel
    {
        public int CountTours
        {
            get
            {
                List<HotelOfTour> listHOT = ClassBase.Base.HotelOfTour.Where(z => z.HotelId == Id).ToList();
                int count = listHOT.Count;
                return count;
            }
        }
    }
}
