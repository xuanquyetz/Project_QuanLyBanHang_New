using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_BanHang2.DTO
{
    class DTO_BHPhieuThu
    {
        private string id;
        private DateTime? ngay;
        private string nguoiThu;
        private string quayThu;
        private string maKhach;
        private string trangThai;
        private string lyDoHuy;
        private bool giam100;
        private string lyDoGiam;
        private decimal tongChiPhi;
        private decimal tongThu;
        private decimal giam;
        private decimal soGiam;
        private decimal tang;
        private decimal soTang;
        private decimal thanhToan;
        private string noiDungThanhToan;
        private decimal tongGiam;
        private decimal tongTang;
        private string soPhieu;
        private string maBan;
        private string hinhThucThanhToan;
        public DTO_BHPhieuThu(string id,DateTime?ngay, string nguoiThu,string quayThu, string maKhach, string trangThai, string lyDoHuy,
            bool giam100, string lyDoGiam, decimal tongChiPhi, decimal tongThu, decimal giam, decimal soGiam, decimal tang, decimal soTang,
            decimal thanhToan, string noiDungThanhToan, decimal tongGiam, decimal tongTang, string soPhieu, string maBan, string hinhThucThanhToan)
        {
            this.id = Id;
            this.ngay = Ngay;
            this.nguoiThu = NguoiThu;
            this.quayThu = QuayThu;
            this.maKhach = MaKhach;
            this.trangThai = TrangThai;
            this.lyDoHuy = LyDoHuy;
            this.giam100 = Giam100;
            this.lyDoGiam = LyDoGiam;
            this.tongChiPhi = TongChiPhi;
            this.tongThu = TongThu;
            this.giam = Giam;
            this.soGiam = SoGiam;
            this.tang = Tang;
            this.soTang = SoTang;
            this.thanhToan = ThanhToan;
            this.noiDungThanhToan = NoiDungThanhToan;
            this.tongGiam = TongGiam;
            this.tongTang = TongTang;
            this.soPhieu = SoPhieu;
            this.maBan = MaBan;
            this.hinhThucThanhToan = HinhThucThanhToan;
        }
        public DTO_BHPhieuThu(DataRow row)
        {
            try
            {
                this.Id = row["ID"].ToString();
                this.Ngay = (DateTime?)row["Ngay"];
                this.NguoiThu = row["NguoiThu"].ToString();
                this.QuayThu = row["QuayThu"].ToString();
                this.MaKhach = row["MaKhach"].ToString();
                this.TrangThai = row["TrangThai"].ToString();
                this.LyDoHuy = row["LyDoHuy"].ToString();
                this.Giam100 = (bool)row["Giam100"];
                this.LyDoGiam = row["LyDoGiam"].ToString();
                this.TongChiPhi =(decimal) row["TongChiPhi"];
                this.TongThu =(decimal) row["TongThu"];
                this.Giam = (decimal)row["Giam"];
                this.SoGiam = (decimal)row["SoGiam"];
                this.Tang = (decimal)row["Tang"];
                this.SoTang = (decimal)row["SoTang"];
                this.ThanhToan = (decimal)row["ThanhToan"];
                this.noiDungThanhToan = row["NoiDungThu"].ToString();
                this.TongGiam = (decimal)row["TongGiam"];
                this.TongTang = (decimal)row["TongTang"];
                this.SoPhieu = row["SoPhieu"].ToString();
                this.MaBan = row["MaBan"].ToString();
                this.HinhThucThanhToan = row["HinhThucThanhToan"].ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Kiểm tra lại dữ liệu " + ex.Message);
            }

        }


        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public DateTime? Ngay
        {
            get
            {
                return ngay;
            }

            set
            {
                ngay = value;
            }
        }

        public string NguoiThu
        {
            get
            {
                return nguoiThu;
            }

            set
            {
                nguoiThu = value;
            }
        }

        public string QuayThu
        {
            get
            {
                return quayThu;
            }

            set
            {
                quayThu = value;
            }
        }

        public string MaKhach
        {
            get
            {
                return maKhach;
            }

            set
            {
                maKhach = value;
            }
        }

        public string TrangThai
        {
            get
            {
                return trangThai;
            }

            set
            {
                trangThai = value;
            }
        }

        public string LyDoHuy
        {
            get
            {
                return lyDoHuy;
            }

            set
            {
                lyDoHuy = value;
            }
        }

        public bool Giam100
        {
            get
            {
                return giam100;
            }

            set
            {
                giam100 = value;
            }
        }

        public string LyDoGiam
        {
            get
            {
                return lyDoGiam;
            }

            set
            {
                lyDoGiam = value;
            }
        }

        public decimal TongChiPhi
        {
            get
            {
                return tongChiPhi;
            }

            set
            {
                tongChiPhi = value;
            }
        }

        public decimal TongThu
        {
            get
            {
                return tongThu;
            }

            set
            {
                tongThu = value;
            }
        }

        public decimal Giam
        {
            get
            {
                return giam;
            }

            set
            {
                giam = value;
            }
        }

        public decimal SoGiam
        {
            get
            {
                return soGiam;
            }

            set
            {
                soGiam = value;
            }
        }

        public decimal Tang
        {
            get
            {
                return tang;
            }

            set
            {
                tang = value;
            }
        }

        public decimal SoTang
        {
            get
            {
                return soTang;
            }

            set
            {
                soTang = value;
            }
        }

        public decimal ThanhToan
        {
            get
            {
                return thanhToan;
            }

            set
            {
                thanhToan = value;
            }
        }

        public string NoiDungThanhToan
        {
            get
            {
                return noiDungThanhToan;
            }

            set
            {
                noiDungThanhToan = value;
            }
        }

        public decimal TongGiam
        {
            get
            {
                return tongGiam;
            }

            set
            {
                tongGiam = value;
            }
        }

        public decimal TongTang
        {
            get
            {
                return tongTang;
            }

            set
            {
                tongTang = value;
            }
        }

        public string SoPhieu
        {
            get
            {
                return soPhieu;
            }

            set
            {
                soPhieu = value;
            }
        }

        public string MaBan
        {
            get
            {
                return maBan;
            }

            set
            {
                maBan = value;
            }
        }

        public string HinhThucThanhToan
        {
            get
            {
                return hinhThucThanhToan;
            }

            set
            {
                hinhThucThanhToan = value;
            }
        }
    }
}
