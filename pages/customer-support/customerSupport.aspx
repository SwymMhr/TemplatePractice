<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="customerSupport.aspx.cs" Inherits="TemplatingPractice.pages.customer_support.customerSupport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content-page">
            
    <div class="content">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <div class="page-title-box">
                        <h4 class="page-title">Customer Support</h4>
                        <ol class="breadcrumb p-0 m-0">
                            <li class="active">Customer Support</li>
                        </ol>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
            <!-- end row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="card-box table-responsive">
                        <div method="post" action="./customersupport" id="ctl00">

                            <div class="form-group">
                                <a href="customersupportmanage" class="btn btn-success waves-effect w-md waves-light">
                                    <i class="mdi mdi-plus"></i> Add Entry 
                                </a>
                            </div>
                            <table id="datatable" class="table table-striped  table-colored table-info">
                                <thead>
                                    <tr>
                                        <th>S. No. </th>
                                        <th>Date</th>
                                        <th>Employee</th>
                                        <th>Client Name</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>1</td>
                                        <td>2080-04-10</td>
                                        <td>Mr. Prem Bahadur KC [ 4 ] </td>
                                        <td>TUTH software closing </td>
                                        <td><div class='button-list'><a href='customersupportmanage?b80bb7740288fda1f201890375a60c8f=vBCzF4RFEvVpC637Q@EiPA==' class='btn btn-primary w-xs waves-effect waves-light'><i class='mdi mdi-pencil'></i></a></div></td>
                                    </tr>
                                    <tr>
                                        <td>2</td>
                                        <td>2080-04-23</td>
                                        <td>Mr. Prem Bahadur KC [ 4 ] </td>
                                        <td>BP hospital </td>
                                        <td><div class='button-list'><a href='customersupportmanage?b80bb7740288fda1f201890375a60c8f=TT@DyJYT/0wCgcyXgP@ZTA==' class='btn btn-primary w-xs waves-effect waves-light'><i class='mdi mdi-pencil'></i></a></div></td>
                                    </tr>                            
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!-- end row -->
        </div> <!-- container -->
    </div> <!-- content -->    

</div>

</asp:Content>
