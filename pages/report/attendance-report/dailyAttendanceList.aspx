<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="dailyAttendanceList.aspx.cs" Inherits="TemplatingPractice.pages.report.attendance_report.dailyAttendanceList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content-page">
    <div class="content">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <div class="page-title-box">
                        <ol class="breadcrumb p-0 m-0">
                            <li class="blue">Home</li>
                            <li class="blue">Reports</li>
                            <li class="blue">Attendance Reports</li>
                            <li class="active">Daily Attendance Report</li>
                        </ol>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
            <!-- end row -->

            <div class="row">
                <div class="col-lg-12">
                    <div class="row">
                        <div class="col-md-6 button-list pull-left">
                            <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary btn-bordered" OnClick="btnNew_Click" />
                            <asp:Button ID="btnExport" runat="server" Text="Excel" CssClass="btn btn-success btn-bordered" OnClick="btnExport_Click" />
                        </div>
                        <div class="col-md-6 form-group pull-left">
                            <div style="font-weight: bold; text-align: center" class="form-group">
                                Date : <asp:Label ID="lblDate" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvReport" runat="server"
                                    CssClass="table table-striped table-bordered table-responsive table-colored"
                                    AutoGenerateColumns="false"
                                    GridLines="None"
                                    EmptyDataText="No employees found for the selected Branch/Department."
                                    UseAccessibleHeader="true"
                                    OnRowDataBound="gvReport_RowDataBound">
                                    <HeaderStyle BackColor="#4B77BE" ForeColor="White" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate><%# Eval("EmployeeName") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate><%# Eval("EmployeeID") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch">
                                            <ItemTemplate><%# Eval("BranchName") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate><%# Eval("DepartmentName") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="In Time">
                                            <ItemTemplate><%# Eval("InTime") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Out Time">
                                            <ItemTemplate><%# Eval("OutTime") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate><%# Eval("Remarks") %></ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphScripts" runat="server">
</asp:Content>