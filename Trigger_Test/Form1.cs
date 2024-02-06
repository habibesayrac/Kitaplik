using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Trigger_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       SqlConnection connection= new SqlConnection(@" Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = TestProject; Integrated Security = True");
       void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBLKITAPLAR", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void sayac()
        {
            connection.Open();
            SqlCommand komut = new SqlCommand("Select*from TBLSAYAC", connection);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblSayac.Text = dr[0].ToString();
            }

            connection.Close();
        }
        void temizle()
        {
            TxtAd.Text = "";
            TxtYazar.Text = "";
            TxtSayfa.Text = "";
            TxtYayinevi.Text = "";
            TxtTur.Text = "";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            sayac();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand komut = new SqlCommand("insert into TBLKITAPLAR (AD,YAZAR,SAYFA,YAYINEVI,TUR) values (@p1,@p2,@p3,@p4,@p5)",connection);
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtYazar.Text);
            komut.Parameters.AddWithValue("@p3", TxtSayfa.Text);
            komut.Parameters.AddWithValue("@p4", TxtYayinevi.Text);
            komut.Parameters.AddWithValue("@p5", TxtTur.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Kitap başarıyla eklendi", "Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);

            connection.Close();

            listele();
            sayac();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand komut = new SqlCommand("delete from TBLKITAPLAR where ID=@p1", connection);
            komut.Parameters.AddWithValue("@p1", TxtId.Text);   
            komut.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Kitap sistemden siindi");
            listele(); sayac(); 
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            TxtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtSayfa.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtYayinevi.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtTur.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }
    }
}
