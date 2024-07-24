<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServerAccess.aspx.cs" Inherits="ServerAccess" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>~ Server Access ~</title>
    <style>
    body{  background-color:skyblue }
    .container{width:100%}
    .row{width:100%;margin:5px;padding:5px;display:inline-flex}
    .row:after,.row:before{content:" ";display:table}
    .col-md-3{width:20%;margin:5px;padding:5px;position:relative}
    .col-md-6{width:75%;margin:5px;padding:5px;position:relative}
     .box
        {
            height: 700px; border:1px solid black; width: 300px;box-shadow:0 0 20px navy;border-top-left-radius:20px;
            border:1px solid black;  background-color:aliceblue
        }
        .folder-panel{ width:100%; height:700px; border:1px solid black; box-shadow:0 0 20px navy; border-top-right-radius:20px;background-color:aliceblue }
        .button-div, .navigationbar{ width:97%; 
margin-top:3px
        }
        .folder-grid{ width:99%; height:86%; margin:5px ; overflow-y:scroll}
        .folder-box{ padding:5px;margin:5px;width:8%; height:100px ; position:relative;float:left}
        .folder-name{ text-align:center;  }
        .box1
        {
            height: 276px; border:1px solid black; width: 428px;box-shadow:0 0 20px navy;border-radius:20px; 
          border:1px solid black;  background-color:aliceblue; position:absolute
        }
        
        
        .style1
        {
            height: 85px;
padding-left:10px
        }
        tr{margin:0px;padding:0px}
    </style>

     <style>
        #popupBox {
        position:fixed; left:50%;top:50%; transform:translate(-50%,-50%); padding :5px
        }
        #popup 
        {
            box-shadow:0 0 20px navy;border-radius:20px; 
          border:1px solid black;  background-color:aliceblue; 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate> 
         
          </ContentTemplate>
    </asp:UpdatePanel>  
    <div class="container">
    <div class="row">
   
      <div class="col-md-3">
       <div class="box">
       <div style="background-color:navy ;border-top-left-radius:20px ; width:300px">
        <center style="color:#fff; font-size:20px; ">
           
               
                <asp:Label ID="lbl" runat="server" Text="Server File Folder" Height="30px" 
                    Width="150px"></asp:Label>
                
          
        </center>
       </div> 
      
      <div>
      <asp:HiddenField ID="prev_link" runat="server" />
      <asp:ImageButton ID="btn_pre"  runat="server" 
              ImageUrl="images/pre_rec.jpg" Width="15px" onclick="btn_pre_Click" />
      <asp:TextBox id="txt_sear" runat="server" Width="249px"  OnTextChanged="txt_sear_TextChange" AutoPostBack="true"
              style="color:grey;font-size:11px"></asp:TextBox>
          <asp:ImageButton ID="btn_sear" runat="server" 
              ImageUrl="images/next_rec.jpg" Width="15px" onclick="btn_sear_Click" />
      </div>
      
      <asp:Panel ID="pnl" runat="server">

      <table  style="overflow:scroll; height:645px; display:block">
      
      <asp:Repeater ID="Repeater1"   runat="server">
      <ItemTemplate>
      <tr style="border:1px solid black; padding-left:00px">
      
      <td style="padding-left:10px"> <img src="images/Folder.PNG" width="5%" style="margin-top:px" />
      <asp:LinkButton ID="lbl" runat="server" Font-Size="13px" style="font-weight:; text-decoration:none; color:black;" onClick="Dir_Click" CommandArgument='<%#Eval("ServerDir") %>' Text= '<%#Eval("ServerDir") %>'></asp:LinkButton>
      </td>
      <tr></tr>
      </ItemTemplate>
      </asp:Repeater>
      <asp:Repeater ID="Repeater2" runat="server">
      <ItemTemplate>
      <tr style="border:1px solid black; padding-left:00px">
      
      <td style="padding-left:10px"> 
      <img  src='<%#Eval("image_url")%>' style="margin-top:0px; width:5%" />
      <%--<asp:Image ID="Image2" runat="server" style="margin-top:0px; width:8%" />--%>
      <asp:Label ID="lbl" runat="server" Font-Size="13px" style="font-weight:" Text= '<%#Eval("ServerDir") %>'></asp:Label>
        
      </td>
      <td style="text-align:right">
      <asp:ImageButton ID="lnl_files"  ImageUrl="images/transfer_data.jpg" runat="server" OnClick="lnl_files_Click"  style="font-weight:; width:20px; text-decoration:none; color:black;"  CommandArgument='<%#Eval("ServerDir") %>' Text= '<%#Eval("ServerDir") %>'></asp:ImageButton>
      </td>
      <tr></tr>
      </ItemTemplate>
      </asp:Repeater>
      </table>

      </asp:Panel>
      </div>
      </div>

      <div class="col-md-6">
      <div class="folder-panel">
       <div style="background-color:navy ;border-top-right-radius:20px ; width:100%">
        <center style="color:#fff; font-size:20px; ">
           
               
                <asp:Label ID="Label2" runat="server" Text="Server File Folder" Height="30px" 
                    Width="550px"></asp:Label>
                
          
        </center>
         </div> 
        <div class="button-div">
         <center>          
            <asp:Button ID="Button1" align="center" runat="server" style="color:#fff; background-color:Green; border-radius:25px"  
             Text="Upload" Width="76px" OnClick="Upload_Click" Height="30px"  />
          
            &nbsp&nbsp&nbsp&nbsp   <asp:Button ID="Button2" Visible="false" align="center" runat="server" style="color:#fff; background-color:Green; border-radius:25px"  
             Text="Log Report" Width="76px" Height="30px" onClick="Test_Click" />
              &nbsp&nbsp&nbsp&nbsp   <asp:Button ID="Button3" align="center" runat="server" style="color:#fff; background-color:Green; border-radius:25px"  
             Text="Create Directory" Width="110px" Height="30px" OnClick="Create_Dir_Click"  />

              
              &nbsp&nbsp&nbsp&nbsp 
             <a href="LogReport.aspx" style="color:#fff;  background-color:green; border-radius:25px; padding:5px; text-decoration:none"   target="_blank">Log Report</a>
            
               &nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <asp:Button ID="Button4" align="right" runat="server" style="color:#fff;  background-color:red; border-radius:25px"  
             Text="Logout" Width="110px" Height="30px" OnClick="btn_logout_Click"  />
             </center>
        </div>
        <div class="navigationbar" style="display:none">
        <asp:TextBox ID="txt_navbar" runat="server" OnTextChanged="txt_navbar_SelectedChanged" AutoPostBack="true" Width="98%"></asp:TextBox>
        </div>
         <%--NEw Navigation Bar--%>
        <div class="navigationbar two" id="two" runat="server" style="padding-left:15px;display:-webkit-box;background-color:#fff">
       <table>
        <tr>
          <asp:Repeater ID="Repeater_Nav" runat="server"><ItemTemplate>
        <td> <asp:Button ID="nav_btn" runat="server" style="background-color:#fff; border:none; cursor:pointer" onClick="nav_btn_click" CommandArgument='<% #Eval("url") %>'  Text='<% #Eval("url_list") %>'></asp:Button><img src="Images/forwardLogo.png" /> </td>
         </ItemTemplate></asp:Repeater>
       
        </tr>
        </table>
        </div>
         <%--NEw Navigation Bar--%>
        
    <%--folder grid--%>
 <div class="folder-grid" style=" border:1px solid black">
 <asp:Repeater ID="Folder_Repeater" runat="server">
        <ItemTemplate>
        <div class="folder-box">
        <div class="folder-image"><asp:ImageButton ImageUrl="images/folderImage.png"  runat="server" ID="folder" OnClick="folder_Click" CommandArgument='<%#Eval("ServerDir") %>' width="100%"/>
        </div>
       <div class="folder-name"><asp:Label ID="lbl_folder" style="font-size:13px;word-wrap:break-word" runat="server" Text='<%#Eval("ServerDir") %>'></asp:Label></div> 
        </div>
        </ItemTemplate>
        </asp:Repeater>
  
  <%--folder grid--%>
     <%--Files grid--%>

 <asp:Repeater ID="Files_Repeater" runat="server" 
         onitemcommand="Files_Repeater_ItemCommand">
        <ItemTemplate>
        <div class="folder-box">
        <div class="folder-image"><asp:ImageButton ID="files_imgbtn" runat="server"  OnClick="files_imgbtn_Click" CommandArgument='<%#Eval("ServerDir") %>' ImageUrl='<%#Eval("image_url")%>' width="70%" /></div>
       <div class="folder-name"><asp:Label ID="lbl_folder" style="font-size:13px;word-wrap:break-word" runat="server" Text='<%#Eval("ServerDir") %>'></asp:Label></div> 
        </div>
        </ItemTemplate>
        </asp:Repeater>
  </div>
  <%--Files grid--%>
  
      
      </div>
      </div>

      </div>
      <div >
     <%-- Upload Files--%>
       <%--<div class="col-md-6" style="display:block">
   
   <div class="box1" >
   <%--<asp:Panel ID="Panel1"  runat="server" Height="272px" >
      
    <table width="100%" style="height: 185px;>
        <tr style=" "><td colspan="2" style="background-color:navy ;border-top-right-radius:20px;border-top-left-radius:20px">
        <center style="color:#fff; font-size:30px; margin:5px">
           
               
                <asp:Label ID="Label1" runat="server" Text="Download Server File " Height="45px" 
                    Width="426px"></asp:Label>
                
          
        </center>
       </td> </tr>
        <tr>
        <td>
        <asp:Label ID="lb" Text="Path :" runat="server"></asp:Label>
        </td>
        <td class="style1"> 
            <asp:TextBox ID="txt_filepath" Width="300px" Height="46px" Font-Size="15px" 
                TextMode="Multiline"   runat="server"></asp:TextBox></td>
        </tr>
        <tr>
        <td colspan="2">
       
            <asp:FileUpload ID="FileUpload1" runat="server" style="margin-left:90px"  
                Width="264px" Height="46px" /></td>
        </tr>
        <tr>
        <td colspan="2"><center>
        <asp:Label ID="lbl_msg1" runat="server" style="color:Red; font-size:20px" ></asp:Label></center>
        </td>
        </tr>
        
        <tr>
        <td colspan="2">  
        <center>          
            <asp:Button ID="btn_Upload" align="center" runat="server" style="color:#fff; background-color:Green; border-radius:25px"  
             Text="Upload" Width="76px" Height="30px"  />
             &nbsp&nbsp&nbsp&nbsp   <asp:Button ID="btn_download" align="center" runat="server" style="color:#fff; background-color:Green; border-radius:25px"  
             Text="Download" Width="76px" Height="30px" onclick="btn_download_Click" />
             </center>
        </td>
</tr>
        </table>
        </asp:Panel>
   </div>
    </div>--%>
        <%-- Upload Files--%>
    </div>
    </div>
   
   
     <div id="popupBox" runat="server" style="display:none">
            <div id="popup">
              <asp:Panel ID="Panel1"  runat="server" Height="272px" >
      
    <table width="100%" style="height: 185px;>
        <tr style=" "><td colspan="2" style="background-color:navy ;border-top-right-radius:20px;border-top-left-radius:20px">
        <center style="color:#fff; font-size:30px; margin:5px">
           
               
                <asp:Label ID="popup_title" runat="server" Text="Upload Server File " Height="45px" 
                    Width="426px"></asp:Label>
                
          
        </center>
       </td> </tr>
        <tr>
        <td>
        <asp:Label ID="lb" Text="Path :" runat="server"></asp:Label>
        </td>
        <td class="style1"> 
            <asp:TextBox ID="txt_filepath" Width="300px" Height="46px" Font-Size="15px" 
                TextMode="Multiline"   runat="server"></asp:TextBox></td>
        </tr>
        <tr>
        <td colspan="2">
        <asp:Label ID="lbl_dir" runat="server" Visible="false" Text = "Directory Name" ></asp:Label>
       <asp:TextBox ID="dir_Name" runat="server" Visible="false" ></asp:TextBox>
            <asp:FileUpload ID="FileUpload1" runat="server" style="margin-left:90px"  
                Width="264px" Height="46px" /></td>
        </tr>
        <tr>
        <td colspan="2"><center>
        <asp:Label ID="lbl_msg1" runat="server" style="color:Red; font-size:20px" ></asp:Label></center>
        </td>
        </tr>
        
        <tr>
        <td colspan="2">  
        <center>          
            <asp:Button ID="btn_Upload" align="center" runat="server"  style="color:#fff; background-color:Green; border-radius:25px"  
             Text="Upload" Width="76px" Height="30px" OnClick="btn_Upload_Click"  />
             &nbsp&nbsp&nbsp&nbsp   <asp:Button ID="btn_download" align="center" runat="server" style="color:#fff; background-color:Green; border-radius:25px"  
             Text="Create" Width="76px" Height="30px" onclick="btn_Create_Dir_Click" />
              &nbsp&nbsp&nbsp&nbsp   <asp:Button ID="btn_Close" align="center" runat="server" style="color:#fff; background-color:Green; border-radius:25px"  
             Text="Close" Width="76px" Height="30px" onclick="btn_Close_Click" />
             </center>
        </td>
</tr>
        </table>
        </asp:Panel>
            </div>
        </div>

  
    </form>
</body>
</html>
