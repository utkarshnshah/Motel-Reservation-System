using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Motel_Reservation_System
{
    public partial class mrs : System.Web.UI.MasterPage
    {
        // Connection string to the database
        string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnContactUs_Click(object sender, EventArgs e)
        {
            bool isEmail = Regex.IsMatch(txtEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (txtContactName.Text==string.Empty || txtEmail.Text==string.Empty || txtMessage.Text == string.Empty || ddlTypeOfQuery.SelectedValue=="liSelect")
            {
                emailSent.Attributes.Add("class", emailSent.Attributes["class"].ToString().Replace("hidden", ""));
                lblEmailSentMsg.Text = "Please enter all fields!";
                btnContactUs.Focus();
            }
            // Validation for email ID.
            else if(isEmail==false)
            {
                emailSent.Attributes.Add("class", emailSent.Attributes["class"].ToString().Replace("hidden", ""));
                lblEmailSentMsg.Text = "Email ID is invalid. Please enter valid email ID !";
                btnContactUs.Focus();
            }
            else if(txtContactName.Text.Length>20 || txtEmail.Text.Length>50 || txtMessage.Text.Length > 300)
            {
                emailSent.Attributes.Add("class", emailSent.Attributes["class"].ToString().Replace("hidden", ""));
                lblEmailSentMsg.Text = "Character limit reached. 20 character for name, 50 character for email and 300 character for message only allowed !";
                btnContactUs.Focus();
            }
            else
            {
                // Inserting data into contact table
                SqlConnection connection = new SqlConnection(connectionString);
                string sql = "INSERT INTO contact(email,name,subject,message) VALUES(@email,@name,@subject,@message)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@email", txtEmail.Text);
                command.Parameters.AddWithValue("@name", txtContactName.Text);
                command.Parameters.AddWithValue("@subject", ddlTypeOfQuery.Text);
                command.Parameters.AddWithValue("@message", txtMessage.Text);
                connection.Open();
                if (command.ExecuteNonQuery() == 0)
                {
                    emailSent.Attributes.Add("class", emailSent.Attributes["class"].ToString().Replace("hidden", ""));
                    lblEmailSentMsg.Text = "Sorry! Could not send the message. Please try again.";
                    btnContactUs.Focus();
                }
                else
                {
                    // Sending Email notification for contact support.
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("contactus@clarionmotel.com");
                    msg.To.Add(txtEmail.Text);
                    msg.Subject = "Contact Us";
                    msg.Body = "A user with email " + txtEmail.Text + "  has sent a message. Message from the user:<br/>" + txtMessage.Text;
                    msg.IsBodyHtml = true;
                    SmtpClient smt = new SmtpClient();
                    smt.Host = "smtp.gmail.com";
                    System.Net.NetworkCredential ntcd = new NetworkCredential();
                    ntcd.UserName = "Email ID";
                    ntcd.Password = "Password";
                    smt.Credentials = ntcd;
                    smt.EnableSsl = true;
                    smt.Port = 587;
                    smt.Send(msg);
                    emailSent.Attributes.Add("class", emailSent.Attributes["class"].ToString().Replace("hidden", ""));
                    lblEmailSentMsg.Text = "Thanks for contacting us. We will reply to you as soon as possible.";
                    txtContactName.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtMessage.Text = string.Empty;
                    btnContactUs.Focus();
                    connection.Close();
                }
            }
        }
    }
}