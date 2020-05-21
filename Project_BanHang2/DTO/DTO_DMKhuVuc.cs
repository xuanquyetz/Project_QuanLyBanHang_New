using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_BanHang2.DTO
{
    class DTO_DMKhuVuc
    {
        private string ma;
        private string ten;
        private string ghiChu;
        public DTO_DMKhuVuc(string ma, string ten, string ghiChu)
        {
            this.ma = Ma;
            this.ten = Ten;
            this.ghiChu = GhiChu;
        }
        public DTO_DMKhuVuc(DataRow row)
        {
            try
            {
                this.Ma = row["Ma"].ToString();
                this.Ten = row["Ten"].ToString();
                this.GhiChu = row["GhiChu"].ToString();
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
    }
}
