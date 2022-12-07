﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace практическая12
{
    public partial class Tour
    {
        public string Actual
        {
            get
            {
                if (IsActual == true) { return "Актуален"; }
                else { return "Не актуален"; }
            }
        }
        public SolidColorBrush ActualColor
        {
            get
            {
                if (IsActual == true) { return Brushes.Green; }
                else { return Brushes.Red; }
            }
        }
        public string PriceFormat
        {
            get
            {
                return string.Format("Цена: {0:C2}", Price);
            }
        }
    }
}
