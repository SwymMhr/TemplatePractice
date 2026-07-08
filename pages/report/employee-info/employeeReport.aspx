<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="employeeReport.aspx.cs" Inherits="TemplatingPractice.pages.report.employee_info.employeeReport" %>
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
                                <li class="blue">Employee </li>
                                <li class="active">Employee Information Report
                                </li>
                            </ol>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
                <!-- end row -->

                <div class="panel panel-color panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title" style="color: red;">* Denotes Mandatory Fields</h3>
                    </div>
                    <div class="panel-body well">

                        <div class="form-horizontal">

                            <asp:UpdatePanel ID="upPnl" runat="server">
                                <ContentTemplate>

                                    <div class="form-group">
                                        <label class="control-label col-md-2">Branch <span style="color: red">*</span></label>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlBranch" runat="server" title="Branch List" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-md-2">Department <span style="color: red">*</span></label>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlDepartment" runat="server" title="Department List" CssClass="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:CheckBox ID="chkAllDept" runat="server" Text=" All" AutoPostBack="true" OnCheckedChanged="chkAllDept_CheckedChanged" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-md-2">Status <span style="color: red">*</span></label>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlStatus" runat="server" title="Status List" CssClass="form-control">
                                                <asp:ListItem Text="Select Status" Value="" Selected="True" />
                                                <asp:ListItem Text="Retired" Value="Retired" />
                                                <asp:ListItem Text="Working" Value="Working" />
                                                <asp:ListItem Text="Suspension" Value="Suspension" />
                                                <asp:ListItem Text="Discharged" Value="Discharged" />
                                                <asp:ListItem Text="Dismissed" Value="Dismissed" />
                                                <asp:ListItem Text="Resigned" Value="Resigned" />
                                                <asp:ListItem Text="Inactive" Value="Inactive" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-md-2">Type <span style="color: red">*</span></label>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlType" runat="server" title="Type List" CssClass="form-control">
                                                <asp:ListItem Text="Select Type" Value="" Selected="True" />
                                                <asp:ListItem Text="Permanent" Value="Permanent" />
                                                <asp:ListItem Text="Temporary" Value="Temporary" />
                                                <asp:ListItem Text="Contract" Value="Contract" />
                                                <asp:ListItem Text="Casual" Value="Casual" />
                                                <asp:ListItem Text="Trainee" Value="Trainee" />
                                                <asp:ListItem Text="Probation" Value="Probation" />
                                                <asp:ListItem Text="Karar" Value="Karar" />
                                                <asp:ListItem Text="Thekka Karar" Value="Thekka Karar" />
                                                <asp:ListItem Text="Daily Waiges" Value="Daily Waiges" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-md-2">Sort <span style="color: red">*</span></label>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlSort" runat="server" title="Sort List" CssClass="form-control">
                                                <asp:ListItem Text="Sort By Name" Value="Name" Selected="True" />
                                                <asp:ListItem Text="Sort By EMP ID" Value="EmpID" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />

                                    <div class="col-md-12 col-md-offset-2">
                                        <div class="button-list">
                                            <asp:Button ID="btnLoad" runat="server" Text="Load" CssClass="btn btn-success col-md-2" OnClick="btnLoad_Click" />

                                            <asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="btn btn-danger col-md-2" CausesValidation="false" OnClick="btnReset_Click" />
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvReport" runat="server"
                                                    CssClass="table table-striped table-bordered table-hover table-responsive table-colored table-info"
                                                    AutoGenerateColumns="false"
                                                    DataKeyNames="EmployeeID"
                                                    GridLines="None"
                                                    EmptyDataText="No employees found for the selected filters."
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

                                                        <asp:BoundField DataField="Gender" HeaderText="Gender" />
                                                        <asp:BoundField DataField="Relationship" HeaderText="Marital Status" />
                                                        <asp:BoundField DataField="DOBEnglish" HeaderText="DOB" DataFormatString="{0:yyyy/MM/dd}"/>
                                                        <asp:BoundField DataField="JoinDateEnglish" HeaderText="JOD" DataFormatString="{0:yyyy/MM/dd}"/>
                                                        <asp:BoundField DataField="DesignationName" HeaderText="Designation" />
                                                        <asp:BoundField DataField="GradeName" HeaderText="Grade" />
                                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />                                                        
                                                        <asp:BoundField DataField="EmployeeType" HeaderText="Type" />
                                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                                        <asp:BoundField DataField="HODName" HeaderText="HOD" />
                                                        <asp:BoundField DataField="UserType" HeaderText="User Type" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                        <!-- end form-horizontal -->
                    </div>
                </div>
            </div>
            <!-- container -->
        </div>
        <!-- content -->

    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphScripts" runat="server">
</asp:Content>
