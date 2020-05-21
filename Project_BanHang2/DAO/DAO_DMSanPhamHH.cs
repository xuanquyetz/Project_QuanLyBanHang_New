using Project_BanHang2.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_BanHang2.DAO
{
    class DAO_DMSanPhamHH
    {
        private static DAO_DMSanPhamHH instance;

        internal static DAO_DMSanPhamHH Instance
        {
            get
            {
                if (instance == null) instance = new DAO_DMSanPhamHH();
                return DAO_DMSanPhamHH.instance;
            }

            set
            {
                instance = value;
            }
        }
        public DAO_DMSanPhamHH() { }
        public List<DTO_DMSanPhamHH> GetListDMSanPhamHH()
        {
            List<DTO_DMSanPhamHH> listdmnhomhh = new List<DTO_DMSanPhamHH>();
            DataTable data = KeNoiData.Instance.ExecuteQuery("SELECT *FROM dbo.DM_HangHoa ");
            foreach (DataRow item in data.Rows)
            {
                DTO_DMSanPhamHH nhom = new DTO_DMSanPhamHH(item);
                listdmnhomhh.Add(nhom);
            }
            return listdmnhomhh;

        }
        public List<DTO_DMSanPhamHH> GetListDMSanPhamHHByTimKiem(string kyTuTimKiem)
        {
            List<DTO_DMSanPhamHH> listdmnhomhh = new List<DTO_DMSanPhamHH>();
            DataTable data = KeNoiData.Instance.ExecuteQuery("SELECT *FROM dbo.DM_HangHoa WHERE Ten LIKE '%"+kyTuTimKiem+"%' ");
            foreach (DataRow item in data.Rows)
            {
                DTO_DMSanPhamHH sp = new DTO_DMSanPhamHH(item);
                listdmnhomhh.Add(sp);
            }
            return listdmnhomhh;

        }
        public void InsertDMSanPhamHH( string ma, string ten, string tenVietTat, string nhom, decimal donGia, string donVi, string noiSX, string nuocSX, bool ngungTheoDoi, string tuKhoaTimKiem, decimal chieuCao, decimal chieuDai, decimal chieuRong, decimal banKinh, string tenHinh, string tenHinh1)
        {
            KeNoiData.Instance.ExecuteNonQuery("INSERT INTO [dbo].[DM_HangHoa] ([Ma],[Ten] ,[TenViettat] ,[Nhom],[DonGia],[DonVi],[NoiSX],[NuocSX],[NgungTheoDoi],[TuKhoaTimKiem],[ChieuCao],[ChieuDai],[ChieuRong],[BanKinh],[TenHinh],[TenHinh2])VALUES (NEWID(),N'"+ten+"',N'"+tenVietTat+"',N'"+nhom+"',"+donGia+",N'"+donVi+"',N'"+noiSX+"',N'"+nuocSX+"',N'"+ngungTheoDoi+"',N'"+tuKhoaTimKiem+"',"+chieuCao+","+chieuDai+","+chieuRong+","+banKinh+",N'"+tenHinh+"',N'"+tenHinh1+"')");
        }
        public void DeleteDMSanPhamHH(string ma)
        {
            KeNoiData.Instance.ExecuteNonQuery("DELETE From DM_HangHoa where Ma ='"+ma+"'");
        }
        public void UpdateDMSanPhamHH(string ma,string ten, string tenVietTat, string nhom, decimal donGia, string donVi, string noiSX, string nuocSX, bool ngungTheoDoi, string tuKhoaTimKiem, decimal chieuCao, decimal chieuDai, decimal chieuRong, decimal banKinh)
        {
            KeNoiData.Instance.ExecuteNonQuery("Update DM_HangHoa set Ten=N'"+ten+"',TenViettat=N'"+tenVietTat+"',Nhom=N'"+nhom+"',DonGia="+donGia+",DonVi=N'"+donVi+"',NoiSX=N'"+noiSX+"', NuocSX=N'"+nuocSX+"',NgungTheoDoi=N'"+ngungTheoDoi+"',TuKhoaTimKiem=N'"+tuKhoaTimKiem+"',ChieuCao="+chieuCao+",ChieuDai="+chieuDai+",ChieuRong="+chieuRong+",BanKinh="+banKinh+"where Ma=N'" + ma + "'");
        }
    }
}
