<%@ Page Title="" Language="C#" MasterPageFile="~/mrs.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Motel_Reservation_System.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Home page head-->
    <!-- Section: intro -->
    <section>
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
                        <li class="active"><a href="#intro">Home</a></li>
                        <li><a href="#about">About</a></li>
                        <li><a href="#login">Login</a></li>
                        <li><a href="#register">Register</a></li>
                        <li><a href="#contact">Contact</a></li>
                    </ul>
                </div>
                <!-- /.navbar-collapse -->
            </div>
            <!-- /.container -->
        </nav>
    </section>
    <!-- /.Home page head -->

    <!-- Section: intro -->
    <section id="intro" class="intro">

        <div class="slogan">
            <h2>WELCOME TO <span class="text_color">CLARION MOTEL</span> </h2>
            <h4>WE ARE GROUP OF PEOPLE SERVING THE BEST</h4>
            <br />
            <br />
        </div>
        <div class="page-scroll">
            <a href="#about" class="btn btn-circle">
                <i class="fa fa-angle-double-down animated"></i>
            </a>
        </div>
    </section>
    <!-- /Section: intro -->

    <!-- Section: about -->
    <section id="about" class="home-section">
        <div class="heading-about">
            <div class="container">
                <div class="row text-center">
                    <div class="col-lg-8 col-lg-offset-2">
                        <div class="wow bounceInDown" data-wow-delay="0.4s">
                            <div class="section-heading">
                                <h2>About us</h2>
                                <i class="fa fa-2x fa-angle-down"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="intro-para">
                    <strong class="text-justify">Let Motel Clarion be your metropolitan home away from home. With our friendly staff and our exceptional facilities, Add spacious guestrooms, upgraded rooms with added facilities, free Wifi, you may never want to leave!
                    <br />
                        <br />
                        Retro style architecture, comfortable rooms, and friendly customer service at Motel Clarion with the closest proximity to the islands main airport.
                    <br />
                        <br />
                        As you roam the guest areas, you will enjoy a collection of Victorian art including a fine replica of the four seasons sculpture and other pieces that add a touch of personality to our chic Motel. Whether you are in Sri Lanka for business or pleasure, our Motel offers the perfect accommodations, location and services to make your visit comfortable and unforgettable.</strong>
                    <br />
                    <br />
                    <strong class="text-center"><u>Business Center</u><br />
                        <br />
                        At Clarion, you can make your next business meeting as inspiring as it is productive. Our attentive planners deliver flawless productions that will enable your group to be focused on the mission at hand. Our passionate and highly-experienced team designs and delivers custom, seamless events that make it a joy to do business. We offer both indoor and outdoor venues ideally suited to your every need.</strong>
                    <br />
                    <br />
                    <strong class="text-center"><u>Laundry Service</u></strong><br />
                    <br />
                    <br />
                    <strong class="text-justify">Get crisp clean in less than 24 hours. Clarion’s in-house laundry service provides an excellent one day service to all our guests. A drive through service is also open till 8.00 pm every day.</strong>
                </div>
            </div>
        </div>
    </section>
    <!-- /Section: about -->

    <!-- Section: login -->
    <section id="login" class="home-section text-center bg-gray">
        <div class="heading-about">
            <div class="container">
                <div class="row">
                    <div class="col-lg-8 col-lg-offset-2">
                        <div class="wow bounceInDown" data-wow-delay="0.4s">
                            <div class="section-heading">
                                <h2>Login</h2>
                                <i class="fa fa-2x fa-angle-down"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container">
            <div class="row">
                <div id="loginMessage" class="alert alert-info col-lg-8 col-lg-offset-2 hidden" runat="server">
                    <asp:Label ID="lblLoginMessage" runat="server"></asp:Label>
                </div>
                <div class="col-lg-2 col-lg-offset-5">
                    <hr class="marginbot-50">
                </div>
            </div>
            <div class="row">
                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" ErrorMessage="*" ValidationGroup="vgLogin" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:Label Text="* User Name" runat="server" />
            </div>
            <div class="row">
                <div class="col-lg-2 col-lg-offset-5">
                    <asp:TextBox ID="txtUserName" CssClass="form-control" placeholder="Enter User Name" runat="server"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="*" ValidationGroup="vgLogin" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:Label Text="* Password" runat="server" />
            </div>
            <div class="row">
                <div class="col-lg-2 col-lg-offset-5">
                    <asp:TextBox ID="txtPassword" CssClass="form-control" TextMode="Password" placeholder="Enter Password" runat="server"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-2 col-lg-offset-5">
                    <asp:Button ID="btnLogin" CssClass="btn btn-skin" runat="server" Text="Login" ValidationGroup="vgLogin" OnClick="btnLogin_Click"></asp:Button>
                </div>
            </div>
        </div>
    </section>
    <!-- /Section: login -->

    <!-- Section: register -->
    <section id="register" class="home-section text-center bg-gray">
        <div class="heading-about">
            <div class="container">
                <div class="row">
                    <div class="col-lg-8 col-lg-offset-2">
                        <div class="wow bounceInDown" data-wow-delay="0.4s">
                            <div class="section-heading">
                                <h2>Register</h2>
                                <i class="fa fa-2x fa-angle-down"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container">
            <asp:Label Text="* Required fields." runat="server" />
            <div class="row">
                <div id="registerMessage" class="alert alert-info col-lg-8 col-lg-offset-2 hidden" runat="server">
                    <asp:Label ID="lblRegisterMessage" Text="" runat="server"></asp:Label>
                </div>
                <div class="col-lg-2 col-lg-offset-5">
                    <hr class="marginbot-50">
                </div>
            </div>
            <div class="row text-left">
                <div class="col-xs-12 col-sm-2 col-sm-offset-2">
                    <asp:RequiredFieldValidator ID="rfvNewFirstName" runat="server" ControlToValidate="txtNewFirstName" ErrorMessage="*" ValidationGroup="vgRegister" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label Text="* First Name" runat="server" />
                </div>
                <div class="col-xs-12 col-sm-2">
                    <asp:TextBox ID="txtNewFirstName" placeholder="Enter First Name" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-12 col-sm-2">
                    <asp:RequiredFieldValidator ID="rfvNewLastName" runat="server" ControlToValidate="txtNewLastName" ErrorMessage="*" ValidationGroup="vgRegister" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label Text="* Last Name" runat="server" />
                </div>
                <div class="col-xs-12 col-sm-2">
                    <asp:TextBox ID="txtNewLastName" placeholder="Enter Last Name" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row text-left">
                <div class="col-xs-12 col-sm-2 col-sm-offset-2">
                    <asp:RequiredFieldValidator ID="rfvNewEmail" runat="server" ControlToValidate="txtNewEmail" ErrorMessage="*" ValidationGroup="vgRegister" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label Text="* Email" runat="server" />
                </div>
                <div class="col-xs-12 col-sm-2">
                    <asp:TextBox ID="txtNewEmail" placeholder="Enter Email" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-12 col-sm-2">
                    <asp:RequiredFieldValidator ID="rfvNewUserName" runat="server" ControlToValidate="txtNewUserName" ErrorMessage="*" ValidationGroup="vgRegister" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label Text="* User Name" runat="server" />
                </div>
                <div class="col-xs-12 col-sm-2">
                    <asp:TextBox ID="txtNewUserName" placeholder="New Username" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row text-left">
                <div class="col-xs-12 col-sm-2 col-sm-offset-2">
                    <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="*" ValidationGroup="vgRegister" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label Text="* Password" runat="server" />
                </div>
                <div class="col-xs-12 col-sm-2">
                    <asp:TextBox ID="txtNewPassword" placeholder="New Password" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-12 col-sm-2">
                    <asp:RequiredFieldValidator ID="rfvConfirmNewPassword" runat="server" ControlToValidate="txtConfirmNewPassword" ErrorMessage="*" ValidationGroup="vgRegister" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label Text="* Retype Password" runat="server" />
                </div>
                <div class="col-xs-12 col-sm-2">
                    <asp:TextBox ID="txtConfirmNewPassword" placeholder="Retype Password" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-2 col-lg-offset-5">
                    <asp:Button ID="btnRegister" CssClass="btn btn-skin" runat="server" Text="Register" ValidationGroup="vgRegister" OnClick="btnRegister_Click"></asp:Button>
                </div>
            </div>
        </div>
    </section>
    <!-- /Section: register -->
</asp:Content>