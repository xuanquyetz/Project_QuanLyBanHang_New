using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.IO;
using Project_BanHang2.DAO;
using Project_BanHang2.DTO;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System.Globalization;
using System.Security.Cryptography;
using DevExpress.XtraReports.UI;

namespace Project_BanHang2
{
    public partial class FormBanHang : DevExpress.XtraBars.TabForm
    {
        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
        #region Method
        public FormBanHang()
        {
            InitializeComponent();
            ReadConnect();
            LoadKVB();
            LoadDMBan2();
            LoadDMKhuVuc2();
            PhanQuyenSuDung();
            // SearchHH();
        }
        public static class BienToanCuc
        {
            private static string ipSV;
            private static string dataBaseName;
            private static string user;
            private static string pass;
            private static string taiKhoanG;

            public static string IpSV
            {
                get
                {
                    return ipSV;
                }

                set
                {
                    ipSV = value;
                }
            }

            public static string DataBaseName
            {
                get
                {
                    return dataBaseName;
                }

                set
                {
                    dataBaseName = value;
                }
            }

            public static string User
            {
                get
                {
                    return user;
                }

                set
                {
                    user = value;
                }
            }

            public static string Pass
            {
                get
                {
                    return pass;
                }

                set
                {
                    pass = value;
                }
            }

            public static string TaiKhoanG
            {
                get
                {
                    return taiKhoanG;
                }

                set
                {
                    taiKhoanG = value;
                }
            }
        }
        private void PhanQuyenSuDung()
        {
            string taiKhoan = BienToanCuc.TaiKhoanG;
            lbTaiKhoanG.Text = taiKhoan;
            List<DTO_DMNguoiDung> dsnguoidung = DAO_DMNguoiDung.Instance.GetListNguoiDung();
            List<DTO_DMNhanSu> dsnhansu = DAO_DMNhanSu.Instance.GetListDM_NhanSu();
            var dsfull = dsnguoidung.Join(dsnhansu,
                o => o.MaNhanSu,
                q => q.Ma,
                (o, q) => new
                {
                    MaNhanSu = o.MaNhanSu,
                    Ten = q.Ten,
                    TaiKhoan = o.TaiKhoan,
                    MatKhau = o.MatKhau,
                    QuyenSuDung = o.QuyenSuDung,
                    NgungSuDung = o.NgungSuDung
                }
                );
            var listnguoidunghientai = dsfull.Where(q => q.TaiKhoan == taiKhoan).SingleOrDefault();
            if (listnguoidunghientai != null)
            {
                lbTenNhanSu.Text = listnguoidunghientai.Ten;
                switch (listnguoidunghientai.QuyenSuDung)
                {
                    case "Nhân Viên":
                        backstageViewTabItem4.Visible = false;
                        backstageViewTabItem9.Visible = false;
                        DM_NhomHH.Visible = false;
                        backstageViewTabItem7.Visible = false;
                        backstageViewTabItem8.Visible = false;
                        backstageViewTabItem9.Visible = false;
                        backstageViewTabItem5.Visible = false;
                        backstageViewTabItem10.Visible = false;
                        break;
                    default:
                        break;
                }
            }
        }
        private static class LoadDM_NhomHHFull
        {
            private static List<DTO_DMNhomHH> listfull = DAO_DMNhomHH.Instance.GetListDM_NhomHH();

