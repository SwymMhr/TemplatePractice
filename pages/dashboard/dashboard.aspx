<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="TemplatingPractice.pages.dashboard.dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<!-- Page Wrapper -->
<div class="content-page">

    <div class="content">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <div class="page-title-box">
                        <h4 class="page-title">Dashboard</h4>
                        <ol class="breadcrumb p-0 m-0">
                            <li class="active">Dashboard</li>
                        </ol>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
            <!-- end row -->
            <div class="row">
                <div class="col-lg-4">
                    <div class="panel panel-color panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Present</h3>
                            <h2 class="text-white">
                                <span data-plugin="counterup">
                                    <asp:Literal ID="litPresentCount" runat="server" Text="0" ValidateRequestMode="Disabled"></asp:Literal>
                                </span>
                            </h2>
                        </div>
                        <div class="panel-body">
                            <div class="inbox-widget slimscroll-alt" style="min-height: 327px;">
                                <asp:Repeater ID="rptPresentEmployees" runat="server">
                                    <ItemTemplate>
                                        <div class="inbox-item">
                                            <div class="inbox-item-img">
                                                <asp:Image runat="server" ImageUrl='<%# GetEmployeeImageUrl(Eval("ImageData")) %>' CssClass="img-circle"/>
                                            </div>
                                            <p class="inbox-item-author"><%# Eval("EmployeeName") %> (<%# Eval("EmployeeCode") %>)</p>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <!-- table-responsive -->
                        </div>
                    </div>
                    <!-- end panel -->
                </div>
                <!-- end col -->

                <div class="col-lg-4">
                    <div class="panel panel-color panel-danger">
                        <div class="panel-heading">
                            <h3 class="panel-title">Absent</h3>
                            <h2 class="text-white">
                                <span data-plugin="counterup">
                                    <asp:Literal ID="litAbsentCount" runat="server">0</asp:Literal>
                                </span>
                            </h2>
                        </div>
                        <div class="panel-body">
                            <div class="inbox-widget slimscroll-alt" style="min-height: 327px;">
                                <asp:Repeater runat="server" ID="rptAbsentEmployees">
                                    <ItemTemplate>
                                        <div class="inbox-item">
                                            <div class="inbox-item-img">
                                                <asp:Image runat="server" ImageUrl='<%# GetEmployeeImageUrl(Eval("ImageData")) %>' CssClass="img-circle" />
                                            </div>
                                            <p class="inbox-item-author"><%# Eval("EmployeeName") %> (<%# Eval("EmployeeCode") %>)</p>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <!-- table-responsive -->
                        </div>
                    </div>
                    <!-- end panel -->
                </div>
                <!-- end col -->

                <div class="col-lg-4">
                    <div class="panel panel-color panel-teal">
                        <div class="panel-heading">
                            <h3 class="panel-title">Leave</h3>
                            <h2 class="text-white">
                                <span data-plugin="counterup">
                                    <asp:Literal ID="litLeaveCount" runat="server">0</asp:Literal>
                                </span>
                            </h2>
                        </div>
                        <div class="panel-body">
                            <div class="inbox-widget slimscroll-alt" style="min-height: 327px;">
                                <asp:Repeater runat="server" ID="rptLeaveEmployees">
                                    <ItemTemplate>
                                        <div class="inbox-item">
                                            <div class="inbox-item-img">
                                                <asp:Image runat="server" ImageUrl='<%# GetEmployeeImageUrl(Eval("ImageData")) %>' CssClass="img-circle" />
                                            </div>
                                            <p class="inbox-item-author"><%# Eval("EmployeeName") %> (<%# Eval("EmployeeCode") %>)</p>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <!-- table-responsive -->
                        </div>
                    </div>
                    <!-- end panel -->
                </div>
                <!-- end col -->
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="panel panel-color panel-orange">
                        <div class="panel-heading">
                            <h3 class="panel-title">Upcoming Public Holidays</h3>
                        </div>
                        <div class="panel-body">
                            <div class="inbox-widget slimscroll-alt">
                                <span id="content_publicHolidays"></span>
                            </div>
                        </div>
                    </div>
                    <!-- end panel -->
                </div>
                <!-- end col -->
                <div class="col-lg-6">
                    <div class="panel panel-color panel-purple">
                        <div class="panel-heading">
                            <h3 class="panel-title">Leave List</h3>
                        </div>
                        <div class="panel-body">
                            <div class="inbox-widget slimscroll-alt">                                
                                <asp:Repeater runat="server" ID="rptLeaveList">
                                    <ItemTemplate>
                                        <div class="inbox-item">
                                            <div class="inbox-item-img">
                                                <asp:Image runat="server" ImageUrl='<%# GetEmployeeImageUrl(Eval("ImageData")) %>' CssClass="img-circle" />
                                            </div>
                                            <p class="inbox-item-author"><%# Eval("EmployeeName") %> (<%# Eval("EmployeeCode") %>)</p>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>           
                            </div>
                        </div>
                    </div>
                    <!-- end panel -->
                </div>
            </div>
            <!-- end row -->
        </div>
        <!-- container -->
    </div>
    <!-- content -->

</div>
<!-- End Page Wrapper -->

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphScripts" runat="server">
    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            document.querySelectorAll('[data-plugin="counterup"]').forEach(function (el) {
                var target = parseInt(el.textContent.trim(), 10) || 0;
                var current = 0;
                var duration = 1000;
                var stepTime = Math.max(Math.floor(duration / Math.max(target, 1)), 20);

                if (target === 0) return;

                var timer = setInterval(function () {
                    current++;
                    el.textContent = current;
                    if (current >= target) {
                        clearInterval(timer);
                    }
                }, stepTime);
            });
        });
    </script>
</asp:Content>
