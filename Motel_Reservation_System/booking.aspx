<%@ Page Title="" Language="C#" MasterPageFile="~/mrs.Master" AutoEventWireup="true" CodeBehind="booking.aspx.cs" Inherits="Motel_Reservation_System.booking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Section: User home page head -->
    <section id="user-homepage-head" class="user-homepage-head">
        <nav class="navbar navbar-custom navbar-fixed-top" role="navigation">
            <div class="container">
                <div class="navbar-header page-scroll">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-main-collapse">
                        <i class="fa fa-bars"></i>
                    </button>
                    <a class="navbar-brand" href="home.aspx">
                        <h1>CLARION MOTEL</h1>
                    </a>
                </div>
                <!-- Collect the nav links, forms, and other content for toggling -->
                <div class="collapse navbar-collapse navbar-right navbar-main-collapse">
                    <ul class="nav navbar-nav">
                        <li class="active"><a href="#bookItNow">Book Now</a></li>
                        <li><a href="#myBookingList">My Booking</a></li>
                        <li><a href="#contact">Contact</a></li>
                        <li>
                            <asp:LinkButton ID="lnkLogOut" Text="Log Out" runat="server" OnClick="lnkLogOut_Click" /></li>
                    </ul>
                </div>
                <!-- /.navbar-collapse -->
            </div>
            <!-- /.container -->
        </nav>
    </section>
    <!-- /Section: User home page head -->

    <!-- Section: Welcome -->
    <section class="text-center" runat="server">
        <div class="container">
            <div class="row">
                <br />
                <h4 class="text-left col-xs-6">
                    <asp:Label ID="lblWelcomeMessage" Text="Hi" runat="server" />
                </h4>
                <h4 class="text-right col-xs-6">
                    <%-- Added script manager to use AJAX in web page --%>
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:Timer ID="currentTime" runat="server" Interval="1000" OnTick="currentTime_Tick"></asp:Timer>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblTime" runat="server"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="currentTime" EventName="Tick"></asp:AsyncPostBackTrigger>
                        </Triggers>
                    </asp:UpdatePanel>
                </h4>
            </div>
        </div>
    </section>
    <!-- /Section: Welcome  -->

    <!-- Section: Book now -->
    <section id="bookNow" class="home-section text-center" runat="server">
        <div class="heading-about" id="bookItNow">
            <div class="container">
                <div class="row text-center">
                    <div class="col-lg-8 col-lg-offset-2">
                        <div class="wow bounceInDown" data-wow-delay="0.4s">
                            <div class="section-heading">
                                <h2>Book Now</h2>
                                <i class="fa fa-2x fa-angle-down"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container">
            <div class="row">
                <div id="bookingStatus" class="alert alert-info col-lg-10 col-lg-offset-1 hidden" runat="server">
                    <asp:Label ID="lblBookingMessage" runat="server"></asp:Label>
                </div>
                <div class="col-lg-2 col-lg-offset-5">
                    <hr class="marginbot-50">
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-body text-left">
                    <div class="row">
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <strong>
                                <asp:RequiredFieldValidator ID="rfvRoomType" InitialValue="liSelectRoom" ControlToValidate="ddlRoomType" ErrorMessage="*" ValidationGroup="vgBooking" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblRoomType" Text="* Select Room" runat="server"></asp:Label></strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:DropDownList ID="ddlRoomType" CssClass="form-control" runat="server">
                                <asp:ListItem Text="Select" Value="liSelectRoom" />
                                <asp:ListItem Text="General Room (80$)" Value="liGeneralRoom" />
                                <asp:ListItem Text="Delux Room (120$)" Value="liDeluxRoom" />
                                <asp:ListItem Text="Luxury Room (150$)" Value="liLuxuryRoom" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <strong>
                                <asp:RequiredFieldValidator ID="rfvBookingFromDate" runat="server" ControlToValidate="txtBookingFromDate" ErrorMessage="*" ValidationGroup="vgBooking" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblBookingFromDate" CssClass="text-left" Text="* Booking From Date" runat="server"></asp:Label></strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:TextBox ID="txtBookingFromDate" class="txtBookingFromDate" CssClass="form-control" placeholder="Booking From" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <strong>
                                <asp:RequiredFieldValidator ID="rfvBookingToDate" runat="server" ControlToValidate="txtBookingToDate" ErrorMessage="*" ValidationGroup="vgBooking" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblBookingToDate" CssClass="text-left" Text="* Booking Upto" placeholder="Booking Upto" runat="server"></asp:Label></strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:TextBox ID="txtBookingToDate" CssClass="form-control" placeholder="Booking Upto" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <strong>
                                <asp:RequiredFieldValidator ID="rfvAdults" runat="server" ControlToValidate="ddlSelectAdults" InitialValue="liSelectAdults" ErrorMessage="*" ValidationGroup="vgBooking" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblAdults" CssClass="text-left" Text="* Adults" runat="server"></asp:Label></strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:DropDownList ID="ddlSelectAdults" CssClass="form-control" runat="server">
                                <asp:ListItem Text="Select" Value="liSelectAdults" />
                                <asp:ListItem Text="1" Value="liOneAdult" />
                                <asp:ListItem Text="2" Value="liTwoAdults" />
                                <asp:ListItem Text="3" Value="liThreeAdults" />
                                <asp:ListItem Text="4" Value="liFourAdults" />
                                <asp:ListItem Text="5" Value="liFiveAdults" />
                                <asp:ListItem Text="6" Value="liSixAdults" />
                                <asp:ListItem Text="7" Value="liSevenAdults" />
                                <asp:ListItem Text="8" Value="liEightAdults" />
                                <asp:ListItem Text="9" Value="liNineAdults" />
                                <asp:ListItem Text="10" Value="liTenAdults" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <strong>
                                <asp:Label ID="lblChildren" Text="Children" runat="server"></asp:Label></strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:DropDownList ID="ddlSelectChildren" CssClass="form-control" runat="server">
                                <asp:ListItem Text="Select" Value="liSelectAdults" />
                                <asp:ListItem Text="1" Value="liOneChild" />
                                <asp:ListItem Text="2" Value="liTwoChildren" />
                                <asp:ListItem Text="3" Value="liThreeChildren" />
                                <asp:ListItem Text="4" Value="liFourChildren" />
                                <asp:ListItem Text="5" Value="liFiveChildren" />
                                <asp:ListItem Text="6" Value="liSixChildren" />
                                <asp:ListItem Text="7" Value="liSevenChildren" />
                                <asp:ListItem Text="8" Value="liEightChildren" />
                                <asp:ListItem Text="9" Value="liNineChildren" />
                                <asp:ListItem Text="10" Value="liTenChildren" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <strong>
                                <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ControlToValidate="txtMobile" ErrorMessage="*" ValidationGroup="vgBooking" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblMobile" Text="* Mobile" runat="server"></asp:Label></strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:TextBox ID="txtMobile" CssClass="form-control" placeholder="Enter Mobile" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <br />
                        <div class="col-xs-12 col-sm-6 col-md-4 text-left">
                            <strong>Select other services you want. Extra Charges apply:</strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:CheckBox ID="chkAC" CssClass="checkbox-inline" Text="Air Conditioner (20$)" runat="server" />
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:CheckBox ID="chkLaundry" CssClass="checkbox-inline" Text="Laundry (10$)" runat="server" />
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:CheckBox ID="chkPickUp" CssClass="checkbox-inline" Text=" Pick Up (40$)" runat="server" />
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:CheckBox ID="chkDropUp" CssClass="checkbox-inline" Text=" Drop Up (40$)" runat="server" />
                        </div>
                    </div>
                    <br />
                    <div class="row hidden" id="divPickDrop" runat="server">
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <strong>
                                <asp:Label ID="lblPickUpAddress" CssClass="hidden" Text="Pick Up Address" runat="server"></asp:Label>
                            </strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-4">
                            <asp:TextBox ID="txtPickUpAddress" CssClass="form-control hidden" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <strong>
                                <asp:Label ID="lblDropUpAddress" CssClass="hidden" Text="Drop Up Address" runat="server"></asp:Label>
                            </strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-4">
                            <asp:TextBox ID="txtDropUpAddress" CssClass="form-control hidden" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="text-center">
                        <asp:Button Text="Book Now" ID="btnNewBooking" CssClass="btn btn-skin btn-huge" runat="server" OnClick="btnNewBooking_Click" />
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- /Section: Book now -->

    <!-- Section: My booking -->
    <section id="myBooking" class="home-section" runat="server">
        <div class="heading-about" id="myBookingList">
            <div class="container text-center">
                <div class="row">
                    <div class="col-lg-8 col-lg-offset-2">
                        <div class="wow bounceInDown" data-wow-delay="0.4s">
                            <div class="section-heading">
                                <h2>My Booking</h2>
                                <i class="fa fa-2x fa-angle-down"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="container">
            <div class="row">
                <div class="col-lg-2 col-lg-offset-5">
                    <hr class="marginbot-50">
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-body" id="bookingDetails" runat="server">
                </div>
            </div>
        </div>
    </section>
    <!-- /Section: My booking -->

    <!-- Section: Update booking -->
    <section id="updateBooking" class="hidden" runat="server">
        <div class="heading-about">
            <div class="container text-center">
                <div class="row">
                    <div class="col-lg-8 col-lg-offset-2">
                        <div class="wow bounceInDown" data-wow-delay="0.4s">
                            <div class="section-heading">
                                <h2>Modify Your Booking</h2>
                                <i class="fa fa-2x fa-angle-down"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="container">
            <div class="row">
                <div id="updateBookingStatus" class="alert alert-info col-lg-10 col-lg-offset-1 hidden" runat="server">
                    <asp:Label ID="lblUpdateBookingStatus" runat="server"></asp:Label>
                </div>
                <div class="col-lg-2 col-lg-offset-5">
                    <hr class="marginbot-50">
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-body" runat="server">
                    <div class="row">
                        <div class="col-xs-12">
                            <h4>Booking ID
                                <asp:Label ID="lblUpdateBookingID" Text="text" runat="server"></asp:Label></h4>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <strong>
                                <asp:RequiredFieldValidator ID="rfvUpdateRoomType" ControlToValidate="ddlUpdateRoomType" ErrorMessage="*" ValidationGroup="vgUpdateBooking" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblUpdateRoomType" Text="* Select Room" runat="server"></asp:Label></strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:DropDownList ID="ddlUpdateRoomType" CssClass="form-control" runat="server">
                                <asp:ListItem Text="Select" Value="liUpdateSelectRoom" />
                                <asp:ListItem Text="General Room (80$)" Value="liGeneralRoom" />
                                <asp:ListItem Text="Delux Room (120$)" Value="liDeluxRoom" />
                                <asp:ListItem Text="Luxury Room (150$)" Value="liLuxuryRoom" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <strong>
                                <asp:RequiredFieldValidator ID="rfvUpdateFromDate" runat="server" ControlToValidate="txtUpdateFromDate" ErrorMessage="*" ValidationGroup="vgUpdateBooking" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblUpdateFromDate" Text="* From Date" runat="server"></asp:Label></strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:TextBox ID="txtUpdateFromDate" CssClass="form-control" placeholder="Booking From" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <strong>
                                <asp:RequiredFieldValidator ID="rfvUpdateToDate" runat="server" ControlToValidate="txtUpdateToDate" ErrorMessage="*" ValidationGroup="vgUpdateBooking" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblUpdateToDate" Text="* To Date" runat="server"></asp:Label></strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:TextBox ID="txtUpdateToDate" CssClass="form-control" placeholder="Booking Upto" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <strong>
                                <asp:RequiredFieldValidator ID="rfvUpdateAdults" runat="server" ControlToValidate="ddlUpdateAdults" ErrorMessage="*" ValidationGroup="vgBooking" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblUpdateAdults" Text="* Adults" runat="server"></asp:Label></strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:DropDownList ID="ddlUpdateAdults" CssClass="form-control" runat="server">
                                <asp:ListItem Text="Select" Value="liUpdateSelectAdults" />
                                <asp:ListItem Text="1" Value="liOneAdult" />
                                <asp:ListItem Text="2" Value="liTwoAdults" />
                                <asp:ListItem Text="3" Value="liThreeAdults" />
                                <asp:ListItem Text="4" Value="liFourAdults" />
                                <asp:ListItem Text="5" Value="liFiveAdults" />
                                <asp:ListItem Text="6" Value="liSixAdults" />
                                <asp:ListItem Text="7" Value="liSevenAdults" />
                                <asp:ListItem Text="8" Value="liEightAdults" />
                                <asp:ListItem Text="9" Value="liNineAdults" />
                                <asp:ListItem Text="10" Value="liTenAdults" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <strong>
                                <asp:Label ID="lblUpdateChildrens" Text="Childrens" runat="server"></asp:Label></strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:DropDownList ID="ddlUpdateChildren" CssClass="form-control" runat="server">
                                <asp:ListItem Text="Select" Value="liUpdateSelectChildren" />
                                <asp:ListItem Text="1" Value="liOneChildren" />
                                <asp:ListItem Text="2" Value="liTwoChildren" />
                                <asp:ListItem Text="3" Value="liThreeChildren" />
                                <asp:ListItem Text="4" Value="liFourChildren" />
                                <asp:ListItem Text="5" Value="liFiveChildren" />
                                <asp:ListItem Text="6" Value="liSixChildren" />
                                <asp:ListItem Text="7" Value="liSevenChildren" />
                                <asp:ListItem Text="8" Value="liEightChildren" />
                                <asp:ListItem Text="9" Value="liNineChildren" />
                                <asp:ListItem Text="10" Value="liTenChildren" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <strong>
                                <asp:RequiredFieldValidator ID="rfvUpdateMobile" runat="server" ControlToValidate="txtUpdateMobile" ErrorMessage="*" ValidationGroup="vgUpdateBooking" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblUpdateMobile" Text="* Mobile" runat="server"></asp:Label></strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:TextBox ID="txtUpdateMobile" CssClass="form-control" placeholder="Enter Mobile" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <br />
                        <div class="col-xs-12 col-sm-6 col-md-4 text-left">
                            <strong>Select other services you want. Extra Charges apply:</strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:CheckBox ID="chkUpdateAC" CssClass="checkbox-inline" Text="Air Conditioner (20$)" runat="server" />
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:CheckBox ID="chkUpdateLaundry" CssClass="checkbox-inline" Text="Laundry (10$)" runat="server" />
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:CheckBox ID="chkUpdatePickUp" CssClass="checkbox-inline" Text=" Pick Up (40$)" runat="server" />
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <asp:CheckBox ID="chkUpdateDropUp" CssClass="checkbox-inline" Text=" Drop (40$)" runat="server" />
                        </div>
                    </div>
                    <br />
                    <div class="row hidden" id="divUpdatePickDrop" runat="server">
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <strong>
                                <asp:Label ID="lblUpdatePickUpAddress" class="hidden" Text="Pick Up Address" runat="server"></asp:Label>
                            </strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-4">
                            <asp:TextBox ID="txtUpdatePickUpAddress" class="form-control hidden" placeholder="Enter Pick Up Address" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-2">
                            <strong>
                                <asp:Label ID="lblUpdateDropUpAddress" class="hidden" Text="Drop Up Address" runat="server"></asp:Label>
                            </strong>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-4">
                            <asp:TextBox ID="txtUpdateDropUpAddress" class="form-control hidden" placeholder="Enter Drop Up Address" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="hidden" id="confirmCancel">
                        Are you sure you want to cancel the booking?
                        <asp:Button ID="btnYesCancel" CssClass="btn btn-skin" Text="Yes" runat="server" OnClick="btnYesCancel_Click" />
                        <asp:Button ID="btnNoCancel" CssClass="btn btn-default" Text="No" OnClientClick="return false;" runat="server" /><br />
                    </div>
                    <div id="updateOrCancel" class="">
                        <asp:Button ID="btnUpdateBooking" CssClass="btn btn-skin" Text="Update" ValidationGroup="vgUpdateBooking" runat="server" OnClick="btnUpdateBooking_Click"></asp:Button>
                        <asp:Button ID="btnCancelBooked" CssClass="btn btn-skin" Text="Cancel Booking" runat="server" OnClientClick="return false;" />
                        <asp:Button ID="btnCancelUpdate" CssClass="btn btn-default" Text="Back" runat="server" OnClick="btnCancelUpdate_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <%-- /Update Booking Section--%>
</asp:Content>