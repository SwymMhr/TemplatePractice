<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="rosterAssign.aspx.cs" Inherits="TemplatingPractice.pages.system_setup.roster.rosterAssign" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content-page">
    <div class="content">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <div class="page-title-box">
                        <h4 class="page-title">System Setup</h4>
                        <ol class="breadcrumb p-0 m-0">
                            <li class="blue">Home</li>
                            <li class="blue">Roster Mgmt</li>
                            <li class="active">Roster Assign</li>
                        </ol>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
            <!-- end row -->

            <div class="form-horizontal">
                <div class="row">

                    <!-- LEFT: Branch / Department / Employee list -->
                    <div class="col-md-6">
                        <div class="well">

                            <asp:UpdatePanel ID="upnlEmployee" runat="server">
                                <ContentTemplate>

                                    <div class="form-group">
                                        <label class="control-label col-md-3">Branch <span style="color:red">*</span></label>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="CmbBranch" runat="server" CssClass="form-control"
                                                AutoPostBack="true" OnSelectedIndexChanged="CmbBranch_SelectedIndexChanged" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-md-3">Department <span style="color:red">*</span></label>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="CmbDepartment" runat="server" CssClass="form-control"
                                                AutoPostBack="true" OnSelectedIndexChanged="CmbDepartment_SelectedIndexChanged" />
                                        </div>
                                    </div>

                                    <div class="table-responsive">
                                        <asp:GridView ID="gvEmployee" runat="server"
                                            CssClass="table table-striped table-colored table-info"
                                            AutoGenerateColumns="False"
                                            DataKeyNames="EmployeeID"
                                            GridLines="None"
                                            EmptyDataText="No employees found for the selected branch/department." CellPadding="4" ForeColor="#333333">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <input type="checkbox" onclick="checkAll(this)" /> All
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkEmployee" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee" />
                                                <asp:BoundField DataField="EmployeeID" HeaderText="ID" />
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>

                    <!-- RIGHT: Dates (cosmetic only) + Shift group + weekday assignment -->
                    <div class="col-md-6">
                        <div class="well clearfix">

                            <!-- These date fields are cosmetic only, to match the template look.
                                 They are NOT saved anywhere — tblEmployeeShift has no date columns,
                                 since shift assignment behaves like Branch/Department (a standing
                                 attribute, not a dated history). They sit outside any UpdatePanel
                                 on purpose, so the datepicker plugins only ever need to bind once. -->

                            <div class="form-group">
                                <label class="control-label col-md-2">From</label>
                                <div class="col-md-5">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control englishDate1" AutoComplete="off" placeholder="English Date"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtNepaliDate" runat="server" CssClass="form-control nepaliDate1" AutoComplete="off" placeholder="Nepali Date"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-md-2">TO</label>
                                <div class="col-md-5">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control englishDate2" AutoComplete="off" placeholder="English Date"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="input-group">
                                        <asp:TextBox ID="nepaliDate2" runat="server" CssClass="form-control nepaliDate2" AutoComplete="off" placeholder="Nepali Date"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                </div>
                            </div>

                            <asp:UpdatePanel ID="upnlShiftGroup" runat="server">
                                <ContentTemplate>

                                    <div class="form-group">
                                        <label class="control-label col-md-4">Shift Group <span style="color:red">*</span></label>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="CmbDefaultSG" runat="server" CssClass="form-control"
                                                AutoPostBack="true" OnSelectedIndexChanged="CmbDefaultSG_SelectedIndexChanged" />
                                        </div>
                                    </div>

                                    <asp:Panel ID="pnlWeekDay" runat="server" Visible="false">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvWeekDay" runat="server"
                                                CssClass="table table-striped table-colored table-info"
                                                AutoGenerateColumns="False"
                                                GridLines="None"
                                                OnRowDataBound="gvWeekDay_RowDataBound" CellPadding="4" ForeColor="#333333">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="DayName" HeaderText="Week Day" />
                                                    <asp:TemplateField HeaderText="Assigned Group">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnDayName" runat="server" Value='<%# Eval("DayName") %>' />
                                                            <asp:DropDownList ID="ddlDayShift" runat="server" CssClass="form-control weekday-shift" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="col-md-12 col-md-offset-4">
                                <div class="col-md-3">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success col-md-12" ValidationGroup="vgRoster" OnClick="btnSave_Click" />
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

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphScripts" runat="server">

    <script src="<%= ResolveUrl("~/assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/assets/nepali.datepicker.v4.0.8/nepali.datepicker.v4.0.8.min.js") %>" type="text/javascript"></script>

    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    inputList[i].checked = objRef.checked;
                }
            }
        }

        // Mirrors forceAttendance.aspx exactly: these date fields are outside
        // any UpdatePanel, so a plain document-ready binding is enough — the
        // Shift Group / weekday UpdatePanel refreshing never touches this DOM.
        $(document).ready(function () {

            $('.englishDate1').change(function () {
                var a = $('.englishDate1').val();
                if (a != "") {
                    $('.nepaliDate1').val(NepaliFunctions.AD2BS(a));
                }
            });

            $('.englishDate1').datepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                todayHighlight: true
            });

            $('.nepaliDate1').nepaliDatePicker({
                npdMonth: true,
                npdYear: true,
                npdYearCount: 10,
                onChange: function () {
                    $('.englishDate1').val(NepaliFunctions.BS2AD($('.nepaliDate1').val()));
                }
            });

            $('.englishDate2').change(function () {
                var a = $('.englishDate2').val();
                if (a != "") {
                    $('.nepaliDate2').val(NepaliFunctions.AD2BS(a));
                }
            });

            $('.englishDate2').datepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                todayHighlight: true
            });

            $('.nepaliDate2').nepaliDatePicker({
                npdMonth: true,
                npdYear: true,
                npdYearCount: 10,
                onChange: function () {
                    $('.englishDate2').val(NepaliFunctions.BS2AD($('.nepaliDate2').val()));
                }
            });

        });
    </script>

</asp:Content>