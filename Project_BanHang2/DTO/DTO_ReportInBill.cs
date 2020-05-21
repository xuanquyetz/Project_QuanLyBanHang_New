using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_BanHang2.DTO
{
   public class DTO_ReportInBill
    {
        private string ten;
        private decimal donGia;
        private decimal soLuong;
        private decimal thanhTien;
        private decimal tongChiPhi;
        private decimal thanhToan;
        private decimal soGiam;
        private DateTime? ngay;
        private int soPhieu;
        private string maBan;
        private string nguoiThu;
        private string hinhThucThanhToan;

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

        public decimal ThanhTien
        {
            get
            {
                return thanhTien;
            }

            set
            {
                thanhTien = value;
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

        public int SoPhieu
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
