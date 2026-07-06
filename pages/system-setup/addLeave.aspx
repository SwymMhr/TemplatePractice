<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="addLeave.aspx.cs" Inherits="TemplatingPractice.pages.system_setup.addLeave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content-page">
            
    <!-- Start content -->
    <div class="content">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <div class="page-title-box">
                        <h4 class="page-title"> Add Leave </h4>
                        <ol class="breadcrumb p-0 m-0">
                            <li>
                                Avighna 
                            </li>
                            <li>
                                System Setup 
                            </li>
                            <li class="active">
                                Add Leave 
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
                                        <label class="col-md-2 control-label">Leave Name <span class="text-danger">* </span> </label>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtLeaveName" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvLeaveName" runat="server" ControlToValidate="txtLeaveName" ErrorMessage="Leave Name is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgLeave" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Leave Type <span class="text-danger">*</span></label>
                                        <div class="col-md-10">
                                            <div class="radio" style="display:inline-block; margin-right:25px;">
                                                <asp:RadioButton ID="rbExpireYearly" runat="server" GroupName="LeaveType" Text="Expire Yearly" Checked="true" />
                                            </div>
                                            <div class="radio" style="display:inline-block; margin-right:25px;">
                                                <asp:RadioButton ID="rbAccumulative" runat="server" GroupName="LeaveType" Text="Accumulative" />
                                            </div>
                                            <div class="radio" style="display:inline-block;">
                                                <asp:RadioButton ID="rbServicePeriod" runat="server" GroupName="LeaveType" Text="Service Period" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Cashable <span class="text-danger">*</span></label>
                                        <div class="col-md-10">
                                            <div class="radio" style="display:inline-block; margin-right:25px;">
                                                <asp:RadioButton ID="rbCashable" runat="server" GroupName="Cashable" Text="Yes" Checked="true" />
                                            </div>
                                            <div class="radio" style="display:inline-block;">
                                                <asp:RadioButton ID="rbNotCashable" runat="server" GroupName="Cashable" Text="No" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Others</label>
                                        <div class="col-md-10">
                                            <div class="checkbox" style="display:inline-block; margin-right:40px;">
                                                <asp:CheckBox ID="chkMonthlyEarning" runat="server" Text="Monthly Earning" />
                                            </div>
                                            <div class="checkbox" style="display:inline-block;">
                                                <asp:CheckBox ID="chkExhaustAllLeaves" runat="server" Text="Must Exhaust All Leaves" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Status <span class="text-danger">*</span></label>
                                        <div class="col-md-10">
                                            <div class="radio" style="display:inline-block; margin-right:25px;">
                                                <asp:RadioButton ID="rbActive" runat="server" GroupName="Status" Text="Yes" Checked="true" />
                                            </div>
                                            <div class="radio" style="display:inline-block;">
                                                <asp:RadioButton ID="rbInactive" runat="server" GroupName="Status" Text="No" />
                                            </div>
                                        </div>
                                    </div>

                                    <br /><br />

                                    <div class="form-group row">
                                        <div class="col-sm-9 col-sm-offset-2">
                                            <div class="col-md-3">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success col-md-12" ValidationGroup="vgLeave" OnClick="btnSave_Click" />
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
