<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="leaveList.aspx.cs" Inherits="TemplatingPractice.pages.system_setup.leaveList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content-page">
            
    <!-- Start content -->
    <div class="content">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <div class="page-title-box">
                        <h4 class="page-title">Leave List </h4>
                        <ol class="breadcrumb p-0 m-0">
                            <li>
                                Avighna 
                            </li>
                            <li>
                                System Setup 
                            </li>
                            <li class="active">
                                Leave List 
                            </li>
                        </ol>
                        <div class="clearfix"> </div>    
                    </div>
                </div>
            </div>
            <!-- end row -->
            <div class="form-horizontal">

                <div class="row">
                    <div class="col-lg-12">

                        <div class="card-box table-responsive">

                            <div class="button-list">
                                <asp:Button ID="btnNew" runat="server" Text="+ Add New" CssClass="btn btn-success w-md m-b-0" OnClick="btnNew_Click" />
                            </div>
                            <div class="clearfix" style="margin-bottom: 15px;"></div>

                            <asp:GridView ID="gvLeave" runat="server"
                                CssClass="table table-striped table-colored table-info"
                                AutoGenerateColumns="false"
                                DataKeyNames="LeaveID"
                                GridLines="None"
                                EmptyDataText="No Leave found."
                                OnRowCommand="gvLeave_RowCommand"
                                ClientIDMode="Static"
                                UseAccessibleHeader="true">
                                <HeaderStyle />

                                <Columns>
                                     <asp:TemplateField HeaderText="S.N">
                                         <ItemTemplate>
                                             <%# Container.DataItemIndex + 1 %>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     
                                     <asp:BoundField DataField="LeaveName" HeaderText="Leave Name" />
                                     <asp:BoundField DataField="LeaveType" HeaderText="Leave Type" />
                                     <asp:TemplateField HeaderText="Cashable">
                                        <ItemTemplate>
                                            <%# GetCashableText(Eval("Cashable")) %>
                                        </ItemTemplate>
                                     </asp:TemplateField>
                                     
                                     <asp:TemplateField HeaderText="Others">
                                        <ItemTemplate>
                                            <%# GetOthersText(Eval("MonthlyEarning"), Eval("ExhaustAllLeaves")) %>
                                        </ItemTemplate>
                                     </asp:TemplateField>

                                     <asp:BoundField DataField="Status" HeaderText="Status" />

                                     <asp:TemplateField HeaderText="Actions">
                                         <ItemTemplate>
                                             <div class='button-list'>
                                                 <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-warning w-xs waves-effect waves-light btn-xs" CommandName="editLeave" CommandArgument='<%# Eval("LeaveID") %>'><i class='mdi mdi-pencil'></i> Edit </asp:LinkButton>
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
        </div> <!-- container -->
    </div> <!-- content -->

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
            $('#gvLeave').DataTable({
                order: [],
                columnDefs: [
                    { orderable: false, targets: -1 } 
                ]
            });
        });
    </script>

</asp:Content>