<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="inactiveList.aspx.cs" Inherits="TemplatingPractice.pages.customer_support.inactiveList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content-page">
            
    <div class="content">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <div class="page-title-box">
                        <h4 class="page-title">HR Management </h4>
                        <ol class="breadcrumb p-0 m-0">
                            <li class="blue">Home</li>
                            <li class="active">Employees List
                            </li>
                        </ol>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
            <!-- end row -->
            <div method="post" id="ctl00" role="form" class="form-horizontal">

                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">

                            <asp:GridView ID="gvInactiveEmployees" runat="server"
                                CssClass="table table-striped table-bordered table-hover table-responsive table-colored table-info"
                                AutoGenerateColumns="false"
                                DataKeyNames="EmployeeID"
                                GridLines="None"
                                EmptyDataText="No employees found."
                                OnRowCommand="gvInactiveEmployees_RowCommand">

                                <Columns>

                                    <asp:TemplateField HeaderText="S.N">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Employee (ID)">
                                        <ItemTemplate>
                                            <%# Eval("EmployeeName") %> (<%# Eval("EmployeeCode") %>)
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                    <asp:BoundField DataField="Grade" HeaderText="Grade" />
                                    <asp:BoundField DataField="Department" HeaderText="Department" />
                                    <asp:BoundField DataField="Branch" HeaderText="Branch" />
                                    <asp:BoundField DataField="UserType" HeaderText="User Type" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" />

                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <div class='button-list'>
                                                <asp:LinkButton ID="lnkView" runat="server" CssClass="btn btn-info waves-effect w-md waves-light" CommandName="ViewDetail" CommandArgument='<%# Eval("EmployeeID") %>'>View Details</asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            
                        </div>
                    </div>
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
