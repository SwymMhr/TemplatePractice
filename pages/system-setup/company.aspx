<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="company.aspx.cs" Inherits="TemplatingPractice.pages.system_setup.company" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content-page">
            
    <!-- Start content -->
    <div class="content">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <div class="page-title-box">
                        <h4 class="page-title">Company </h4>
                        <ol class="breadcrumb p-0 m-0">
                            <li>
                                Avighna 
                            </li>
                            <li>
                                System Setup 
                            </li>
                            <li class="active">
                                Company 
                            </li>
                        </ol>
                        <div class="clearfix"></div>    
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
                                        <label class="col-md-2 control-label"> Company Name <span class="text-danger">* </span> </label>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" Text="Avighna Tech"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCompantName" runat="server" ControlToValidate="txtCompanyName" ErrorMessage="Company Name is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgCompany" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label"> Address <span class="text-danger">* </span> </label>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtAdress" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAdress" runat="server" ControlToValidate="txtAdress" ErrorMessage="Adress is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgCompany" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label"> Telephone <span class="text-danger">* </span> </label>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtTelephone" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTelephone" runat="server" ControlToValidate="txtTelephone" ErrorMessage="Telephone is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgCompany" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label"> Fax </label>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtFax" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label"> Email </label>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label"> Website </label>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br /><br />

                                    <div class="form-group row">
                                        <div class="col-sm-9 col-sm-offset-2">
                                            <div class="col-md-3">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success col-md-12" ValidationGroup="vgCompany" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger col-md-12" CausesValidation="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- end row -->
        </div> <!-- container -->
    </div> <!-- content -->

</div>


</asp:Content>
