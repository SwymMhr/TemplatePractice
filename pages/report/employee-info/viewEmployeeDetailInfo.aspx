<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="viewEmployeeDetailInfo.aspx.cs" Inherits="TemplatingPractice.pages.report.employee_info.viewEmployeeDetailInfo" %>
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
                            <li class="active">View Employee Details Information Report</li>
                        </ol>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
            <!-- end row -->

            <div class="row">
                <div class="col-lg-12 button-list">
                    <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-success btn-bordered" CausesValidation="false" OnClick="btnNew_Click" />
                </div>
            </div>

            <div class="col-md-12 well">
                <div class="col-md-6">
                    <h3 style="color: darkblue;">General Information</h3>
                    <div class="form-group">
                        <span class="col-md-4">Employee Id :-</span>
                        <div class="col-md-5">
                            <asp:Label ID="lblEmployeeId" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <span class="col-md-4">Full Name :-</span>
                        <div class="col-md-8">
                            <asp:Label ID="lblFullName" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <span class="col-md-4">Gender :-</span>
                        <div class="col-md-8">
                            <asp:Label ID="lblGender" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <span class="col-md-4">Date Of Birth :-</span>
                        <div class="col-md-8">
                            <asp:Label ID="lblDOB" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <span class="col-md-4">Join Date :-</span>
                        <div class="col-md-8">
                            <asp:Label ID="lblJoinDate" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <span class="col-md-4">Email :-</span>
                        <div class="col-md-8">
                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <h3 style="color: darkblue;">Official Information</h3>
                    <div class="form-group">
                        <span class="col-md-4">Branch :-</span>
                        <div class="col-md-8">
                            <asp:Label ID="lblBranch" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <span class="col-md-4">Department :-</span>
                        <div class="col-md-8">
                            <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <span class="col-md-4">Login Id :-</span>
                        <div class="col-md-8">
                            <asp:Label ID="lblLoginId" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <span class="col-md-4">Password :-</span>
                        <div class="col-md-8">
                            <span>******</span>
                        </div>
                    </div>
                    <div class="form-group">
                        <span class="col-md-4">Designation :-</span>
                        <div class="col-md-8">
                            <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <span class="col-md-4">Type :-</span>
                        <div class="col-md-3">
                            <asp:Label ID="lblType" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <span class="col-md-4">Status :-</span>
                        <div class="col-md-3">
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <span class="col-md-4">User Type :-</span>
                        <div class="col-md-8">
                            <asp:Label ID="lblUserType" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <span class="col-md-4">Grade :-</span>
                        <div class="col-md-8">
                            <asp:Label ID="lblGrade" runat="server"></asp:Label>
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