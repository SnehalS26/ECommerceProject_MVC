using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProductCrud_MVC.Models
{
    public class UsersCrud
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        
        IConfiguration configuration;
        public UsersCrud(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("defaultConnection"));
        }
        
        public int AddUsers(Users users)
        {
            int result = 0;
            string qry = "insert into Users(firstName,lastName,email,password,phone,gender,city,state) values(@firstName,@lastName,@email,@password,@phone,@gender,@city,@state)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@firstName", users.First_Name);
            cmd.Parameters.AddWithValue("@lastName", users.Last_Name);
            cmd.Parameters.AddWithValue("@email", users.Email);
            cmd.Parameters.AddWithValue("@password", users.Password);
            cmd.Parameters.AddWithValue("@phone", users.Phone);
            cmd.Parameters.AddWithValue("@gender", users.Gender);
            cmd.Parameters.AddWithValue("@city", users.City);
            cmd.Parameters.AddWithValue("@state", users.State);
            
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;

        }
        public Users GetEmailPassword(string email, string password)
        {
            Users user = new Users();
            string qry = "select * from Users where email=@email and password=@password";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    user.Uid = Convert.ToInt32(dr["uid"]);
                    user.First_Name = dr["firstName"].ToString();
                    user.Last_Name = dr["lastName"].ToString();
                    user.Email = dr["email"].ToString();
                   
                    user.Roleid = Convert.ToInt32(dr["roleid"]);

                }
            }
            con.Close();
            return user;
        }
        
           
    }
}
