using Project_BanHang2.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Project_BanHang2.FormBanHang;

namespace Project_BanHang2.DAO
{
    class DAO_DMNguoiDung
    {
        private static DAO_DMNguoiDung instance;
        internal static DAO_DMNguoiDung Instance
        {
            get
            {
                if (instance == null) instance = new DAO_DMNguoiDung();
                return DAO_DMNguoiDung.instance;
            }

            set
            {
                instance = value;
            }
        }
        public DAO_DMNguoiDung() { }
        public List<DTO_DMNguoiDung> GetListNguoiDung()
        {
            List<DTO_DMNguoiDung> listnguoidung = new List<DTO_DMNguoiDung>();
            DataTable data = KeNoiData.Instance.ExecuteQuery("SELECT *FROM dbo.DM_NguoiDung");
            foreach (DataRow item in data.Rows)
            {
                DTO_DMNguoiDung b = new DTO_DMNguoiDung(item);
                listnguoidung.Add(b);
            }
            return listnguoidung;
        }
        public void InsertNguoiDung(string taiKhoan, string matKhau, string quyenSuDung, string maNhanSU, bool ngungTheoDoi)
        {
            KeNoiData.Instance.ExecuteNonQuery("INSERT INTO [dbo].[DM_NguoiDung]([TaiKhoan],[MatKhau],[QuyenSuDung],[MaNhanSu],[NgungSuDung]) VALUES(N'"+taiKhoan+"','"+matKhau+"',N'"+quyenSuDung+"',N'"+maNhanSU+"','"+ngungTheoDoi+"')");
        }
        public void UpdateNguoiDung(string taiKhoan, string matKhau, string quyenSuDung, string maNhanSU, bool ngungTheoDoi)
        {
            KeNoiData.Instance.ExecuteNonQuery("UPDATE dbo.DM_NguoiDung SET MatKhau='" + matKhau + "',QuyenSuDung=N'" + quyenSuDung + "',NgungSuDung='" + ngungTheoDoi + "',MaNhanSu='" + maNhanSU + "' WHERE TaiKhoan=N'" + taiKhoan + "'");
        }
        public void ReadConnect_Chung()
        {
            try
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

                if (!Directory.Exists(path + @"\log"))
                {
                    Directory.CreateDirectory(path + @"\log");
                }
                FileStream streamCLS = new FileStream(Path.Combine(path, "log\\config.txt"), FileMode.Open);
                StreamReader readCLS = new StreamReader(streamCLS, Encoding.Unicode);
                var chuoicls = readCLS.ReadLine();
                string[] araylistchuoi = chuoicls.Split(new char[] { '\t' });
                BienToanCuc.IpSV = araylistchuoi[0];
                BienToanCuc.DataBaseName = araylistchuoi[1];
                BienToanCuc.User = araylistchuoi[2];
                BienToanCuc.Pass = araylistchuoi[3];
                readCLS.Close();
                streamCLS.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
    }
}
