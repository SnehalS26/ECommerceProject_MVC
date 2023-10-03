using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProductCrud_MVC.Models
{
    public class CartCrud
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public CartCrud(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("defaultConnection"));
        }
        public int AddToCart(Cart cart)
        {
            int result = 0;
            string qry = "insert into Cart values(@uid,@id,@qty)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@uid", cart.Userid);
            cmd.Parameters.AddWithValue("@id", cart.Productid);
            cmd.Parameters.AddWithValue("@qty", cart.Quantity);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public List<Product> ViewCart(int uid)
        {
            List<Product> products = new List<Product>();
            string qry = "select prod.* , cart.qty ,cart.cartid from Product prod join Cart cart on cart.id=prod.id where cart.uid=@uid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@uid", uid);
            con.Open();
            dr=cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    Product prod = new Product();
                    prod.Id = Convert.ToInt32(dr["id"]);
                    prod.Name = dr["name"].ToString();
                    prod.Price = Convert.ToInt32(dr["price"]);
                    prod.Imageurl = dr["imageurl"].ToString();                  
                    prod.Quantity = Convert.ToInt32(dr["qty"]);
                    prod.Cartid = Convert.ToInt32(dr["cartid"]);
                    products.Add(prod);
                }
            }
            con.Close() ;
            return products;
        }
        public int DeleteCart(int Cartid)
        {
            int result = 0;

            string qry = " delete from Cart where cartid=@cartid";
            cmd = new SqlCommand(qry, con);

            cmd.Parameters.AddWithValue("@cartid", Cartid);

            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();

            return result;
        }
    }
}
