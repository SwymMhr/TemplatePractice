<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="datewiseAttendanceList.aspx.cs" Inherits="TemplatingPractice.pages.report.attendance_report.datewiseAttendanceList" %>
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
                            <li class="active">Datewise Attendance List</li>
                        </ol>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
            <!-- end row -->

            <div class="col-md-12 form-group">
                <div class="col-md-6 button-list">
                    <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary col-md-2" OnClick="btnNew_Click" />
                    <asp:Button ID="tbnExport" runat="server" Text="Excell" CssClass="btn btn-success col-md-2" OnClick="btnExport_Click" />
                </div>
                <div>
                    <div style="font-weight: bold;" class="col-md-6">
                        From : <asp:Label ID="lblStartDate" runat="server" Text=""></asp:Label>
                        To : <asp:Label ID="lblEndDate" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">

                    <div class="card-box table-responsive">

                        <asp:GridView ID="gvDatewiseAttendance" runat="server"
                            CssClass="table table-striped table-bordered table-hover table-responsive table-colored table-info"
                            GridLines="Vertical"
                            AutoGenerateColumns="False"
                            EmptyDataText="No attendance records found."
                            ClientIDMode="Static"
                            OnRowDataBound="gvDatewiseAttendance_RowDataBound" CellPadding="4" ForeColor="#333333">
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                            <AlternatingRowStyle BackColor="White" />

                            <Columns>
                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                <asp:BoundField DataField="EmployeeID" HeaderText="ID" />
                                <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                <asp:BoundField DataField="InTime" HeaderText="In Time" />
                                <asp:BoundField DataField="OutTime" HeaderText="Out Time" />
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </div>

                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphScripts" runat="server">
</asp:Content>