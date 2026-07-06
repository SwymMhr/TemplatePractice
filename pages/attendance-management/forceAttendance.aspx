<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="forceAttendance.aspx.cs" Inherits="TemplatingPractice.pages.attendance_management.forceAttendance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content-page">

    <div class="content">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <div class="page-title-box">
                        <ol class="breadcrumb p-0 m-0">
                            <li class="blue">Home</li>
                            <li class="blue">Attendance Management</li>
                            <li class="active">Force Attendance</li>
                        </ol>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
            <!-- end row -->

            <div class="panel-heading">
                <h3 class="panel-title" style="color: red;">* Denotes Mandatory Fields</h3>
            </div>

            <div class="form-horizontal">

                <div class="well">

                    <asp:UpdatePanel ID="upnlEmployee" runat="server">
                         <ContentTemplate>

                            <div class="form-group">
                                <label class="control-label col-md-6">Employee ID <span style="color: red">*</span></label>
                                <div class="col-md-1">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtId" runat="server" CssClass="form-control" AutoComplete="off" AutoPostBack="true" OnTextChanged="txtId_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <span class="control-label col-md-2">Employee Name</span>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtEmp" runat="server" CssClass="form-control" ReadOnly="true" AutoComplete="off"></asp:TextBox>
                                </div>
                                <span class="control-label col-md-2">Designation</span>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" ReadOnly="true" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <span class="control-label col-md-2">Department</span>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtDept" runat="server" CssClass="form-control" ReadOnly="true" AutoComplete="off"></asp:TextBox>
                                </div>
                                <span class="control-label col-md-2">Branch</span>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control" ReadOnly="true" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="form-group hidden">
                        <label class="control-label col-md-2">Shift <span style="color: red">*</span></label>
                        <div class="col-md-5">
                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control">
                                <asp:ListItem Text="First" Value="1" />
                                <asp:ListItem Text="Second" Value="2" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label col-md-2">Start Date <span style="color: red">*</span></label>

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
                        <label class="control-label col-md-2">Till Date <span style="color: red">*</span></label>

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

                </div>
                <!-- end well -->

                <asp:HiddenField ID="hfEmployeeId" runat="server" />
                <asp:HiddenField ID="hfDateRangeJson" runat="server" />

                <asp:UpdatePanel ID="upnlAttendance" runat="server">
                    <ContentTemplate>

                        <div class="form-group">
                            <span id="content_Label1" class="control-label col-md-2"> Attendance Type <span style="color: red">*</span> </span>
                            <div class="col-md-3">
                                <asp:RadioButton ID="rbSignIn" runat="server" GroupName="AttendanceType" Text="Sign In" />
                                <asp:RadioButton ID="rbSignOut" runat="server" GroupName="AttendanceType" Text="Sign Out" />
                                <asp:RadioButton ID="rbBoth" runat="server" GroupName="AttendanceType" Text="Both" Checked="true" />
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-2 col-md-offset-10">
                                <asp:Button ID="btnAdd" runat="server" Text="+" CssClass="btn btn-info" OnClick="btnAdd_Click" OnClientClick="return prepareAttendanceRange();" />
                            </div>
                        </div>

                        <asp:Panel ID="pnlAttendanceList" runat="server" Visible="false">

                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvAttendance" runat="server"
                                                CssClass="table table-striped table-colored table-info"
                                                AutoGenerateColumns="false"
                                                GridLines="None"
                                                EmptyDataText="No attendance entries added yet."
                                                OnRowCommand="gvAttendance_RowCommand"
                                                UseAccessibleHeader="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.N">
                                                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee" />
                                                    <asp:BoundField DataField="AttendanceDateEnglish" HeaderText="Date (AD)" DataFormatString="{0:yyyy-MM-dd}" />
                                                    <asp:BoundField DataField="AttendanceDateNepali" HeaderText="Date (BS)" />
                                                    <asp:BoundField DataField="AttendanceType" HeaderText="Type" />
                                                    <asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkRemove" runat="server" CssClass="btn btn-danger btn-xs" CommandName="removeRow" CommandArgument="<%# Container.DataItemIndex %>">Remove</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-8 col-md-offset-4">
                                <div class="col-md-3">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success col-md-12" OnClick="btnSave_Click" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger col-md-12" CausesValidation="false" OnClick="btnCancel_Click" />
                                </div>
                            </div>

                        </asp:Panel>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <!-- end form-horizontal -->
        </div>
        <!-- container -->
    </div>
    <!-- content -->
</div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphScripts" runat="server">

    <script src="<%= ResolveUrl("~/assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/assets/nepali.datepicker.v4.0.8/nepali.datepicker.v4.0.8.min.js") %>" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            // Start Date pair
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

            // Till Date pair
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

    <script type="text/javascript">
        function prepareAttendanceRange() {
            var start = $('.englishDate1').val();
            var end = $('.englishDate2').val();

            if (!start || !end) {
                alert('Please select both Start Date and Till Date.');
                return false;
            }

            var startDate = moment(start, 'YYYY-MM-DD');
            var endDate = moment(end, 'YYYY-MM-DD');

            if (!startDate.isValid() || !endDate.isValid()) {
                alert('Please enter valid dates.');
                return false;
            }
            if (endDate.isBefore(startDate)) {
                alert('Till Date must be on or after Start Date.');
                return false;
            }

            var dates = [];
            var current = startDate.clone();
            while (current.isSameOrBefore(endDate)) {
                var adStr = current.format('YYYY-MM-DD');
                var bsStr = NepaliFunctions.AD2BS(adStr, 'YYYY-MM-DD', 'YYYY-MM-DD');
                dates.push({ ad: adStr, bs: bsStr });
                current.add(1, 'days');
            }

            document.getElementById('<%= hfDateRangeJson.ClientID %>').value = JSON.stringify(dates);
            return true;
        }
    </script>

</asp:Content>