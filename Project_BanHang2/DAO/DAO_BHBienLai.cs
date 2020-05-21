using Project_BanHang2.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_BanHang2.DAO
{
    class DAO_BHBienLai
    {
        private static DAO_BHBienLai instance;
        internal static DAO_BHBienLai Instance
        {
            get
            {
                if (instance == null) instance = new DAO_BHBienLai();
                return DAO_BHBienLai.instance;
            }

            set
            {
                instance = value;
            }
        }
        public DAO_BHBienLai() { }
        public List<DTO_BHPhieuThu> GetListBH_PhieuThu()
        {
            List<DTO_BHPhieuThu> listpt = new List<DTO_BHPhieuThu>();
            DataTable data = KeNoiData.Instance.ExecuteQuery("SELECT *FROM dbo.BH_PhieuThu");
            foreach (DataRow item in data.Rows)
            {
                DTO_BHPhieuThu pt = new DTO_BHPhieuThu(item);
                listpt.Add(pt);
            }
            return listpt;
        }
        public List<DTO_BHPhieuThuCT> GetListBH_PhieuThuCT()
        {
            List<DTO_BHPhieuThuCT> listptct = new List<DTO_BHPhieuThuCT>();
            DataTable data = KeNoiData.Instance.ExecuteQuery("SELECT *FROM dbo.BH_PhieuThuCT");
            foreach (DataRow item in data.Rows)
            {
                DTO_BHPhieuThuCT ptct = new DTO_BHPhieuThuCT(item);
                listptct.Add(ptct);
            }
            return listptct;
        }
        public void InsertBHPhieuThu( string nguoiThu, string quayThu, string maKhach, string trangThai, string maBan)
        {
            KeNoiData.Instance.ExecuteNonQuery("exec sp_InsertBH_PhieuThu @nguoiThu , @quayThu , @maKhach , @trangThai , @maBan", new object[] { nguoiThu, quayThu, maKhach, trangThai,maBan});
        }
        public void InsertBHPhieuThuCT( string idPhieuThu, string maKhach, string maHH, string donVi, string maKho, string ghiChu,
           decimal soLuong, decimal donGia, decimal giam, decimal soGiam, decimal tang, decimal soTang, string trangThai, string maBan)
        {
            KeNoiData.Instance.ExecuteNonQuery("exec sp_InsertBH_PhieuThuCT @maKhach , @maHH , @donVi , @maKho , @ghiChu , @soLuong , @donGia , @giam , @soGiam , @tang , @soTang , @trangThai , @maBan , @idPhieuThu", new object[] { maKhach, maHH, donVi, maKho, ghiChu, soLuong, donGia, giam, soGiam, tang, soTang, trangThai, maBan, idPhieuThu });
        }
        public void UpdateSLBHPhieuThuCT(decimal soLuong, string Id, decimal thanhToan)
        {
            KeNoiData.Instance.ExecuteNonQuery("UPDATE dbo.BH_PhieuThuCT SET SoLuong="+soLuong+",ThanhToan="+thanhToan+" WHERE ID='"+Id+"'");
        }
        public void UpdateBHPhieuThu_TongThu_ThanhToan(string id, decimal tongChiPhi)
        {
            KeNoiData.Instance.ExecuteNonQuery("UPDATE dbo.BH_PhieuThu SET TongChiPhi="+tongChiPhi+",ThanhToan=("+tongChiPhi+"-SoGiam+SoTang) WHERE ID='"+id+"'");
        }
        public void DeletePhieuThuCT(string id)
        {
            KeNoiData.Instance.ExecuteNonQuery("DELETE dbo.BH_PhieuThuCT WHERE ID='"+id+"'");
        }
        public void UpdateBHPhieuThuKhiThu(string idPT, decimal soTang,decimal soGiam, decimal thanhToan, string trangThai,string hinhThucThanhToan)
        {
            KeNoiData.Instance.ExecuteNonQuery("UPDATE dbo.BH_PhieuThu SET SoGiam="+soGiam+", SoTang="+soTang+",ThanhToan="+thanhToan+",TrangThai=N'"+trangThai+"',HinhThucThanhToan=N'"+hinhThucThanhToan+"' WHERE ID='"+idPT+"'");
        }
        public void UpdateTrangThai_BH_PhieuThuCT(string idPT, string trangThai)
        {
            KeNoiData.Instance.ExecuteNonQuery("UPDATE dbo.BH_PhieuThuCT SET TrangThai=N'"+trangThai+"' WHERE IDPhieuThu='"+idPT+"'");
        }

    }
}
