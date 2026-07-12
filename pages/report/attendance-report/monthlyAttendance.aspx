<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="monthlyAttendance.aspx.cs" Inherits="TemplatingPractice.pages.report.attendance_report.monthlyAttendance" %>
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
                            <li class="active">Monthly Attendance Report</li>
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
                            <div class="form-horizontal">

                                <div class="col-md-12">

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Start Date <span class="text-danger">* </span></label>
                                        <div class="col-md-5">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtStartDateNepali" runat="server" CssClass="form-control nepaliDate1" AutoComplete="off" placeholder="Nepali Date"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtStartDateEnglish" runat="server" CssClass="form-control englishDate1" AutoComplete="off" placeholder="English Date"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">End Date <span class="text-danger">* </span></label>
                                        <div class="col-md-5">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtEndDateNepali" runat="server" CssClass="form-control nepaliDate2" AutoComplete="off" placeholder="Nepali Date"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtEndDateEnglish" runat="server" CssClass="form-control englishDate2" AutoComplete="off" placeholder="English Date"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:UpdatePanel ID="upPnl" runat="server">
                                        <ContentTemplate>

                                            <div class="form-group">
                                                <label class="control-label col-md-2">Branch <span class="text-danger">* </span></label>
                                                <div class="col-md-5">
                                                    <asp:DropDownList ID="ddlBranch" runat="server" title="Branch List" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-md-2">Department <span class="text-danger">* </span></label>
                                                <div class="col-md-5">
                                                    <asp:DropDownList ID="ddlDepartment" runat="server" title="Department List" CssClass="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-md-2">Employee <span class="text-danger">* </span></label>
                                                <div class="col-md-5">
                                                    <asp:DropDownList ID="ddlEmployee" runat="server" title="Employee List" CssClass="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>

                                                <label class="control-label col-md-2">Employee Id</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtEmpId" runat="server" CssClass="form-control" AutoComplete="off" AutoPostBack="true" OnTextChanged="txtEmpId_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
s
                                            <div class="form-group row">
                                                <div class="col-sm-9 col-sm-offset-2">
                                                    <div class="button-list">
                                                        <asp:Button ID="btnLoad" runat="server" Text="Load" CssClass="btn btn-success btn-bordered w-md col-md-1" OnClick="btnLoad_Click" />
                                                        <asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="btn btn-danger btn-bordered w-md col-md-1" CausesValidation="false" OnClick="btnReset_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                           
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

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

    <script src="<%= ResolveUrl("~/assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/assets/nepali.datepicker.v4.0.8/nepali.datepicker.v4.0.8.min.js") %>" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            $('.englishDate1').on('changeDate change', function () {
                var a = $('.englishDate1').val();
                if (a != "") { $('.nepaliDate1').val(NepaliFunctions.AD2BS(a, 'YYYY-MM-DD', 'YYYY-MM-DD')); }
            });
            $('.englishDate1').datepicker({ format: 'yyyy-mm-dd', autoclose: true, todayHighlight: true });
            $('.nepaliDate1').nepaliDatePicker({
                ndpMonth: true, ndpYear: true, ndpYearCount: 10,
                onChange: function () { $('.englishDate1').val(NepaliFunctions.BS2AD($('.nepaliDate1').val())); }
            });

            $('.englishDate2').on('changeDate change', function () {
                var a = $('.englishDate2').val();
                if (a != "") { $('.nepaliDate2').val(NepaliFunctions.AD2BS(a, 'YYYY-MM-DD', 'YYYY-MM-DD')); }
            });
            $('.englishDate2').datepicker({ format: 'yyyy-mm-dd', autoclose: true, todayHighlight: true });
            $('.nepaliDate2').nepaliDatePicker({
                ndpMonth: true, ndpYear: true, ndpYearCount: 10,
                onChange: function () { $('.englishDate2').val(NepaliFunctions.BS2AD($('.nepaliDate2').val())); }
            });

        });
    </script>

</asp:Content>