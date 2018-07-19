<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Styles/StyleSheet.css" type="text/css" rel="Stylesheet" />
    <link type="text/css" href="CSS/ui.all.css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/jquery-1.3.2.js"></script>
    <script type="text/javascript" src="Scripts/ui.core.js"></script>
    <script type="text/javascript" src="Scripts/ui.progressbar.js"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table class="table">
        <tr>
            <td style="color: Red">
                <asp:Label runat="server" ID="lblException"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="color: Red">
                <asp:Label runat="server" ID="lblResult"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="table" style="border: 2px solid silver; color: Blue;">
        <tr>
            <td>
                Select use-case file:
            </td>
            <td>
                <asp:FileUpload ID="FileUploadControl" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Available groups
            </td>
            <td>
                <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ListBox ID="lstBrowser" runat="server" Width="300px" SelectionMode="single"
                            AutoPostBack="false" Height="300px"></asp:ListBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr >
          <td >
            Generate English Version:
          
          </td>
          <td>
          
            <asp:CheckBox ID="chkEnglishVersion" runat="server" />
          
          </td>
            
        
        </tr>



        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnOk" Text="SUBMIT" runat="server" OnClick="btnOk_Click" />
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
