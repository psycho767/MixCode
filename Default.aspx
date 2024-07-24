<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaptureImages.aspx.cs" Inherits="CaptureImages" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Capture Images</title>
    <script src="Scripts/jquery-3.6.4.min.js"></script>
    <script src="Scripts/Script.js"></script>
    <style>
        /* Add your styles here */
        video {
            width: 640px;
            height: 480px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="display:none">
            <video id="cameraFeed" autoplay ></video></div>
          
            <button onclick="captureAndSend()">Capture and Send</button>
           <asp:Label ID="lblmsg" runat="server"></asp:Label>
            <asp:TextBox ID="txt_path" runat="server" Width="" ></asp:TextBox><br />
            <asp:ListBox ID="List" runat="server" Width="400px" Height="800px" ></asp:ListBox>
        </div>
    </form>
   
</body>
</html>
