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
                            <li class="active">Rooster Assign
                            </li>
                        </ol>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
            <!-- end row -->

            <form method="post" action="./rosterAssign" id="ctl00" role="form" class="form-horizontal">

<script type="text/javascript">
//<![CDATA[
var theForm = document.forms['ctl00'];
if (!theForm) {
    theForm = document.ctl00;
}
function __doPostBack(eventTarget, eventArgument) {
    if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
        theForm.__EVENTTARGET.value = eventTarget;
        theForm.__EVENTARGUMENT.value = eventArgument;
        theForm.submit();
    }
}
//]]>
</script>


<script src="/WebResource.axd?d=pynGkmcFUV13He1Qd6_TZGyEG_Xf2-j4xHC8872EKnkDmFM8K9Ypu-JR0gzaZG3nf0N2arkJBZcrvWA1mvpNEw2&amp;t=638169929356345901" type="text/javascript"></script>


<script src="/ScriptResource.axd?d=NJmAwtEo3Ipnlaxl6CMhvoeIuGrPiWwOqMDjh_GvkrBKutiCPSDpyFYIDkH67BAbUieqDccZFLTOfHzsp7c59TIl9-x3I97pLItejxSFNSjY3rxMmW_ENMPU6d47NJsGuzy0cJRzvY7_zQFRr1qQUNe6HdiBvNBvpnkPwtUmG_U1&amp;t=ffffffffcd368728" type="text/javascript"></script>
<script src="/ScriptResource.axd?d=dwY9oWetJoJoVpgL6Zq8OFnmi8sqSL5ckm9_8kKMQeISv8E2uz8kmnc6omb_KqiOuSk-JlVQXyqnpK_nj-2T5hbkGt-ytK0DwW1_uAIl2nOZki_BNscoGT_LKlciyvn2gU2yKjqMCSCVl52xTNnFTp4SdyFOJ6WbYbfgOIgGMvU1&amp;t=ffffffffcd368728" type="text/javascript"></script>
<div class="aspNetHidden">

	<input type="hidden" name="__VIEWSTATEGENERATOR" id="__VIEWSTATEGENERATOR" value="3E543AF9" />
	<input type="hidden" name="__EVENTVALIDATION" id="__EVENTVALIDATION" value="5H0IanvogFVGde5KU2JfPvISIaSNj17NAHmyirvjEeVMh1M21v5VdEMR+eyYy04ntQ12BNO4RSJW+KtFxkqA+1bIlXGfU1MxoPLV6Rwl3VhSTAtL/xe6Xt/7U4QgwdOAcnCPOOCvvOSVI/lw0c08qRs2SayWPGymlt9CmA915rPQld5Y1d6U+zx2+UPdkyvJLLrkCpJMEFbNBz8Nt9mIGmEmRyP+eGssVDaXH7fThZV7RIsXE2JC846UXhTm0ey/Uh0zibrDRbVSbv31bAKpjtqLcNFd08MHxHf6AmVRuFicx9jY81uwstzp24vWedaACkkAdXWkk8Z74z2VVY0TVLzEpdWqEN2pfpFhl5jTi5J3d4mOP7CTsNKYtf1llq2byjGeoUmC3PCxDIWEDH4blS95yGxdA35F60XAFINUVynEC23WWfFUW6AU/pZF0+0zlDeq3YBRkb5KBwGNqMfK3k2O69zSOA2sMQG7XFVuiaU=" />
</div>
                <script type="text/javascript">
