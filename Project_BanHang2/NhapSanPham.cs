using DevExpress.XtraEditors;
using Project_BanHang2.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_BanHang2
{
    public partial class NhapSanPham : DevExpress.XtraEditors.XtraForm
    {
        public NhapSanPham()
        {
            InitializeComponent();
            LoadNhom();
            txtTen.Focus();
            DAO_DMSanPhamHH.Instance.GetListDMSanPhamHH();
        }
        public void LoadNhom()
        {
            cbNhom.DataSource = DAO_DMNhomHH.Instance.GetListDM_NhomHH();
            cbNhom.ValueMember = "Ma";
            cbNhom.DisplayMember = "Ten";
        }
       
    public void InsertDMSanPhamHH()
        {
            decimal donGia = decimal.Parse(txtDonGia.Text);
            decimal chieuDai = decimal.Parse(txtChieuDai.Text);
            decimal chieuRong = decimal.Parse(txtChieuRong.Text);
            decimal chieuCao = decimal.Parse(txtChieuCao.Text);
            decimal canNawng = decimal.Parse(txtCanNang.Text);
            decimal banKinh = decimal.Parse(txtBanKinh.Text);
           
          //  bool ngungBan = bool.Parse(CheckNgung.Tag);
            string nameHinh;
            string nameHinh2;
            if (pictureBox1.Image != null)
            {
                nameHinh = txtTen.Text +"_"+txtDonGia.Text+ ".jpg";
                string forder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                string pathHinh = System.IO.Path.Combine(forder, "images\\" + nameHinh);
                Image i = pictureBox1.Image;
                i.Save(pathHinh);
                DAO_DMSanPhamHH.Instance.GetListDMSanPhamHH();
                // FileStream stream = new FileStream(Path.Combine(forder, "log\\" + nameHinh), FileMode.Create);
            }
            else { nameHinh = ""; }

            if (pictureBox2.Image != null)
            {
                nameHinh2 = txtTen.Text + "_" + txtDonGia.Text + "_2" + ".jpg";
                string forder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                string pathHinh = System.IO.Path.Combine(forder, "images\\" + nameHinh2);
                Image i = pictureBox2.Image;
                i.Save(pathHinh);
                // FileStream stream = new FileStream(Path.Combine(forder, "log\\" + nameHinh), FileMode.Create);
            }
            else { nameHinh2 = ""; }
            // 
            try
            {
                if (txtTen.Text!="")
                {
                   DAO_DMSanPhamHH.Instance.InsertDMSanPhamHH(txtMa.Text,txtTen.Text,txtTenVietTat.Text,cbNhom.Text,donGia,txtDonVi.Text,txtNoiSX.Text,txtNuocSX.Text,CheckNgung.Checked,txtTenVietTat.Text, chieuCao,chieuDai,chieuRong,banKinh,nameHinh,nameHinh2);
                    XtraMessageBox.Show("Thêm thành công ", "Thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    nameHinh2 = txtTen.Text + "_" + txtDonGia.Text + "_2" + ".jpg";
                    string forder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                    string pathHinh = System.IO.Path.Combine(forder, "images\\" + nameHinh2);
                    //string ma= DAO_DMSanPhamHH.Instance.GetListDMSanPhamHH().Single(q=>q.Ma)
                    //Image i = pictureBox2.Image;
                    //i.Save(pathHinh);
                    RefreshDMSanPhamHH();
                    txtTen.Focus();
                }
                else XtraMessageBox.Show("Tên sản phẩm không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public void RefreshDMSanPhamHH()
        {
            txtBanKinh.Text = "0";
            txtCanNang.Text = "0";
            txtChieuCao.Text = "0";
            txtChieuDai.Text = "0";
            txtChieuRong.Text = "0";
            txtDonGia.Text = "0";
            txtDonVi.Text = "";
            txtMa.Text = "";
            txtNoiSX.Text = "";
            txtNuocSX.Text = "";
            txtTen.Text = "";
            txtTenVietTat.Text = "";
            cbNhom.Text = "";
            CheckNgung.Checked = false;
            pictureBox1.Image = pictureBox1.Image;
            pictureBox2.Image = pictureBox2.Image;
        }

        private void imageEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pic1_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            PictureBox p = sender as PictureBox;
            if (p != null)
            {
                open.Filter = "(*.jpg;*.jpeg;*.bmp;)| *.jpg; *.jpeg; *.bmp";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    p.Image = Image.FromFile(open.FileName);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            PictureBox p = sender as PictureBox;
            if (p != null)
            {
                open.Filter = "(*.jpg;*.jpeg;*.bmp;)| *.jpg; *.jpeg; *.bmp";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    p.Image = Image.FromFile(open.FileName);
                }
            }
        }

        private void NhapSanPham_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            PictureBox p = sender as PictureBox;
            if (p != null)
            {
                open.Filter = "(*.jpg;*.jpeg;*.bmp;*.png;)| *.jpg; *.jpeg; *.bmp; *.png";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    p.Image = Image.FromFile(open.FileName);
                }
            }
        }

        private void bntLuu_Click(object sender, EventArgs e)
        {
            InsertDMSanPhamHH();
        }

        private void bntLamMoi_Click(object sender, EventArgs e)
        {
            RefreshDMSanPhamHH();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            PictureBox p = sender as PictureBox;
            if (p != null)
            {
                open.Filter = "(*.jpg;*.jpeg;*.bmp;*.png;)| *.jpg; *.jpeg; *.bmp; *.png";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    p.Image = Image.FromFile(open.FileName);
                }
            }
        }
    }
}