            internal static List<DTO_DMNhomHH> Listfull
            {
                get
                {

                    return listfull;
                }

                set
                {
                    listfull = value;
                }
            }
        } //Tạo hàm xài chung trong class
        private void SearchHH()
        {
            searchHangHoa.Visible = true;
            //var list=  DAO_DMSanPhamHH.Instance.GetListDMSanPhamHH().Where(q => q.NgungTheoDoi == false);
            //searchLookUpEdit1View.DataSource=
            string forder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            List<DTO_DMSanPhamHH> dsfull = DAO_DMSanPhamHH.Instance.GetListDMSanPhamHH();
            var ds2 = dsfull.Select(q => { q.TenHinh = System.IO.Path.Combine(forder, "images\\" + q.TenHinh); q.TenHinh1 = System.IO.Path.Combine(forder, "images\\" + q.TenHinh1); return q; }).ToList();
            searchHangHoa.Properties.DataSource = ds2;
            searchHangHoa.Properties.ValueMember = "Ma";
            searchHangHoa.Properties.DisplayMember = "Ma";
            // searchHangHoa.Properties.DataSource = DAO_DMSanPhamHH.Instance.GetListDMSanPhamHH().Where(q => q.NgungTheoDoi == false);
        }
        public void ReadConnect()
        {
            try
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

                if (!Directory.Exists(path + @"\log"))
                {
                    Directory.CreateDirectory(path + @"\log");
                }
                FileStream streamCLS = new FileStream(Path.Combine(path, "log\\config.txt"), FileMode.Open);
                StreamReader readCLS = new StreamReader(streamCLS, Encoding.Unicode);
                var chuoicls = readCLS.ReadLine();
                string[] araylistchuoi = chuoicls.Split(new char[] { '\t' });
                BienToanCuc.IpSV = araylistchuoi[0];
                BienToanCuc.DataBaseName = araylistchuoi[1];
                BienToanCuc.User = araylistchuoi[2];
                BienToanCuc.Pass = araylistchuoi[3];
                readCLS.Close();
                streamCLS.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        public void LoadDM_NhomHH()
        {
            List<DTO_DMNhomHH> dsfull = DAO_DMNhomHH.Instance.GetListDM_NhomHH();
            gridControl1.DataSource = dsfull;
        }
        public void LoadBillDaThu(DateTime? tuNgay, DateTime? denNgay)
        {
            var listBan = DAO_DMBan.Instance.GetListDMBan();
            var listBillDaThu = DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.TrangThai == "Da_Thu" && q.Ngay >= tuNgay && q.Ngay <= denNgay);
            gridControl7.DataSource = listBillDaThu.Join(listBan,
                o => o.MaBan,
                p => p.Ma,
                (o, p) => new
                {
                    Ten = p.Ten,
                    SoPhieu = o.SoPhieu,
                    Ngay = o.Ngay,
                    ThanhToan = o.ThanhToan
                }).OrderByDescending(q => q.Ngay);
        }
        public void LoadBillChoThanhToan()
        {

            //string strTongHoaDon = lbTongChiPhi.Text;
            try
            {
                var listPT = DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.MaBan == lbMaBan.Text && q.TrangThai == "Chua_Thu").SingleOrDefault();
                if (listPT != null)
                {
                    flBan.Controls.Clear();
                    pnTimKiem.Controls.Clear();
                    flKhuVuc.Controls.Clear();
                    flKhuVuc.Controls.Add(flTieuDeBill);
                    //  flKhuVuc.Controls.Add(flTieuDe2);
                    flBan.Controls.Add(pnThanhToan);
                    // flFood.Visible = false;
                    flFood2.Visible = true;
                    flBill.Visible = true;
                    button1.Visible = false;
                    // gridControl6.Enabled = false;
                    bntThanhToan.Enabled = false;
                    bntGiaoHang.Enabled = false;
                    lbTenBan2.Text = lbTenBan.Text;
                    txtBillTongHoaDon.Text = lbTongChiPhi.Text;
                    decimal PhiPhuThu = 0;
                    decimal TongGiam = 0;
                    decimal TongTang = 0;
                    decimal TienKhachDua = 0;
                    txtBillPhiPhuThu.Text = "0";
                    txtBillTongTang.Text = "0";
                    txtBillTongGiam.Text = "0";
                    txtBillTienKhachDua.Text = "0";
                    decimal TongHoaDon = decimal.Parse(lbTongChiPhi.Text);
                    PhiPhuThu = decimal.Parse(txtBillPhiPhuThu.Text);
                    TongGiam = decimal.Parse(txtBillTongGiam.Text);
                    TongTang = PhiPhuThu + decimal.Parse(txtBillTongTang.Text);
                    decimal ThanhTien = TongHoaDon - TongGiam + TongTang;
                    TienKhachDua = decimal.Parse(txtBillTienKhachDua.Text);
                    decimal TienThoiLai = TienKhachDua - ThanhTien;
                    txtBillThanhTien.Text = ThanhTien.ToString();
                    txtBiilThanhTienLamTron.Text = ThanhTien.ToString();
                    txtBillTienThoiLai.Text = TienThoiLai.ToString();
                    txtBillSoBienLai.Text = listPT.SoPhieu;
                }
                else XtraMessageBox.Show("Vui lòng chọn bàn CÓ KHÁCH để thanh toán", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception ex)
            {

                MessageBox.Show("Vui lòng kiểm tra lại dữ liệu phiếu thu");
            }

        }
        public void InsertDM_NhomHH()
        {
            if (txtMaNhom.Text != "")
            {
                string ma = txtMaNhom.Text;
                if (DAO_DMNhomHH.Instance.GetListDM_NhomHH().Count(q => q.Ma == ma) <= 0)
                {
                    DAO_DMNhomHH.Instance.InsertDMNhomHH(txtMaNhom.Text, txtTenNhom.Text, txtPhanLoai.Text);
                    XtraMessageBox.Show("Thêm thành công " + ma, "Thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshDM_NhomHH();
                    txtMaNhom.Focus();
                }
                else XtraMessageBox.Show("Mã " + ma + " đã tồn tại trong danh sách", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else XtraMessageBox.Show("Mã nhóm HH không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void RefreshDM_NhomHH()
        {
            txtMaNhom.Text = "";
            txtTenNhom.Text = "";
            txtPhanLoai.Text = "";
        }
        public void LoadDMSanPhamHH()
        {
            string forder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            List<DTO_DMSanPhamHH> dsfull = DAO_DMSanPhamHH.Instance.GetListDMSanPhamHH();
            var ds2 = dsfull.Select(q => { q.TenHinh = System.IO.Path.Combine(forder, "images\\" + q.TenHinh); q.TenHinh1 = System.IO.Path.Combine(forder, "images\\" + q.TenHinh1); return q; }).ToList();
            gridControl2.DataSource = ds2;
        }
        public void LoadDM_NhanSu()
        {
            List<DTO_DMNhanSu> dsfull = DAO_DMNhanSu.Instance.GetListDM_NhanSu();
            gridControl3.DataSource = dsfull;
        }
        public void LoadDM_NguoiDung()
        {
            List<DTO_DMNguoiDung> dsnguoidung = DAO_DMNguoiDung.Instance.GetListNguoiDung();
            List<DTO_DMNhanSu> dsnhansu = DAO_DMNhanSu.Instance.GetListDM_NhanSu();
            var dsfull = dsnguoidung.Join(dsnhansu,
                o =>o.MaNhanSu,
                q=>q.Ma,
                (o,q) => new
                {
                    MaNhanSu=o.MaNhanSu,
                    Ten=q.Ten,
                    TaiKhoan=o.TaiKhoan,
                    MatKhau=o.MatKhau,
                    QuyenSuDung=o.QuyenSuDung,
                    NgungSuDung=o.NgungSuDung
                }
                );
            gridControl8.DataSource = dsfull;
        }
        public void RefreshDMNhanSu()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtPhanLoaiNV.Text = "";
        }
        public void InsertDM_NhanSu()
        {
            if (txtMaNV.Text != "")
            {
                if (DAO_DMNhanSu.Instance.GetListDM_NhanSu().Count(q => q.MaSoNV == txtMaNV.Text) <= 0)
                {
                    DAO_DMNhanSu.Instance.Insert_DMNhanSu(txtMaNV.Text, txtTenNV.Text, txtDiaChi.Text, txtSDT.Text, cbLoai.Text);
                    XtraMessageBox.Show("Thêm thành công ", "Thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshDMNhanSu();
                    txtMaNV.Focus();
                    LoadDM_NhanSu();
                }
                else XtraMessageBox.Show("Mã NV " + txtMaNV.Text + " đã tồn tại trong danh sách", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else XtraMessageBox.Show("Mã NV không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void RefreshDMBan()
        {
            txtMaB.Text = "";
            txtGhiChuB.Text = "";
            txtTenB.Text = "";
            chVip.Checked = false;
            chTrangThai.Checked = false;
            cbKhucVucB.Text = "";
        }
        public void LoadDMBan()
        {
            List<DTO_DMBan> dsfull = DAO_DMBan.Instance.GetListDMBan();
            gridControl4.DataSource = dsfull;
        }
        public void InsertDMBan()
        {
            if (txtMaB.Text != "")
            {
                if (DAO_DMBan.Instance.GetListDMBan().Count(q => q.Ma == txtMaB.Text) <= 0)
                {
                    //string makvv = lookUpMaKV4.ToString();
                    DAO_DMBan.Instance.InsertDMBan(txtMaB.Text, txtTenB.Text, chTrangThai.Checked, chVip.Checked, txtGhiChuB.Text, lookUpMaKV4.Text);
                    XtraMessageBox.Show("Thêm thành công ", "Thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshDMBan();
                    txtMaB.Focus();
                    LoadDMBan();
                }
                else XtraMessageBox.Show("Mã " + txtMaB.Text + " đã tồn tại trong danh sách", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else XtraMessageBox.Show("Mã bàn không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        LookUpEdit lookup = new LookUpEdit();
        public void LoadDMBan2()
        {
            List<DTO_DMBan> dsfull = DAO_DMBan.Instance.GetListDMBan();
            flBan.Controls.Clear();
            foreach (var item in dsfull)
            {
                Button bt = new Button()
                { Width = DAO_DMBan.chieuRong, Height = DAO_DMBan.chieuDai };
                bt.ImageAlign = ContentAlignment.MiddleCenter;
                bt.TextAlign = ContentAlignment.MiddleCenter;
                //var i = item.DonGia;
                string trangthai = item.TrangThai == true ? "CÓ KHÁCH" : "TRỐNG";
                bt.Text = item.Ten + "  (" + trangthai + ")";
                bt.ForeColor = Color.White;
                bt.Click += bt_Click;
                bt.Tag = item.Ma;
                bt.Tag = item;
                bt.BackColor = item.TrangThai == false ? Color.DodgerBlue : Color.Orange;
                // bt.Focus(bt.BackColor=Color.Green);
                flBan.Controls.Add(bt);
                flFood.Visible = true;
            }
        }
        public void LoadDMKhuVuc2()
        {
            List<DTO_DMKhuVuc> dsfull = DAO_DMKhuVuc.Instance.GetListDMKhuVuc();
            flKhuVuc.Controls.Clear();
            lbTenKhuVuc.Text = "Danh sách bàn khám ";
            foreach (var item in dsfull)
            {
                Button bt = new Button()
                { Width = 93, Height = 40 };
                bt.ImageAlign = ContentAlignment.MiddleCenter;
                bt.TextAlign = ContentAlignment.MiddleCenter;
                //var i = item.DonGia;
                // string trangthai = item.TrangThai == true ? "Có Khác" : "TRỐNG";
                bt.Text = item.Ten;
                bt.ForeColor = Color.White;
                bt.Click += bt_Click2;
                bt.Tag = item.Ma;
                bt.Tag = item;
                bt.BackColor = Color.DodgerBlue;
                // bt.Focus(bt.BackColor=Color.Green);
                flKhuVuc.Controls.Add(bt);
            }
        }
        public void LoadDMHH2()
        {
            // List<DTO_DMSanPhamHH> dsfull = DAO_DMSanPhamHH.Instance.GetListDMSanPhamHH();
            flBan.Controls.Clear();
            lbTenKhuVuc.Text = "Danh sách thực đơn ";
            txtKyTuTimKiem.Visible = true;
            lbTimKiem.Visible = true;
            string forder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            if (txtKyTuTimKiem.Text == "")
            {
                List<DTO_DMSanPhamHH> dsfull2 = DAO_DMSanPhamHH.Instance.GetListDMSanPhamHH();
                var ds2 = dsfull2.Select(q => { q.TenHinh = System.IO.Path.Combine(forder, "images\\" + q.TenHinh); q.TenHinh1 = System.IO.Path.Combine(forder, "images\\" + q.TenHinh1); return q; }).ToList();
                flFood2.Visible = true;
                gridControl9.DataSource = dsfull2;
            }
            else
            {
                List<DTO_DMSanPhamHH> dsfull2 = DAO_DMSanPhamHH.Instance.GetListDMSanPhamHHByTimKiem(txtKyTuTimKiem.Text);
                var ds2 = dsfull2.Select(q => { q.TenHinh = System.IO.Path.Combine(forder, "images\\" + q.TenHinh); q.TenHinh1 = System.IO.Path.Combine(forder, "images\\" + q.TenHinh1); return q; }).ToList();
                flFood2.Visible = true;
                gridControl9.DataSource = dsfull2;
            }
            gridControl9.Visible = true;
            //searchHangHoa.Visible = true;
        }
        public void LoadNhomHH2()
        {
            List<DTO_DMNhomHH> dsfull = DAO_DMNhomHH.Instance.GetListDM_NhomHH();
            flKhuVuc.Controls.Clear();
            foreach (var item in dsfull)
            {
                Button bt = new Button()
                { Width = 100, Height = 40 };
                bt.ImageAlign = ContentAlignment.MiddleCenter;
                bt.TextAlign = ContentAlignment.MiddleCenter;
                //var i = item.DonGia;
                // string trangthai = item.TrangThai == true ? "Có Khác" : "TRỐNG";
                bt.Text = item.Ten;
                bt.ForeColor = Color.White;
                bt.Click += bt_ClickNhomHH;
                bt.Tag = item.Ma;
                bt.Tag = item;
                bt.BackColor = Color.DodgerBlue;
                // bt.Focus(bt.BackColor=Color.Green);
                flKhuVuc.Controls.Add(bt);
            }
        }
        public void LoadKVB()
        {
            cbKhucVucB.DataSource = DAO_DMKhuVuc.Instance.GetListDMKhuVuc();
            lookUpMaKV.DataSource = DAO_DMKhuVuc.Instance.GetListDMKhuVuc();
            lookUpMaKV.ValueMember = "Ma";
            lookUpMaKV.DisplayMember = "Ten";
            lookUpMaKV4.Properties.DataSource = DAO_DMKhuVuc.Instance.GetListDMKhuVuc();
            lookUpMaKV4.Properties.ValueMember = "Ma";
            lookUpMaKV4.Properties.DisplayMember = "Ma";
        }
        public void LoadBillByBan()
        {
            var dsfullPhieuThuCT = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.MaBan == lbMaBan.Text && q.TrangThai != "Da_Thu");
            //var dsfullPhieuThuCT = dsfull.Where(q => q.MaBan == lbMaBan.Text);
            var dsDMHH = DAO_DMSanPhamHH.Instance.GetListDMSanPhamHH();
            var dstheoBan = dsfullPhieuThuCT.Join(dsDMHH,
                o => o.MaHH,
                p => p.Ma,
                (o, p) => new
                {
                    Ten = p.Ten,
                    DonGia = o.DonGia,
                    SoLuong = o.SoLuong,
                    MaHH = o.MaHH,
                    MaCTPT = o.Id,
                    ThanhTien = o.ThanhToan,
                    NhomHH = p.Nhom,
                    TrangThai = o.TrangThai
                }).Where(q => q.TrangThai != "Da_Huy");

            gridControl6.DataSource = dstheoBan;
            
        }
        
        public void RefreshDM_KhuVuc()
        {
            txtMaKV.Text = "";
            txtTenKV.Text = "";
            txtGhiChuKV.Text = "";
        }
        public void LoadDMKhuVuc()
        {
            List<DTO_DMKhuVuc> dsfull = DAO_DMKhuVuc.Instance.GetListDMKhuVuc();
            gridControl5.DataSource = dsfull;
        }
        public void InsertDMKhuVuc()
        {
            if (txtMaKV.Text != "")
            {
                if (DAO_DMKhuVuc.Instance.GetListDMKhuVuc().Count(q => q.Ma == txtMaKV.Text) <= 0)
                {
                    DAO_DMKhuVuc.Instance.InsertDMKhuVuc(txtMaKV.Text, txtTenKV.Text, txtGhiChuKV.Text);
                    XtraMessageBox.Show("Thêm thành công ", "Thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshDM_KhuVuc();
                    txtMaKV.Focus();
                    LoadDMKhuVuc();
                }
                else XtraMessageBox.Show("Mã " + txtMaKV.Text + " đã tồn tại trong danh sách", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else XtraMessageBox.Show("Mã khu vực không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void Load_HeaderBill(string maBan)
        {
            //System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            lbTongChiPhi.Text = String.Format(culture, "{0:N0}", DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.MaBan == maBan && q.TrangThai == "Chua_Thu").Sum(q => q.ThanhToan));
            //lbThoiGianVao.Text = String.Format(culture, "{HH:ss dd/MM/yyy}", DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.MaBan == maBan && q.TrangThai == "Chua_Thu").SingleOrDefault().Ngay);
            // lbTongChiPhi.Text = DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.MaBan == maBan && q.TrangThai=="Chua_Thu").Sum(q => q.ThanhToan).ToString();
            var listPT = DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.MaBan == maBan && q.TrangThai == "Chua_Thu");
            lbThoiGianVao.Text = listPT.Count() > 0 ? DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.MaBan == maBan && q.TrangThai == "Chua_Thu").SingleOrDefault().Ngay.ToString() : "0";
            lbIdPT.Text = listPT.Count() > 0 ? DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.MaBan == maBan && q.TrangThai == "Chua_Thu").Single().Id.ToString() : "0";
        }
        #endregion
        #region Event
        private void bt_ClickNhomHH(object sender, EventArgs e)
        {
            string MaNhom = ((sender as Button).Tag as DTO_DMNhomHH).Ma;
            string TenNhom = ((sender as Button).Tag as DTO_DMNhomHH).Ten;
            lbTenKhuVuc.Text = "Danh sách nhóm: " + TenNhom;
            List<DTO_DMSanPhamHH> dsfull2 = DAO_DMSanPhamHH.Instance.GetListDMSanPhamHH();
            var listtheonhom = dsfull2.Where(q => q.Nhom == TenNhom);
            string forder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var ds2 = listtheonhom.Select(q => { q.TenHinh = System.IO.Path.Combine(forder, "images\\" + q.TenHinh); q.TenHinh1 = System.IO.Path.Combine(forder, "images\\" + q.TenHinh1); return q; }).ToList();
            flFood2.Visible = true;
            gridControl9.DataSource = listtheonhom;
        }

        private void bt_ClickSanPham(object sender, EventArgs e)
        {
            string nguoiThu = "";
            string quayThu = "";
            string maKhach = "";
            string trangThai = "Chua_Thu";
            string maBan = lbMaBan.Text;
            string maHH = ((sender as Button).Tag as DTO_DMSanPhamHH).Ma;
            string donVi = ((sender as Button).Tag as DTO_DMSanPhamHH).DonVi;
            string maKho = "";
            string ghiChu = "";
            decimal soLuong = 1;
            decimal donGia = ((sender as Button).Tag as DTO_DMSanPhamHH).DonGia;
            decimal giam = 0;
            decimal soGiam = 0;
            decimal tang = 0;
            decimal soTang = 0;
            var listChuaThu = DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.TrangThai == "Chua_Thu");
            if (listChuaThu.Count(q => q.MaBan == lbMaBan.Text) > 0)
            {
                var listMax = DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.MaBan == lbMaBan.Text).OrderByDescending(k => k.Ngay).First();
                string IDPhieuThu = listMax.Id;
                var dsDMHH = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.MaBan == lbMaBan.Text && q.TrangThai == "Chua_Thu");
                if (dsDMHH.Count(q => q.MaHH == maHH) <= 0)
                {
                    DAO_BHBienLai.Instance.InsertBHPhieuThuCT(IDPhieuThu, maKhach, maHH, donVi, maKho, ghiChu, soLuong, donGia, giam, soGiam, tang, soTang, trangThai, maBan);
                    LoadBillByBan();
                }
                else
                {
                    var lsList = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.MaBan == lbMaBan.Text && q.TrangThai == "Chua_Thu" && q.MaHH == maHH).Single();
                    decimal sl = lsList.SoLuong += 1;
                    string id = lsList.Id;
                    string idPT = lsList.IdPhieuThu;
                    decimal thanhToan = lsList.DonGia * sl;

                    DAO_BHBienLai.Instance.UpdateSLBHPhieuThuCT(sl, id, thanhToan);

                    LoadBillByBan();
                }
                var lsList2 = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.MaBan == lbMaBan.Text && q.TrangThai == "Chua_Thu" && q.MaHH == maHH).Single();
                string IDPT = lsList2.IdPhieuThu;
                decimal tongChiPhiPT = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.IdPhieuThu == IDPT).Sum(q => q.ThanhToan);
                DAO_BHBienLai.Instance.UpdateBHPhieuThu_TongThu_ThanhToan(IDPT, tongChiPhiPT);
            }
            else if (listChuaThu.Count(q => q.MaBan == lbMaBan.Text) <= 0)
            {
                DAO_BHBienLai.Instance.InsertBHPhieuThu(nguoiThu, quayThu, maKhach, trangThai, maBan);
                var listMax = DAO_BHBienLai.Instance.GetListBH_PhieuThu().OrderByDescending(q => q.Ngay).First();
                string IDPhieuThu = listMax.Id;
                DAO_BHBienLai.Instance.InsertBHPhieuThuCT(IDPhieuThu, maKhach, maHH, donVi, maKho, ghiChu, soLuong, donGia, giam, soGiam, tang, soTang, trangThai, maBan);
                //listChuaThu.Select(c => { c.TongChiPhi = 9; return c; }).ToList().Where(q=>q.Id==listMax.Id);
                //decimal tongChiPhi = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.IdPhieuThu == IDPhieuThu).Sum(q => q.ThanhToan);
                DAO_BHBienLai.Instance.UpdateBHPhieuThu_TongThu_ThanhToan(IDPhieuThu, donGia);
                DAO_DMBan.Instance.UpdateTrangThai(lbMaBan.Text, true);
                LoadBillByBan();
            }
            Load_HeaderBill(maBan);
            // System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            // lbTongChiPhi.Text = String.Format(culture, "{0:N0}", DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.MaBan == maBan && q.TrangThai == "Chua_Thu").Sum(q => q.ThanhToan));
            // //lbThoiGianVao.Text = String.Format(culture, "{HH:ss dd/MM/yyy}", DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.MaBan == maBan && q.TrangThai == "Chua_Thu").SingleOrDefault().Ngay);
            //// lbTongChiPhi.Text = DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.MaBan == maBan && q.TrangThai=="Chua_Thu").Sum(q => q.ThanhToan).ToString();
            //lbThoiGianVao.Text = DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.MaBan == maBan && q.TrangThai == "Chua_Thu").SingleOrDefault().Ngay.ToString();


        }

        private void bt_Click2(object sender, EventArgs e)
        {
            string MaKV = ((sender as Button).Tag as DTO_DMKhuVuc).Ma;
            string TenKV = ((sender as Button).Tag as DTO_DMKhuVuc).Ten;
            lbTenKhuVuc.Text = "Danh sách bàn khu vực: " + TenKV;
            List<DTO_DMBan> dsfull = DAO_DMBan.Instance.GetListDMBan();
            var dsfiter = dsfull.Where(q => q.KhuVuc == MaKV);
            flBan.Controls.Clear();
            foreach (var item in dsfiter)
            {
                Button bt = new Button()
                { Width = DAO_DMBan.chieuRong, Height = DAO_DMBan.chieuDai };
                bt.ImageAlign = ContentAlignment.MiddleCenter;
                bt.TextAlign = ContentAlignment.MiddleCenter;
                //var i = item.DonGia;
                string trangthai = item.TrangThai == true ? "CÓ KHÁCH" : "TRỐNG";
                bt.Text = item.Ten + "  (" + trangthai + ")";
                bt.ForeColor = Color.White;
                bt.Click += bt_Click;
                bt.Tag = item.Ma;
                bt.Tag = item;
                bt.BackColor = item.TrangThai == false ? Color.DodgerBlue : Color.Orange;
                // bt.Focus(bt.BackColor=Color.Green);
                flBan.Controls.Add(bt);
            }
            //
            //SearchHH();

        }//Event click của khu vưc

        private void bt_Click(object sender, EventArgs e)
        {
            txtKyTuTimKiem.Visible = true;
            lbTimKiem.Visible = true;
            string TenBan = ((sender as Button).Tag as DTO_DMBan).Ten;
            string maBan = ((sender as Button).Tag as DTO_DMBan).Ma;
            // string TenBanH = ChuHoa(TenBan);
            string ViTriB = ((sender as Button).Tag as DTO_DMBan).KhuVuc;
            bool TrangThaiBan = ((sender as Button).Tag as DTO_DMBan).TrangThai;
            string strTrangThaiBan = TrangThaiBan == true ? "CÓ KHÁCH" : "TRỐNG";
            lbTenBan.Text = "BIÊN LAI THU TIỀN " + TenBan;
            lbTrangThai.Text = strTrangThaiBan;
            lbViTriB.Text = ViTriB;
            lbMaBan.Text = maBan;
            //SearchHH();
            var dsfullPhieuThuCT = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.MaBan == maBan && q.TrangThai != "Da_Thu");
            //var dsfullPhieuThuCT = dsfull.Where(q => q.MaBan == lbMaBan.Text);
            var dsDMHH = DAO_DMSanPhamHH.Instance.GetListDMSanPhamHH();
            var dstheoBan = dsfullPhieuThuCT.Join(dsDMHH,
                o => o.MaHH,
                p => p.Ma,
                (o, p) => new
                {
                    Ten = p.Ten,
                    DonGia = o.DonGia,
                    SoLuong = o.SoLuong,
                    MaHH = o.MaHH,
                    MaCTPT = o.Id,
                    ThanhTien = o.ThanhToan,
                    NhomHH = p.Nhom,
                    TrangThai = o.TrangThai
                }).Where(q => q.TrangThai != "Da_Huy");

            if (dstheoBan.Count() > 0)
            {
                LoadDMBan2();
                if (XtraMessageBox.Show("Bạn có muôn Oder thêm cho bàn này không?", "Bàn đã có khách", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    LoadDMHH2();
                    LoadNhomHH2();
                    LoadBillByBan();
                    Load_HeaderBill(maBan);
                    button1.Visible = false;
                }
            }
            else
            {
                LoadDMHH2();
                LoadNhomHH2();
                LoadBillByBan();
                Load_HeaderBill(maBan);
                button1.Visible = false;
            }
        }//Event click của bàn  
        private void backstageViewClientControl1_Load(object sender, EventArgs e)
        {

        }

        private void btCauHinhCSDL_ItemClick(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            CauHinhCSDL ch = new CauHinhCSDL();
            ch.ShowDialog();
        }

        private void DM_NhomHH_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            LoadDM_NhomHH();
        }

        private void searchControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bntLuu_Click(object sender, EventArgs e)
        {
            InsertDM_NhomHH();
            LoadDM_NhomHH();
        }

        private void bntLamMoi_Click(object sender, EventArgs e)
        {
            RefreshDM_NhomHH();
        }
        string Ma;
        private void bntXoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int row = gridView1.FocusedRowHandle;
                string MaID = "Ma";
                object value = gridView1.GetRowCellValue(row, MaID);
                if (value != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xóa nhóm có mã " + value + " này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Ma = value.ToString();
                        DAO_DMNhomHH.Instance.DeleteDMNhomHH(Ma);
                        LoadDM_NhomHH();
                        //XtraMessageBox.Show("Xóa thành công", "Đã xóa!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void bntCapNhat_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int row = gridView1.FocusedRowHandle;
                string MaIDN = "Ma";
                string TenN = "Ten";
                string PhanLoaiN = "PhanLoai";
                object ma = gridView1.GetRowCellValue(row, MaIDN);
                object ten = gridView1.GetRowCellValue(row, TenN);
                object phanloai = gridView1.GetRowCellValue(row, PhanLoaiN);
                string strMa = ma.ToString();
                string strTen = ten.ToString();
                string strPhanLoai = phanloai.ToString();
                var listnhomdata = DAO_DMNhomHH.Instance.GetListDM_NhomHH();
                //  int s=listnhomdata.Count(q => q.Ten == strTen);
                // int s2 = listnhomdata.Count(q => q.PhanLoai == strPhanLoai);
                if (listnhomdata.Count(q => q.Ma == strMa) > 0 && listnhomdata.Count(k => k.Ten == strTen) <= 0 || listnhomdata.Count(k => k.PhanLoai == strPhanLoai) <= 0)
                {
                    //Ma = value.ToString();
                    DAO_DMNhomHH.Instance.UpdateDMNhomHH(strMa, strTen, strPhanLoai);
                    XtraMessageBox.Show("Cập nhật xong", "Hoàn Thành!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else XtraMessageBox.Show("Mã nhóm không được thay đổi, hoặc không có dịch vụ nào cần update", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadDM_NhomHH();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void bntThemHH_Click(object sender, EventArgs e)
        {
            NhapSanPham nhap = new NhapSanPham();
            nhap.ShowDialog();
        }

        private void backstageViewTabItem6_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            LoadDMSanPhamHH();
        }

        private void bntCapNhatHH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //NhapSanPham nhap = new NhapSanPham();
            //nhap.ShowDialog();
            try
            {
                int row = gridView2.FocusedRowHandle;
                string MaIDN = "Ma";
                string TenN = "Ten";
                string TenVietTatN = "TenVietTat";
                string NhomN = "Nhom";
                string DonGiaN = "DonGia";
                string DonViN = "DonVi";
                string NoiSXN = "NoiSX";
                string NuocSXN = "NuocSX";
                string NgungTheoDoiN = "NgungTheoDoi";
                string TuKhoaTimKiemN = "TuKhoaTimKiem";
                string ChieuCaoN = "ChieuCao";
                string ChieuDaiN = "ChieuDai";
                string ChieuRongN = "ChieuRong";
                string BanKinhN = "BanKinh";
                string TenHinhN = "TenHinh";
                string TenHinh2N = "TenHinh2";
                object ma = gridView2.GetRowCellValue(row, MaIDN);
                object ten = gridView2.GetRowCellValue(row, TenN);
                object tenVietTat = gridView2.GetRowCellValue(row, TenVietTatN);
                object nhom = gridView2.GetRowCellValue(row, NhomN);
                object donGia = gridView2.GetRowCellValue(row, DonGiaN);
                object donVi = gridView2.GetRowCellValue(row, DonViN);
                object noiSX = gridView2.GetRowCellValue(row, NoiSXN);
                object nuocSX = gridView2.GetRowCellValue(row, NuocSXN);
                object ngungTheoDoi = gridView2.GetRowCellValue(row, NgungTheoDoiN);
                object tuKhoaTimKiem = gridView2.GetRowCellValue(row, TuKhoaTimKiemN);
                object chieuCao = gridView2.GetRowCellValue(row, ChieuCaoN);
                object chieuDai = gridView2.GetRowCellValue(row, ChieuDaiN);
                object chieuRong = gridView2.GetRowCellValue(row, ChieuRongN);
                object banKinh = gridView2.GetRowCellValue(row, BanKinhN);
                object tenHinh = gridView2.GetRowCellValue(row, TenHinhN);
                object tenHinh2 = gridView2.GetRowCellValue(row, TenHinh2N);
                string MaF = ma.ToString();
                string TenF = ten.ToString();
                string TenVietTatF = tenVietTat.ToString();
                string NhomF = nhom.ToString();
                decimal DonGiaF = (decimal)donGia;
                string DonViF = donVi.ToString();
                string NoiSXF = noiSX.ToString();
                string NuocSXF = nuocSX.ToString();
                bool NgungTheoDoiF = (bool)ngungTheoDoi;
                string TuKhoaTimKiemF = tuKhoaTimKiem.ToString();
                decimal ChieuCaoF = (decimal)chieuCao;
                decimal ChieuDaiF = (decimal)chieuDai;
                decimal ChieuRongF = (decimal)chieuRong;
                decimal BanKinhF = (decimal)banKinh;
                //Image TenHinhF = (Image)tenHinh;
                var listnhomdata = DAO_DMSanPhamHH.Instance.GetListDMSanPhamHH();
                //  int s=listnhomdata.Count(q => q.Ten == strTen);
                // int s2 = listnhomdata.Count(q => q.PhanLoai == strPhanLoai);
                if (listnhomdata.Count(q => q.Ma == MaF) > 0)
                {
                    DAO_DMSanPhamHH.Instance.UpdateDMSanPhamHH(MaF, TenF, TenVietTatF, NhomF, DonGiaF, DonViF, NoiSXF, NuocSXF, NgungTheoDoiF, TuKhoaTimKiemF, ChieuCaoF, ChieuDaiF, ChieuRongF, BanKinhF);

                    XtraMessageBox.Show("Cập nhật xong", "Hoàn Thành!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else XtraMessageBox.Show("Mã HH không được thay đổi", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadDMSanPhamHH();
        }


        private void bntGetHH_Click(object sender, EventArgs e)
        {
            LoadDMSanPhamHH();
        }

        private void bntXoaHH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int row = gridView2.FocusedRowHandle;
                string MaID = "Ma";
                object value = gridView2.GetRowCellValue(row, MaID);
                if (value != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xóa HH có mã " + value + " này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Ma = value.ToString();
                        DAO_DMSanPhamHH.Instance.DeleteDMSanPhamHH(Ma);
                        LoadDMSanPhamHH();
                        // XtraMessageBox.Show("Xóa thành công", "Đã xóa!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void bntHinhHH_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("Noi làm");
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("Noi làm");

        }

        private void picAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            PictureBox p = sender as PictureBox;
            PictureEdit p2 = sender as PictureEdit;
            if (p2 != null)
            {
                // MessageBox.Show("nhucc");
                open.Filter = "(*.jpg;*.jpeg;*.bmp;*.png;)| *.jpg; *.jpeg; *.bmp; *.png";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    p2.Image = Image.FromFile(open.FileName);
                }
            }

        }

        private void gridControl3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void backstageViewTabItem7_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            LoadDM_NhanSu();
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            InsertDM_NhanSu();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            RefreshDMNhanSu();
        }

        private void bntCapNhatNS_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int row = gridView3.FocusedRowHandle;
                string MaIDN = "MaSoNV";
                string TenN = "Ten";
                string DiaChiN = "DiaChi";
                string DienThoaiN = "DienThoai";
                string PhanLoaiN = "Loai";
                object ma = gridView3.GetRowCellValue(row, MaIDN);
                object ten = gridView3.GetRowCellValue(row, TenN);
                object diaChi = gridView3.GetRowCellValue(row, DiaChiN);
                object dienThoai = gridView3.GetRowCellValue(row, DienThoaiN);
                object loai = gridView3.GetRowCellValue(row, PhanLoaiN);
                string MaF = ma.ToString();
                string TenF = ten.ToString();
                string DiaChiF = diaChi.ToString();
                string DienThoaiF = dienThoai.ToString();
                string LoaiF = loai.ToString();
                var listdatans = DAO_DMNhanSu.Instance.GetListDM_NhanSu();
                //  int s=listnhomdata.Count(q => q.Ten == strTen);
                // int s2 = listnhomdata.Count(q => q.PhanLoai == strPhanLoai);
                if (listdatans.Count(q => q.MaSoNV == MaF) > 0 && listdatans.Count(k => k.Ten == TenF) <= 0 || listdatans.Count(k => k.DiaChi == DiaChiF) <= 0 || listdatans.Count(q => q.DienThoai == DienThoaiF) <= 0 || listdatans.Count(q => q.Loai == LoaiF) <= 0)
                {
                    //Ma = value.ToString();
                    DAO_DMNhanSu.Instance.Update_DMNhanSu(MaF, TenF, DiaChiF, DienThoaiF, LoaiF);
                    XtraMessageBox.Show("Cập nhật xong", "Hoàn Thành!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else XtraMessageBox.Show("Mã Số NV không được thay đổi, hoặc không có dịch vụ nào cần update", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadDM_NhanSu();
        }

        private void bntXoaNS_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int row = gridView3.FocusedRowHandle;
                string MaID = "Ma";
                object value = gridView3.GetRowCellValue(row, MaID);
                if (value != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xóa nhân sự có mã " + value + " này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Ma = value.ToString();
                        DAO_DMNhanSu.Instance.Delete_DMNhanSu(Ma);
                        LoadDM_NhanSu();
                        //XtraMessageBox.Show("Xóa thành công", "Đã xóa!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void bntLuuB_Click(object sender, EventArgs e)
        {
            InsertDMBan();
        }

        private void bntLamMoiB_Click(object sender, EventArgs e)
        {
            RefreshDMBan();
        }

        private void backstageViewTabItem5_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            LoadDMBan();
        }

        private void bntXoaB_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int row = gridView4.FocusedRowHandle;
                string MaID = "Ma";
                object value = gridView4.GetRowCellValue(row, MaID);
                if (value != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xóa bàn có mã " + value + " này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Ma = value.ToString();
                        DAO_DMBan.Instance.DeleteDMBan(Ma);
                        LoadDMBan();
                        //XtraMessageBox.Show("Xóa thành công", "Đã xóa!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void bntCapNhatB_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int row = gridView4.FocusedRowHandle;
                string MaIDN = "Ma";
                string TenN = "Ten";
                string TrangThaiN = "TrangThai";
                string VipN = "Vip";
                string GhiChuN = "GhiChu";
                string KhuVucN = "KhuVuc";
                object ma = gridView4.GetRowCellValue(row, MaIDN);
                object ten = gridView4.GetRowCellValue(row, TenN);
                object trangThai = gridView4.GetRowCellValue(row, TrangThaiN);
                object vip = gridView4.GetRowCellValue(row, VipN);
                object ghiChu = gridView4.GetRowCellValue(row, GhiChuN);
                object khuVuc = gridView4.GetRowCellValue(row, KhuVucN);
                string MaF = ma.ToString();
                string TenF = ten.ToString();
                bool TrangThaiF = (bool)trangThai;
                bool VipF = (bool)vip;
                string GhiChuF = ghiChu.ToString();
                string KhuVucF = khuVuc.ToString();
                var listdatab = DAO_DMBan.Instance.GetListDMBan();
                //  int s=listnhomdata.Count(q => q.Ten == strTen);
                // int s2 = listnhomdata.Count(q => q.PhanLoai == strPhanLoai);
                if (listdatab.Count(q => q.Ma == MaF) > 0)
                {
                    //Ma = value.ToString();
                    DAO_DMBan.Instance.UpdateDMBan(MaF, TenF, TrangThaiF, VipF, GhiChuF, KhuVucF);
                    XtraMessageBox.Show("Cập nhật xong", "Hoàn Thành!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else XtraMessageBox.Show("Mã bàn không được thay đổi, hoặc không có dịch vụ nào cần update", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadDMBan();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bntLuuKV_Click(object sender, EventArgs e)
        {
            InsertDMKhuVuc();
        }

        private void bntLamMoiKV_Click(object sender, EventArgs e)
        {
            RefreshDM_KhuVuc();
        }

        private void backstageViewTabItem10_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            LoadDMKhuVuc();
        }

        private void bntXoaKV_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int row = gridView5.FocusedRowHandle;
                string MaID = "Ma";
                object value = gridView5.GetRowCellValue(row, MaID);
                if (value != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xóa kv có mã " + value + " này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Ma = value.ToString();
                        DAO_DMKhuVuc.Instance.DeleteDMKhuVuc(Ma);
                        LoadDMKhuVuc();
                        //XtraMessageBox.Show("Xóa thành công", "Đã xóa!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void bntSuaKV_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int row = gridView5.FocusedRowHandle;
                string MaIDN = "Ma";
                string TenN = "Ten";
                string GhiChuN = "GhiChu";
                object ma = gridView5.GetRowCellValue(row, MaIDN);
                object ten = gridView5.GetRowCellValue(row, TenN);
                object ghiChu = gridView5.GetRowCellValue(row, GhiChuN);
                string MaF = ma.ToString();
                string TenF = ten.ToString();
                string GhiChuF = ghiChu.ToString();
                var listdatab = DAO_DMKhuVuc.Instance.GetListDMKhuVuc();
                //  int s=listnhomdata.Count(q => q.Ten == strTen);
                // int s2 = listnhomdata.Count(q => q.PhanLoai == strPhanLoai);
                if (listdatab.Count(q => q.Ma == MaF) > 0)
                {
                    //Ma = value.ToString();
                    DAO_DMKhuVuc.Instance.UpdateDMKhuVuc(MaF, TenF, GhiChuF);
                    XtraMessageBox.Show("Cập nhật xong", "Hoàn Thành!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else XtraMessageBox.Show("Mã KV không được thay đổi, hoặc không có KV nào cần update", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadDMKhuVuc();
        }

        private void cbMaKV2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void backstageViewTabItem1_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            LoadDMBan2();
            LoadDMKhuVuc2();
        }

        private void ribbonStatusBar_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDMBan2();
            lbTenKhuVuc.Text = "Tất cả các bàn";
        }

        private void bntQuayLaiBan_Click(object sender, EventArgs e)
        {
            LoadDMKhuVuc2();
            LoadDMBan2();
            flFood2.Visible = false;
            //flFood.Visible = true;
            button1.Visible = true;
            gridControl9.Visible = false;
            gridView9.OptionsBehavior.Editable = true;
            searchHangHoa.Visible = false;
            // SearchHH();
            txtKyTuTimKiem.Visible = false;
            lbTimKiem.Visible = false;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            // LoadDMHH2();
        }

        private void bntAddHH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                // int row = gridView9.FocusedRowHandle;

                int row = gridView9.FocusedRowHandle;
                string MaCTPTN = "MaCTPT";
                string SoLuongN = "SoLuong";
                string DonGiaN = "DonGia";
                string MaHHN = "MaHH";
                object maCTPT = gridView9.GetRowCellValue(row, MaCTPTN);
                object SoLuong = gridView9.GetRowCellValue(row, SoLuongN);
                object donGia = gridView9.GetRowCellValue(row, DonGiaN);
                object maHH = gridView9.GetRowCellValue(row, MaHHN);
                if (maCTPT != null)
                {
                    string MaCTPTF = maCTPT.ToString();
                    decimal SoLuongF = (decimal)SoLuong;
                    decimal DonGiaF = (decimal)donGia;
                    decimal SoLuongF2 = SoLuongF += 1;
                    decimal ThanhTienF2 = DonGiaF * SoLuongF;
                    string MaHHF = maHH.ToString();
                    DAO_BHBienLai.Instance.UpdateSLBHPhieuThuCT(SoLuongF2, MaCTPTF, ThanhTienF2);
                    var lsList2 = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.MaBan == lbMaBan.Text && q.TrangThai == "Chua_Thu" && q.MaHH == MaHHF).Single();
                    string IDPT = lsList2.IdPhieuThu;
                    decimal tongChiPhiPT = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.IdPhieuThu == IDPT).Sum(q => q.ThanhToan);
                    DAO_BHBienLai.Instance.UpdateBHPhieuThu_TongThu_ThanhToan(IDPT, tongChiPhiPT);
                    LoadBillByBan();
                    Load_HeaderBill(lbMaBan.Text);
                    // XtraMessageBox.Show("Xóa thành công", "Đã xóa!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bntDeleteHH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int row = gridView9.FocusedRowHandle;
                string MaCTPTN = "MaCTPT";
                string SoLuongN = "SoLuong";
                string DonGiaN = "DonGia";
                string MaHHN = "MaHH";
                object maCTPT = gridView9.GetRowCellValue(row, MaCTPTN);
                object SoLuong = gridView9.GetRowCellValue(row, SoLuongN);
                object donGia = gridView9.GetRowCellValue(row, DonGiaN);
                object maHH = gridView9.GetRowCellValue(row, MaHHN);
                if (maCTPT != null)
                {
                    string MaCTPTF = maCTPT.ToString();
                    decimal SoLuongF = (decimal)SoLuong;
                    decimal DonGiaF = (decimal)donGia;
                    decimal SoLuongF2 = SoLuongF -= 1;
                    decimal ThanhTienF2 = DonGiaF * SoLuongF;
                    string MaHHF = maHH.ToString();
                    if (SoLuongF2 >= 1)
                    {
                        DAO_BHBienLai.Instance.UpdateSLBHPhieuThuCT(SoLuongF2, MaCTPTF, ThanhTienF2);
                        var lsList2 = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.MaBan == lbMaBan.Text && q.TrangThai == "Chua_Thu" && q.MaHH == MaHHF).Single();
                        string IDPT = lsList2.IdPhieuThu;
                        decimal tongChiPhiPT = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.IdPhieuThu == IDPT).Sum(q => q.ThanhToan);
                        DAO_BHBienLai.Instance.UpdateBHPhieuThu_TongThu_ThanhToan(IDPT, tongChiPhiPT);
                        LoadBillByBan();
                        Load_HeaderBill(lbMaBan.Text);
                    }
                    else { XtraMessageBox.Show("Không thể giảm số lượng về 0", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    // XtraMessageBox.Show("Xóa thành công", "Đã xóa!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bntDelAll_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int row = gridView9.FocusedRowHandle;
                //string MaID = "Ma";
                string MaCTPTN = "MaCTPT";
                string MaHHN = "MaHH";
                string TenN = "Ten";
                object value = gridView9.GetRowCellValue(row, MaCTPTN);
                object maHH = gridView9.GetRowCellValue(row, MaHHN);
                object ten = gridView9.GetRowCellValue(row, TenN);
                string tenF = ten.ToString();
                if (value != null)
                {
                    if (DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Count(q => q.MaBan == lbMaBan.Text && q.TrangThai == "Chua_Thu") > 1)
                    {
                        if (XtraMessageBox.Show("Bạn có muốn xóa " + tenF + " không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string id = value.ToString();
                            string MaHHF = maHH.ToString();
                            DAO_BHBienLai.Instance.DeletePhieuThuCT(id);
                            var lsList2 = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.MaBan == lbMaBan.Text && q.TrangThai == "Chua_Thu").First();
                            string IDPT = lsList2.IdPhieuThu;
                            decimal tongChiPhiPT = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.IdPhieuThu == IDPT).Sum(q => q.ThanhToan);
                            DAO_BHBienLai.Instance.UpdateBHPhieuThu_TongThu_ThanhToan(IDPT, tongChiPhiPT);
                            LoadBillByBan();
                            Load_HeaderBill(lbMaBan.Text);
                        }
                    }
                    else { XtraMessageBox.Show("Biên lai không được để trống, nếu xóa vui lòng bấm hủy bill ở bên dưới", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

                }
                else XtraMessageBox.Show("Vui lòng chọn dòng để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void bntThanhToan_Click(object sender, EventArgs e)
        {
            gridControl9.Visible = false;
            gridView9.OptionsBehavior.Editable = false;
            searchHangHoa.Visible = false;
            LoadBillChoThanhToan();
            DateTuNgay.DateTime = DateTime.Today;
            DataDenNgay.DateTime = DateTime.Today;
            LoadBillDaThu(DateTuNgay.DateTime, DataDenNgay.DateTime.AddDays(1));
            txtKyTuTimKiem.Visible = false;
            lbTimKiem.Visible = false;
            chTienMat.Checked = true;
            bntInBill.Enabled = false;
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void textEdit10_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit14_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void bntQuayLaiKham_Click(object sender, EventArgs e)
        {
            LoadDMKhuVuc2();
            LoadDMBan2();
            flFood2.Visible = false;
            flBill.Visible = false;
            pnTimKiem.Controls.Add(lbTenKhuVuc);
            button1.Visible = true;
            bntThanhToan.Enabled = true;
            bntInTam.Enabled = true;
            bntGiaoHang.Enabled = true;
            gridControl6.Enabled = true;
            LoadBillByBan();
            Load_HeaderBill(lbMaBan.Text);
            // searchMenu.Visible = true;
            // SearchHH();
            gridControl9.Visible = false;
            gridView9.OptionsBehavior.Editable = true;
            searchHangHoa.Visible = false;
            txtKyTuTimKiem.Visible = false;
            lbTimKiem.Visible = false;
        }

        private void txtBillPhiPhuThu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal TongHoaDon = decimal.Parse(lbTongChiPhi.Text);
                decimal PhiPhuThu = decimal.Parse(txtBillPhiPhuThu.Text);
                decimal TongGiam = decimal.Parse(txtBillTongGiam.Text);
                decimal TongTang = PhiPhuThu + (decimal.Parse(txtBillTongTang.Text));
                decimal ThanhTien = TongHoaDon - TongGiam + TongTang;
                decimal TienKhachDua = decimal.Parse(txtBillTienKhachDua.Text);
                decimal TienThoiLai = TienKhachDua - ThanhTien;
                txtBillThanhTien.Text = ThanhTien.ToString();
                txtBiilThanhTienLamTron.Text = ThanhTien.ToString();
                txtBillTienThoiLai.Text = TienThoiLai.ToString();
                // txtBillTongTang.Text = TongTang.ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Phí Phụ Thu không được để trống hoặc phải là số");
            }

        }

        private void txtBillTongGiam_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal TongHoaDon = decimal.Parse(lbTongChiPhi.Text);
                decimal PhiPhuThu = decimal.Parse(txtBillPhiPhuThu.Text);
                decimal TongGiam = decimal.Parse(txtBillTongGiam.Text);
                decimal TongTang = PhiPhuThu + decimal.Parse(txtBillTongTang.Text);
                decimal ThanhTien = TongHoaDon - TongGiam + TongTang;
                decimal TienKhachDua = decimal.Parse(txtBillTienKhachDua.Text);
                decimal TienThoiLai = TienKhachDua - ThanhTien;
                txtBillThanhTien.Text = ThanhTien.ToString();
                txtBiilThanhTienLamTron.Text = ThanhTien.ToString();
                txtBillTienThoiLai.Text = TienThoiLai.ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Tổng giảm không được để trống hoặc phải là số");
            }

        }

        private void txtBillTongTang_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal TongHoaDon = decimal.Parse(lbTongChiPhi.Text);
                decimal PhiPhuThu = decimal.Parse(txtBillPhiPhuThu.Text);
                decimal TongGiam = decimal.Parse(txtBillTongGiam.Text);
                decimal TongTang = PhiPhuThu + decimal.Parse(txtBillTongTang.Text);
                decimal ThanhTien = TongHoaDon - TongGiam + TongTang;
                decimal TienKhachDua = decimal.Parse(txtBillTienKhachDua.Text);
                decimal TienThoiLai = TienKhachDua - ThanhTien;
                txtBillThanhTien.Text = ThanhTien.ToString();
                txtBiilThanhTienLamTron.Text = ThanhTien.ToString();
                txtBillTienThoiLai.Text = TienThoiLai.ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Tổng Tăng không được để trống hoặc phải là số");
            }

        }

        private void txtBillTienThoiLai_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBillTienKhachDua_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal TongHoaDon = decimal.Parse(lbTongChiPhi.Text);
                decimal PhiPhuThu = decimal.Parse(txtBillPhiPhuThu.Text);
                decimal TongGiam = decimal.Parse(txtBillTongGiam.Text);
                decimal TongTang = PhiPhuThu + decimal.Parse(txtBillTongTang.Text);
                decimal ThanhTien = TongHoaDon - TongGiam + TongTang;
                decimal TienKhachDua = decimal.Parse(txtBillTienKhachDua.Text);
                decimal TienThoiLai = TienKhachDua - ThanhTien;
                txtBillThanhTien.Text = ThanhTien.ToString();
                txtBiilThanhTienLamTron.Text = ThanhTien.ToString();
                txtBillTienThoiLai.Text = TienThoiLai.ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Tiền khách đưa không được để trống hoặc phải là số");
            }

        }

        private void bntLuuBill_Click(object sender, EventArgs e)
        {
            try
            {
                decimal PhiPhuThu = 0;
                PhiPhuThu = decimal.Parse(txtBillPhiPhuThu.Text);
                decimal TongGiam = 0;
                TongGiam = decimal.Parse(txtBillTongGiam.Text);
                decimal TongTang = 0;
                TongTang = (decimal.Parse(txtBillTongTang.Text)) + PhiPhuThu;
                decimal TongChiPhi = decimal.Parse(txtBillTongHoaDon.Text);
                decimal ThanhTien = TongChiPhi - TongGiam + TongTang;
                DateTime? ngay = DateTime.Parse(lbThoiGianVao.Text);
                string HinhThucThanhToan = chTienMat.Checked == true ? "TienMat" : "DienTu";
                var listPT = DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.MaBan == lbMaBan.Text && q.TrangThai == "Chua_Thu" && q.SoPhieu == txtBillSoBienLai.Text).SingleOrDefault();
                if (listPT != null)
                {
                    string idPT = listPT.Id;
                    string idBan = listPT.MaBan;
                    if (idPT != "")
                    {
                        DAO_BHBienLai.Instance.UpdateBHPhieuThuKhiThu(idPT, TongTang, TongGiam, ThanhTien, "Da_Thu", HinhThucThanhToan);
                        DAO_BHBienLai.Instance.UpdateTrangThai_BH_PhieuThuCT(idPT, "Da_Thu");
                        DAO_DMBan.Instance.UpdateTrangThai(idBan, false);
                        bntThanhToan.Enabled = false;
                        bntInTam.Enabled = false;
                        bntGiaoHang.Enabled = false;
                        bntInBill.Enabled = true;
                        gridView9.EditingValue = false;
                        LoadBillDaThu(DateTuNgay.DateTime, DataDenNgay.DateTime.AddDays(1));
                        XtraMessageBox.Show("Đã thanh toán", "Hoàn Thành!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                // bntInTam.Enabled = false;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex + "Vui long kiểm tra lại hóa đơn");
            }

        }

        private void bntGetDSDaThu_Click(object sender, EventArgs e)
        {
            LoadBillDaThu(DateTuNgay.DateTime, DataDenNgay.DateTime.AddDays(1));
        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void bntLuuNguoIDung_Click(object sender, EventArgs e)
        {
            if (DAO_DMNguoiDung.Instance.GetListNguoiDung().Count(q => q.TaiKhoan == txtTaiKhoan.Text) <= 0)
            {
                if (txtTaiKhoan.Text != "")
                {
                    if (txtMatKhau.Text == txtNhapLaiMatKhau.Text)
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
                        DAO_DMNguoiDung.Instance.InsertNguoiDung(txtTaiKhoan.Text, matKhauMH, cbQuyenSuDung.Text, lookUpMaNhanSu.Text, checkNgungSuDung.Checked);
                        XtraMessageBox.Show("Lưu thành công ", "Thành Công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDM_NguoiDung();
                        txtTaiKhoan.Text = "";
                        txtMatKhau.Text = "";
                        cbQuyenSuDung.Text = "";
                        lookUpMaNhanSu.Text = "";
                        checkNgungSuDung.Checked = false;
                    }
                    else XtraMessageBox.Show("Xác nhận mật khẩu chưa đúng vui lòng nhập lại", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else XtraMessageBox.Show("Tài khoản không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (txtTaiKhoan.Text != "")
                {
                    if (txtMatKhau.Text == txtNhapLaiMatKhau.Text)
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
                        var f= DAO_DMNguoiDung.Instance.GetListNguoiDung().Where(q => q.TaiKhoan == txtTaiKhoan.Text).SingleOrDefault();
                        string maNhanSu = f.MaNhanSu;
                        string MaNhanSu = lookUpMaNhanSu.Text == "" ? lookUpMaNhanSu.Text = maNhanSu : lookUpMaNhanSu.Text;
                        DAO_DMNguoiDung.Instance.UpdateNguoiDung(txtTaiKhoan.Text, matKhauMH, cbQuyenSuDung.Text, lookUpMaNhanSu.Text, checkNgungSuDung.Checked);
                        XtraMessageBox.Show("Cập nhật thành công ", "Thành Công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDM_NguoiDung();
                        txtTaiKhoan.Text = "";
                        txtMatKhau.Text = "";
                        cbQuyenSuDung.Text = "";
                        lookUpMaNhanSu.Text = "";
                        checkNgungSuDung.Checked = false;
                        txtNhapLaiMatKhau.Text = "";
                    }
                    else XtraMessageBox.Show("Xác nhận mật khẩu chưa đúng vui lòng nhập lại", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else XtraMessageBox.Show("Tài khoản không được để trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void backstageViewTabItem8_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            LoadDM_NguoiDung();
            lookUpMaNhanSu.Properties.PopupWidth = 200;
            lookUpMaNhanSu.Properties.PopupSizeable = true;
            lookUpMaNhanSu.Properties.DataSource = DAO_DMNhanSu.Instance.GetListDM_NhanSu();
            lookUpMaNhanSu.Properties.ValueMember = "Ma";
            lookUpMaNhanSu.Properties.DisplayMember = "Ma";
        }
        //private void searchHangHoa_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        //{
        //    if (e.Column.FieldName == "clTenTK")
        //    {
        //        SearchHH();
        //    }
        //}
        #endregion
            //Click HH
        private void tileView2_Click(object sender, EventArgs e)
        {
            string nguoiThu = "";
            string quayThu = "";
            string maKhach = "";
            string trangThai = "Chua_Thu";
            string maBan = lbMaBan.Text;
            int row = tileView2.FocusedRowHandle;
            string MaIDN = "Ma";
            string DonGiaN = "DonGia";
            string DonViN = "DonVi";
            object ma = tileView2.GetRowCellValue(row, MaIDN);
            object donGia = tileView2.GetRowCellValue(row, DonGiaN);
            object donVi2 = tileView2.GetRowCellValue(row, DonViN);
            string MaF = ma.ToString();
            decimal DonGiaF = (decimal)donGia;
            string DonViF = donVi2.ToString();
            //string maHH = ((sender as Button).Tag as DTO_DMSanPhamHH).Ma;
            //string donVi = ((sender as Button).Tag as DTO_DMSanPhamHH).DonVi;
            string maKho = "";
            string ghiChu = "";
            decimal soLuong = 1;
            // decimal donGia = ((sender as Button).Tag as DTO_DMSanPhamHH).DonGia;
            decimal giam = 0;
            decimal soGiam = 0;
            decimal tang = 0;
            decimal soTang = 0;
            var listChuaThu = DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.TrangThai == "Chua_Thu");
            if (listChuaThu.Count(q => q.MaBan == lbMaBan.Text) > 0)
            {
                var listMax = DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.MaBan == lbMaBan.Text).OrderByDescending(k => k.Ngay).First();
                string IDPhieuThu = listMax.Id;
                var dsDMHH = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.MaBan == lbMaBan.Text && q.TrangThai == "Chua_Thu");
                if (dsDMHH.Count(q => q.MaHH == MaF) <= 0)
                {
                    DAO_BHBienLai.Instance.InsertBHPhieuThuCT(IDPhieuThu, maKhach, MaF, DonViF, maKho, ghiChu, soLuong, DonGiaF, giam, soGiam, tang, soTang, trangThai, maBan);
                    LoadBillByBan();
                }
                else
                {
                    var lsList = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.MaBan == lbMaBan.Text && q.TrangThai == "Chua_Thu" && q.MaHH == MaF).Single();
                    decimal sl = lsList.SoLuong += 1;
                    string id = lsList.Id;
                    string idPT = lsList.IdPhieuThu;
                    decimal thanhToan = lsList.DonGia * sl;
                    DAO_BHBienLai.Instance.UpdateSLBHPhieuThuCT(sl, id, thanhToan);
                    LoadBillByBan();
                }
                var lsList2 = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.MaBan == lbMaBan.Text && q.TrangThai == "Chua_Thu" && q.MaHH == MaF).Single();
                string IDPT = lsList2.IdPhieuThu;
                decimal tongChiPhiPT = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.IdPhieuThu == IDPT).Sum(q => q.ThanhToan);
                DAO_BHBienLai.Instance.UpdateBHPhieuThu_TongThu_ThanhToan(IDPT, tongChiPhiPT);
            }
            else if (listChuaThu.Count(q => q.MaBan == lbMaBan.Text) <= 0)
            {
                DAO_BHBienLai.Instance.InsertBHPhieuThu(nguoiThu, quayThu, maKhach, trangThai, maBan);
                var listMax = DAO_BHBienLai.Instance.GetListBH_PhieuThu().OrderByDescending(q => q.Ngay).First();
                string IDPhieuThu = listMax.Id;
                DAO_BHBienLai.Instance.InsertBHPhieuThuCT(IDPhieuThu, maKhach, MaF, DonViF, maKho, ghiChu, soLuong, DonGiaF, giam, soGiam, tang, soTang, trangThai, maBan);
                //listChuaThu.Select(c => { c.TongChiPhi = 9; return c; }).ToList().Where(q=>q.Id==listMax.Id);
                //decimal tongChiPhi = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.IdPhieuThu == IDPhieuThu).Sum(q => q.ThanhToan);
                DAO_BHBienLai.Instance.UpdateBHPhieuThu_TongThu_ThanhToan(IDPhieuThu, DonGiaF);
                DAO_DMBan.Instance.UpdateTrangThai(lbMaBan.Text, true);
                LoadBillByBan();
            }
            Load_HeaderBill(maBan);
        }

        private void searchLookUpEdit1View_MouseDown(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Dang phat trien");
        }

        private void searchLookUpEdit1View_MouseEnter(object sender, EventArgs e)
        {
            MessageBox.Show("Dang phat trien");
        }

        private void txtKyTuTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadDMHH2();
        }

        private void gridControl9_Click(object sender, EventArgs e)
        {

        }

        private void chViDienTu_CheckedChanged(object sender, EventArgs e)
        {
            if (chViDienTu.Checked == true)
            {
                chTienMat.Checked = false;
            }
        }

        private void chTienMat_CheckedChanged(object sender, EventArgs e)
        {
            if (chTienMat.Checked == true)
            {
                chViDienTu.Checked = false;
            }
        }

        private void bntInBill_Click(object sender, EventArgs e)
        {
            DateTime? tgvao = DateTime.Parse(lbThoiGianVao.Text);
            var dsfullPhieuThuCT = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.IdPhieuThu == lbIdPT.Text);
            var listPhieuThu = DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.Id == lbIdPT.Text);
            var dsDMHH = DAO_DMSanPhamHH.Instance.GetListDMSanPhamHH();
            var dstheoBan = dsfullPhieuThuCT.Join(dsDMHH,
                o => o.MaHH,
                p => p.Ma,
                (o, p) => new
                {
                    Ten = p.Ten,
                    DonGia = o.DonGia,
                    SoLuong = o.SoLuong,
                    MaHH = o.MaHH,
                    MaCTPT = o.Id,
                    ThanhTien = o.ThanhToan,
                    NhomHH = p.Nhom,
                    IdPT = o.IdPhieuThu
                }).Join(listPhieuThu,
                k => k.IdPT,
                q => q.Id,
                (k, q) => new
                {
                    Ten = k.Ten,
                    DonGia = k.DonGia,
                    SoLuong = k.SoLuong,
                    ThanhTien = k.ThanhTien,
                    TongChiPhi = q.TongChiPhi,
                    ThanhToan = q.ThanhToan,
                    SoGiam = q.SoGiam,
                    Ngay = q.Ngay,
                    SoPhieu = q.SoPhieu,
                    MaBan = q.MaBan,
                    NguoiThu = q.NguoiThu,
                    HinhThucThanhToan = q.HinhThucThanhToan == "TienMat" ? "Tiền Mặt" : "Thanh Toán Online",
                    TrangThai = q.TrangThai
                }
                ).Where(q => q.TrangThai == "Da_Thu");
            if (dstheoBan.Count() > 0)
            {
                ReportInBill bill = new ReportInBill();
                bill.DataSource = dstheoBan;
                bill.Print();
                bill.ShowPreviewDialog();
            }
            else XtraMessageBox.Show("Chọn biên lai cần in", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void bntInTam_Click(object sender, EventArgs e)
        {
            var dsfullPhieuThuCT = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.IdPhieuThu == lbIdPT.Text);
            var listPhieuThu = DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.Id == lbIdPT.Text);
            var dsDMHH = DAO_DMSanPhamHH.Instance.GetListDMSanPhamHH();
            var dstheoBan = dsfullPhieuThuCT.Join(dsDMHH,
                o => o.MaHH,
                p => p.Ma,
                (o, p) => new
                {
                    Ten = p.Ten,
                    DonGia = o.DonGia,
                    SoLuong = o.SoLuong,
                    MaHH = o.MaHH,
                    MaCTPT = o.Id,
                    ThanhTien = o.ThanhToan,
                    NhomHH = p.Nhom,
                    IdPT = o.IdPhieuThu
                }).Join(listPhieuThu,
                k => k.IdPT,
                q => q.Id,
                (k, q) => new
                {
                    Ten = k.Ten,
                    DonGia = k.DonGia,
                    SoLuong = k.SoLuong,
                    ThanhTien = k.ThanhTien,
                    TongChiPhi = q.TongChiPhi,
                    ThanhToan = q.ThanhToan,
                    SoGiam = q.SoGiam,
                    Ngay = q.Ngay,
                    SoPhieu = q.SoPhieu,
                    MaBan = q.MaBan,
                    NguoiThu = q.NguoiThu,
                    HinhThucThanhToan = q.HinhThucThanhToan == "TienMat" ? "Tiền Mặt" : "Thanh Toán Online",
                    TrangThai = q.TrangThai
                }
                ).Where(q => q.TrangThai == "Chua_Thu");
            // var listChuaThu = DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.TrangThai == "Chua_Thu");
            // gridControl6.DataSource = dstheoBan;
            if (dstheoBan.Count() > 0)
            {
                ReportInBill bill = new ReportInBill();
                bill.DataSource = dstheoBan;
                bill.Print();
                bill.ShowPreviewDialog();
            }
            else XtraMessageBox.Show("Chọn biên lai cần in", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void FormBanHang_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control == true && e.KeyCode == Keys.I)
            {
                bntInTam.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.T)
            {
                bntThanhToan.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.H)
            {
                bntGiaoHang.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.S)
            {
                bntLuuBill.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.P)
            {
                bntInBill.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.D)
            {
                bntQuayLaiKham.PerformClick();
            }
        }

        private void flBill_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void FormBanHang_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        private void bntGiaoHang_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn xóa " + lbTenBan.Text + " không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    LoadBillChoThanhToan();
                    decimal TongGiam = 0;
                    decimal TongTang = 0;
                    TongTang = 0;
                    decimal ThanhTien = 0;
                    // DateTime? ngay = DateTime.Parse(lbThoiGianVao.Text);
                    string HinhThucThanhToan = "";
                    var listPT = DAO_BHBienLai.Instance.GetListBH_PhieuThu().Where(q => q.MaBan == lbMaBan.Text && q.TrangThai == "Chua_Thu" && q.SoPhieu == txtBillSoBienLai.Text).SingleOrDefault();
                    if (listPT != null)
                    {
                        string idPT = listPT.Id;
                        string idBan = listPT.MaBan;
                        if (idPT != "")
                        {
                            DAO_BHBienLai.Instance.UpdateBHPhieuThuKhiThu(idPT, TongTang, TongGiam, ThanhTien, "Da_Huy", HinhThucThanhToan);
                            DAO_BHBienLai.Instance.UpdateTrangThai_BH_PhieuThuCT(idPT, "Da_Huy");
                            DAO_DMBan.Instance.UpdateTrangThai(idBan, false);
                            LoadBillByBan();
                            Load_HeaderBill(idBan);
                            bntThanhToan.Enabled = true;
                            bntInTam.Enabled = true;
                            bntGiaoHang.Enabled = true;
                            bntInBill.Enabled = true;
                            gridView9.EditingValue = false;
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex + "Vui long kiểm tra lại hóa đơn");
                }

            }
        }

        private void tabFormControl1_Click(object sender, EventArgs e)
        {

        }

        private void tabFormControl1_PageClosed(object sender, PageClosedEventArgs e)
        {
            //Login f = new Login();
            //f.ShowDialog();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            Login f = new Login();
            this.Hide();
            this.Close();
            f.ShowDialog();
        }

        private void gridView8_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //try
            //{
            //    int row = gridView8.FocusedRowHandle;
            //    string TaiKhoanN = "TaiKhoan";
            //   // string TenNguoiDungN = "Ten";
            //    string QuyenSuDungN = "QuyenSuDung";
            //    string NgungSuDungN = "NgungSuDung";
            //    string MatKhauN = "MatKhau";
            //    object taiKhoan = gridView8.GetRowCellValue(row, TaiKhoanN);
            //    //object tenNguoiDung = gridView8.GetRowCellValue(row, TenNguoiDungN);
            //    object quyenSuDung = gridView8.GetRowCellValue(row, QuyenSuDungN);
            //    object ngungSuDung = gridView8.GetRowCellValue(row, NgungSuDungN);
            //    object matKhau = gridView8.GetRowCellValue(row, MatKhauN);
            //    string strTaiKhoan = taiKhoan.ToString();
            //    //string strTenNguoiDung = tenNguoiDung.ToString();
            //    string strQuyenSuDung = quyenSuDung.ToString();
            //    bool NgungSuDung = (bool)ngungSuDung;
            //    string MatKhau = matKhau.ToString();
            //    txtTaiKhoan.Text = strTaiKhoan;
            //    txtMatKhau.Text = MatKhau;
            //    txtNhapLaiMatKhau.Text = MatKhau;
            //    cbQuyenSuDung.Text = strQuyenSuDung;
            //    checkNgungSuDung.Checked = NgungSuDung;

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void gridView8_LostFocus(object sender, EventArgs e)
        {
           
        }

        private void gridView8_GotFocus(object sender, EventArgs e)
        {
            
        }

        private void gridView8_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void gridView8_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
         
        }

        private void gridView8_GridMenuItemClick(object sender, DevExpress.XtraGrid.Views.Grid.GridMenuItemClickEventArgs e)
        {
            
        }

        private void bntCapNhatNguoiDung_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int row = gridView8.FocusedRowHandle;
                string TaiKhoanN = "TaiKhoan";
                // string TenNguoiDungN = "Ten";
                string QuyenSuDungN = "QuyenSuDung";
                string NgungSuDungN = "NgungSuDung";
                string MatKhauN = "MatKhau";
                object taiKhoan = gridView8.GetRowCellValue(row, TaiKhoanN);
                //object tenNguoiDung = gridView8.GetRowCellValue(row, TenNguoiDungN);
                object quyenSuDung = gridView8.GetRowCellValue(row, QuyenSuDungN);
                object ngungSuDung = gridView8.GetRowCellValue(row, NgungSuDungN);
                object matKhau = gridView8.GetRowCellValue(row, MatKhauN);
                string strTaiKhoan = taiKhoan.ToString();
                //string strTenNguoiDung = tenNguoiDung.ToString();
                string strQuyenSuDung = quyenSuDung.ToString();
                bool NgungSuDung = (bool)ngungSuDung;
                string MatKhau = matKhau.ToString();
                txtTaiKhoan.Text = strTaiKhoan;
                txtMatKhau.Text = MatKhau;
                txtNhapLaiMatKhau.Text = MatKhau;
                cbQuyenSuDung.Text = strQuyenSuDung;
                checkNgungSuDung.Checked = NgungSuDung;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bntDoiMatKhau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DoiMatKhauNguoiDung f = new DoiMatKhauNguoiDung();
            f.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int c = gridView9.DataRowCount;
            var dsfullPhieuThuCT = DAO_BHBienLai.Instance.GetListBH_PhieuThuCT().Where(q => q.MaBan == lbMaBan.Text && q.TrangThai != "Da_Thu");
            //var dsfullPhieuThuCT = dsfull.Where(q => q.MaBan == lbMaBan.Text);
            var dsDMHH = DAO_DMSanPhamHH.Instance.GetListDMSanPhamHH();
            var dstheoBan = dsfullPhieuThuCT.Join(dsDMHH,
                o => o.MaHH,
                p => p.Ma,
                (o, p) => new
                {
                    Ten = p.Ten,
                    DonGia = o.DonGia,
                    SoLuong = o.SoLuong,
                    MaHH = o.MaHH,
                    MaCTPT = o.Id,
                    ThanhTien = o.ThanhToan,
                    NhomHH = p.Nhom,
                    TrangThai = o.TrangThai
                }).Where(q => q.TrangThai != "Da_Huy");

            int cdata = dstheoBan.Count();
            if (c != cdata)
            {
                LoadBillByBan();
            }
        }
    }
}