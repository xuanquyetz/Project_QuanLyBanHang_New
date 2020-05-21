using Project_BanHang2.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_BanHang2.DAO
{
    class DAO_DMKhuVuc
    {
        private static DAO_DMKhuVuc instance;

        internal static DAO_DMKhuVuc Instance
        {
            get
            {
                if (instance == null) instance = new DAO_DMKhuVuc();
                return DAO_DMKhuVuc.instance;
            }

            set
            {
                instance = value;
            }
        }
        public DAO_DMKhuVuc() { }
        public List<DTO_DMKhuVuc> GetListDMKhuVuc()
        {
            List<DTO_DMKhuVuc> listban = new List<DTO_DMKhuVuc>();
            DataTable data = KeNoiData.Instance.ExecuteQuery("select * from DM_KhuVuc ");
            foreach (DataRow item in data.Rows)
            {
                DTO_DMKhuVuc b = new DTO_DMKhuVuc(item);
                listban.Add(b);
            }
            return listban;
        }
        public void InsertDMKhuVuc(string ma, string ten, string ghiChu)
        {
            KeNoiData.Instance.ExecuteNonQuery("INSERT INTO [dbo].[DM_KhuVuc]([Ma] ,[Ten] ,[GhiChu] ) VALUES(N'" + ma + "',N'" + ten  + "', N'" + ghiChu + "')");

        }
        public void DeleteDMKhuVuc(string id)
        {
            KeNoiData.Instance.ExecuteNonQuery("DELETE FROM dbo.DM_KhuVuc where Ma= " + "'" + id + "'");
        }
        public void UpdateDMKhuVuc(string ma, string ten, string ghiChu)
        {
            KeNoiData.Instance.ExecuteNonQuery("update DM_KhuVuc set Ten=N'" + ten + "', GhiChu=N'" + ghiChu  + "' where Ma=N'" + ma + "'");
        }
    }
}
