using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_BanHang2.DAO
{
    class DAO_BHPhieuThuCT
    {
        private static DAO_BHPhieuThuCT instance;
        internal static DAO_BHPhieuThuCT Instance
        {
            get
            {
                if (instance == null) instance = new DAO_BHPhieuThuCT();
                return DAO_BHPhieuThuCT.instance;
            }

            set
            {
                instance = value;
            }
        }
        public DAO_BHPhieuThuCT() { }
    }
}
