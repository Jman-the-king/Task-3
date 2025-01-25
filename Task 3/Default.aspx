<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Task_3._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<!DOCTYPE html>

<html>

<body>

<form id="form1" enctype="multipart/form-data">

<div>

<label for="Id">User ID(For adding, updating and removing):</label> <br />

<asp:TextBox ID="IdTextBox" runat="server"></asp:TextBox>

<br />


<label for="Firstname">First Name:</label><br/>

<asp:TextBox ID="FirstNameTextBox" runat="server"></asp:TextBox>

<br />


<label for="Lastname">Last Name:</label><br/>

<asp:TextBox ID="LastNameTextBox" runat="server"></asp:TextBox>

<br />


<label for="Numberofgroups">Number of group(s):</label><br/>

<asp:TextBox ID="NumberOfGroupsTextBox" runat="server"></asp:TextBox>

<br />


<label for="Groupname">Group Name(s):</label><br/>

<asp:TextBox ID="GroupNameTextBox" runat="server"></asp:TextBox>

<br />





<asp:Button ID="SubmitButton" runat="server" Text="Add" OnClick="SubmitButton_Click" />
    
<asp:Button ID="RemoveButton" runat="server" Text="Remove" OnClick="RemoveButton_Click" />

<asp:Button ID="UpdateButton" runat="server" Text="Update" OnClick="UpdateButton_Click" />

<br /><br />


<asp:GridView ID="UserGridView" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="UserGridView_SelectedIndexChanged">

<Columns>

        <asp:BoundField DataField="UserId" HeaderText="User ID" />

        <asp:BoundField DataField="FirstName" HeaderText="First Name" />

        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
    
        <asp:BoundField DataField="GroupName" HeaderText="Group Names" />

        <asp:BoundField DataField="NumberOfGroups" HeaderText="Number Of Groups" />

        <asp:CommandField ShowSelectButton="True" />

</Columns>

</asp:GridView>

<br /><br />


<asp:Label ID="UserCountLabel" runat="server" Text="Total Users: 0" Style="font-weight:bold;"></asp:Label>


</div>

</form>

</body>

</html>
</asp:Content>

