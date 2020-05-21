using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_BanHang2.DTO
{
    class DTO_DMBan
    {
        private string ma;
        private string ten;
        private bool trangThai;
        private bool vip;
        private string ghiChu;
        private string khuVuc;
        public DTO_DMBan(string ma, string ten, bool trangThai, bool vip, string ghiChu, string khuVuc)
        {
            this.ma = Ma;
            this.ten = Ten;
            this.trangThai = TrangThai;
            this.vip = Vip;
            this.ghiChu = GhiChu;
            this.khuVuc = KhuVuc;
        }
        public DTO_DMBan(DataRow row)
        {
            try
            {
                this.Ma = row["Ma"].ToString();
                this.Ten = row["Ten"].ToString();
                this.TrangThai = (bool)row["TrangThai"];
                this.Vip =(bool) row["Vip"];
                this.GhiChu = row["GhiChu"].ToString();
                this.KhuVuc = row["MaKV"].ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Kiểm tra lại dữ liệu " + ex.Message);
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

        public bool TrangThai
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

        public bool Vip
        {
            get
            {
                return vip;
            }

            set
            {
                vip = value;
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

        public string KhuVuc
        {
            get
            {
                return khuVuc;
            }

            set
            {
                khuVuc = value;
            }
        }
    }
}
