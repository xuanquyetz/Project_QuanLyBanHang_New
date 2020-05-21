using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_BanHang2.DTO
{
    class DTO_BHPhieuThuCT
    {
        private string id;
        private string idPhieuThu;
        private DateTime? ngay;
        private string maKhach;
        private DateTime? tGThuTien;
        private string maHH;
        private string donVi;
        private string maKho;
        private string ghiChu;
        private decimal soLuong;
        private decimal donGia;
        private decimal giam;
        private decimal soGiam;
        private decimal tang;
        private decimal soTang;
        private decimal thanhToan;
        private string trangThai;
        private string maBan;
        public DTO_BHPhieuThuCT(string id, string idPhieuThu, DateTime? ngay,string maKhach,DateTime? tGThuTien, string maHH, string donVi, string maKho,string ghiChu,
           decimal soLuong, decimal donGia, decimal giam, decimal soGiam, decimal tang, decimal soTang, decimal thanhToan, string trangThai, string maBan)
        {
            this.id = Id;
            this.idPhieuThu = IdPhieuThu;
            this.ngay = Ngay;
            this.maKhach = MaKhach;
            this.tGThuTien = TGThuTien;
            this.maHH = MaHH;
            this.donVi = DonVi;
            this.maKho = MaKho;
            this.ghiChu = GhiChu;
            this.soLuong = SoLuong;
            this.donGia = DonGia;
            this.giam = Giam;
            this.soGiam = SoGiam;
            this.tang = Tang;
            this.soTang = SoTang;
            this.thanhToan = ThanhToan;
            this.trangThai = TrangThai;
            this.maBan = MaBan;
        }
        public DTO_BHPhieuThuCT(DataRow row)
        {
            try
            {
                this.Id = row["ID"].ToString();
                this.IdPhieuThu = row["IDPhieuThu"].ToString();
                this.Ngay = (DateTime?)row["Ngay"];
                this.MaKhach = row["MaKhach"].ToString();
                this.TGThuTien = (DateTime?)row["TGThuTien"];
                this.MaKhach = row["MaKhach"].ToString();
                this.MaHH = row["MaHH"].ToString();
                this.DonVi= row["DonVi"].ToString();
                this.MaKho = row["MaKho"].ToString();
                this.GhiChu = row["GhiChu"].ToString();
                this.SoLuong = (decimal)row["SoLuong"];
                this.DonGia = (decimal)row["DonGia"];
                this.Giam = (decimal)row["Giam"];
                this.SoGiam = (decimal)row["SoGiam"];
                this.Tang = (decimal)row["Tang"];
                this.SoTang = (decimal)row["SoTang"];
                this.ThanhToan = (decimal)row["ThanhToan"];
                this.TrangThai = row["TrangThai"].ToString();
                this.MaBan = row["MaBan"].ToString();
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

      public string IdPhieuThu
        {
            get
            {
                return idPhieuThu;
            }

            set
            {
                idPhieuThu = value;
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

        public DateTime? TGThuTien
        {
            get
            {
                return tGThuTien;
            }

            set
            {
                tGThuTien = value;
            }
        }

        public string MaHH
        {
            get
            {
                return maHH;
            }

            set
            {
                maHH = value;
            }
        }

        public string GhiChu
        {
            get
            {
                return ghiChu;
            }

            set
            {
                ghiChu = value;
            }
        }

        public decimal SoLuong
        {
            get
            {
                return soLuong;
            }

            set
            {
                soLuong = value;
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

        public string MaKho
        {
            get
            {
                return maKho;
            }

            set
            {
                maKho = value;
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
    }
}
