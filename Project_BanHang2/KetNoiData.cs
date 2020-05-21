using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///*/*using System.Windows.Forms**//;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using static Project_BanHang2.CauHinhCSDL;
using static Project_BanHang2.FormBanHang;
using System.IO;

namespace Project_BanHang2
{

    public class KeNoiData // Tạo class ket nối
    {
       
        private static KeNoiData instance; //Ctrl+.
        public static KeNoiData Instance
        {
            
            get
            { if (instance == null) instance = new KeNoiData();
                return instance;
            }

           private set
            {
              KeNoiData.instance = value;
            }
            
        }
       
        public KeNoiData() {
           
            
        }
       // string s = ..IpSV;
        private string connectionstr = @"Data Source=" + BienToanCuc.IpSV + ";Initial Catalog=" + BienToanCuc.DataBaseName + ";User ID=" + BienToanCuc.User + ";Password=" + BienToanCuc.Pass ;
        // private string connectionstr = @"Data Source=113.161.147.161;Initial Catalog=SUNS_HIS;User ID=suns;Password=suns@12346#";
  
        public DataTable ExecuteQuery(string query,object[] paramater =null)
            
       {
           
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionstr))

                try
                {
                    {

                        connection.Open();

                        SqlCommand Command = new SqlCommand(query, connection);
                        if (paramater != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 0;
                            foreach (string Item in listPara)
                            {
                                if (Item.Contains('@'))
                                {
                                    Command.Parameters.AddWithValue(Item, paramater[i]);
                                    i++;
                                }

                            }

                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(Command);
                        adapter.Fill(data);
                        connection.Close();

                    }
                }
                catch (Exception ex )
                {

                    MessageBox.Show("Không kết nối được CSLD " + ex.Message);
                }
            return data;
        }

        public int ExecuteNonQuery(string query, object[] paramater = null)
        {
           int data = 0;
                using (SqlConnection connection = new SqlConnection(connectionstr))
                try
                {
                    {
                        connection.Open();
                        SqlCommand Command = new SqlCommand(query, connection);
                        if (paramater != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 0;
                            foreach (string Item in listPara)
                            {
                                if (Item.Contains('@'))
                                {
                                    Command.Parameters.AddWithValue(Item, paramater[i]);
                                    i++;
                                }
                            }
                        }

                        data = Command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Không kết nối được CSDL " + ex.Message);
                }
                return data;
        }
         public object ExecuteScalar(string query, object[] paramater = null)

        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionstr))

                try
                {
                    {

                        connection.Open();

                        SqlCommand Command = new SqlCommand(query, connection);
                        if (paramater != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 0;
                            foreach (string Item in listPara)
                            {
                                if (Item.Contains('@'))
                                {
                                    Command.Parameters.AddWithValue(Item, paramater[i]);
                                    i++;
                                }

                            }

                        }

                        data = Command.ExecuteScalar();
                        connection.Close();

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Không thể kết nối đến CSDL " + ex.Message);
                }
            return data;
        }
    }

}
