<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page-logout.aspx.cs" Inherits="TemplatingPractice.page_logout" %>

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
                                        <asp:HyperLink runat="server" NavigateUrl="~/page-logout.aspx" CssClass="text-success">
                                            <span><asp:Image runat="server" ImageUrl="~/assets/images/logo.png" height="36"></asp:Image></span>
                                        </asp:HyperLink>
                                    </h2>
                                    <!--<h4 class="text-uppercase font-bold m-b-0">Sign In</h4>-->
                                </div>
                                <div class="account-content">
                                    <div class="text-center m-b-20">
                                        <div class="m-b-20">
                                            <div class="checkmark">
                                                <svg version="1.1" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 130.2 130.2">
                                                  <circle class="path circle" fill="none" stroke="#4bd396" stroke-width="6" stroke-miterlimit="10" cx="65.1" cy="65.1" r="62.1"/>
                                                  <polyline class="path check" fill="none" stroke="#4bd396" stroke-width="6" stroke-linecap="round" stroke-miterlimit="10" points="100.2,40.2 51.5,88.8 29.8,67.5 "/>
                                                </svg>
                                            </div>
                                        </div>

                                        <h3>See You Again !</h3>

                                        <p class="text-muted font-13 m-t-10"> You are now successfully sign out. </p>
                                    </div>

                                </div>
                            </div>
                            <!-- end card-box-->


                            <div class="row m-t-30">
                                <div class="col-sm-12 text-center">
                                    <p class="text-muted">Return to <asp:HyperLink runat="server" NavigateUrl="~/page-login.aspx" CssClass="text-primary m-l-5 m-r-5"><strong>Log In</strong></asp:HyperLink>
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

    </form>
</body>
</html>
