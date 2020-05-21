using Project_BanHang2.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_BanHang2.DAO
{
    class DAO_DMBan
    {
        private static DAO_DMBan instance;
        internal static int chieuRong=160;
        internal static int chieuDai=80;

        internal static DAO_DMBan Instance
        {
            get
            {
                if (instance == null) instance = new DAO_DMBan();
                return DAO_DMBan.instance;
            }

            set
            {
                instance = value;
            }
        }
        public DAO_DMBan() { }
        public List<DTO_DMBan> GetListDMBan()
        {
            List<DTO_DMBan> listban = new List<DTO_DMBan>();
            DataTable data = KeNoiData.Instance.ExecuteQuery("SELECT *FROM dbo.DM_Ban ");
            foreach (DataRow item in data.Rows)
            {
                DTO_DMBan b = new DTO_DMBan(item);
                listban.Add(b);
            }
            return listban;
        }
        public void InsertDMBan(string ma, string ten, bool trangThai,bool vip, string ghiChu, string khuVuc)
        {
            KeNoiData.Instance.ExecuteNonQuery("INSERT INTO [dbo].[DM_Ban]([Ma] ,[Ten] ,[TrangThai] ,[Vip] ,[GhiChu], [MaKV] ) VALUES(N'" + ma + "',N'" + ten + "','" + trangThai + "','"+vip+"', N'"+ghiChu+"',N'"+khuVuc+"')");
        }
        public void DeleteDMBan(string id)
        {
            KeNoiData.Instance.ExecuteNonQuery("DELETE FROM dbo.DM_Ban where Ma= " + "'" + id + "'");
        }
        public void UpdateDMBan(string ma, string ten, bool trangThai, bool vip, string ghiChu, string khuVuc)
        {
            KeNoiData.Instance.ExecuteNonQuery("update DM_Ban set Ten=N'" + ten + "', TrangThai='"+trangThai+"', Vip='"+vip+"', GhiChu=N'"+ghiChu+"', MaKV=N'"+khuVuc+"' where Ma=N'"+ma+"'");
        }
        public void UpdateTrangThai(string ma, bool trangThai)
        {
            KeNoiData.Instance.ExecuteNonQuery("UPDATE dbo.DM_Ban SET TrangThai='"+trangThai+"' WHERE Ma=N'"+ma+"'");
        }
    }
}
