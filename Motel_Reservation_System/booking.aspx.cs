using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;

namespace Motel_Reservation_System
{
    public partial class booking : System.Web.UI.Page
    {
        // Connection string to the database
        string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] != null)
            {
                // Displaying welcome message
                string uName = Session["firstName"].ToString() + " " + Session["lastName"].ToString();
                lblWelcomeMessage.Text =  "HI " + uName;
                // Displaying date and time
                lblTime.Text = DateTime.Now.ToString();
                int uID = Convert.ToInt32(Session["userID"]);
                // Getting the booking data of user from database and displaying it on my booking section
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("SELECT * FROM booking WHERE userId=@uID", con);
                cmd.Parameters.AddWithValue("@uID", uID);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        HtmlGenericControl h4 = new HtmlGenericControl("h4");
                        h4.Controls.Add(new Label() { Text = "Booking ID: "+ dr.GetString(1) });
                        h4.Controls.Add(new LiteralControl("<br /><br />"));
                        if (dr.GetString(3) == "liGeneralRoom")
                        {
                            h4.Controls.Add(new Label() { Text = "Room Type: General Room"});
                        }
                        if(dr.GetString(3) == "liDeluxRoom")
                        {
                            h4.Controls.Add(new Label() { Text = "Room Type: Delux Room" });
                        }
                        if (dr.GetString(3) == "liLuxuryRoom")
                        {
                            h4.Controls.Add(new Label() { Text = "Room Type: Luxury Room" });
                        }
                        h4.Controls.Add(new LiteralControl("<br /><br />"));
                        h4.Controls.Add(new Label() { Text = "From Date: " + dr.GetDateTime(4).ToString() + " " });
                        h4.Controls.Add(new Label() { Text = "To Date: " + dr.GetDateTime(5).ToString() +" "});
                        h4.Controls.Add(new LiteralControl("<br /><br />"));
                        h4.Controls.Add(new Label() { Text = "Adults: " + dr.GetInt32(6).ToString() + " " });
                        h4.Controls.Add(new Label() { Text = "Children: " + dr.GetInt32(7).ToString() + " " });
                        h4.Controls.Add(new Label() { Text = "Mobile: " + dr.GetInt32(8).ToString() + " " });
                        h4.Controls.Add(new LiteralControl("<br /><br />"));
                        h4.Controls.Add(new Label() { Text = "AC: " + dr.GetInt32(9).ToString() + "$ " });
                        h4.Controls.Add(new Label() { Text = "Laundry: " + dr.GetInt32(10).ToString() + "$ " });
                        h4.Controls.Add(new Label() { Text = "Pick Up: " + dr.GetInt32(11).ToString() + "$ " });
                        h4.Controls.Add(new Label() { Text = "Drop Up: " + dr.GetInt32(12).ToString() + "$ " });
                        h4.Controls.Add(new LiteralControl("<br /><br />"));
                        h4.Controls.Add(new Label() { Text = "Pick Up Address: " + dr.GetString(13) + " " });
                        h4.Controls.Add(new LiteralControl("<br /><br />"));
                        h4.Controls.Add(new Label() { Text = "Drop Up Address: " + dr.GetString(14) + " " });
                        h4.Controls.Add(new LiteralControl("<br /><br />"));
                        h4.Controls.Add(new Label() { Text = "Bill To Pay: " + dr.GetInt32(15).ToString() +"$ "});
                        h4.Controls.Add(new LiteralControl("<br /><br />"));
                        Button btnModifyBooking = new Button() { CssClass = "btn btn-skin", Text = "Modify", ID = dr.GetString(1) };
                        btnModifyBooking.Click += new System.EventHandler(btnModifyBooking_Click);
                        h4.Controls.Add(btnModifyBooking);
                        h4.Controls.Add(new LiteralControl("<br /><hr />"));

                        bookingDetails.Controls.Add(h4);
                    }
                }
                con.Close();
            }
            else
            {
                Response.Redirect("Home.aspx");
            }
            // Creating session for storing data
            if (Convert.ToString(Session["bookedMessage"]) == "y")
            {
                revealBookingStatus();
                lblBookingMessage.Text = "You have booked the room successfully! Go to My Booking section for Booking ID and details.";
                Session["bookedMessage"] = string.Empty;
            }
            if (Convert.ToString(Session["updateMessage"]) == "y")
            {
                revealBookingStatus();
                lblBookingMessage.Text = "You have updated your booking successfully! Go to My Booking section to get Booking ID and details.";
                Session["updateMessage"] = string.Empty;
            }
            if (Convert.ToString(Session["cancelMessage"]) == "y")
            {
                revealBookingStatus();
                lblBookingMessage.Text = "You cancelled your booking successfully!";
                Session["cancelMessage"] = string.Empty;
            }
        }
        // Displaying Current Time
        protected void currentTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString();
        }

        // Loadin data in modify section
        protected void btnModifyBooking_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string btnID = button.ID;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM booking WHERE bookingId=@bookingID", con);
            cmd.Parameters.AddWithValue("@bookingID", btnID);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlUpdateRoomType.ClearSelection();
                    ddlUpdateAdults.ClearSelection();
                    ddlUpdateChildren.ClearSelection();

                    lblUpdateBookingID.Text = dr.GetString(1);
                    ddlUpdateRoomType.Items.FindByValue(dr.GetString(3)).Selected=true;
                    txtUpdateFromDate.Text= dr.GetDateTime(4).ToString();
                    txtUpdateToDate.Text = dr.GetDateTime(5).ToString();
                    ddlUpdateAdults.Items.FindByText(dr.GetInt32(6).ToString()).Selected = true;
                    ddlUpdateChildren.Items.FindByText(dr.GetInt32(7).ToString()).Selected = true;
                    txtUpdateMobile.Text = dr.GetInt32(8).ToString();
                    if (dr.GetInt32(9).ToString() == "0")
                    {
                        chkUpdateAC.Checked = false;
                    }
                    else
                    {
                        chkUpdateAC.Checked = true;
                    }
                    if (dr.GetInt32(10).ToString() == "0")
                    {
                        chkUpdateLaundry.Checked = false;
                    }
                    else
                    {
                        chkUpdateLaundry.Checked = true;
                    }
                    if (dr.GetInt32(11).ToString() == "0")
                    {
                        chkUpdatePickUp.Checked = false;
                    }
                    else
                    {
                        chkUpdatePickUp.Checked = true;
                        divUpdatePickDrop.Attributes.Add("class", divUpdatePickDrop.Attributes["class"].ToString().Replace("hidden", ""));
                        lblUpdatePickUpAddress.Attributes.Add("class", lblUpdatePickUpAddress.Attributes["class"].ToString().Replace("hidden", ""));
                        txtUpdatePickUpAddress.Attributes.Add("class", txtUpdatePickUpAddress.Attributes["class"].ToString().Replace("hidden", ""));
                    }
                    if (dr.GetInt32(12).ToString() == "0")
                    {
                        chkUpdateDropUp.Checked = false;
                    }
                    else
                    {
                        chkUpdateDropUp.Checked = true;
                        divUpdatePickDrop.Attributes.Add("class", divUpdatePickDrop.Attributes["class"].ToString().Replace("hidden", ""));
                        lblUpdateDropUpAddress.Attributes.Add("class", lblUpdateDropUpAddress.Attributes["class"].ToString().Replace("hidden", ""));
                        txtUpdateDropUpAddress.Attributes.Add("class", txtUpdateDropUpAddress.Attributes["class"].ToString().Replace("hidden", ""));
                    }
                    txtUpdatePickUpAddress.Text = dr.GetString(13);
                    txtUpdateDropUpAddress.Text = dr.GetString(14);
                }
            }
            Session["currentBookingID"] = btnID;
            bookNow.Attributes.Add("class", "hidden");
            myBooking.Attributes.Add("class", "hidden");
            updateBooking.Attributes.Add("class", updateBooking.Attributes["class"].ToString().Replace("hidden", ""));
        }
        // Cancel Update
        protected void btnCancelUpdate_Click(object sender, EventArgs e)
        {
            bookingStatus.Attributes.Add("class", "hidden");
            lblBookingMessage.Text = string.Empty;
            updateBooking.Attributes.Add("class", "hidden");
            bookNow.Attributes.Add("class", updateBooking.Attributes["class"].ToString().Replace("hidden", ""));
            myBooking.Attributes.Add("class", updateBooking.Attributes["class"].ToString().Replace("hidden", ""));
        }
        protected void revealBookingStatus()
        {
            bookingStatus.Attributes.Add("class", bookingStatus.Attributes["class"].ToString().Replace("hidden", ""));
        }
        private void revealUpdateBookingStatus()
        {
            updateBookingStatus.Attributes.Add("class", updateBookingStatus.Attributes["class"].ToString().Replace("hidden", ""));
        }
        protected void btnNewBooking_Click(object sender, EventArgs e)
        {
            // If Book Now button is clicked than perform booking operation after validation check
            DateTime today = DateTime.Today;
            DateTime availableDay = today.AddDays(1);
            string finalAvailable = availableDay.ToShortDateString();
            DateTime finalAvailableDay = Convert.ToDateTime(finalAvailable);
            DateTime fromDate = Convert.ToDateTime(txtBookingFromDate.Text);
            DateTime toDate = Convert.ToDateTime(txtBookingToDate.Text);
            DateTime endDate = fromDate.AddDays(30);
            if (ddlRoomType.SelectedValue == "liSelectRoom" || txtBookingFromDate.Text == string.Empty || txtBookingToDate.Text == string.Empty || ddlSelectAdults.SelectedValue == "liSelectAdults" || txtMobile.Text == string.Empty)
            {
                revealBookingStatus();
                lblBookingMessage.Text = "Fields with star are necessary for booking!";
            }
            else if (fromDate.Date < finalAvailableDay.Date)
            {
                revealBookingStatus();
                lblBookingMessage.Text = "Advance booking can only be made 1 day before.";
            }
            else if (toDate.Date > endDate.Date)
            {
                revealBookingStatus();
                lblBookingMessage.Text = "Sorry! Cannot book for more than 30 days.";
            }
            else if (toDate.Date < fromDate.Date)
            {
                revealBookingStatus();
                lblBookingMessage.Text = "Invalid date selection!";
            }
            else if (txtMobile.Text.Length > 10)
            {
                revealBookingStatus();
                lblBookingMessage.Text = "Mobile number should be upto 10 digits long.";
            }
            else if (txtPickUpAddress.Text.Length > 100 || txtDropUpAddress.Text.Length > 100)
            {
                revealBookingStatus();
                lblBookingMessage.Text = "Pick Up address and Drop Up address should be upto 100 characters long!";
            }
            else
            {
                int ac = 0, laundry = 0, pickUp = 0, dropUp = 0, totalBookedDays = 0, roomCharge = 0, totalRoomCharge = 0, total = 0;
                if (ddlRoomType.SelectedValue == "liGeneralRoom")
                {
                    roomCharge = 80;
                }
                if (ddlRoomType.SelectedValue == "liDeluxRoom")
                {
                    roomCharge = 120;
                }
                if (ddlRoomType.SelectedValue == "liLuxuryRoom")
                {
                    roomCharge = 150;
                }
                if (chkAC.Checked)
                {
                    ac = 20;
                }
                if (chkLaundry.Checked)
                {
                    laundry = 10;
                }
                if (chkPickUp.Checked)
                {
                    pickUp = 40;
                }
                else
                {
                    txtPickUpAddress.Text = string.Empty;
                }
                if (chkDropUp.Checked)
                {
                    dropUp = 40;
                }
                else
                {
                    txtDropUpAddress.Text = string.Empty;
                }
                totalBookedDays = (toDate - fromDate).Days;
                totalRoomCharge = roomCharge * totalBookedDays;
                total = totalRoomCharge + ac + laundry + pickUp + dropUp;
                Random rnd = new Random();
                int uniqueNumber = rnd.Next(10, 99);
                string bookingID = "B" + DateTime.Now.ToString("ddMMyyhhmm") + Convert.ToString(uniqueNumber);
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("INSERT INTO booking(bookingId,userId,roomType,fromDate,toDate,adults,childrens,mobile,ac,laundry,pickUp,dropUp,pickUpAddress,dropUpAddress,totalAmount) VALUES(@bookingId,@userId,@roomType,@fromDate,@toDate,@adults,@childrens,@mobile,@ac,@laundry,@pickUp,@dropUp,@pickUpAddress,@dropUpAddress,@totalAmount)", con);
                cmd.Parameters.AddWithValue("@bookingId", bookingID);
                cmd.Parameters.AddWithValue("@userId", Convert.ToInt32(Session["userID"]));
                cmd.Parameters.AddWithValue("@roomType", ddlRoomType.Text);
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);
                cmd.Parameters.AddWithValue("@adults", Convert.ToInt32(ddlSelectAdults.SelectedItem.Text));
                cmd.Parameters.AddWithValue("@childrens", Convert.ToInt32(ddlSelectChildren.SelectedItem.Text));
                cmd.Parameters.AddWithValue("@mobile", Convert.ToInt32(txtMobile.Text));
                cmd.Parameters.AddWithValue("@ac", ac);
                cmd.Parameters.AddWithValue("@laundry", laundry);
                cmd.Parameters.AddWithValue("@pickUp", pickUp);
                cmd.Parameters.AddWithValue("@dropUp", dropUp);
                cmd.Parameters.AddWithValue("@pickUpAddress", txtPickUpAddress.Text);
                cmd.Parameters.AddWithValue("@dropUpAddress", txtDropUpAddress.Text);
                cmd.Parameters.AddWithValue("@totalAmount", total);
                con.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    // Sending Email notification for booking.
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("contactus@clarionmotel.com");
                    msg.To.Add(Session["userEmail"].ToString());
                    msg.Subject = "Booking Successful";
                    msg.Body = "Congratulations!<br/>You have booked a room. Your booking ID is: " + bookingID + "<br/>Please contact our receptionist for further process.<br/><br/>Cheers,<br/>Clarion Motels";
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

                    ddlRoomType.SelectedIndex = 0;
                    txtBookingFromDate.Text = string.Empty;
                    txtBookingToDate.Text = string.Empty;
                    ddlSelectAdults.SelectedIndex = 0;
                    ddlSelectChildren.SelectedIndex = 0;
                    txtMobile.Text = string.Empty;
                    chkAC.Checked = false;
                    chkLaundry.Checked = false;
                    chkPickUp.Checked = false;
                    chkDropUp.Checked = false;
                    Session["bookedMessage"] = "y";
                    Response.Redirect("booking.aspx");
                }
                else
                {
                    revealBookingStatus();
                    lblBookingMessage.Text = "Could not book! Please try again.";
                    ddlRoomType.Focus();
                }
                con.Close();
            }
        }

        protected void btnUpdateBooking_Click(object sender, EventArgs e)
        {
            // If Update Booking button is clicked Update Booking after validation check
            DateTime today = DateTime.Today;
            DateTime availableDay = today.AddDays(1);
            string finalAvailable = availableDay.ToShortDateString();
            DateTime finalAvailableDay = Convert.ToDateTime(finalAvailable);
            DateTime fromDate = Convert.ToDateTime(txtUpdateFromDate.Text);
            DateTime toDate = Convert.ToDateTime(txtUpdateToDate.Text);
            DateTime endDate = fromDate.AddDays(30);
            if (ddlUpdateRoomType.SelectedValue == "liUpdateSelectRoom" || txtUpdateFromDate.Text == string.Empty || txtUpdateToDate.Text == string.Empty || ddlUpdateAdults.SelectedValue == "liUpdateSelectAdults" || txtUpdateMobile.Text == string.Empty)
            {
                revealUpdateBookingStatus();
                lblUpdateBookingStatus.Text = "Fields with star are necessary for Updating!";
            }
            else if (fromDate.Date < finalAvailableDay.Date)
            {
                revealUpdateBookingStatus();
                lblUpdateBookingStatus.Text = "Advance booking can only be made 1 day before.";
            }
            else if (toDate.Date > endDate.Date)
            {
                revealUpdateBookingStatus();
                lblUpdateBookingStatus.Text = "Sorry! Cannot book for more than 30 days.";
            }
            else if (toDate.Date < fromDate.Date)
            {
                revealUpdateBookingStatus();
                lblUpdateBookingStatus.Text = "Invalid date selection!";
            }
            else if (txtUpdateMobile.Text.Length > 10)
            {
                revealUpdateBookingStatus();
                lblUpdateBookingStatus.Text = "Mobile number should be upto 10 digits long.";
            }
            else if (txtUpdatePickUpAddress.Text.Length > 100 || txtUpdateDropUpAddress.Text.Length > 100)
            {
                revealUpdateBookingStatus();
                lblUpdateBookingStatus.Text = "Pick Up address and Drop Up address should be upto 100 characters long!";
            }
            else
            {
                int ac = 0, laundry = 0, pickUp = 0, dropUp = 0, totalBookedDays = 0, roomCharge = 0, totalRoomCharge = 0, total = 0;
                if (ddlUpdateRoomType.SelectedValue == "liGeneralRoom")
                {
                    roomCharge = 80;
                }
                if (ddlUpdateRoomType.SelectedValue == "liDeluxRoom")
                {
                    roomCharge = 120;
                }
                if (ddlUpdateRoomType.SelectedValue == "liLuxuryRoom")
                {
                    roomCharge = 150;
                }
                if (chkUpdateAC.Checked)
                {
                    ac = 20;
                }
                if (chkUpdateLaundry.Checked)
                {
                    laundry = 10;
                }
                if (chkUpdatePickUp.Checked)
                {
                    pickUp = 40;
                }
                else
                {
                    txtUpdatePickUpAddress.Text = string.Empty;
                }
                if (chkUpdateDropUp.Checked)
                {
                    dropUp = 40;
                }
                else
                {
                    txtUpdateDropUpAddress.Text = string.Empty;
                }
                totalBookedDays = (toDate - fromDate).Days;
                totalRoomCharge = roomCharge * totalBookedDays;
                total = totalRoomCharge + ac + laundry + pickUp + dropUp;
                string currentBookingID = Convert.ToString(Session["currentBookingID"]);
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("UPDATE booking SET bookingId=@bookingId,userId=@userId,roomType=@roomType,fromDate=@fromDate,toDate=@toDate,adults=@adults,childrens=@childrens,mobile=@mobile,ac=@ac,laundry=@laundry,pickUp=@pickUp,dropUp=@dropUp,pickUpAddress=@pickUpAddress,dropUpAddress=@dropUpAddress,totalAmount=@totalAmount WHERE bookingId=@bookingId", con);
                cmd.Parameters.AddWithValue("@bookingId", currentBookingID);
                cmd.Parameters.AddWithValue("@userId", Convert.ToInt32(Session["userID"]));
                cmd.Parameters.AddWithValue("@roomType", ddlUpdateRoomType.Text);
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);
                cmd.Parameters.AddWithValue("@adults", Convert.ToInt32(ddlUpdateAdults.SelectedItem.Text));
                cmd.Parameters.AddWithValue("@childrens", Convert.ToInt32(ddlUpdateChildren.SelectedItem.Text));
                cmd.Parameters.AddWithValue("@mobile", Convert.ToInt32(txtUpdateMobile.Text));
                cmd.Parameters.AddWithValue("@ac", ac);
                cmd.Parameters.AddWithValue("@laundry", laundry);
                cmd.Parameters.AddWithValue("@pickUp", pickUp);
                cmd.Parameters.AddWithValue("@dropUp", dropUp);
                cmd.Parameters.AddWithValue("@pickUpAddress", txtUpdatePickUpAddress.Text);
                cmd.Parameters.AddWithValue("@dropUpAddress", txtUpdateDropUpAddress.Text);
                cmd.Parameters.AddWithValue("@totalAmount", total);
                con.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    // Sending Email notification for modified booking.
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("contactus@clarionmotel.com");
                    msg.To.Add(Session["userEmail"].ToString());
                    msg.Subject = "Booking Modified";
                    msg.Body = "Congratulations!<br/>You have modified your booking. Your modified booking ID is: " + currentBookingID + "<br/>Please contact our receptionist for further process.<br/><br/>Cheers,<br/>Clarion Motels";
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

                    ddlUpdateRoomType.SelectedIndex = 0;
                    txtUpdateFromDate.Text = string.Empty;
                    txtUpdateToDate.Text = string.Empty;
                    ddlUpdateAdults.SelectedIndex = 0;
                    ddlUpdateChildren.SelectedIndex = 0;
                    txtUpdateMobile.Text = string.Empty;
                    chkUpdateAC.Checked = false;
                    chkUpdateLaundry.Checked = false;
                    chkUpdatePickUp.Checked = false;
                    chkDropUp.Checked = false;
                    Session["updateMessage"] = "y";
                    Session.Remove("currentBookingID");
                    Response.Redirect("booking.aspx");
                }
                else
                {
                    revealUpdateBookingStatus();
                    lblUpdateBookingStatus.Text = "Could not Update! Please try again.";
                    ddlUpdateRoomType.Focus();
                }
                con.Close();
            }
        }

        protected void btnYesCancel_Click(object sender, EventArgs e)
        {
            // If user confirms Cancel Booking operation delete booking data from database.
            string currentBookingID = Convert.ToString(Session["currentBookingID"]);
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM booking WHERE bookingId=@bookingId", con);
            cmd.Parameters.AddWithValue("@bookingId", currentBookingID);
            con.Open();
            if (cmd.ExecuteNonQuery() == 1)
            {
                Session["cancelMessage"] = "y";
                // Sending Email notification for cancel booking.
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("contactus@clarionmotel.com");
                msg.To.Add(Session["userEmail"].ToString());
                msg.Subject = "Booking Cancelled";
                msg.Body = "Congratulations!<br/>You have cancelled your booking successfully. Your cancelled booking ID is: " + currentBookingID + "<br/><br/>Cheers,<br/>Clarion Motels";
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
                con.Close();
                Session.Remove("currentBookingID");
                Response.Redirect("booking.aspx");
            }
        }
        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            // Logout on Log Out button click
            Session.Abandon();
            Session.Clear();
            Response.Redirect("Home.aspx");
        }
    }
}