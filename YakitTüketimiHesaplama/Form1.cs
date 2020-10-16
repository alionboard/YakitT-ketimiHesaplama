using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YakitTüketimiHesaplama
{
    public partial class Form1 : Form
    {
        BindingList<Eleman> elemanlar = new BindingList<Eleman>();
        public Form1()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");
            InitializeComponent();
            dgvListe.DataSource = elemanlar;
            #region DataGridView Sütun İsimleri
            dgvListe.Columns[0].HeaderText = "Tarih";
            dgvListe.Columns[1].HeaderText = "Mesafe(km)";
            dgvListe.Columns[2].HeaderText = "Ödenen(₺)";
            dgvListe.Columns[3].HeaderText = "Litre Fiyatı(₺)";
            dgvListe.Columns[4].HeaderText = "Ort Tüketim(km/₺)";
            dgvListe.Columns[5].HeaderText = "Ort Tüketim(100km/lt)";
            #endregion

            try
            {
                string json = File.ReadAllText("veri.json");
                Eleman[] okunan = JsonConvert.DeserializeObject<Eleman[]>(json);

                foreach (var item in okunan)
                    elemanlar.Add(item);
            }
            catch (Exception)
            {
            }
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {

            if (txtOdenenTutar.Text == "" || txtMesafe.Text == "" || txtLitreFiyatı.Text == "")
            {
                MessageBox.Show("Tüm alanlar dolu olmalıdır.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            else
            {
                Eleman e1 = new Eleman();
                e1.Tarih = dtpTarih.Value.Date;
                e1.OdenenTutar = double.Parse(txtOdenenTutar.Text);
                e1.Mesafe = double.Parse(txtMesafe.Text);
                e1.LitreFiyatı = double.Parse(txtLitreFiyatı.Text);
                e1.KmOrtTuketim = string.Format("{0:0.00}", e1.OdenenTutar / e1.Mesafe);
                e1.LtOrtTuketim = string.Format("{0:0.00}", double.Parse(e1.KmOrtTuketim) * 100 / e1.LitreFiyatı);
                elemanlar.Add(e1);

                var sortedListInstance = new BindingList<Eleman>(elemanlar.OrderByDescending(x => x.Tarih).ToList());
                elemanlar.Clear();
                foreach (var item in sortedListInstance)
                    elemanlar.Add(item);

                elemanlar.ResetBindings();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Kaydet();
        }

        private void Kaydet()
        {
            string json = JsonConvert.SerializeObject(elemanlar);
            File.WriteAllText("veri.json", json);
            MessageBox.Show("Başarıyla Kaydedildi.", "Kaydet", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnSifirla_Click(object sender, EventArgs e)
        {
            elemanlar.Clear();
            MessageBox.Show("Liste silindi.", "Sıfırla", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var mb = MessageBox.Show("Değişiklikleri kaydetmek istiyor musunuz?", "Değişiklikleri Kaydet", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            switch (mb)
            {
                case DialogResult.Yes:
                    Kaydet();
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;
            }
        }
    }
}
