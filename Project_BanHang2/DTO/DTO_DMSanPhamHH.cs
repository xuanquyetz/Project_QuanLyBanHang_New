using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static DevExpress.XtraPrinting.Native.ExportOptionsPropertiesNames;

namespace Project_BanHang2.DTO
{
    class DTO_DMSanPhamHH
    {
        private string ma;
        private string ten;
        private string tenVietTat;
        private string nhom;
        private decimal donGia;
        private string donVi;
        private string noiSX;
        private string nuocSX;
        private bool ngungTheoDoi;
        private string tuKhoaTimKiem;
        private decimal chieuCao;
        private decimal chieuDai;
        private decimal chieuRong;
        private decimal banKinh;
        private string tenHinh;
        private string tenHinh1;
        private string tenHinh2;
        [NotMapped]
        private Image hinhAnh;
        private Image hinhAnh2;
        public DTO_DMSanPhamHH(string ma, string ten, string tenVietTat, string nhom, decimal donGia, string donVi, string noiSX, string nuocSX, bool ngungTheoDoi, string tuKhoaTimKiem, decimal chieuCao, decimal chieuDai, decimal chieuRong, decimal banKinh, string tenHinh, string tenHinh1, string tenHinh2)
        {
            this.ma = Ma;
            this.ten = Ten;
            this.tenVietTat = TenVietTat;
            this.nhom = Nhom;
            this.donGia = DonGia;
            this.donVi = DonVi;
            this.noiSX = NoiSX;
            this.nuocSX = NuocSX;
            this.ngungTheoDoi = NgungTheoDoi;
            this.tuKhoaTimKiem = TuKhoaTimKiem;
            this.chieuCao = ChieuCao;
            this.chieuDai = ChieuDai;
            this.chieuRong = ChieuRong;
            this.tenHinh = TenHinh;
            this.tenHinh1 = TenHinh1;
            this.tenHinh2 = TenHinh2;
        }
        public DTO_DMSanPhamHH(DataRow row)
        {
            try
            {
                this.Ma = row["Ma"].ToString();
                this.Ten = row["Ten"].ToString();
                this.TenVietTat = row["TenViettat"].ToString();
                this.Nhom = row["Nhom"].ToString();
                this.DonGia = (decimal)row["DonGia"];
                this.DonVi = row["DonVi"].ToString();
                this.NoiSX = row["NoiSX"].ToString();
                this.NuocSX = row["NuocSX"].ToString();
                this.NgungTheoDoi = (bool)row["NgungTheoDoi"];
                this.TuKhoaTimKiem = row["TuKhoaTimKiem"].ToString();
                this.ChieuCao = (decimal)row["ChieuCao"];
                this.ChieuDai = (decimal)row["ChieuDai"];
                this.ChieuRong = (decimal)row["ChieuRong"];
                this.BanKinh = (decimal)row["BanKinh"];
                this.TenHinh = row["TenHinh"].ToString();
                this.TenHinh1 = row["TenHinh2"].ToString();
                this.TenHinh2 = row["TenHinh3"].ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }
        public string Ma
        {
            get
            {
                return ma;
            }

            set
            {
                ma = value;
            }
        }

        public string Ten
        {
            get
            {
                return ten;
            }

            set
            {
                ten = value;
            }
        }

        public string TenVietTat
        {
            get
            {
                return tenVietTat;
            }

            set
            {
                tenVietTat = value;
            }
        }

        public string Nhom
        {
            get
            {
                return nhom;
            }

            set
            {
                nhom = value;
            }
        }

        public decimal DonGia
        {
            get
            {
                return donGia;
            }

            set
            {
                donGia = value;
            }
        }

        public string DonVi
        {
            get
            {
                return donVi;
            }

            set
            {
                donVi = value;
            }
        }

        public string NoiSX
        {
            get
            {
                return noiSX;
            }

            set
            {
                noiSX = value;
            }
        }

        public string NuocSX
        {
            get
            {
                return nuocSX;
            }

            set
            {
                nuocSX = value;
            }
        }

        public bool NgungTheoDoi
        {
            get
            {
                return ngungTheoDoi;
            }

            set
            {
                ngungTheoDoi = value;
            }
        }

        public string TuKhoaTimKiem
        {
            get
            {
                return tuKhoaTimKiem;
            }

            set
            {
                tuKhoaTimKiem = value;
            }
        }

        public decimal ChieuCao
        {
            get
            {
                return chieuCao;
            }

            set
            {
                chieuCao = value;
            }
        }

        public decimal ChieuDai
        {
            get
            {
                return chieuDai;
            }

            set
            {
                chieuDai = value;
            }
        }

        public decimal ChieuRong
        {
            get
            {
                return chieuRong;
            }

            set
            {
                chieuRong = value;
            }
        }

        public decimal BanKinh
        {
            get
            {
                return banKinh;
            }

            set
            {
                banKinh = value;
            }
        }

        public string TenHinh
        {
            get
            {
                return tenHinh;
            }

            set
            {
                tenHinh = value;
            }
        }

        public string TenHinh1
        {
            get
            {
                return tenHinh1;
            }

            set
            {
                tenHinh1 = value;
            }
        }

        public string TenHinh2
        {
            get
            {
                return tenHinh2;
            }

            set
            {
                tenHinh2 = value;
            }
        }

        public Image HinhAnh
        {
            get
            {
                try
                {
                    if (!string.IsNullOrEmpty(TenHinh))
                    {
                        if (File.Exists(TenHinh))
                            return Image.FromFile(TenHinh);
                    }
                    return null;
                }
                catch (Exception ex)
                {

                    throw;
                }
                
                
            }
            set
            {
                
                //string forder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                //string pathHinh = System.IO.Path.Combine(forder, "images\\"+"ttt" );
                //Image i = hinhAnh;
                //i.Save(pathHinh);
                ////Image i = hinhAnh;
                ////i.Save("jjj");
                hinhAnh = value;
            }


        }

        public Image HinhAnh2
        {
            get
            {
                if (!string.IsNullOrEmpty(TenHinh1))
                {
                    if (File.Exists(TenHinh1))
                        return Image.FromFile(TenHinh1);
                }
                return null;
            }

            set
            {
                hinhAnh2 = value;
            }
        }
    }
}
