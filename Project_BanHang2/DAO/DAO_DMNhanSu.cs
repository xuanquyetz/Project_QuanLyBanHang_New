using Project_BanHang2.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_BanHang2.DAO
{
    class DAO_DMNhanSu
    {
        private static DAO_DMNhanSu instance;

        internal static DAO_DMNhanSu Instance
        {
            get
            {
                if (instance == null) instance = new DAO_DMNhanSu();
                return DAO_DMNhanSu.instance;
            }

            set
            {
                instance = value;
            }
        }
        public DAO_DMNhanSu() { }
        public List<DTO_DMNhanSu> GetListDM_NhanSu()
        {
            List<DTO_DMNhanSu> listdmnhansu = new List<DTO_DMNhanSu>();
            DataTable data = KeNoiData.Instance.ExecuteQuery("SELECT *FROM dbo.DM_NhanSu ");
            foreach (DataRow item in data.Rows)
            {
                DTO_DMNhanSu ns = new DTO_DMNhanSu(item);
                listdmnhansu.Add(ns);
            }
            return listdmnhansu;
        }
        public void Insert_DMNhanSu(string maSoNV, string ten, string diaChi, string dienThoai, string loai)
        {
            KeNoiData.Instance.ExecuteNonQuery("INSERT INTO [dbo].[DM_NhanSu]([Ma] ,[MaSoNV] ,[Ten] ,[DiaChi] ,[DienThoai] ,[Loai] ) VALUES(NEWID() ,N'" + maSoNV + "',N'" + ten + "',N'"+diaChi+"',N'"+dienThoai+"',N'"+loai+"')");
        }
        public void Delete_DMNhanSu(string ma)
        {
            KeNoiData.Instance.ExecuteNonQuery("DELETE FROM dbo.DM_NhanSu where Ma= " + "'" + ma + "'");
        }
        public void Update_DMNhanSu( string maSoNV, string ten, string diaChi, string dienThoai, string loai)
        {
            KeNoiData.Instance.ExecuteNonQuery("update DM_NhanSu set Ten=N'"+ten+"',DiaChi=N'"+diaChi+"',DienThoai=N'"+dienThoai+"',Loai=N'"+loai+"' where MaSoNV=N'"+maSoNV+"'");
        }
    }
}
