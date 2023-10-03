using System.Data.SqlClient;

namespace ProductCrud_MVC.Models
{
    public class OrderCrud
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public OrderCrud(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("defaultConnection"));
        }
        public int AddOrder(Orders orders)
        {
            int result = 0;
            string qry = "insert into Orders values(@id,@price,@uid,@quantity)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", orders.Prodid);
            cmd.Parameters.AddWithValue("@price", orders.Price);
            cmd.Parameters.AddWithValue("@uid", orders.Uid);
            cmd.Parameters.AddWithValue("@quantity", orders.Quantity);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public Product GetOrderById(int uid)
        {
            Product prod = new Product();
            string qry = "select prod.* , ord.quantity from Product prod inner join Orders ord on ord.id = prod.id where ord.uid=@uid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@uid", uid);
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
                    prod.Quantity = Convert.ToInt32(dr["quantity"]);

                }
            }
            con.Close();
            return prod;
        }
        public List<Product> ViewOrder(int uid)
        {
            List<Product> products = new List<Product>();
            string qry = "select prod.* , orders.orderid , orders.quantity from Product prod join Orders orders on orders.id = prod.id where orders.uid=@uid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@uid", uid);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Product prod = new Product();
                    prod.Id = Convert.ToInt32(dr["id"]);
                    prod.Name = dr["name"].ToString();
                    prod.Price = Convert.ToDouble(dr["price"]);
                    prod.Imageurl = dr["imageurl"].ToString();
                    prod.Quantity = Convert.ToInt32(dr["quantity"]);
                    prod.Oid = Convert.ToInt32(dr["orderid"]);
                    products.Add(prod);
                }
            }
            con.Close();
            return products;
        }
        public int CancelOrder(int orderid)
        {
            int result = 0;

            string qry = "delete from Orders where orderid=@orderid";
            cmd = new SqlCommand(qry, con);

            cmd.Parameters.AddWithValue("@orderid", orderid);

            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();

            return result;
        }
    }
}
