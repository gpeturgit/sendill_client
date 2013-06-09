<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ledgerlist.aspx.cs" Inherits="sendil_mobile_web.ledgerlist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dagbókarlisti</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
	<link rel="stylesheet" href="jqm/jquery.mobile-1.1.1.css" />
	<script src="jq/jquery-1.7.1.js" type="text/javascript" ></script>
	<script src="jqm/jquery.mobile-1.1.1.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div data-role="page">

	<div data-role="header">
		<h1>My Title</h1>
	</div><!-- /header -->

	<div data-role="content">	
<%--		<ul data-role="listview" data-inset="true" data-filter="true">
	        <li><a href="#">Acura</a></li>
	        <li><a href="#">Audi</a></li>
	        <li><a href="#">BMW</a></li>
	        <li><a href="#">Cadillac</a></li>
	        <li><a href="#">Ferrari</a></li>
        </ul>--%>
        <asp:Repeater ID="ledger_list" runat=server>
        <HeaderTemplate>
            <ul data-role="listview" data-inset="true" data-filter="true">
        </HeaderTemplate>
        <ItemTemplate>
            <li> 
                <a href="#"> 
                <%# Eval("name")%></a> 
            </li>
        </ItemTemplate>
        </asp:Repeater>		
	</div><!-- /content -->

</div><!-- /page -->
    </form>
</body>
</html>
