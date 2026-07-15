<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="employeeDetailInfo.aspx.cs" Inherits="TemplatingPractice.pages.report.employee_info.employeeDetailInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<style>
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
                            <li class="active">Employee Report</li>
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