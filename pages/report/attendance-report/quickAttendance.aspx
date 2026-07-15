<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="quickAttendance.aspx.cs" Inherits="TemplatingPractice.pages.report.attendance_info.quickAttendanceReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<style>
    .weekend-holiday-row {
        background-color: #28a745 !important;
        color: rgba(255,255,255,0.65);
        font-weight: bold;
    }
    .absent-row {
        background-color: #ff8a94 !important;
        color: rgba(90,90,90,0.75);
        font-weight: bold;
    }
    .employee-suggestions-list {
    position: absolute;
    top: 100%;
    left: 0;
    right: 0;
    z-index: 1000;
    max-height: 220px;
    overflow-y: auto;
    background: #fff;
    border: 1px solid #ccc;
    border-top: none;
    display: none;
    box-shadow: 0 2px 6px rgba(0,0,0,0.15);
    }
    .employee-suggestions-list .suggestion-item {
        padding: 6px 12px;
        cursor: pointer;
    }
    .employee-suggestions-list .suggestion-item:hover {
        background-color: #00BCD4;
        color: #fff;
    }
</style>

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
                            <li class="active">QuickAttendance Report</li>
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
                                                    <asp:DropDownList ID="ddlDepartment" runat="server" title="Department List" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-md-2">Employee <span class="text-danger">* </span></label>
                                                <div class="col-md-5">
                                                    <div class="employee-search-wrapper" style="position: relative;">
                                                        <asp:TextBox ID="txtEmployeeSearch" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Type employee name or ID..."></asp:TextBox>
                                                        <div id="employeeSuggestions" class="employee-suggestions-list"></div>
                                                    </div>
                                                </div>

                                                <label class="control-label col-md-2">Employee Id</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtEmpId" runat="server" CssClass="form-control" AutoComplete="off" AutoPostBack="true" OnTextChanged="txtEmpId_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>

                                            <asp:HiddenField ID="hfEmployeeId" runat="server" />

                                            <div class="form-group row">
                                                <div class="col-sm-9 col-sm-offset-2">
                                                    <div class="button-list">
                                                        <asp:Button ID="btnLoad" runat="server" Text="Load" CssClass="btn btn-success btn-bordered w-md col-md-1" OnClick="btnLoad_Click" />
                                                        <asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="btn btn-danger btn-bordered w-md col-md-1" CausesValidation="false" OnClick="btnReset_Click" />
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:Panel ID="pnlResults" runat="server" Visible="false">

                                                <div class="col-md-12 form-group">
                                                    <div style="font-weight: bold; text-align: center" class="form-group">
                                                        From : <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                                                        To : <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-md-12 form-group" style="font-weight: bold;">
                                                    <div class="col-md-12 form-group">
                                                        <div class="col-md-6">
                                                            Employee : <asp:Label ID="lblEmployee" runat="server"></asp:Label> (<asp:Label ID="lblEmployeeID" runat="server"></asp:Label>)
                                                        </div>
                                                        <div class="col-md-6">
                                                            Designation : <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12 form-group">
                                                        <div class="col-md-6">
                                                            Department : <asp:Label ID="lblDept" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            Branch : <asp:Label ID="lblBranch" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="gvReport" runat="server"
                                                                CssClass="table table-striped table-bordered table-responsive table-colored table-info"
                                                                AutoGenerateColumns="false"
                                                                GridLines="None"
                                                                EmptyDataText="No attendance records found for the selected range."
                                                                UseAccessibleHeader="true"
                                                                OnRowDataBound="gvReport_RowDataBound">
                                                                <HeaderStyle BackColor="#00BCD4" ForeColor="White" />

                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <ItemTemplate><%# Eval("DateNepali") %></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Day">
                                                                        <ItemTemplate><%# Eval("Day") %></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Roster">
                                                                        <ItemTemplate><%# Eval("Roster") %></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="In Time">
                                                                        <ItemTemplate><%# Eval("InTime") %></ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="In Diff.">
                                                                        <ItemTemplate><%# Eval("InDiff") %></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Mode">
                                                                        <ItemTemplate><%# Eval("InMode") %></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Out Time">
                                                                        <ItemTemplate><%# Eval("OutTime") %></ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Out Diff.">
                                                                        <ItemTemplate><%# Eval("OutDiff") %></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Mode">
                                                                        <ItemTemplate><%# Eval("OutMode") %></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Worked Hour">
                                                                        <ItemTemplate><%# Eval("WorkedHour") %></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Remarks">
                                                                        <ItemTemplate><%# Eval("DayRemarks") %></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>

                                                        <div class="col-md-12 form-group" style="font-weight: bold;">
                                                            <div class="col-md-4">Total Days : <asp:Label ID="lblTotalDays" runat="server"></asp:Label></div>
                                                            <div class="col-md-4">PresentDays : <asp:Label ID="lblPresentDays" runat="server"></asp:Label></div>
                                                            <div class="col-md-4">Absent : <asp:Label ID="lblAbsentDays" runat="server"></asp:Label></div>
                                                        </div>
                                                        <div class="col-md-12 form-group" style="font-weight: bold;">
                                                            <div class="col-md-4">Weekend : <asp:Label ID="lblWeekend" runat="server"></asp:Label></div>
                                                            <div class="col-md-4">Public Holiday : <asp:Label ID="lblPH" runat="server"></asp:Label></div>
                                                            <div class="col-md-4">Leave : <asp:Label ID="lblLeaveCount" runat="server"></asp:Label></div>
                                                        </div>
                                                        <div class="col-md-12 form-group" style="font-weight: bold;">
                                                            <div class="col-md-4">Worked on Weekend : <asp:Label ID="lblWOW" runat="server"></asp:Label></div>
                                                            <div class="col-md-4">Worked on Public Holiday : <asp:Label ID="lblWOPH" runat="server"></asp:Label></div>
                                                            <div class="col-md-4">LWOP : <asp:Label ID="lblLWOP" runat="server"></asp:Label></div>
                                                        </div>
                                                        <div class="col-md-12 form-group" style="font-weight: bold;">
                                                            <div class="col-md-6">Total Worked Hour : <asp:Label ID="lblWHRS" runat="server"></asp:Label></div>
                                                            <div class="col-md-6">Total Payable Days : <asp:Label ID="lblTotalPaybleDays" runat="server"></asp:Label></div>
                                                        </div>

                                                    </div>
                                                </div>

                                            </asp:Panel>

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

    <script type="text/javascript">
        $(document).ready(function () {

            var employeeTextboxSelector = '#<%= txtEmployeeSearch.ClientID %>';
        var hiddenIdSelector = '#<%= hfEmployeeId.ClientID %>';
        var branchSelector = '#<%= ddlBranch.ClientID %>';
        var departmentSelector = '#<%= ddlDepartment.ClientID %>';

        function filterEmployees(term) {
            var branchId = $(branchSelector).val();
            var departmentId = $(departmentSelector).val();
            var lowerTerm = term.toLowerCase();

            return allEmployees.filter(function (emp) {
                var matchesTerm = lowerTerm === '' ||
                    emp.name.toLowerCase().indexOf(lowerTerm) !== -1 ||
                    emp.id.toString().indexOf(lowerTerm) === 0;

                var matchesBranch = !branchId || emp.branchId.toString() === branchId;
                var matchesDept = !departmentId || emp.departmentId.toString() === departmentId;

                return matchesTerm && matchesBranch && matchesDept;
            }).slice(0, 20);
        }

        function renderSuggestions(items) {
            var $box = $('#employeeSuggestions');
            $box.empty();

            if (!items || items.length === 0) {
                $box.hide();
                return;
            }

            $.each(items, function (i, emp) {
                $('<div class="suggestion-item"></div>')
                    .text(emp.name + ' (' + emp.id + ')')
                    .data('id', emp.id)
                    .data('name', emp.name)
                    .appendTo($box);
            });

            $box.show();
        }

        $(document).on('focus keyup', employeeTextboxSelector, function (e) {
            if (e.type === 'keyup' && [13, 27, 38, 40].indexOf(e.keyCode) !== -1) return;
            renderSuggestions(filterEmployees($(this).val().trim()));
        });

        $(document).on('click', '#employeeSuggestions .suggestion-item', function () {
            var id = $(this).data('id');
            var name = $(this).data('name');

            $(employeeTextboxSelector).val(name + ' (' + id + ')');
            $(hiddenIdSelector).val(id);
            $('#employeeSuggestions').hide();

            var $empIdBox = $('#<%= txtEmpId.ClientID %>');
            $empIdBox.val(id);
            __doPostBack('<%= txtEmpId.UniqueID %>', '');
        });

        $(document).on('click', function (e) {
            if (!$(e.target).closest('.employee-search-wrapper').length) {
                $('#employeeSuggestions').hide();
            }
        });

        $(document).on('change', branchSelector + ', ' + departmentSelector, function () {
            $(employeeTextboxSelector).val('');
            $(hiddenIdSelector).val('');
        });

    });
    </script>
</asp:Content>
