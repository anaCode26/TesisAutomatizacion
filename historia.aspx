<%@ Page Language="C#" AutoEventWireup="true" CodeFile="historia.aspx.cs" Inherits="historia" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>


<html lang="en">

<head runat="server">

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>QA</title>

    <!-- Bootstrap core CSS -->
    <link href="web/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom fonts for this template -->
    <link href="web/vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Lato:400,700,400italic,700italic" rel="stylesheet" type="text/css">

    <!-- Plugin CSS -->
    <link href="web/vendor/magnific-popup/magnific-popup.css" rel="stylesheet" type="text/css">

    <!-- Custom styles for this template -->
    <link href="web/css/freelancer.min.css" rel="stylesheet">
</head>



<body id="page-top">
    <form id="form2" runat="server">
    <!-- Navigation -->
    <nav class="navbar navbar-expand-lg bg-secondary fixed-top text-uppercase" id="mainNav">
        <div class="container">

            <a class="navbar-brand js-scroll-trigger" href="#page-top">&nbsp&nbsp QA Testing</a>
            <button class="navbar-toggler navbar-toggler-right text-uppercase bg-primary text-white rounded" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
                Menu
         <i class="fas fa-bars"></i>
            </button>
            <div class="collapse navbar-collapse" id="navbarResponsive">
                <ul class="navbar-nav ml-auto">
                   <%-- <li class="nav-item mx-0 mx-lg-1">
                        <a class="nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger" href="#portfolio">Inicio</a>
                    </li>
                    <li class="nav-item mx-0 mx-lg-1">
                        <a class="nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger" href="#about">Acerca de</a>
                    </li>
                    <li class="nav-item mx-0 mx-lg-1">
                        <a class="nav-link py-3 px-0 px-lg-3 rounded js-scroll-trigger" href="#contact">Contáctanos</a>
                    </li>--%>
                    <li class="nav-item dropdown mx-0 mx-lg-1">
                        <a class="nav-link dropdown py-3 px-0 px-lg-3 rounded js-scroll-trigger" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                               
                                <asp:Label ID="usuario" runat="server" Text="Label"></asp:Label>

                            </a>
                            <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                               <%-- <a class="dropdown-item">Login</a>--%>
                                <a class="dropdown-item" href="clientes.aspx ">Clientes</a>
                                <a class="dropdown-item" href =" graficas.aspx">graficas</a>
                                <a class="dropdown-item" href =" Default.aspx">pruebas</a>
                                <asp:Button ID="cerrar_sesion" runat="server" class="dropdown-item" Text="CERRAR SESION" OnClick="cerrar_sesion_Click" />
                            </div>
                    </li>
                </ul>
            </div>
        </div>
    </nav>


    <!--  Start Login -->

    <!--  End Login -->
    <!-- Header -->
    <header class="masthead bg-primary text-white text-center">
        <div class="container">
            <h2 class="text-uppercase mb-0">Reportes</h2>
            <br>
            <div class="alert alert-secondary" role="alert" style="width: 100%; height: 350px" >
                <a href="#" class="alert-link"></a>

                 <div class =" container ">
                     
                     
                     <asp:DropDownList ID="DropDownList1" runat="server" Width="238px" class="btn btn-secondary dropdown-toggle .bg-secondary" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" ></asp:DropDownList>
                     <asp:Button ID="btn_cliente" runat="server" Text="SELECCIONE EL CLIENTE"  type="button" class="btn btn-success" OnClick="btn_cliente_Click" />
                     
                     <br>
                     <br>


                        <asp:GridView ID="GridView1" runat="server" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" DataKeyNames="nombre,fecha"  >
                            
                            <AlternatingRowStyle BackColor="White" />
                            
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" SelectText="PDF" />
                                 
                                
                            </Columns>
                            
                            <EditRowStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                            
                        </asp:GridView>
                        
                     <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        
                    </div>
                
             <%--   <asp:Chart ID="Grafica" runat="server">
                    <Series>
                        <asp:Series Name="Series" YValuesPerPoint="6"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>--%>
               
            </div>
    </header>
    </form>
    <!-- Footer -->
    <footer class="footer text-center">
        <div class="container">
            <div class="row">

                <div class="col-md-12 mb-5 mb-lg-0">
                    <h4 class="text-uppercase mb-4">Around the Web</h4>
                    <ul class="list-inline mb-0">
                        <li class="list-inline-item">
                            <a class="btn btn-outline-light btn-social text-center rounded-circle" href="#">
                                <i class="fab fa-fw fa-facebook-f"></i>
                            </a>
                        </li>
                        <li class="list-inline-item">
                            <a class="btn btn-outline-light btn-social text-center rounded-circle" href="#">
                                <i class="fab fa-fw fa-twitter"></i>
                            </a>
                        </li>
                        <li class="list-inline-item">
                            <a class="btn btn-outline-light btn-social text-center rounded-circle" href="#">
                                <i class="fab fa-fw fa-linkedin-in"></i>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </footer>

    <div class="copyright py-4 text-center text-white">
        <div class="container">
            <small>QA TESTING &nbsp &copy; &nbsp Website 2019</small>
        </div>
    </div>

    <!-- Scroll to Top Button (Only visible on small and extra-small screen sizes) -->
    <div class="scroll-to-top d-lg-none position-fixed ">
        <a class="js-scroll-trigger d-block text-center text-white rounded" href="#page-top">
            <i class="fa fa-chevron-up"></i>
        </a>
    </div>










    <!-- Bootstrap core JavaScript -->
    <script src="web/vendor/jquery/jquery.min.js"></script>
    <script src="web/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Plugin JavaScript -->
    <script src="web/vendor/jquery-easing/jquery.easing.min.js"></script>
    <script src="web/vendor/magnific-popup/jquery.magnific-popup.min.js"></script>

    <!-- Contact Form JavaScript -->
    <script src="web/js/jqBootstrapValidation.js"></script>
    <script src="web/js/contact_me.js"></script>

</body>

</html>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>--%>
