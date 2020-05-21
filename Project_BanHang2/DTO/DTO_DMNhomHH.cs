using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_BanHang2.DTO
{
    class DTO_DMNhomHH
    {
        private string ma;
        private string ten;
        private string phanLoai;
        //private int stt;
        public DTO_DMNhomHH(string ma, string ten, string phanLoai)
        {
            this.ma = Ma;
            this.ten = Ten;
            this.phanLoai = PhanLoai;
            //this.stt = Stt; 
        }
        public DTO_DMNhomHH(DataRow row)
        {
            this.Ma = row["Ma"].ToString();
            this.Ten = row["Ten"].ToString();
            this.PhanLoai = row["PhanLoai"].ToString();
            //this.Stt = (int)row["STT"];
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

        public string PhanLoai
        {
            get
            {
                return phanLoai;
            }

            set
            {
                phanLoai = value;
            }
        }

        //public int Stt
        //{
        //    get
        //    {
        //        return stt;
        //    }

        //    set
        //    {
        //        stt = value;
        //    }
        //}
    }
}

