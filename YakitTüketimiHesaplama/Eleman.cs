using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YakitTüketimiHesaplama
{
    public class Eleman
    {
        public DateTime Tarih { get; set; }
        public double Mesafe { get; set; }
        public double OdenenTutar { get; set; }
        public double LitreFiyatı { get; set; }
        public string KmOrtTuketim { get; set; }
        public string LtOrtTuketim { get; set; }

    }
}