//<![CDATA[
Sys.WebForms.PageRequestManager._initialize('ctl00$content$scrManager', 'ctl00', ['tctl00$content$upPnl','content_upPnl','tctl00$content$UpdatePanel1','content_UpdatePanel1'], [], [], 90, 'ctl00');
//]]>
</script>

                <div class="col-md-6">
                    <div id="content_upPnl">
	
                            <div class="well">
                                <div class="form-group">
                                    <label class="control-label col-md-3">Branch <span style="color: red">*</span></label>
                                    <div class="col-md-9">
                                        <select name="ctl00$content$CmbBranch" onchange="javascript:setTimeout(&#39;__doPostBack(\&#39;ctl00$content$CmbBranch\&#39;,\&#39;\&#39;)&#39;, 0)" id="content_CmbBranch" title="Branch List" class="form-control">
		<option selected="selected" value="Select Branch">Select Branch</option>
		<option value="1">Kupondole</option>
		<option value="3">Dhangadi</option>
		<option value="4">Biratnagar</option>

	</select>
                                    </div>
                                    </div>

                                <div class="form-group">
                                    <label class="control-label col-md-3">Department <span style="color: red">*</span></label>
                                    <div class="col-md-9">
                                        <select name="ctl00$content$CmbDepartment" onchange="javascript:setTimeout(&#39;__doPostBack(\&#39;ctl00$content$CmbDepartment\&#39;,\&#39;\&#39;)&#39;, 0)" id="content_CmbDepartment" title="Department List" class="form-control">

	</select>
                                    </div>
                                </div>

                                <div class="table-responsive">
                                    <div>

	</div>
                                </div>
                            </div>

                        
</div>
                </div>
                <div class="col-md-6">
                    <div class="well clearfix">
                        <div class="form-group">
                            <label class="control-label col-md-2">From <span style="color: red">*</span></label>
                            <div class="col-md-5 ">
                                <div class="input-group">
                                    <input name="ctl00$content$txtStartDate" type="text" id="content_txtStartDate" class="form-control englishDate1" AutoComplete="off" placeholder="English Date" />
                                    <span class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="input-group">
                                    <input name="ctl00$content$txtNepaliDate" type="text" id="content_txtNepaliDate" class="form-control nepaliDate1" AutoComplete="off" placeholder="Nepali Date" />
                                    <span class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-md-2">TO <span style="color: red">*</span></label>
                            <div class="col-md-5 ">
                                <div class="input-group">
                                    <input name="ctl00$content$txtEndDate" type="text" id="content_txtEndDate" class="form-control englishDate2" AutoComplete="off" placeholder="English Date" />
                                    <span class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="input-group">
                                    <input name="ctl00$content$nepaliDate2" type="text" id="content_nepaliDate2" class="form-control nepaliDate2" AutoComplete="off" placeholder="Nepali Date" />
                                    <span class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </span>
                                </div>
                            </div>
                        </div>


                        <div id="content_UpdatePanel1">
	

                                <div class="form-group">
                                    <label class="control-label col-md-4"> Shift Group <span style="color: red">*</span></label>
                                    <div class="col-md-8">
                                        <select name="ctl00$content$CmbDefaultSG" onchange="javascript:setTimeout(&#39;__doPostBack(\&#39;ctl00$content$CmbDefaultSG\&#39;,\&#39;\&#39;)&#39;, 0)" id="content_CmbDefaultSG" class="form-control" EnableLoadOnDemand="true" EnableVirtualScrolling="true" AutoValidate="true" AllowCustomText="true">
		                                    <option selected="selected" value="Select" disabled="disabled">Select</option>
		                                    <option value="1">10AM-5PM</option>
		                                    <option value="2">10AM-3PM</option>
		                                    <option value="3">10AM-4PM</option>
		                                    <option value="4">10AM-1PM</option>

	                                    </select>
                                    </div>
                                </div>

                                

                                <div class="table-responsive">
                                    <div>

	</div>
                                </div>

                            
</div>

                                <div class="col-md-12 col-md-offset-4">
                                    <div class="col-md-3">
                                        <input type="submit" name="ctl00$content$BtnSaveRoosterMgmt" value="Save" id="content_BtnSaveRoosterMgmt" class="btn btn-success col-md-12" />
                                    </div>

                                    <div class="col-md-3">
                                        <input type="submit" name="ctl00$content$BtnCancel" value="Cancel" id="content_BtnCancel" class="btn btn-danger col-md-12" />
                                    </div>
                                </div>
                    </div>
                </div>
            </form>
        </div>
        <!-- container -->
    </div>
    <!-- content -->
    <script type="text/javascript">
        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {

                        inputList[i].checked = true;

                    }

                    else {
                        inputList[i].checked = false;
                    }

                }

            }

        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphScripts" runat="server">
</asp:Content>
