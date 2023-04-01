using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using ExpensifySoftwareSec.Models;
using System.Data.SqlClient;
using System.Globalization;
using System.Data;

namespace ExpensifySoftwareSec.Account
{
    public partial class Login : Page
    {
        static string connectionString = @"Data Source=DESKTOP-I65NSSU;Initial Catalog=proj;User ID=sa;Password=a";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("select COUNT(*)FROM tblUsers WHERE UserId='" + txtUserId.Text + "' and Password='" + txtPassword.Text + "'");
                cmd.Connection = con;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    Session["UserId"] = txtUserId.Text.ToString();
                    txtInfo.Text = "Login Successful!";

                    reader.Close();
                    con.Close();

                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    string getid = "select id FROM tblUsers WHERE UserId='" + txtUserId.Text + "' and Password='" + txtPassword.Text + "'";
                    SqlDataAdapter da = new SqlDataAdapter(getid, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        hdnid.Text = Convert.ToInt32(dt.Rows[0]["id"]).ToString();

                    }
                    Session["Id"] = hdnid.Text;


                    Response.Redirect("/About.aspx");
                }
                else
                {
                    txtInfo.Text = "Invalid credentials";
                }

                reader.Close();

                con.Close();
            }

            catch (Exception ex)
            {

            }
        }
    }
}