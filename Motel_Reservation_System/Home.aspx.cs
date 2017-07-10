using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Motel_Reservation_System
{
    public partial class Home : System.Web.UI.Page
    {
        // Connection string to the database
        string constr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void revealLoginStatus()
        {
            loginMessage.Attributes.Add("class", loginMessage.Attributes["class"].ToString().Replace("hidden", ""));
            registerMessage.Attributes.Add("class", "hidden");
        }
        protected void revealRegistrationStatus()
        {
            registerMessage.Attributes.Add("class", registerMessage.Attributes["class"].ToString().Replace("hidden", ""));
            loginMessage.Attributes.Add("class", "hidden");
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Validation for login
            if (txtUserName.Text == string.Empty && txtPassword.Text == string.Empty)
            {
                revealLoginStatus();
                lblLoginMessage.Text = "Please enter all fields!";
            }
            else if (txtUserName.Text.Length > 15 || txtPassword.Text.Length > 15)
            {
                revealLoginStatus();
                lblLoginMessage.Text = "User Name and Password should be only 15 characters long!";
            }
            else
            {
                // Getting the user from database
                SqlConnection con = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand("SELECT * FROM registration WHERE userName=@userName AND password=@password",con);
                cmd.Parameters.AddWithValue("@userName", txtUserName.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                con.Open();
                SqlDataReader dr= cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    // Session start
                    Session["userID"] = dr[0];
                    Session["firstName"] = dr[1];
                    Session["lastName"] = dr[2];
                    Session["userEmail"] = dr[3];
                    Session["userName"] = dr[4];
                    Response.Redirect("booking.aspx");
                }
                else
                {
                    // User not found
                    revealLoginStatus();
                    lblLoginMessage.Text = "No such user found!";
                    txtUserName.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    txtUserName.Focus();
                }
            }
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Validation for registration
            string regExp = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (txtNewFirstName.Text == string.Empty || txtNewLastName.Text == string.Empty || txtNewEmail.Text == string.Empty || txtNewUserName.Text == string.Empty || txtNewPassword.Text == string.Empty || txtConfirmNewPassword.Text == string.Empty)
            {
                revealRegistrationStatus();
                lblRegisterMessage.Text = "Fields with star are necessary for registration!";
            }
            else if(txtNewUserName.Text.Length>15 || txtNewPassword.Text.Length>15)
            {
                revealRegistrationStatus();
                lblRegisterMessage.Text = "User Name and Password should be only 15 characters long!";
            }
            else if(txtNewFirstName.Text.Length>15 || txtNewLastName.Text.Length > 15)
            {
                revealRegistrationStatus();
                lblRegisterMessage.Text = "First Name and Last Name should be only 15 characters long!";
            }
            else if (!Regex.IsMatch(txtNewEmail.Text, regExp))
            {
                revealRegistrationStatus();
                lblRegisterMessage.Text = "Invalid Email ID!";
            }
            else if (txtNewPassword.Text != txtConfirmNewPassword.Text)
            {
                revealRegistrationStatus();
                lblRegisterMessage.Text = "Password and Confirm Password does'nt match!";
            }
            else
            {
                SqlConnection con = new SqlConnection(constr);
                bool already = false;
                using (SqlCommand cmd = new SqlCommand("select count(*) from registration where email = @email", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@email", txtNewEmail.Text);
                    already = (int)cmd.ExecuteScalar() > 0;
                    con.Close();
                }
                if (already)
                {
                    revealRegistrationStatus();
                    lblRegisterMessage.Text = ("A user with email ID "+txtNewEmail.Text + " is already registered. Please enter other email.");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("insert into registration (firstName,lastName,email,userName,password) values (@txtNewFirstName,@txtNewLastName,@txtNewEmail,@txtNewUserName,@txtNewPassword)", con);
                    cmd.Parameters.AddWithValue("@txtNewFirstName", txtNewFirstName.Text);
                    cmd.Parameters.AddWithValue("@txtNewLastName", txtNewLastName.Text);
                    cmd.Parameters.AddWithValue("@txtNewEmail", txtNewEmail.Text);
                    cmd.Parameters.AddWithValue("@txtNewUsername", txtNewUserName.Text);
                    cmd.Parameters.AddWithValue("@txtNewPassword", txtNewPassword.Text);
                    con.Open();
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        revealRegistrationStatus();
                        lblRegisterMessage.Text = "You have registered successfully! Login to continue.";
                        txtNewFirstName.Text = string.Empty;
                        txtNewLastName.Text = string.Empty;
                        txtNewEmail.Text = string.Empty;
                        txtNewUserName.Text = string.Empty;
                        txtNewPassword.Text = string.Empty;
                        btnRegister.Focus();
                        con.Close();
                    }
                    else
                    {
                        revealRegistrationStatus();
                        lblRegisterMessage.Text = "Could not register! Please try again.";
                        txtNewFirstName.Focus();
                    }
                }
            }
        }
    }
}