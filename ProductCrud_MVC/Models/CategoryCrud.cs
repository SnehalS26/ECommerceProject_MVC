using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProductCrud_MVC.Models
{
    public class CategoryCrud
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public CategoryCrud(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("defaultConnection"));
        }
        public int AddCategory(Category cat)
        {
            int result = 0;
            string qry = "insert into Category values(@cname)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@cname", cat.Cname);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int UpdateCategory(Category cat)
        {
            int result = 0;
            string qry = "update Category set cname=@cname where cid=@cid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@cname", cat.Cname);
            cmd.Parameters.AddWithValue("@cid", cat.Cid);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int DeleteCategory(Category cat)
        {
            int result = 0;
            string qry = "delete Category where cid=@cid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@cid", cat.Cid);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public IEnumerable<Category> GetCategory()
        {
            List<Category> list = new List<Category>();
            string qry = "select * from Category";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    Category cat = new Category();
                    cat.Cid = Convert.ToInt32(dr["cid"]);
                    cat.Cname = dr["cname"].ToString();
                    list.Add(cat);
                }
            }
            con.Close();
            return list;
        }
        public Category GetCategoryById(int id) 
        {
            Category cat = new Category();
            string qry = "select * from Category where cid=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {                    
                    cat.Cid = Convert.ToInt32(dr["cid"]);
                    cat.Cname = dr["cname"].ToString();                   
                }
            }
            con.Close();
            return cat;
        }

    }
}
