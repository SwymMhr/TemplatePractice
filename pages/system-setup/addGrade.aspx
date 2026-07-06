<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="addGrade.aspx.cs" Inherits="TemplatingPractice.pages.system_setup.addGrade" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content-page">
<!-- Start content -->
    <div class="content">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <div class="page-title-box">
                        <h4 class="page-title"> Add Grade </h4>
                        <ol class="breadcrumb p-0 m-0">
                            <li>
                                Avighna
                            </li>
                            <li>
                                System Setup
                            </li>
                            <li class="active">
                                Add Grade
                            </li>
                        </ol>
                        <div class="clearfix"> </div>    
                    </div>
                </div>
            </div>
            <!-- end row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-color panel-info">

                        <div class="panel-heading">
                            <h3 class="panel-title"><span class="text-danger">* </span>Denotes Mandatory Fields </h3>
                        </div>

                        <div class="panel-body">
                            <h4 class="m-t-0 header-title"></h4>
                            <p class="text-muted m-b-30 font-13"></p>
                            <div class="form-horizontal">

                                <div class="col-md-12">                                    

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Grade Name <span class="text-danger">* </span> </label>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtGradeName" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvGradeName" runat="server" ControlToValidate="txtGradeName" ErrorMessage="Grade Name is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgGrade" />
                                        </div>
                                    </div>

                                   <div class="form-group">
                                        <label class="col-md-2 control-label">Grade Code <span class="text-danger">* </span> </label>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtGradeType" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvGradeType" runat="server" ControlToValidate="txtGradeType" ErrorMessage="Grade Type is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgGrade" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Status <span class="text-danger">*</span></label>
                                        <div class="col-md-10">
                                            <div class="radio col-md-1">
                                                <asp:RadioButton ID="rbActive" runat="server" GroupName="Status" Text="Yes" Checked="true" />
                                            </div>
                                            <div class="radio col-md-1">
                                                <asp:RadioButton ID="rbInactive" runat="server" GroupName="Status" Text="No" />
                                            </div>
                                        </div>
                                    </div>

                                    <br /><br />

                                    <div class="form-group row">
                                        <div class="col-sm-9 col-sm-offset-2">
                                            <div class="col-md-3">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success col-md-12" ValidationGroup="vgEmployee" OnClick="btnSave_Click" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger col-md-12" CausesValidation="false" OnClick="btnCancel_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
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
