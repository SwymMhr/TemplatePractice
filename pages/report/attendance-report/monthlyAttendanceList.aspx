<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="monthlyAttendanceList.aspx.cs" Inherits="TemplatingPractice.pages.report.attendance_report.monthlyAttendanceList" %>
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
                            <li class="blue">Monthly Attendance Report</li>
                            <li class="active">View Monthly Attendance Report</li>
                        </ol>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
            <!-- end row -->

            <div class="row">
                <div class="col-lg-12">

                    <div class="row">
                        <div class="col-md-4 button-list pull-left">
                            <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary btn-bordered" OnClick="btnNew_Click" />
                            <asp:Button ID="btnExport" runat="server" Text="Excel" CssClass="btn btn-success btn-bordered" OnClick="btnExport_Click" />
                        </div>
                        <div class="col-md-8">
                            <div style="font-weight: bold; text-align: center">
                                From : <asp:Label ID="lblStartDate" runat="server"></asp:Label>  
                                To : <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="font-weight: bold; margin-top: 15px; margin-bottom: 15px;">
                        <div class="col-md-4">
                            Branch : <asp:Label ID="lblBranch" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-8" style="text-align: center">
                            Department : <asp:Label ID="lblDept" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvReport" runat="server"
                                    CssClass="table table-striped table-bordered table-responsive table-colored"
                                    AutoGenerateColumns="false"
                                    GridLines="None"
                                    EmptyDataText="No data found for the selected Employee/Date range."
                                    UseAccessibleHeader="true"
                                    OnRowCreated="gvReport_RowCreated">
                                    <HeaderStyle BackColor="#00BCD4" ForeColor="White" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Employee (ID)">
                                            <ItemTemplate><%# Eval("EmployeeDisplay") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TotalDays">
                                            <ItemTemplate><%# Eval("TotalDays") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Weekend">
                                            <ItemTemplate><%# Eval("Weekend") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PH">
                                            <ItemTemplate><%# Eval("PH") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WorkingDay">
                                            <ItemTemplate><%# Eval("WorkingDay") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Absent Days">
                                            <ItemTemplate><%# Eval("AbsentDays") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kriya">
                                            <ItemTemplate><%# Eval("Kriya") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Maternity">
                                            <ItemTemplate><%# Eval("Maternity") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paternity">
                                            <ItemTemplate><%# Eval("Paternity") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Isolation">
                                            <ItemTemplate><%# Eval("Isolation") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Travel">
                                            <ItemTemplate><%# Eval("Travel") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Present Days">
                                            <ItemTemplate><%# Eval("PresentDays") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Worked On Weekend">
                                            <ItemTemplate><%# Eval("WorkedOnWeekend") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Worked On PH">
                                            <ItemTemplate><%# Eval("WorkedOnPH") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Present Days">
                                            <ItemTemplate><%# Eval("TotalPresentDays") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Worked Hours">
                                            <ItemTemplate><%# Eval("WorkedHours") %></ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 15px;">
                        <div class="col-md-12 text-muted">
                            <p>Total Days - Weekends - Public Holiday = Working Days</p>
                            <p>Working Days - Absent Days - Total Leave = Present Days</p>
                            <p>Present Days + Worked on Weekends + Worked on Public Holidays = Total Payable Days</p>
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