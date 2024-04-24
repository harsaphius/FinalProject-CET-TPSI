<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="StatisticalPage.aspx.cs" Inherits="FinalProject.StatisticalPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="text-center">
        <div class="h3">Estatísticas</div>
        <figure style="padding-right:20px; padding-top: 20px;">
            <blockquote class="blockquote text-center">
                <p class="ps-2">Innovation distinguishes between a leader and a follower.</p>
                </blockquote>
                <figcaption class="blockquote-footer ps-3 text-center">Steve Jobs
                </figcaption>
            </blockquote>
        </figure>
    </div>
    <div class="container-fluid py-4">
        <div class="row">
            <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                <div class="card card-stats">
                    <div class="card-body p-3">
                        <div class="row">
                            <div class="col-8">
                                <div class="numbers">
                                    <p class="text-sm mb-0 text-capitalize font-weight-bold">Total de Turmas Terminadas</p>
                                    <h5 class="font-weight-bolder mb-0">
                                        <asp:Label runat="server" ID="lblClassgroupOver"></asp:Label>
                                    </h5>
                                </div>
                            </div>
                            <div class="col-4 text-end">
                                <div class="icon icon-shape bg-gradient-primary shadow text-center border-radius-md">
                                    <i class="ni ni-money-coins text-lg opacity-10" aria-hidden="true"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                <div class="card card-stats">
                    <div class="card-body p-3">
                        <div class="row">
                            <div class="col-8">
                                <div class="numbers">
                                    <p class="text-sm mb-0 text-capitalize font-weight-bold">Total de Turmas a Decorrer</p>
                                    <h5 class="font-weight-bolder mb-0">
                                        <asp:Label runat="server" ID="lblClassgroupOngoing"></asp:Label>
                                    </h5>
                                </div>
                            </div>
                            <div class="col-4 text-end">
                                <div class="icon icon-shape bg-gradient-primary shadow text-center border-radius-md">
                                    <i class="ni ni-world text-lg opacity-10" aria-hidden="true"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                <div class="card card-stats">
                    <div class="card-body p-3">
                        <div class="row">
                            <div class="col-8">
                                <div class="numbers">
                                    <p class="text-sm mb-0 text-capitalize font-weight-bold">Total de Formandos Atualmente Ativos</p>
                                    <h5 class="font-weight-bolder mb-0">
                                        <asp:Label runat="server" ID="lblStudentsOngoing"></asp:Label>
                                    </h5>
                                </div>
                            </div>
                            <div class="col-4 text-end">
                                <div class="icon icon-shape bg-gradient-primary shadow text-center border-radius-md">
                                    <i class="ni ni-paper-diploma text-lg opacity-10" aria-hidden="true"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
     <div class="row mt-4">
            <div class="col-xl-6">
                <div class="card card-chart">
                    <div class="card-header">
                        <h5 class="card-category">Total Cursos</h5>
                    </div>
                    <div class="card-body">
                        <asp:Chart ID="Chart1" runat="server" Width="500" DataSourceID="SQLDSCursos">
                            <Series>
                                <asp:Series Name="Series1" XValueMember="Area" YValueMembers="TotalCursos" Color="#cc3a60"></asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" BackColor="#f9f9f9">
                                    <AxisY Title="Total Cursos" TitleForeColor="#cc3a60" TitleFont="Calibri, 12pt" LineColor="#e0e0e0">
                                        <MajorGrid LineColor="#e0e0e0"></MajorGrid>
                                    </AxisY>
                                    <AxisX LineColor="#e0e0e0">
                                        <MajorGrid LineColor="#e0e0e0"></MajorGrid>
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:SqlDataSource ID="SQLDSCursos" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT A.nomeArea AS Area, COUNT(*) AS TotalCursos FROM curso AS C INNER JOIN area AS A ON C.codArea = A.codArea GROUP BY A.nomeArea;"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
            <div class="col-xl-6">
                <div class="card card-chart">
                    <div class="card-header">
                        <h5 class="card-category">Total Horas</h5>
                    </div>
                    <div class="card-body">
                        <asp:Chart ID="Chart2" runat="server" Width="500px" DataSourceID="SQLDSFormadores">
                            <Series>
                                <asp:Series Name="Series1" XValueMember="Utilizador" YValueMembers="TotalHoras" Color="#cc3a60"></asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" BackColor="#f9f9f9">
                                    <AxisY Title="Total Horas" TitleForeColor="#cc3a60" TitleFont="Calibri, 12pt" LineColor="#e0e0e0">
                                        <MajorGrid LineColor="#e0e0e0"></MajorGrid>
                                    </AxisY>
                                    <AxisX LineColor="#e0e0e0">
                                        <MajorGrid LineColor="#e0e0e0"></MajorGrid>
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:SqlDataSource ID="SQLDSFormadores" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT TOP 10 MT.codFormador, U.utilizador AS Utilizador, SUM(M.duracao) AS TotalHoras FROM moduloFormadorTurma AS MT INNER JOIN modulo AS M ON MT.codModulo = M.codModulos INNER JOIN turma AS T ON MT.codTurma = T.codTurma INNER JOIN utilizador AS U ON MT.codFormador = U.codUtilizador WHERE T.DataFim < GETDATE() GROUP BY MT.codFormador, U.utilizador ORDER BY TotalHoras DESC;"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
