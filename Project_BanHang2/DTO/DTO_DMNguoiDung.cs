using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_BanHang2.DTO
{
    class DTO_DMNguoiDung
    {
        private string taiKhoan;
        private string matKhau;
        private string quyenSuDung;
        private string maNhanSu;
        private bool ngungSuDung;
        public DTO_DMNguoiDung(string taiKhoan, string matKhau, string quyenSuDung, string maNhanSu, bool ngungSuDung)
        {
            this.taiKhoan = TaiKhoan;
            this.matKhau = MatKhau;
            this.quyenSuDung = QuyenSuDung;
            this.maNhanSu = MaNhanSu;
            this.ngungSuDung = NgungSuDung;
        }
        public DTO_DMNguoiDung(DataRow row)
        {
            try
            {
                this.TaiKhoan = row["TaiKhoan"].ToString();
                this.MatKhau = row["MatKhau"].ToString();
                this.QuyenSuDung = row["QuyenSuDung"].ToString();
                this.MaNhanSu = row["MaNhanSu"].ToString();
                this.NgungSuDung = (bool)row["NgungSuDung"];
            }
            catch (Exception ex)
            {

                MessageBox.Show("Kiểm tra lại dữ liệu " + ex.Message);
            }

        }
        public string TaiKhoan
        {
            get
            {
                return taiKhoan;
            }

            set
            {
                taiKhoan = value;
            }
        }

        public string MatKhau
        {
            get
            {
                return matKhau;
            }

            set
            {
                matKhau = value;
            }
        }

        public string QuyenSuDung
        {
            get
            {
                return quyenSuDung;
            }

            set
            {
                quyenSuDung = value;
            }
        }

        public string MaNhanSu
        {
            get
            {
                return maNhanSu;
            }

            set
            {
                maNhanSu = value;
            }
        }

        public bool NgungSuDung
        {
            get
            {
                return ngungSuDung;
            }

            set
            {
                ngungSuDung = value;
            }
        }
    }
}
