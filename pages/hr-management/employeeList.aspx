<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="employeeList.aspx.cs" Inherits="TemplatingPractice.pages.hr_management.employeeList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content-page">

    <div class="content">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <div class="page-title-box">
                        <h4 class="page-title">HR Management </h4>
                        <ol class="breadcrumb p-0 m-0">
                            <li>
                                <a href="~/Pages/dashboard/dashboard.aspx" runat="server">Home</a>
                            </li>
                            <li class="active">Employees List</li>
                        </ol>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
            <!-- end row -->

            <div class="form-horizontal">
                <div class="row">
                    <div class="col-lg-12">


                            <div class="button-list">
                                <asp:Button ID="btnNew" runat="server" Text="Add" CssClass="btn btn-primary w-sm m-b-0" OnClick="btnNew_Click" />
                            </div>
                            <div class="clearfix" style="margin-bottom: 15px;"></div>

                            <asp:GridView ID="gvEmployees" runat="server"
                                CssClass="table table-striped table-bordered table-hover table-responsive table-colored table-info"
                                AutoGenerateColumns="false"
                                DataKeyNames="EmployeeID"
                                GridLines="None"
                                EmptyDataText="No employee found."
                                OnRowCommand="gvEmployees_RowCommand"
                                ClientIDMode="Static"
                                UseAccessibleHeader="true">
                                <HeaderStyle />

                                <Columns>
                                    <asp:TemplateField HeaderText="S.N">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Employee (ID)">
                                        <ItemTemplate>
                                            <%# Eval("EmployeeName") %> (<%# Eval("EmployeeID") %>)
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="DesignationName" HeaderText="Designation" />
                                    <asp:BoundField DataField="GradeName" HeaderText="Grade" />
                                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                    <asp:BoundField DataField="BranchName" HeaderText="Branch" />
                                    <asp:BoundField DataField="UserType" HeaderText="User Type" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" />

                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <div class='button-list'>
                                                <asp:LinkButton ID="lnkView" runat="server" CssClass="btn btn-info w-sm waves-effect waves-light btn-md" CommandName="viewEmployee" CommandArgument='<%# Eval("EmployeeID") %>'>View Details</asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                    </div>
                </div>
            </div>
            <!-- end row -->
        </div> <!-- container -->
    </div> <!-- content -->

</div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphScripts" runat="server">

    <!-- DataTables core -->
    <script src="<%= ResolveUrl("~/assets/plugins/datatables/jquery.dataTables.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/assets/plugins/datatables/dataTables.bootstrap.js") %>"></script>

    <!-- DataTables Buttons extension (export/print) -->
    <script src="<%= ResolveUrl("~/assets/plugins/datatables/dataTables.buttons.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/assets/plugins/datatables/buttons.bootstrap.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/assets/plugins/datatables/jszip.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/assets/plugins/datatables/pdfmake.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/assets/plugins/datatables/vfs_fonts.js") %>"></script>
    <script src="<%= ResolveUrl("~/assets/plugins/datatables/buttons.html5.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/assets/plugins/datatables/buttons.print.min.js") %>"></script>

    <script>
        $(document).ready(function () {
            $('#gvEmployees').DataTable({
                order: [],
                columnDefs: [
                    { orderable: false, targets: -1 }
                ]
            });
        });
    </script>

</asp:Content>