using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Emit;
using System.Drawing;

namespace ExpensifySoftwareSec
{

    public partial class _Default : Page
    {
        static string connectionString = @"Data Source=DESKTOP-I65NSSU;Initial Catalog=proj;User ID=sa;Password=a";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Account/Login.aspx");
            }
            bindFriends();
            bindCurrency();



        }

        protected void btnnewexpense_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        void bindFriends()
        {
            SqlConnection con = new SqlConnection(connectionString);
            string com = "Select * from tblUsers";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            friend_ddl.DataSource = dt;
            friend_ddl.DataBind();
            friend_ddl.DataTextField = "UserId";
            friend_ddl.DataValueField = "Id";
            friend_ddl.DataBind();
            //friend_ddl.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        void bindCurrency()
        {
            SqlConnection con = new SqlConnection(connectionString);
            string com = "Select * from tblcurrency";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            currency.DataSource = dt;
            currency.DataBind();
            currency.DataTextField = "currency_name";
            currency.DataValueField = "id";
            currency.DataBind();
        }

        private void Expenses_Info()
        {

            SqlConnection conn = new SqlConnection(connectionString);
            string sql = "INSERT INTO tblexpenses_info (categoryid,createdbyuserid,splitwithuserid,description,currencyid,created_datetime,split_type, isSettled, amount) VALUES (@categoryid,@createdbyid,@friend_ddl,@desc,@currency, @created_datetime, @splitype, @isSettled, @amount)";
            try
            {



                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@friend_ddl", friend_ddl.SelectedValue);
                cmd.Parameters.AddWithValue("@desc", description.InnerText);
                cmd.Parameters.AddWithValue("@currency", currency.SelectedValue);
                cmd.Parameters.AddWithValue("@splitype", splittype.SelectedValue);
                cmd.Parameters.AddWithValue("@categoryid", 0);
                cmd.Parameters.AddWithValue("@createdbyid", Session["Id"]);
                cmd.Parameters.AddWithValue("@created_datetime", DateTime.Now);
                cmd.Parameters.AddWithValue("@isSettled", 0);
                cmd.Parameters.AddWithValue("@amount", amount.Text);
                cmd.CommandType = CommandType.Text;
                int Success = cmd.ExecuteNonQuery();

                if (Success == 1)
                {
                    int before_expenses = 0;
                    int total_expenses = 0;
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    string sqlquerytogetexpense = "SELECT expenses from tblmyexpenses where id = @user_id";
                    SqlDataAdapter da = new SqlDataAdapter(sqlquerytogetexpense, sqlConnection);
                    da.SelectCommand.Parameters.AddWithValue("@user_id", Session["Id"]);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        before_expenses = Convert.ToInt32(dt.Rows[0]["expenses"]);

                    }

                    int after_expenses = 0;
                    if ((splittype.SelectedValue == "1") || (splittype.SelectedValue == "3"))
                    {
                        if ((splittype.SelectedValue == "3"))
                        {
                            after_expenses = Convert.ToInt32(amount.Text.ToString());
                        }
                        if ((splittype.SelectedValue == "1"))
                        {
                            after_expenses = Convert.ToInt32(amount.Text.ToString());
                            after_expenses = Convert.ToInt32((after_expenses / 0.5));
                        }
                        total_expenses = before_expenses + after_expenses;
                    }
                    else
                    {
                        SqlConnection sqlConnectionn = new SqlConnection(connectionString);
                        string checkexistingexpense = "SELECT expenses from tblmyexpenses where id = @user_id";
                        SqlDataAdapter adap = new SqlDataAdapter(checkexistingexpense, sqlConnectionn);
                        adap.SelectCommand.Parameters.AddWithValue("@user_id", friend_ddl.SelectedValue);
                        DataTable dataTable = new DataTable();
                        adap.Fill(dataTable);

                        if (dt.Rows.Count > 0)
                        {
                            string updatexpense = "update tblmyexpenses set expenses = @expenses where user_id = @user_id ";
                            SqlCommand updatesqlcommand = new SqlCommand(updatexpense, conn);
                            updatesqlcommand.Parameters.AddWithValue("@user_id", friend_ddl.SelectedValue);
                            updatesqlcommand.Parameters.AddWithValue("@expenses", total_expenses);
                            updatesqlcommand.CommandType = CommandType.Text;
                            updatesqlcommand.ExecuteNonQuery();
                        }

                        else
                        {
                            string allby_friend = "INSERT INTO tblmyexpenses (user_id,expenses,expenses_datetime) VALUES (@user_id,@expenses,@expenses_datetime)";
                            SqlCommand mysqlcommand = new SqlCommand(allby_friend, conn);
                            mysqlcommand.Parameters.AddWithValue("@user_id", friend_ddl.SelectedValue);
                            mysqlcommand.Parameters.AddWithValue("@expenses", total_expenses);
                            mysqlcommand.Parameters.AddWithValue("@expenses_datetime", DateTime.Now);
                            mysqlcommand.CommandType = CommandType.Text;
                            mysqlcommand.ExecuteNonQuery();
                        }

                    }


                    SqlConnection sqlConnectionnn = new SqlConnection(connectionString);
                    string checkexistingexpenseforself = "SELECT expenses from tblmyexpenses where id = @user_id";
                    SqlDataAdapter adapter = new SqlDataAdapter(checkexistingexpenseforself, sqlConnectionnn);
                    adapter.SelectCommand.Parameters.AddWithValue("@user_id", Session["Id"]);
                    DataTable dataTablee = new DataTable();
                    adapter.Fill(dataTablee);

                    if (dt.Rows.Count > 0)
                    {
                        string updatexpense = "update tblmyexpenses set expenses = @expenses where user_id = @user_id ";
                        SqlCommand updatesqlcommand = new SqlCommand(updatexpense, conn);
                        updatesqlcommand.Parameters.AddWithValue("@user_id", Session["Id"]);
                        updatesqlcommand.Parameters.AddWithValue("@expenses", total_expenses);
                        updatesqlcommand.CommandType = CommandType.Text;
                        updatesqlcommand.ExecuteNonQuery();
                    }

                    else
                    {
                        string queryforexpensesdata = "INSERT INTO tblmyexpenses (user_id,expenses,expenses_datetime) VALUES (@user_id,@expenses,@expenses_datetime)";
                        SqlCommand sqlcommand = new SqlCommand(queryforexpensesdata, conn);
                        sqlcommand.Parameters.AddWithValue("@user_id", Session["Id"]);
                        sqlcommand.Parameters.AddWithValue("@expenses", total_expenses);
                        sqlcommand.Parameters.AddWithValue("@expenses_datetime", DateTime.Now);
                        sqlcommand.CommandType = CommandType.Text;
                        sqlcommand.ExecuteNonQuery();
                    }

                }
                else
                {

                }

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
                throw new Exception(msg);

            }
            finally
            {
                conn.Close();
            }
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            Expenses_Info();
        }
    }
}