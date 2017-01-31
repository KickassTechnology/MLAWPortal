<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Render_Inspection_Form.aspx.cs" Inherits="MLAW_Portal.Inspection_Portal.Render_Inspection_Form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="text-align:center">
<head runat="server">
    <title></title>
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" type="text/css" href="../MLAW.css" media="screen" />
</head>
<body style="margin:0 auto;text-align:center;">
    <form id="form1" runat="server">

        <div id="form_body" style="margin:0 auto;text-align:center;margin-top:50px;">

        </div>

    </form>
        <script src="../js/jquery-1.11.0.min.js"></script>
        <script src="../MLAW.js"></script>
        <script>

             $(document).ready(function () {

                    $('#form_body').load("html/Prepour_Form.html", function () {
                    });

            });
        </script>

</body>
</html>
