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
        public string KmOrtTuketim => KmOrtTuketimFonk();
        public string LtOrtTuketim => LtOrtTuketimFonk();
        private string KmOrtTuketimFonk()
        {
            return (OdenenTutar / Mesafe).ToString("0.00");
        }
        private string LtOrtTuketimFonk()
        {
            return (100 / (Mesafe / (OdenenTutar / LitreFiyatı))).ToString("0.00");
        }


    }
}
