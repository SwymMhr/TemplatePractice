<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="addEmployee.aspx.cs" Inherits="TemplatingPractice.pages.hr_management.addEmployee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content-page">

    <div class="content">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <div class="page-title-box">
                        <h4 class="page-title">HR Management </h4>
                        <ol class="breadcrumb p-0 m-0">
                            <li>
                                Dashboard
                            </li>
                            <li>
                                Employee List
                            </li>
                            <li class="active">Add Employees
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

                            <div class="form-horizontal">

                                <h3 style="color: darkblue;">1. Personal Information</h3>

                                <div class="form-group">
                                    <div class="col-md-3 pull-right">
                                        <asp:Image ID="imgPreview" runat="server" CssClass="blah" ImageUrl="~/assets/images/users/avatar-1.jpg" Style="height: 150px; width: 150px; border-style: ridge; border-width: 5px;" />
                                        <asp:FileUpload ID="fuPhoto" runat="server" onchange="readURL(this);" accept=".png,.jpg,.jpeg" Style="height:22px;" /><br />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-2">Employee ID <span style="color: red">*</span></label>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="txtEmployeeId" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvEmployeeId" runat="server" ControlToValidate="txtEmployeeId" ErrorMessage="Employee ID is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgEmployee" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-2">Full Name <span style="color: red">*</span></label>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlSalutation" runat="server" CssClass="form-control" title="Salutation List">
                                            <asp:ListItem Text="Select" Value="" Selected="True" />
                                            <asp:ListItem Text="Mr." Value="Mr." />
                                            <asp:ListItem Text="Miss." Value="Miss." />
                                            <asp:ListItem Text="Mrs." Value="Mrs." />
                                            <asp:ListItem Text="Mx." Value="Mx." />
                                            <asp:ListItem Text="Ms." Value="Ms." />
                                            <asp:ListItem Text="Dr." Value="Dr." />
                                            <asp:ListItem Text="Er." Value="Er." />
                                            <asp:ListItem Text="Prof." Value="Prof." />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" autocomplete="off" placeholder="FIRSTNAME"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First name is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgEmployee" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control"  autocomplete="off" placeholder="MIDDLENAME"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"  autocomplete="off" placeholder="LASTNAME"></asp:TextBox>
                                    </div>                                    
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-2">Gender <span style="color: red">*</span></label>
                                    <div class="col-md-3">                                        
                                        <asp:RadioButton ID="rbGenderFemale" runat="server" GroupName="Gender" Text="Female" />
                                        <asp:RadioButton ID="rbGenderMale" runat="server" GroupName="Gender" Text="Male" />
                                        <asp:RadioButton ID="rbGenderOther" runat="server" GroupName="Gender" Text="Others" />
                                    </div>

                                    <label class="control-label col-md-2">Relationship <span style="color: red">*</span></label>
                                    <div class="col-md-4">                                        
                                        <asp:RadioButton ID="rbRelSingle" runat="server" GroupName="Relationship" Text="Single" Checked="true" />
                                        <asp:RadioButton ID="rbRelMarried" runat="server" GroupName="Relationship" Text="Married" />
                                        <asp:RadioButton ID="rbRelSeparated" runat="server" GroupName="Relationship" Text="Separated" />
                                        <asp:RadioButton ID="rbRelDivorced" runat="server" GroupName="Relationship" Text="Divorced" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-2">DOB <span style="color: red">*</span></label>
                                    <div class="col-md-2">                                        
                                        <asp:TextBox ID="txtDobEnglish" runat="server" TextMode="Date" CssClass="form-control englishDate1" autocomplete="off" placeholder="English Date"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtDobNepali" runat="server" CssClass="form-control nepaliDate1" autocomplete="off" placeholder="Nepali Date (BS)"></asp:TextBox>
                                    </div>

                                    <label class="control-label col-md-2">Join Date <span style="color: red">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtJoinDateEnglish" runat="server" TextMode="Date" CssClass="form-control englishDate2" autocomplete="off" placeholder="English Date"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvJoinDate" runat="server" ControlToValidate="txtJoinDateEnglish" ErrorMessage="Join date is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgEmployee" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtJoinDateNepali" runat="server" CssClass="form-control nepaliDate2" autocomplete="off" placeholder="Nepali Date (BS)"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-2">E mail </label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtPersonalEmail" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revPersonalEmail" runat="server" ControlToValidate="txtPersonalEmail" ErrorMessage="Enter a valid email address." ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgEmployee" />
                                    </div>
                                </div>

                                <h3 style="color: darkblue">2. Official Information</h3>

                                <div class="form-group">
                                    <label class="control-label col-md-2">Branch <span style="color: red">*</span></label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" title="Branch List">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" InitialValue="" ErrorMessage="Branch is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgEmployee" />
                                    </div>

                                    <label class="control-label col-md-2">Department <span style="color: red">*</span></label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" title="Department List">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment" InitialValue="" ErrorMessage="Department is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgEmployee" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-2" for="userName">Login ID <span style="color: red">*</span></label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtLoginId" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <label class="control-label col-md-2">Password <span style="color: red">*</span></label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" autocomplete="off" Text="123"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-2">User Type <span style="color: red">*</span></label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Select" Value="" Selected="True" />
                                            <asp:ListItem Text="Admin" Value="Admin" />
                                            <asp:ListItem Text="User" Value="User" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvUserType" runat="server" ControlToValidate="ddlUserType" InitialValue="" ErrorMessage="User type is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgEmployee" />
                                    </div>

                                    <label class="control-label col-md-2">Supervisor</label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlSupervisor" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="NO HOD" Value="" Selected="True" />
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-2">Designation <span style="color: red">*</span></label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" title="Designation List">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="ddlDesignation" InitialValue="" ErrorMessage="Designation is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgEmployee" />
                                    </div>

                                    <label class="control-label col-md-2">Grade <span style="color: red">*</span></label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control" title="Grade List">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvGrade" runat="server" ControlToValidate="ddlGrade" InitialValue="" ErrorMessage="Grade is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="vgEmployee" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-2">Status <span style="color: red">*</span></label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" title="Status List">
                                            <asp:ListItem Text="Working" Value="Working" Selected="True" />
                                            <asp:ListItem Text="Inactive" Value="Inactive" />
                                        </asp:DropDownList>
                                    </div>

                                    <label class="control-label col-md-2">Type <span style="color: red">*</span></label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Permanent" Value="Permanent" />
                                            <asp:ListItem Text="Temporary" Value="Temporary" Selected="True" />
                                            <asp:ListItem Text="Contract" Value="Contract" />
                                            <asp:ListItem Text="Casual" Value="Casual" />
                                            <asp:ListItem Text="Trainee" Value="Trainee" />
                                            <asp:ListItem Text="Probation" Value="Probation" />
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-md-2">Official Email</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtOfficialEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revOfficialEmail" runat="server" ControlToValidate="txtOfficialEmail" ErrorMessage="Enter a valid email address." ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgEmployee" />
                                    </div>
                                </div>

                                <br />

                                <div class="col-md-8 col-md-offset-4">
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

    <script type="text/javascript">
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('.blah')
                        .attr('src', e.target.result)
                        .width(150)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>

