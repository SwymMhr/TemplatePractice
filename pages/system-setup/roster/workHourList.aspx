<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="workHourList.aspx.cs" Inherits="TemplatingPractice.pages.system_setup.roster.workHourList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content-page">

    <div class="content">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <div class="page-title-box">
                        <h4 class="page-title">Work Hour List </h4>
                        <ol class="breadcrumb p-0 m-0">
                            <li>Avighna</li>
                            <li>System Setup</li>
                            <li>Roster</li>
                            <li class="active">Work Hour List</li>
                        </ol>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
            <!-- end row -->

            <div class="form-horizontal">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card-box table-responsive">

                            <div class="button-list">
                                <asp:Button ID="btnNew" runat="server" Text="+ Add New Work Hour" CssClass="btn btn-success w-sm m-b-0" OnClick="btnNew_Click" />
                            </div>
                            <div class="clearfix" style="margin-bottom: 15px;"></div>

                            <asp:GridView ID="gvWorkHour" runat="server"
                                CssClass="table table-striped table-colored table-info"
                                AutoGenerateColumns="false"
                                DataKeyNames="WorkHourID"
                                GridLines="None"
                                EmptyDataText="No Shifts found."
                                OnRowCommand="gvWorkHour_RowCommand"
                                OnRowDataBound="gvWorkHour_RowDataBound"
                                ClientIDMode="Static"
                                UseAccessibleHeader="true">
                                <HeaderStyle />

                                <Columns>
                                    <asp:TemplateField HeaderText="S.N">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="ShiftName" HeaderText="Shift Name" />
                                    <asp:BoundField DataField="StartTime" HeaderText="Start Time" />
                                    <asp:BoundField DataField="LateInBy" HeaderText="Late In By" />
                                    <asp:BoundField DataField="TotalHour" HeaderText="Total Hour" />
                                    <asp:BoundField DataField="LunchTime" HeaderText="Lunch Time" />
                                    <asp:BoundField DataField="EndTime" HeaderText="End Time" />
                                    <asp:BoundField DataField="LateOutBy" HeaderText="Late Out By" />
                                    <asp:BoundField DataField="Shift" HeaderText="Shift" />

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <div class='button-list'>
                                                <asp:LinkButton ID="lnkStatus" runat="server"
                                                    CssClass="btn btn-success w-xs waves-effect waves-light btn-xs"
                                                    CommandName="changeStatus"
                                                    CommandArgument='<%# Eval("WorkHourID") %>'
                                                    OnClientClick="return confirm('Change status of this work hour?');">
                                                    <i class='mdi mdi-pencil'></i> Active
                                                </asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <div class='button-list'>
                                                <asp:LinkButton ID="lnkDelete" runat="server"
                                                    CssClass="btn btn-warning w-xs waves-effect waves-light btn-xs"
                                                    CommandName="deleteShift"
                                                    CommandArgument='<%# Eval("WorkHourID") %>'
                                                    OnClientClick="return confirm('Delete this work hour permanently?');">
                                                    <i class='mdi mdi-delete'></i> Delete
                                                </asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
            <!-- end row -->
        </div>
    </div>

</div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphScripts" runat="server">

     <!-- DataTables core -->
     <script src="<%= ResolveUrl("~/assets/plugins/datatables/jquery.dataTables.min.js") %>"></script>
     <script src="<%= ResolveUrl("~/assets/plugins/datatables/dataTables.bootstrap.js") %>"></script>

     <!-- DataTables Buttons extension (export/print) -->
     <script src="<%= ResolveUrl("~/assets/plugins/datatables/dataTables.buttons.min.js") %>"></script>
     <script src="<%= ResolveUrl("~/assets/plugins/datatables/buttons.bootstrap.min.js") %>"></script>
     <script src="<%= ResolveUrl("~/assets/plugins/datatables/jszip.min.js") %>"></script>
     <script src="<%= ResolveUrl("~/assets/plugins/datatables/pdfmake.min.js") %>"></script>
     <script src="<%= ResolveUrl("~/assets/plugins/datatables/vfs_fonts.js") %>"></script>
     <script src="<%= ResolveUrl("~/assets/plugins/datatables/buttons.html5.min.js") %>"></script>
     <script src="<%= ResolveUrl("~/assets/plugins/datatables/buttons.print.min.js") %>"></script>

     <script>
         $(document).ready(function () {
             $('#gvWorkHour').DataTable({
                 order: [],
                 columnDefs: [
                     { orderable: false, targets: -1 } 
                 ]
             });
         });
     </script>

</asp:Content>