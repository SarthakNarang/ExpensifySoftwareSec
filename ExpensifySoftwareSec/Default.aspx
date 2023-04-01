<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ExpensifySoftwareSec._Default" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <html>
    <head>
        <title></title>
        <%--   <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
            rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>--%>
    </head>



    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="col-md-4">

        <asp:Button ID="btnnewexpense" runat="server" CssClass="btn btn-primary" Text="New Expense" OnClick="btnnewexpense_Click" Width="150px" Height="70px" />

    </div>

    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Add an expense!</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="expenseType">Choose a Friend </label>
                        <asp:DropDownList CssClass="form-control" ID="friend_ddl" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="amount">Amount:</label>
                        <asp:TextBox type="number" runat="server" class="form-control" ID="amount" placeholder="Enter amount" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Description:" />
                        <textarea runat="server" class="form-control" id="description" rows="3"></textarea>
                    </div>
                    <div class="form-group">
                        <label for="currency">Currency:</label>
                        <asp:DropDownList CssClass="form-control" ID="currency" runat="server">
                        </asp:DropDownList>

                    </div>
                    <div class="form-group">
                        <label for="split">Split:</label>
                        <asp:DropDownList runat="server" class="form-control" ID="splittype">
                            <asp:ListItem Text="Equally" Value="1"></asp:ListItem>
                            <asp:ListItem Text="All paid by you, They owe you the whole amount." Value="2"></asp:ListItem>
                            <asp:ListItem Text="All paid by them, You owe the whole amount." Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    <asp:Button runat="server" Text="Save" ID="modalsave" class="btn btn-success" OnClick="Save_Click" />
                </div>
            </div>
        </div>
    </div>
    </html>
    <script type="text/javascript">

        function openModal() {
            $('#myModal').modal('show');
            var myModal = document.getElementById("myModal");
            myModal.style.display = "block";
        }

        function closeModal() {
            var myModal = document.getElementById("myModal");
            myModal.style.display = "none";
        }

    </script>

</asp:Content>


