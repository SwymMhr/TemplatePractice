<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page-register.aspx.cs" Inherits="TemplatingPractice.RegisterForm" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <link rel="shortcut icon" href="~/assets/images/favicon.ico">
    <title> Avighna Technologies </title>

    <link runat="server" href="~/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link runat="server" href="~/assets/css/core.css" rel="stylesheet" type="text/css" />
    <link runat="server" href="~/assets/css/components.css" rel="stylesheet" type="text/css" />
    <link runat="server" href="~/assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link runat="server" href="~/assets/css/pages.css" rel="stylesheet" type="text/css" />
    <link runat="server" href="~/assets/css/menu.css" rel="stylesheet" type="text/css" />
    <link runat="server" href="~/assets/css/responsive.css" rel="stylesheet" type="text/css" />

    <script src="~/assets/js/modernizr.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        
        <!-- Loader -->
        <div id="preloader">
            <div id="status">
                <div class="spinner">
                  <div class="spinner-wrapper">
                    <div class="rotator">
                      <div class="inner-spin"></div>
                      <div class="inner-spin"></div>
                    </div>
                  </div>
                </div>
            </div>
        </div>

        <!-- HOME -->
        <section>
            <div class="container-alt">
                <div class="row">
                    <div class="col-sm-12">

                        <div class="wrapper-page">

                            <div class="m-t-40 account-pages">
                                <div class="text-center account-logo-box">
                                    <h2 class="text-uppercase">
                                        <asp:HyperLink runat="server" NavigateUrl="~/page-register.aspx" CssClass="text-success">
                                            <span><asp:Image runat="server" ImageUrl="~/assets/images/logo.png" height="36"></asp:Image></span>
                                        </asp:HyperLink>
                                    </h2>
                                    <!--<h4 class="text-uppercase font-bold m-b-0">Sign In</h4>-->
                                </div>
                                <div class="account-content">
                                    <div class="form-horizontal">

                                        <div class="form-group ">
                                            <div class="col-xs-12">
                                                <asp:Textbox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" required="required" placeholder="Email"></asp:Textbox>
                                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is Required" Display="None"></asp:RequiredFieldValidator>                 
                                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid email format" ValidationExpression="\w+([-\+\.']\w+)*@\w+([-\.]\w+)*\.\w+([-\.]\w+)*" Display="None" />
                                            </div>
                                        </div>

                                        <div class="form-group ">
                                            <div class="col-xs-12">
                                                <asp:Textbox ID="txtUsername" runat="server" CssClass="form-control" required="required" placeholder="Username"></asp:Textbox>
                                                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is Required" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-xs-12">
                                                <asp:Textbox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" required="required" placeholder="Password"></asp:Textbox>
                                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is Required" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-xs-12">
                                                <div class="checkbox checkbox-success">
                                                    <asp:CheckBox runat="server" ID="chkTerms" Checked="true" Text="I accept"></asp:CheckBox>                                                    
                                                    <asp:Label runat="server"><asp:HyperLink runat="server" NavigateUrl="#">Terms and Conditions</asp:HyperLink></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:ValidationSummary ID="vsLogin" runat="server" ForeColor="Red" />

                                        <div class="form-group account-btn text-center m-t-10">
                                            <div class="col-xs-12">
                                                <button runat="server" id="btnRegister" onserverclick="btnRegister_Click" class="btn w-md btn-danger btn-bordered waves-effect waves-light" type="submit">Register</button>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="clearfix"></div>

                                </div>
                            </div>
                            <!-- end card-box-->


                            <div class="row m-t-50">
                                <div class="col-sm-12 text-center">
                                    <p class="text-muted">
                                        Already have account?<asp:HyperLink runat="server" NavigateUrl="page-login.aspx" CssClass="text-primary m-l-5"><strong>Sign In</strong></asp:HyperLink>
                                    </p>
                                </div>
                            </div>

                        </div>
                        <!-- end wrapper -->

                    </div>
                </div>
            </div>
          </section>
          <!-- END HOME -->

        <script>
            var resizefunc = [];
        </script>

        <!-- jQuery  -->
        <script src="<%= ResolveUrl("~/assets/js/jquery.min.js") %>"></script>
        <script src="<%= ResolveUrl("~/assets/js/bootstrap.min.js") %>"></script>
        <script src="<%= ResolveUrl("~/assets/js/detect.js") %>"></script>
        <script src="<%= ResolveUrl("~/assets/js/fastclick.js") %>"></script>
        <script src="<%= ResolveUrl("~/assets/js/jquery.blockUI.js") %>"></script>
        <script src="<%= ResolveUrl("~/assets/js/waves.js") %>"></script>
        <script src="<%= ResolveUrl("~/assets/js/jquery.slimscroll.js") %>"></script>
        <script src="<%= ResolveUrl("~/assets/js/jquery.scrollTo.min.js") %>"></script>
        <script src="<%= ResolveUrl("~/assets/js/jquery.core.js") %>"></script>
        <script src="<%= ResolveUrl("~/assets/js/jquery.app.js") %>"></script>

        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    </form>
</body>
</html>
