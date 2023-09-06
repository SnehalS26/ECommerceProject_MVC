using System.Data.SqlClient;

namespace ProductCrud_MVC.Models
{
    public class ProductCrud
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public ProductCrud(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("defaultConnection"));
        }
        public IEnumerable<Product> GetProducts()
        {
            List<Product> list = new List<Product>();
            string qry = "select prod.* , cat.Cname from Product prod inner join Category cat on cat.cid = prod.cid";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    Product prod = new Product();
                    prod.Id = Convert.ToInt32(dr["id"]);
                    prod.Name = dr["name"].ToString();
                    prod.Price = Convert.ToInt32(dr["price"]);
                    prod.Imageurl = dr["imageurl"].ToString();
                    prod.Cid = Convert.ToInt32(dr["cid"]);
                    prod.Cname = dr["cname"].ToString();
                    list.Add(prod);
                }
            }
            con.Close();
            return list;
        }
        public Product GetProductById(int id)
        {
            Product prod = new Product();
            string qry = "select prod.* , cat.Cname from Product prod inner join Category cat on cat.cid = prod.cid where prod.id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                   
                    prod.Id = Convert.ToInt32(dr["id"]);
                    prod.Name = dr["name"].ToString();
                    prod.Price = Convert.ToInt32(dr["price"]);
                    prod.Imageurl = dr["imageurl"].ToString();
                    prod.Cid = Convert.ToInt32(dr["cid"]);
                    prod.Cname = dr["cname"].ToString();
                    
                }
            }
            con.Close();
            return prod;
        }
        public int AddProduct(Product prod)
        {
            int result = 0;
            string qry = "insert into Product values(@name,@price,@imageurl,@cid)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", prod.Name);
            cmd.Parameters.AddWithValue("@price", prod.Price);
            cmd.Parameters.AddWithValue("@imageurl", prod.Imageurl);
            cmd.Parameters.AddWithValue("@cid", prod.Cid);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;

        }
        public int UpdateProduct(Product prod)
        {
            int result = 0;
            string qry = "update Product set name=@name,price=@price,imageurl=@imageurl,cid=@cid where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", prod.Name);
            cmd.Parameters.AddWithValue("@price", prod.Price);
            cmd.Parameters.AddWithValue("@imageurl", prod.Imageurl);
            cmd.Parameters.AddWithValue("@cid", prod.Cid);
            cmd.Parameters.AddWithValue("@id", prod.Id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;

        }
        public int DeleteProduct(int id)
        {
            int result = 0;
            string qry = "delete from Product where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;

        }
    }
}
