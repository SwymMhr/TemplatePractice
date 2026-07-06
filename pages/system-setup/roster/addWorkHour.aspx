<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="addWorkHour.aspx.cs" Inherits="TemplatingPractice.pages.system_setup.roster.addWorkHour" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content-page">

    <div class="content">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <div class="page-title-box">
                        <h4 class="page-title">Add WorkHour </h4>
                        <ol class="breadcrumb p-0 m-0">
                            <li>Avighna</li>
                            <li>System Setup</li>
                            <li>Roster</li>
                            <li class="active">Add WorkHour</li>
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
                                        <label class="col-md-2 control-label">Group Name <span class="text-danger">* </span></label>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtGroupName" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvGroupName" runat="server" ControlToValidate="txtGroupName" ErrorMessage="Group Name is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgWorkHour" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">In Time <span class="text-danger">* </span></label>
                                        <div class="col-md-5">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtInTime" runat="server" CssClass="form-control timePicker12hr" AutoComplete="off"></asp:TextBox>
                                                <span class="input-group-addon"><i class="mdi mdi-clock"></i></span>
                                            </div>
                                            <asp:RequiredFieldValidator ID="rfvInTime" runat="server" ControlToValidate="txtInTime" ErrorMessage="In Time is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgWorkHour" />
                                        </div>
                                        <div class="col-md-5">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtInTimeLate" runat="server" CssClass="form-control timePicker12hr" AutoComplete="off"></asp:TextBox>
                                                <span class="input-group-addon"><i class="mdi mdi-clock"></i></span>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Out Time <span class="text-danger">* </span></label>
                                        <div class="col-md-5">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtOutTime" runat="server" CssClass="form-control timePicker12hr" AutoComplete="off"></asp:TextBox>
                                                <span class="input-group-addon"><i class="mdi mdi-clock"></i></span>
                                            </div>
                                            <asp:RequiredFieldValidator ID="rfvOutTime" runat="server" ControlToValidate="txtOutTime" ErrorMessage="Out Time is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgWorkHour" />
                                        </div>
                                        <div class="col-md-5">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtOutTimeLate" runat="server" CssClass="form-control timePicker12hr" AutoComplete="off"></asp:TextBox>
                                                <span class="input-group-addon"><i class="mdi mdi-clock"></i></span>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Hour <span class="text-danger">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtHour" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <label class="col-md-2 control-label">Minute <span class="text-danger">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtMinute" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <label class="col-md-2 control-label">Lunch Time <span class="text-danger">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtLunchTime" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvLunchTime" runat="server" ControlToValidate="txtLunchTime" ErrorMessage="Lunch Time is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgWorkHour" />
                                            <asp:RegularExpressionValidator ID="revLunchTime" runat="server" ControlToValidate="txtLunchTime" ErrorMessage="Numbers only." ValidationExpression="^\d+$" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgWorkHour" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Night Shift <span class="text-danger">* </span></label>
                                        <div class="col-md-10">
                                            <div class="radio col-md-1">
                                                <asp:RadioButton ID="rbNightShiftYes" runat="server" GroupName="NightShift" Text="Yes" />
                                            </div>
                                            <div class="radio col-md-1">
                                                <asp:RadioButton ID="rbNightShiftNo" runat="server" GroupName="NightShift" Text="No" Checked="true" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Default for all weekend <span class="text-danger">* </span></label>
                                        <div class="col-md-10">
                                            <div class="radio col-md-1">
                                                <asp:RadioButton ID="rbWeekendYes" runat="server" GroupName="Weekend" Text="Yes" />
                                            </div>
                                            <div class="radio col-md-1">
                                                <asp:RadioButton ID="rbWeekendNo" runat="server" GroupName="Weekend" Text="No" Checked="true" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Status <span class="text-danger">*</span></label>
                                        <div class="col-md-10">
                                            <div class="radio col-md-1">
                                                <asp:RadioButton ID="rbStatusYes" runat="server" GroupName="Status" Text="Yes" Checked="true" />
                                            </div>
                                            <div class="radio col-md-1">
                                                <asp:RadioButton ID="rbStatusNo" runat="server" GroupName="Status" Text="No" />
                                            </div>
                                        </div>
                                    </div>

                                    <br /><br />

                                    <div class="form-group row">
                                        <div class="col-sm-9 col-sm-offset-2">
                                            <div class="col-md-3">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success col-md-12" ValidationGroup="vgWorkHour" OnClick="btnSave_Click" />
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

    <script src="<%= ResolveUrl("~/assets/plugins/timepicker/bootstrap-timepicker.js") %>"></script>
    <script src="<%= ResolveUrl("~/assets/plugins/moment/moment.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            $('.timePicker12hr').timepicker({
                showMeridian: true,
                minuteStep: 5,
                defaultTime: false
            });

            function recalcHours() {
                var inTime = $('#<%= txtInTime.ClientID %>').val();
                var outTime = $('#<%= txtOutTime.ClientID %>').val();
                var lunch = parseInt($('#<%= txtLunchTime.ClientID %>').val(), 10) || 0;

                if (!inTime || !outTime) return;

                var start = moment(inTime, 'h:mm A');
                var end = moment(outTime, 'h:mm A');

                if (!start.isValid() || !end.isValid()) return;

                var diffMinutes = end.diff(start, 'minutes') - lunch;
                if (diffMinutes < 0) diffMinutes += 24 * 60; // handle overnight/night-shift spans

                var hours = Math.floor(diffMinutes / 60);
                var minutes = diffMinutes % 60;

                $('#<%= txtHour.ClientID %>').val(hours);
                $('#<%= txtMinute.ClientID %>').val(minutes);
            }

            $('.timePicker12hr').on('changeTime.timepicker', recalcHours);
            $('#<%= txtLunchTime.ClientID %>').on('change keyup', recalcHours);

        });
    </script>

</asp:Content>
