using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_BanHang2.DTO
{
    class DTO_DMNhanSu
    {
        private string ma;
        private string maSoNV;
        private string ten;
        private string dienThoai;
        private string diaChi;
        private string loai;
        public DTO_DMNhanSu(string ma, string maSoNV, string ten, string dienThoai, string diaChi, string loai)
        {
            this.ma = Ma;
            this.maSoNV = MaSoNV;
            this.ten = Ten;
            this.dienThoai = DienThoai;
            this.diaChi = DiaChi;
            this.loai = Loai;
        }
        public DTO_DMNhanSu(DataRow row)
        {
            try
            {
                this.Ma = row["Ma"].ToString();
                this.MaSoNV = row["MaSoNV"].ToString();
                this.Ten = row["Ten"].ToString();
                this.DienThoai = row["DienThoai"].ToString();
                this.DiaChi = row["DiaChi"].ToString();
                this.Loai = row["Loai"].ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Kiểm tra lại dữ liệu "+ex.Message);
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

        public string MaSoNV
        {
            get
            {
                return maSoNV;
            }

            set
            {
                maSoNV = value;
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

        public string DiaChi
        {
            get
            {
                return diaChi;
            }

            set
            {
                diaChi = value;
            }
        }

        public string Loai
        {
            get
            {
                return loai;
            }

            set
            {
                loai = value;
            }
        }

        public string DienThoai
        {
            get
            {
                return dienThoai;
            }

            set
            {
                dienThoai = value;
            }
        }
    }
}
