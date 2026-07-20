<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="logHistoryList.aspx.cs" Inherits="TemplatingPractice.pages.logHistoryList" %>
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
                            <li class="active">Log History</li>
                        </ol>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>

            <asp:HiddenField ID="hfEmployee" runat="server" />
            <asp:HiddenField ID="hfStartDate" runat="server" />
            <asp:HiddenField ID="hfEndDate" runat="server" />

            <div class="row">
                <div class="col-lg-12">

                    <div class="card-box table-responsive">                        
                        <asp:LinkButton ID="lnkNew" runat="server" CssClass="btn btn-success w-md" OnClick="lnkNew_Click">
                            <i class="mdi mdi mdi-autorenew"></i> New 
                        </asp:LinkButton><br /> <br />  
                        <asp:GridView ID="gvLogHistory" runat="server"
                            CssClass="table table-striped table-bordered table-hover table-colored table-info"
                            AutoGenerateColumns="false"
                            GridLines="None"
                            EmptyDataText="No log records found for the selected range."
                            OnRowCommand="gvLogHistory_RowCommand"
                            ClientIDMode="Static"
                            UseAccessibleHeader="true">
                            <HeaderStyle />

                            <Columns>
                                <asp:TemplateField HeaderText="S.N">
                                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID" />
                                <asp:BoundField DataField="VerifyMode" HeaderText="Verify Mode" />
                                <asp:BoundField DataField="Mode" HeaderText="Mode" />
                                <asp:BoundField DataField="LogDate" HeaderText="Log Date" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" />
                                <asp:BoundField DataField="LogTime" HeaderText="Log Time" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="btn btn-warning btn-sm" CommandName="UpdateLog" CommandArgument='<%# Eval("AttendanceID") %>'>
                                            <i class="fa fa-pencil"></i> Update
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
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
           $('#gvLogHistory').DataTable({
               order: [],
               columnDefs: [
                   { orderable: false, targets: -1 }
               ]
           });
       });
   </script>

</asp:Content>