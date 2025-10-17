using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;  

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    internal class KETNOI_CSDL
    {
        public SqlConnection cnn;
        public SqlCommand cmd;
        public SqlDataAdapter ada;
        public DataTable dta;

        //Data Source=LAPTOP-54RR747S\HSON;Initial Catalog=QLDKHP;Integrated Security=True;Trust Server Certificate=True
        //Data Source=THE-CHOSEN-ONE;Initial Catalog=QLDKHP;Integrated Security=True;Trust Server Certificate=True
        public void KetNoi_Dulieu()
        {
            string strKetNoi = @"Data Source=THE-CHOSEN-ONE;Initial Catalog=QLDKHP;Integrated Security=True";
            cnn = new SqlConnection(strKetNoi);
            cnn.Open();
        }

        public void NgatKetNoi()
        {
            if (cnn.State == ConnectionState.Open)
                cnn.Close();
        }

        public DataTable Lay_DulieuBang(string SQL)
        {
            KetNoi_Dulieu();
            ada = new SqlDataAdapter(SQL, cnn);
            dta = new DataTable();
            ada.Fill(dta);
            return dta;
        }

        public void ThucThiSQL(string sql)
        {
            KetNoi_Dulieu();
            cmd = new SqlCommand(sql, cnn);
            cmd.ExecuteNonQuery();
            NgatKetNoi();
        }
    }
}
