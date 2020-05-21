using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Project_BanHang2.DAO;
using Project_BanHang2.DTO;
using System.Security.Cryptography;
using DevExpress.XtraSplashScreen;
using static Project_BanHang2.FormBanHang;

namespace Project_BanHang2
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public Login()
        {
            InitializeComponent();
            DAO_DMNguoiDung.Instance.ReadConnect_Chung();
        }
        public void LamfMoi()
        {
            txtMatKhau.Text = "";
            txtTenDangNhap.Text = "";
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bntDangNhap_Click(object sender, EventArgs e)
        {
            List<DTO_DMNguoiDung> dsfull = DAO_DMNguoiDung.Instance.GetListNguoiDung();
            if (dsfull != null)
            {
                MD5 mh = MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(txtMatKhau.Text);
                byte[] hash = mh.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X"));
                }
                string matKhauMH = "Q" + sb.ToString();
                var listDangNhap = dsfull.Where(q => q.TaiKhoan == txtTenDangNhap.Text && q.MatKhau == matKhauMH).SingleOrDefault();
                if (listDangNhap != null)
                {
                    if (matKhauMH == listDangNhap.MatKhau && txtTenDangNhap.Text == listDangNhap.TaiKhoan)
                    {
                        BienToanCuc.TaiKhoanG = listDangNhap.TaiKhoan;
                        SplashScreenManager.ShowForm(typeof(SplashScreen1));
                        FormBanHang f = new FormBanHang();
                        SplashScreenManager.CloseForm();
                        //Login f2 = new Login();
                        //f2.Close();
                        this.Hide();
                        f.ShowDialog();
                        this.Close();
                        LamfMoi();
                    }
                    else XtraMessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else XtraMessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else XtraMessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void bntNhapLai_Click(object sender, EventArgs e)
        {
            LamfMoi();
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.KeyCode == Keys.Enter)
            {
                bntDangNhap.PerformClick();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }
    }
}