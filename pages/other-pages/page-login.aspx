<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page-login.aspx.cs" Inherits="TemplatingPractice.LoginPage" %>

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
                                    <asp:Hyperlink runat="server" NavigateUrl="~/page-login.aspx" CssClass="text-success">
                                        <asp:Image runat="server" ImageUrl="~/assets/images/logo.png" height="36"></asp:Image>
                                    </asp:Hyperlink>
                                </h2>
                            </div>

                            <div class="account-content">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-xs-12">
                                            <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" PlaceHolder="Username" required="required"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="Username is Required"  Display="None" ControlToValidate="txtUsername"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-xs-12">
                                            <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="password" PlaceHolder="Password" required="required"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Password is Required" Display="None" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="form-group ">
                                        <div class="col-xs-12">
                                            <div class="checkbox checkbox-success">
                                                <asp:CheckBox runat="server" ID="chkRemember" Checked="true" Text="Remember me"></asp:CheckBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group text-center m-t-30">
                                        <div class="col-sm-12">
                                            <asp:HyperLink runat="server" NavigateUrl="~/page-recoverpw.aspx" CssClass="text-muted"><i class="fa fa-lock m-r-5"></i> Forgot your password?</asp:HyperLink>
                                        </div>
                                    </div>

                                    <asp:ValidationSummary ID="vsLogin" ForeColor="Red" runat="server" />

                                    <div class="form-group account-btn text-center m-t-10">
                                        <div class="col-xs-12">
                                            <button runat="server" id="btnLogin" onserverclick="btnLogin_Click" class="btn w-md btn-bordered btn-danger waves-effect waves-light" type="submit"> Log In </button>
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
                                    Don't have an account? <asp:HyperLink runat="server" NavigateUrl="~/page-register.aspx" CssClass="text-primary m-l-5" ><strong>Sign Up</strong></asp:HyperLink>
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