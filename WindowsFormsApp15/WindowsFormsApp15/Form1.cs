using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
namespace WindowsFormsApp15
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void LoadDataGridView()
        {
            
            string link = "https://localhost:44363/api/sanpham";

            HttpWebRequest request = WebRequest.CreateHttp(link);

            WebResponse response = request.GetResponse();

            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SanPham[]));

            object data = js.ReadObject(response.GetResponseStream());
            SanPham[] arr = data as SanPham[];
            dataGridView1.DataSource = arr;

        }
        public void LoadComboBox()
        {
            string link = "https://localhost:44363/api/danhmuc";
            HttpWebRequest request = WebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js =new DataContractJsonSerializer(typeof(DanhMuc[]));
            object data= js.ReadObject(response.GetResponseStream());
            DanhMuc[] arr1 = data as DanhMuc[];  
            comboBox1.DataSource = arr1;
            comboBox1.ValueMember = "MaDanhMuc";
            comboBox1.DisplayMember = "TenDanhMuc";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            LoadComboBox();

        }

        private void Search_Click(object sender, EventArgs e)
        {
            string madm=txtMaDM.Text;
            string link = "https://localhost:44363/api/sanpham?madm=" + madm;
            HttpWebRequest req = WebRequest.CreateHttp(link);
            WebResponse resp = req.GetResponse();
            DataContractJsonSerializer js =new DataContractJsonSerializer (typeof(SanPham[]));
            object data=js.ReadObject(resp.GetResponseStream());
            SanPham[] arr = data as SanPham[];
            dataGridView1.DataSource = arr;
        }

        private void Thêm_Click(object sender, EventArgs e)
        {
            string postString = string.Format("?ma={0}&ten={1}&gia={2}&madm={3}", txtMaSP.Text, txtTenSP.Text,
            txtDonGia.Text, comboBox1.SelectedValue);
               
            string link = "https://localhost:44363/api/sanpham" + postString;
            HttpWebRequest request = WebRequest.CreateHttp(link);
            request.Method = "POST";

            Stream dataStream=request.GetRequestStream();
            DataContractJsonSerializer js=new DataContractJsonSerializer(typeof(bool));
            object data=js.ReadObject(request.GetResponse().GetResponseStream());
            bool kq = (bool)data;
            if (kq)
            {
                LoadDataGridView();
                MessageBox.Show("Them san pham thanh cong ");
            }
            else
                MessageBox.Show("Them san pham that bai");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            txtMaSP.Text = dataGridView1.Rows[d].Cells[0].Value.ToString();
            txtTenSP.Text = dataGridView1.Rows[d].Cells[1].Value.ToString();
            txtDonGia.Text = dataGridView1.Rows[d].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[d].Cells[3].Value.ToString();
        }

        private void Sửa_Click(object sender, EventArgs e)
        {
            string putString = string.Format("?ma={0}&ten={1}&gia={2}&madm={3}", txtMaSP.Text, txtTenSP.Text,
            txtDonGia.Text, comboBox1.SelectedValue);
            string link = "https://localhost:44363/api/sanpham" + putString;
            HttpWebRequest request= WebRequest.CreateHttp(link);
            request.Method = "PUT";
            Stream dataStream=request.GetRequestStream();
            DataContractJsonSerializer js= new DataContractJsonSerializer(typeof(bool));
            object data = js.ReadObject(request.GetResponse().GetResponseStream());
            bool kq = (bool)data;
            if (kq)
            {
                MessageBox.Show("Sua san pham thanh cong");
                LoadDataGridView();
            }
            else
                MessageBox.Show("Sua san pham that bai");

        }

        private void Xoá_Click(object sender, EventArgs e)
        {

            string masp=txtMaSP.Text;
            string deleteString =string.Format("?id={0}",masp);

            string link = "https://localhost:44363/api/sanpham/" + deleteString;
            HttpWebRequest request= HttpWebRequest.CreateHttp(link);

            request.Method = "DELETE";
            DataContractJsonSerializer js= new DataContractJsonSerializer(typeof(bool));
            object data =js.ReadObject(request.GetResponse().GetResponseStream());
            bool kq = (bool)data;
            if (kq)
            {
                LoadDataGridView();
                MessageBox.Show("Xoa san pham thanh cong ");
            }
        }
    }

   
}