</div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphScripts" runat="server">
    <script src="<%= ResolveUrl("~/assets/nepali.datepicker.v4.0.8/nepali.datepicker.v4.0.8.min.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            var dobEnglishInput = document.getElementById('<%= txtDobEnglish.ClientID %>');
            var dobNepaliInput = document.getElementById('<%= txtDobNepali.ClientID %>');
            var joinEnglishInput = document.getElementById('<%= txtJoinDateEnglish.ClientID %>');
            var joinNepaliInput = document.getElementById('<%= txtJoinDateNepali.ClientID %>');

            dobNepaliInput.nepaliDatePicker({
                ndpEnglishInput: '<%= txtDobEnglish.ClientID %>',
                dateFormat: 'YYYY-MM-DD'
            });

            joinNepaliInput.nepaliDatePicker({
                ndpEnglishInput: '<%= txtJoinDateEnglish.ClientID %>',
                dateFormat: 'YYYY-MM-DD'
            });

            function syncNepaliFromEnglish(englishInput, nepaliInput) {
                englishInput.addEventListener('change', function () {
                    if (!this.value) {
                        nepaliInput.value = '';
                        return;
                    }
                    try {
                        var bsDate = NepaliFunctions.AD2BS(this.value, 'YYYY-MM-DD', 'YYYY-MM-DD');
                        nepaliInput.value = bsDate;
                    } catch (e) {
                        console.error('AD to BS conversion failed:', e);
                    }
                });
            }

            syncNepaliFromEnglish(dobEnglishInput, dobNepaliInput);
            syncNepaliFromEnglish(joinEnglishInput, joinNepaliInput);
        };
    </script>

</asp:Content>