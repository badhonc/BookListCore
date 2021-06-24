using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BookListM.db_acccess_layer
{
    public class db
    {
        SqlConnection con = new SqlConnection("data source=DESKTOP-8M1VK37\\SQLEXPRESS; database=BookList; user=sa; password=123123; Integrated Security=True");

        //get category
        public DataSet GetCategory()
        {
            SqlCommand cmd = new SqlCommand("Sp_Category", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        //get subcategory
        public DataSet GetSubcategory(int category_id)
        {
            SqlCommand cmd = new SqlCommand("Sp_Subcategory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Category_id", category_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        //get maincategory
        public DataSet GetMaincategory(int subcategory_id)
        {
            SqlCommand cmd = new SqlCommand("Sp_Maincategory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Subcategory_id", subcategory_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

    }
}
