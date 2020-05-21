using Project_BanHang2.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_BanHang2.DAO
{
    class DAO_DMNhomHH
    {
        private static DAO_DMNhomHH instance;

        internal static DAO_DMNhomHH Instance
        {
            get
            {
                if (instance == null) instance = new DAO_DMNhomHH();
                return DAO_DMNhomHH.instance;
            }

            set
            {
                instance = value;
            }
        }
        public DAO_DMNhomHH() { }
        public List<DTO_DMNhomHH> GetListDM_NhomHH()
        {
            List<DTO_DMNhomHH> listdmnhomhh = new List<DTO_DMNhomHH>();
            DataTable data = KeNoiData.Instance.ExecuteQuery("SELECT *FROM dbo.DM_NhomHH ");
            foreach (DataRow item in data.Rows)
            {
                DTO_DMNhomHH nhom = new DTO_DMNhomHH(item);
                listdmnhomhh.Add(nhom);
            }
            return listdmnhomhh;

        }
        public void InsertDMNhomHH (string maNhom, string tenNhom, string phanLoai)
        {
            KeNoiData.Instance.ExecuteNonQuery("INSERT INTO [dbo].[DM_NhomHH]([Ma] ,[Ten] ,[PhanLoai] ) VALUES(N'"+maNhom+"',N'"+tenNhom+"',N'"+phanLoai+"')");
            
        }
        public void DeleteDMNhomHH(string id)
        {
            KeNoiData.Instance.ExecuteNonQuery("DELETE FROM dbo.DM_NhomHH where Ma= " + "'" + id + "'");
        }
        public void UpdateDMNhomHH(string ma, string ten, string phanLoai)
        {
            KeNoiData.Instance.ExecuteNonQuery("update DM_NhomHH set Ten=N'"+ten+"', PhanLoai=N'"+phanLoai+"' where Ma=N'"+ma+"'");
        }
    }
}
