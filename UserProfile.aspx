<%@ Page Title="Perfil" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="FinalProject.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="page-header min-height-300 border-radius-xl mt-4" style="background-image: url('../assets/img/curved-images/curved0.jpg'); background-position-y: 50%;">
            <span class="mask bg-gradient-primary opacity-6"></span>
        </div>
        <div class="card card-body blur shadow-blur mx-4 mt-n6 overflow-hidden">
            <div class="row gx-4">
                <div class="col-auto">
                    <div class="avatar avatar-xl position-relative">
                        <asp:Image ID="foto" runat="server" alt="profile_image" class="w-100 border-radius-lg shadow-sm"></asp:Image>
                    </div>
                </div>
                <div class="col-auto my-auto">
                    <div class="h-100">
                        <h5 class="mb-1">
                            <a href="/UserProfile.aspx">
                                <asp:Label runat="server" ID="profilename"></asp:Label>
                            </a>
                        </h5>
                        <p class="mb-0 font-weight-bold text-sm">
                            <asp:Label runat="server" ID="profileemail"></asp:Label>
                        </p>
                    </div>
                </div>
                <div class="col-lg-5 col-md-6 my-sm-auto ms-sm-auto me-sm-0 mx-auto mt-3">
                    <div class="nav-wrapper position-relative end-0">
                        <ul class="nav nav-pills nav-fill p-1 bg-transparent" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link mb-0 px-0 py-1 active " data-bs-toggle="tab" href="#" role="tab" aria-selected="true" onclick="showChangeCursos(event); return false;">
                                    <svg width="16px" class="text-dark" height="16px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                                        <!--!Font Awesome Free 6.5.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2024 Fonticons, Inc.-->
                                        <path d="M160 96a96 96 0 1 1 192 0A96 96 0 1 1 160 96zm80 152V512l-48.4-24.2c-20.9-10.4-43.5-17-66.8-19.3l-96-9.6C12.5 457.2 0 443.5 0 427V224c0-17.7 14.3-32 32-32H62.3c63.6 0 125.6 19.6 177.7 56zm32 264V248c52.1-36.4 114.1-56 177.7-56H480c17.7 0 32 14.3 32 32V427c0 16.4-12.5 30.2-28.8 31.8l-96 9.6c-23.2 2.3-45.9 8.9-66.8 19.3L272 512z" />
                                        <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                            <g transform="translate(-2319.000000, -291.000000)" fill="#FFFFFF" fill-rule="nonzero">
                                                <g transform="translate(1716.000000, 291.000000)">
                                                    <g transform="translate(603.000000, 0.000000)">
                                                        <path class="color-background" d="M22.7597136,19.3090182 L38.8987031,11.2395234 C39.3926816,10.9925342 39.592906,10.3918611 39.3459167,9.89788265 C39.249157,9.70436312 39.0922432,9.5474453 38.8987261,9.45068056 L20.2741875,0.1378125 L20.2741875,0.1378125 C19.905375,-0.04725 19.469625,-0.04725 19.0995,0.1378125 L3.1011696,8.13815822 C2.60720568,8.38517662 2.40701679,8.98586148 2.6540352,9.4798254 C2.75080129,9.67332903 2.90771305,9.83023153 3.10122239,9.9269862 L21.8652864,19.3090182 C22.1468139,19.4497819 22.4781861,19.4497819 22.7597136,19.3090182 Z">
                                                        </path>
                                                        <path class="color-background" d="M23.625,22.429159 L23.625,39.8805372 C23.625,40.4328219 24.0727153,40.8805372 24.625,40.8805372 C24.7802551,40.8805372 24.9333778,40.8443874 25.0722402,40.7749511 L41.2741875,32.673375 L41.2741875,32.673375 C41.719125,32.4515625 42,31.9974375 42,31.5 L42,14.241659 C42,13.6893742 41.5522847,13.241659 41,13.241659 C40.8447549,13.241659 40.6916418,13.2778041 40.5527864,13.3472318 L24.1777864,21.5347318 C23.8390024,21.7041238 23.625,22.0503869 23.625,22.429159 Z" opacity="0.7"></path>
                                                        <path class="color-background" d="M20.4472136,21.5347318 L1.4472136,12.0347318 C0.953235098,11.7877425 0.352562058,11.9879669 0.105572809,12.4819454 C0.0361450918,12.6208008 6.47121774e-16,12.7739139 0,12.929159 L0,30.1875 L0,30.1875 C0,30.6849375 0.280875,31.1390625 0.7258125,31.3621875 L19.5528096,40.7750766 C20.0467945,41.0220531 20.6474623,40.8218132 20.8944388,40.3278283 C20.963859,40.1889789 21,40.0358742 21,39.8806379 L21,22.429159 C21,22.0503869 20.7859976,21.7041238 20.4472136,21.5347318 Z" opacity="0.7"></path>
                                                    </g>
                                                </g>
                                            </g>
                                        </g>
                                    </svg>
                                    <span class="ms-1">Cursos</span>
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link mb-0 px-0 py-1 " data-bs-toggle="tab" href="#" role="tab" aria-selected="false" onclick="showChangeAvaliacao(event); return false;">
                                    <svg width="16px" class="text-dark" height="16px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512">
                                        <!--!Font Awesome Free 6.5.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2024 Fonticons, Inc.-->
                                        <path d="M219.3 .5c3.1-.6 6.3-.6 9.4 0l200 40C439.9 42.7 448 52.6 448 64s-8.1 21.3-19.3 23.5L352 102.9V160c0 70.7-57.3 128-128 128s-128-57.3-128-128V102.9L48 93.3v65.1l15.7 78.4c.9 4.7-.3 9.6-3.3 13.3s-7.6 5.9-12.4 5.9H16c-4.8 0-9.3-2.1-12.4-5.9s-4.3-8.6-3.3-13.3L16 158.4V86.6C6.5 83.3 0 74.3 0 64C0 52.6 8.1 42.7 19.3 40.5l200-40zM111.9 327.7c10.5-3.4 21.8 .4 29.4 8.5l71 75.5c6.3 6.7 17 6.7 23.3 0l71-75.5c7.6-8.1 18.9-11.9 29.4-8.5C401 348.6 448 409.4 448 481.3c0 17-13.8 30.7-30.7 30.7H30.7C13.8 512 0 498.2 0 481.3c0-71.9 47-132.7 111.9-153.6z" />
                                        <title>document</title>
                                        <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                            <g transform="translate(-1870.000000, -591.000000)" fill="#FFFFFF" fill-rule="nonzero">
                                                <g transform="translate(1716.000000, 291.000000)">
                                                    <g transform="translate(154.000000, 300.000000)">
                                                        <path class="color-background" d="M40,40 L36.3636364,40 L36.3636364,3.63636364 L5.45454545,3.63636364 L5.45454545,0 L38.1818182,0 C39.1854545,0 40,0.814545455 40,1.81818182 L40,40 Z" opacity="0.603585379"></path>
                                                        <path class="color-background" d="M30.9090909,7.27272727 L1.81818182,7.27272727 C0.814545455,7.27272727 0,8.08727273 0,9.09090909 L0,41.8181818 C0,42.8218182 0.814545455,43.6363636 1.81818182,43.6363636 L30.9090909,43.6363636 C31.9127273,43.6363636 32.7272727,42.8218182 32.7272727,41.8181818 L32.7272727,9.09090909 C32.7272727,8.08727273 31.9127273,7.27272727 30.9090909,7.27272727 Z M18.1818182,34.5454545 L7.27272727,34.5454545 L7.27272727,30.9090909 L18.1818182,30.9090909 L18.1818182,34.5454545 Z M25.4545455,27.2727273 L7.27272727,27.2727273 L7.27272727,23.6363636 L25.4545455,23.6363636 L25.4545455,27.2727273 Z M25.4545455,20 L7.27272727,20 L7.27272727,16.3636364 L25.4545455,16.3636364 L25.4545455,20 Z">
                                                        </path>
                                                    </g>
                                                </g>
                                            </g>
                                        </g>
                                    </svg>
                                    <span class="ms-1">Avaliações</span>
                                </a>
                            </li>
                            <li class="nav-item" id="alterarPW">
                                <a class="nav-link mb-0 px-0 py-1 " data-bs-toggle="tab" href="#" role="tab" aria-selected="false" onclick="showChangePw(event); return false;">
                                    <svg width="16px" class="text-dark" height="16px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                                        <!--!Font Awesome Free 6.5.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2024 Fonticons, Inc.-->
                                        <path d="M336 352c97.2 0 176-78.8 176-176S433.2 0 336 0S160 78.8 160 176c0 18.7 2.9 36.8 8.3 53.7L7 391c-4.5 4.5-7 10.6-7 17v80c0 13.3 10.7 24 24 24h80c13.3 0 24-10.7 24-24V448h40c13.3 0 24-10.7 24-24V384h40c6.4 0 12.5-2.5 17-7l33.3-33.3c16.9 5.4 35 8.3 53.7 8.3zM376 96a40 40 0 1 1 0 80 40 40 0 1 1 0-80z" />
                                        <title>password</title>
                                        <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                            <g transform="translate(-2020.000000, -442.000000)" fill="#FFFFFF" fill-rule="nonzero">
                                                <g transform="translate(1716.000000, 291.000000)">
                                                    <g transform="translate(304.000000, 151.000000)">
                                                        <polygon class="color-background" opacity="0.596981957" points="18.0883333 15.7316667 11.1783333 8.82166667 13.3333333 6.66666667 6.66666667 0 0 6.66666667 6.66666667 13.3333333 8.82166667 11.1783333 15.315 17.6716667">
                                                        </polygon>
                                                        <path class="color-background" d="M31.5666667,23.2333333 C31.0516667,23.2933333 30.53,23.3333333 30,23.3333333 C29.4916667,23.3333333 28.9866667,23.3033333 28.48,23.245 L22.4116667,30.7433333 L29.9416667,38.2733333 C32.2433333,40.575 35.9733333,40.575 38.275,38.2733333 L38.275,38.2733333 C40.5766667,35.9716667 40.5766667,32.2416667 38.275,29.94 L31.5666667,23.2333333 Z" opacity="0.596981957"></path>
                                                        <path class="color-background" d="M33.785,11.285 L28.715,6.215 L34.0616667,0.868333333 C32.82,0.315 31.4483333,0 30,0 C24.4766667,0 20,4.47666667 20,10 C20,10.99 20.1483333,11.9433333 20.4166667,12.8466667 L2.435,27.3966667 C0.95,28.7083333 0.0633333333,30.595 0.00333333333,32.5733333 C-0.0583333333,34.5533333 0.71,36.4916667 2.11,37.89 C3.47,39.2516667 5.27833333,40 7.20166667,40 C9.26666667,40 11.2366667,39.1133333 12.6033333,37.565 L27.1533333,19.5833333 C28.0566667,19.8516667 29.01,20 30,20 C35.5233333,20 40,15.5233333 40,10 C40,8.55166667 39.685,7.18 39.1316667,5.93666667 L33.785,11.285 Z">
                                                        </path>
                                                    </g>
                                                </g>
                                            </g>
                                        </g>
                                    </svg>
                                    <span class="ms-1">Alterar PW</span>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Profile Div -->
    <div id="profileSinapse" class="container-fluid py-4">
        <div class="row">
            <div class="col-12 col-xl-4">
                <div class="card h-100">
                    <div class="card-header pb-0 p-3">
                        <div class="row">
                            <div class="col-md-8 d-flex align-items-center">
                                <h6 class="mb-0">Informações de Perfil</h6>
                            </div>
                            <div class="col-md-4 text-end">
                                <a href="javascript:;">
                                    <i class="fas fa-user-edit text-secondary text-sm" data-bs-toggle="tooltip" data-bs-placement="top" title="Edit Profile" onclick="showEdit(event); return false;"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="card-body p-3">

                        <hr class="horizontal gray-light my-2">
                        <ul class="list-group">

                            <li class="list-group-item border-0 ps-0 pt-0 text-sm"><strong class="text-dark">Nome Completo:</strong> &nbsp;
                                <asp:Label runat="server" ID="infoname"></asp:Label></li>
                            <li class="list-group-item border-0 ps-0 text-sm"><strong class="text-dark">Telemóvel:</strong> &nbsp;
                                <asp:Label runat="server" ID="infocell"></asp:Label></li>
                            <li class="list-group-item border-0 ps-0 text-sm"><strong class="text-dark">E-mail:</strong> &nbsp;
                                <asp:Label runat="server" ID="infoemail"></asp:Label></li>

                            <li class="list-group-item border-0 ps-0 pb-0">
                                <p class="text-sm">
                                    <strong class="text-dark">Life Motto:</strong> &nbsp;<i>
                                        <asp:Label runat="server" ID="lbLifeMotto"></asp:Label></i>
                                </p>
                            </li>
                            <li class="list-group-item border-0 ps-0 pb-0">
                                <strong class="text-dark text-sm">Social:</strong> &nbsp;
                                <a class="btn btn-facebook btn-simple mb-0 ps-1 pe-2 py-0" href="javascript:;">
                                    <i class="fab fa-facebook fa-lg"></i>
                                </a>
                                <a class="btn btn-twitter btn-simple mb-0 ps-1 pe-2 py-0" href="javascript:;">
                                    <i class="fab fa-twitter fa-lg"></i>
                                </a>
                                <a class="btn btn-instagram btn-simple mb-0 ps-1 pe-2 py-0" href="javascript:;">
                                    <i class="fab fa-instagram fa-lg"></i>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>

            <div class="col-12 col-xl-4">
                <div class="card h-100">
                    <div class="card-header pb-0 p-3">
                        <h6 class="mb-0">Documentos Submetidos</h6>
                    </div>
                    <div class="card-body p-3">
                        <asp:Repeater ID="fileRepeater" runat="server">
                            <HeaderTemplate></HeaderTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="downloadLink" runat="server" Target="_blank"
                                    NavigateUrl='<%# FileControlInstance.GenerateFileUrl() %>'
                                    Text='<%# Eval("FileName") %>' />
                                <br />
                            </ItemTemplate>
                            <FooterTemplate></FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <%--            <div class="col-12 col-xl-4">
                <div class="card h-100">
                    <div class="card-header pb-0 p-3">
                        <h6 class="mb-0">Conversations</h6>
                    </div>
                    <div class="card-body p-3">
                        <ul class="list-group">
                            <li class="list-group-item border-0 d-flex align-items-center px-0 mb-2">
                                <div class="avatar me-3">
                                    <img src="../assets/img/kal-visuals-square.jpg" alt="kal" class="border-radius-lg shadow">
                                </div>
                                <div class="d-flex align-items-start flex-column justify-content-center">
                                    <h6 class="mb-0 text-sm">Sophie B.</h6>
                                    <p class="mb-0 text-xs">Hi! I need more information..</p>
                                </div>
                                <a class="btn btn-link pe-3 ps-0 mb-0 ms-auto" href="javascript:;">Reply</a>
                            </li>
                            <li class="list-group-item border-0 d-flex align-items-center px-0 mb-2">
                                <div class="avatar me-3">
                                    <img src="../assets/img/marie.jpg" alt="kal" class="border-radius-lg shadow">
                                </div>
                                <div class="d-flex align-items-start flex-column justify-content-center">
                                    <h6 class="mb-0 text-sm">Anne Marie</h6>
                                    <p class="mb-0 text-xs">Awesome work, can you..</p>
                                </div>
                                <a class="btn btn-link pe-3 ps-0 mb-0 ms-auto" href="javascript:;">Reply</a>
                            </li>
                            <li class="list-group-item border-0 d-flex align-items-center px-0 mb-2">
                                <div class="avatar me-3">
                                    <img src="../assets/img/ivana-square.jpg" alt="kal" class="border-radius-lg shadow">
                                </div>
                                <div class="d-flex align-items-start flex-column justify-content-center">
                                    <h6 class="mb-0 text-sm">Ivanna</h6>
                                    <p class="mb-0 text-xs">About files I can..</p>
                                </div>
                                <a class="btn btn-link pe-3 ps-0 mb-0 ms-auto" href="javascript:;">Reply</a>
                            </li>
                            <li class="list-group-item border-0 d-flex align-items-center px-0 mb-2">
                                <div class="avatar me-3">
                                    <img src="../assets/img/team-4.jpg" alt="kal" class="border-radius-lg shadow">
                                </div>
                                <div class="d-flex align-items-start flex-column justify-content-center">
                                    <h6 class="mb-0 text-sm">Peterson</h6>
                                    <p class="mb-0 text-xs">Have a great afternoon..</p>
                                </div>
                                <a class="btn btn-link pe-3 ps-0 mb-0 ms-auto" href="javascript:;">Reply</a>
                            </li>
                            <li class="list-group-item border-0 d-flex align-items-center px-0">
                                <div class="avatar me-3">
                                    <img src="../assets/img/team-3.jpg" alt="kal" class="border-radius-lg shadow">
                                </div>
                                <div class="d-flex align-items-start flex-column justify-content-center">
                                    <h6 class="mb-0 text-sm">Nick Daniel</h6>
                                    <p class="mb-0 text-xs">Hi! I need more information..</p>
                                </div>
                                <a class="btn btn-link pe-3 ps-0 mb-0 ms-auto" href="javascript:;">Reply</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>--%>
            <div class="col-12 mt-4">
                <div class="card mb-4">
                    <div class="card-header pb-0 p-3">
                        <h6 class="mb-1">Projects</h6>
                        <p class="text-sm">Architects design houses</p>
                    </div>
                    <div class="card-body p-3">
                        <div class="row">
                            <div class="col-xl-3 col-md-6 mb-xl-0 mb-4">
                                <div class="card card-blog card-plain">
                                    <div class="position-relative">
                                        <a class="d-block shadow-xl border-radius-xl">
                                            <img src="../assets/img/home-decor-1.jpg" alt="img-blur-shadow" class="img-fluid shadow border-radius-xl">
                                        </a>
                                    </div>
                                    <div class="card-body px-1 pb-0">
                                        <p class="text-gradient text-dark mb-2 text-sm">Project #2</p>
                                        <a href="javascript:;">
                                            <h5>Modern
                                            </h5>
                                        </a>
                                        <p class="mb-4 text-sm">
                                            As Uber works through a huge amount of internal management turmoil.
                                        </p>
                                        <div class="d-flex align-items-center justify-content-between">
                                            <button type="button" class="btn btn-outline-primary btn-sm mb-0">View Project</button>
                                            <div class="avatar-group mt-2">
                                                <a href="javascript:;" class="avatar avatar-xs rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Elena Morison">
                                                    <img alt="Image placeholder" src="../assets/img/team-1.jpg">
                                                </a>
                                                <a href="javascript:;" class="avatar avatar-xs rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Ryan Milly">
                                                    <img alt="Image placeholder" src="../assets/img/team-2.jpg">
                                                </a>
                                                <a href="javascript:;" class="avatar avatar-xs rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Nick Daniel">
                                                    <img alt="Image placeholder" src="../assets/img/team-3.jpg">
                                                </a>
                                                <a href="javascript:;" class="avatar avatar-xs rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Peterson">
                                                    <img alt="Image placeholder" src="../assets/img/team-4.jpg">
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6 mb-xl-0 mb-4">
                                <div class="card card-blog card-plain">
                                    <div class="position-relative">
                                        <a class="d-block shadow-xl border-radius-xl">
                                            <img src="../assets/img/home-decor-2.jpg" alt="img-blur-shadow" class="img-fluid shadow border-radius-lg">
                                        </a>
                                    </div>
                                    <div class="card-body px-1 pb-0">
                                        <p class="text-gradient text-dark mb-2 text-sm">Project #1</p>
                                        <a href="javascript:;">
                                            <h5>Scandinavian
                                            </h5>
                                        </a>
                                        <p class="mb-4 text-sm">
                                            Music is something that every person has his or her own specific opinion about.
                                        </p>
                                        <div class="d-flex align-items-center justify-content-between">
                                            <button type="button" class="btn btn-outline-primary btn-sm mb-0">View Project</button>
                                            <div class="avatar-group mt-2">
                                                <a href="javascript:;" class="avatar avatar-xs rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Nick Daniel">
                                                    <img alt="Image placeholder" src="../assets/img/team-3.jpg">
                                                </a>
                                                <a href="javascript:;" class="avatar avatar-xs rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Peterson">
                                                    <img alt="Image placeholder" src="../assets/img/team-4.jpg">
                                                </a>
                                                <a href="javascript:;" class="avatar avatar-xs rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Elena Morison">
                                                    <img alt="Image placeholder" src="../assets/img/team-1.jpg">
                                                </a>
                                                <a href="javascript:;" class="avatar avatar-xs rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Ryan Milly">
                                                    <img alt="Image placeholder" src="../assets/img/team-2.jpg">
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6 mb-xl-0 mb-4">
                                <div class="card card-blog card-plain">
                                    <div class="position-relative">
                                        <a class="d-block shadow-xl border-radius-xl">
                                            <img src="../assets/img/home-decor-3.jpg" alt="img-blur-shadow" class="img-fluid shadow border-radius-xl">
                                        </a>
                                    </div>
                                    <div class="card-body px-1 pb-0">
                                        <p class="text-gradient text-dark mb-2 text-sm">Project #3</p>
                                        <a href="javascript:;">
                                            <h5>Minimalist
                                            </h5>
                                        </a>
                                        <p class="mb-4 text-sm">
                                            Different people have different taste, and various types of music.
                                        </p>
                                        <div class="d-flex align-items-center justify-content-between">
                                            <button type="button" class="btn btn-outline-primary btn-sm mb-0">View Project</button>
                                            <div class="avatar-group mt-2">
                                                <a href="javascript:;" class="avatar avatar-xs rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Peterson">
                                                    <img alt="Image placeholder" src="../assets/img/team-4.jpg">
                                                </a>
                                                <a href="javascript:;" class="avatar avatar-xs rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Nick Daniel">
                                                    <img alt="Image placeholder" src="../assets/img/team-3.jpg">
                                                </a>
                                                <a href="javascript:;" class="avatar avatar-xs rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Ryan Milly">
                                                    <img alt="Image placeholder" src="../assets/img/team-2.jpg">
                                                </a>
                                                <a href="javascript:;" class="avatar avatar-xs rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Elena Morison">
                                                    <img alt="Image placeholder" src="../assets/img/team-1.jpg">
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6 mb-xl-0 mb-4">
                                <div class="card h-100 card-plain border">
                                    <div class="card-body d-flex flex-column justify-content-center text-center">
                                        <a href="javascript:;">
                                            <i class="fa fa-plus text-secondary mb-3"></i>
                                            <h5 class=" text-secondary">New project </h5>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Fim de Profile Div -->
    <!-- Registration Completion -->
    <div id="registration" class="container-fluid mt-4 py-4 hidden">
        <!-- Begin Registration Page 1-->
        <div id="registerCompletionpage1">
            <div class="card">
                <div class="card-header pb-0 p-3">
                    <h6 class="mb-1">Completa o teu registo</h6>
                    <p class="text-sm">Preenche todos os campos da tabela abaixo</p>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-9">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row px-xl-5 px-sm-4 px-3">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblNome" runat="server" AssociatedControlID="tbNome">Nome</asp:Label>
                                                <asp:RequiredFieldValidator ValidationGroup="Page1" ID="rfvNome" Text="*" ErrorMessage="Nome Completo Obrigatório" runat="server" ControlToValidate="tbNome" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="tbNome" runat="server" ValidationGroup="Page1" CssClass="form-control" placeholder="Nome Completo"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Label ID="lblSexo" runat="server" AssociatedControlID="ddlSexo">Sexo</asp:Label>
                                                <asp:DropDownList ID="ddlSexo" runat="server" ValidationGroup="Page1" CssClass="form-control">
                                                    <asp:ListItem Value="0">Feminino</asp:ListItem>
                                                    <asp:ListItem Value="1">Masculino</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="lblDataNascimento" runat="server" AssociatedControlID="tbDataNascimento">Data de Nascimento</asp:Label>
                                                <div class="input-group">
                                                    <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                                                    <asp:TextBox ID="tbDataNascimento" ValidationGroup="Page1" runat="server" CssClass="form-control datepicker" TextMode="DateTime"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row px-xl-5 px-sm-4 px-3">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="lbltipoDocumento" runat="server" AssociatedControlID="ddlDocumentoIdent">Documento de Identificação</asp:Label>
                                                <asp:RequiredFieldValidator ID="rfvDocumentIdent" Text="*" ErrorMessage="Documento de Identificação Obrigatório" ValidationGroup="Page1" runat="server" ControlToValidate="ddlDocumentoIdent" InitialValue="" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlDocumentoIdent" ValidationGroup="Page1" runat="server" CssClass="form-control" DataSourceID="SQLDSDocIdent" DataTextField="tipoDocumentoIdent" DataValueField="codTipoDoc">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="SQLDSDocIdent" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [tipoDocIdent]"></asp:SqlDataSource>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="lblCC" runat="server" AssociatedControlID="tbCC">Nr.º do Documento</asp:Label>
                                                <asp:RequiredFieldValidator ID="rfvCC" Text="*" ValidationGroup="Page1" ErrorMessage="Nrº do Documento Obrigatório" runat="server" ControlToValidate="tbCC" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="tbCC" runat="server" ValidationGroup="Page1" CssClass="form-control" placeholder="e.g., 123456789 Z Z1Z"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="lblDataValidade" runat="server" AssociatedControlID="tbDataValidade">Data de Validade</asp:Label>
                                                <asp:RequiredFieldValidator ID="rfvDataValidade" Text="*" ValidationGroup="Page1" ErrorMessage="Data de Validade Obrigatória" runat="server" ControlToValidate="tbDataValidade" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                <div class="input-group">
                                                    <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                                                    <asp:TextBox ID="tbDataValidade" ValidationGroup="Page1" runat="server" CssClass="form-control datepicker" TextMode="DateTime"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row px-xl-5 px-sm-4 px-3">
                                        <div class="col-md-7">
                                            <div class="form-group">
                                                <asp:Label ID="lblNrSegSocial" runat="server" AssociatedControlID="tbNrSegSocial">Número de Segurança Social</asp:Label>
                                                <asp:TextBox ID="tbNrSegSocial" ValidationGroup="Page1" runat="server" CssClass="form-control" placeholder="12345678910"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <asp:Label ID="lblNIF" runat="server" AssociatedControlID="tbNIF">NIF</asp:Label>
                                                <asp:TextBox ID="tbNIF" ValidationGroup="Page1" runat="server" CssClass="form-control" placeholder="123456789"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row px-xl-5 px-sm-4 px-3">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lblMorada" runat="server" AssociatedControlID="tbMorada">Morada</asp:Label>
                                                <asp:TextBox ID="tbMorada" ValidationGroup="Page1" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Introduza a sua morada completa"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row px-xl-5 px-sm-4 px-3">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="lblCodPostal" runat="server" AssociatedControlID="tbCodPostal">Código-Postal</asp:Label>
                                                <asp:TextBox runat="server" ID="tbCodPostal" CssClass="form-control" placeholder="0000-000"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="lblfreguesia" runat="server" AssociatedControlID="tbLocalidade">Localidade</asp:Label>
                                                <asp:TextBox runat="server" ID="tbLocalidade" CssClass="form-control" ValidationGroup="Page1"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="lblCodPais" runat="server" AssociatedControlID="ddlCodPais">País</asp:Label>
                                                <asp:DropDownList ID="ddlCodPais" ValidationGroup="Page1" runat="server" CssClass="form-control" DataSourceID="SQLDSPais" DataTextField="nomePT" DataValueField="codPais">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="SQLDSPais" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [pais] ORDER BY nomePT"></asp:SqlDataSource>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card card-footer">
                                        <div class="col-md-12 align-items-start">
                                            <asp:Button runat="server" ID="btnBackMainPage" ValidationGroup="Page1" OnClientClick="showMainPage();" CausesValidation="False" class="btn btn-outline-primary btn-sm mb-0" Text="Voltar" />
                                            &nbsp;
                                            <asp:Button runat="server" ID="btnNextPage" ValidationGroup="Page1" OnClientClick="showNextDiv(); return false;" CausesValidation="True" class="btn btn-outline-primary btn-sm mb-0" Text="Seguinte" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBackMainPage"/>
                                    <asp:AsyncPostBackTrigger ControlID="btnNextPage" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-3">
                            <div>
                                <div class="alert text-white font-weight-bold" role="alert">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Page1" ForeColor="#cc3a60" DisplayMode="List" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- End Registration Page 1-->

        <!-- Begin Registration Page 2-->
        <div id="registerCompletionpage2" class="hidden">
            <div class="card">
                <div class="card-header pb-0 p-3">
                    <h6 class="mb-1">Completa o teu registo</h6>
                    <p class="text-sm">Preenche todos os campos da tabela abaixo</p>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-9">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>

                                    <div class="row px-xl-5 px-sm-4 px-3">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="lblCodEstadoCivil" runat="server" AssociatedControlID="ddlCodEstadoCivil">Estado Civil</asp:Label>
                                                <asp:DropDownList ID="ddlCodEstadoCivil" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSEstadoCivil" DataTextField="estadoCivil" DataValueField="codEstadoCivil">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="SQLDSEstadoCivil" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [estadoCivil]"></asp:SqlDataSource>
                                            </div>
                                        </div>

                                        <div class="col-md-9">
                                            <div class="form-group">
                                                <asp:Label ID="lblIBAN" runat="server" AssociatedControlID="tbIBAN">IBAN</asp:Label>
                                                <asp:TextBox ID="tbIBAN" runat="server" ValidationGroup="Page2" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row px-xl-5 px-sm-4 px-3">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="lblCodNaturalidade" runat="server" AssociatedControlID="tbNaturalidade">Naturalidade</asp:Label>
                                                <asp:TextBox runat="server" ID="tbNaturalidade" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="lblCodNacionalidade" runat="server" AssociatedControlID="ddlCodNacionalidade">Nacionalidade</asp:Label>
                                                <asp:DropDownList ID="ddlCodNacionalidade" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSPais" DataTextField="nacionalidade" DataValueField="codPais">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <asp:Label ID="lblCodSituacaoProfissional" runat="server" AssociatedControlID="ddlCodSituacaoProfissional">Situação Profissional</asp:Label>
                                                <asp:DropDownList ID="ddlCodSituacaoProfissional" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSsituacaoProfissional" DataTextField="situacaoProfissional" DataValueField="codSituacaoProfissional">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="SQLDSsituacaoProfissional" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [situacaoProfissional]"></asp:SqlDataSource>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row px-xl-5 px-sm-4 px-3">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="lblCodPrefixo" runat="server" AssociatedControlID="ddlprefixo">Prefixo</asp:Label>
                                                <asp:DropDownList ID="ddlprefixo" CssClass="form-control" runat="server" DataSourceID="SQLDSPrefixo" DataTextField="prefixo" DataValueField="codPais"></asp:DropDownList>
                                                <asp:SqlDataSource ID="SQLDSPrefixo" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT codPais,CONCAT(nomePT, ': ' , prefixo) AS prefixo FROM [pais] order by nomePT"></asp:SqlDataSource>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="lblTelemovel" runat="server" AssociatedControlID="tbTelemovel">Telemóvel</asp:Label>
                                                <asp:RequiredFieldValidator ID="rfvTelemovel" ValidationGroup="Page2" runat="server" ControlToValidate="tbTelemovel" ForeColor="#cc3a60" Text="*" ErrorMessage="Telemóvel Obrigatório"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="tbTelemovel" runat="server" ValidationGroup="Page2" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="tbEmail">Email</asp:Label>
                                                <asp:RequiredFieldValidator ID="rfvEmail" ErrorMessage="E-mail Obrigatório" ValidationGroup="Page2" runat="server" ControlToValidate="tbEmail" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="tbEmail" runat="server" ValidationGroup="Page2" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row px-xl-5 px-sm-4 px-3">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="lblCodGrauAcademico" runat="server" AssociatedControlID="ddlCodGrauAcademico">Grau Académico</asp:Label>
                                                <asp:DropDownList ID="ddlCodGrauAcademico" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSGrauAcademico" DataTextField="grauAcademico" DataValueField="codGrauAcademico">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="SQLDSGrauAcademico" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [grauAcademico]"></asp:SqlDataSource>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="lblFoto" runat="server" AssociatedControlID="fuFoto">Foto</asp:Label>
                                                <asp:FileUpload ID="fuFoto" ValidationGroup="Page2" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="lblAnexo" runat="server" AssociatedControlID="fuAnexo">Anexo</asp:Label>
                                                <asp:FileUpload ID="fuAnexo" ValidationGroup="Page2" runat="server" CssClass="form-control" AllowMultiple="True" />
                                                <small>CV, Documento de Identificação</small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row px-xl-5 px-sm-4 px-3">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lblLifeMotto" runat="server" AssociatedControlID="tbCodPostal">Life Motto</asp:Label>
                                                <asp:TextBox runat="server" ID="tbLifeMotto" CssClass="form-control" placeholder="Vini, Vidi, Vici"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card card-footer">
                                        <div class="col-md-12 align-items-center">
                                            <asp:Button runat="server" ID="btnBackToPage1" ValidationGroup="Page2" OnClientClick="showPrevDiv(); return false;" CausesValidation="False" class="btn btn-outline-primary btn-sm mb-0" Text="Voltar" />
                                            &nbsp;

                                            <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" ValidationGroup="Page2" CausesValidation="True" class="btn btn-outline-primary btn-sm mb-0" Text="Submeter"></asp:Button>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-3">
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Page2" ForeColor="#cc3a60" DisplayMode="List" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- End Registration Page 2-->
    </div>
    <div id="registrationMessage" class="hidden">
        <div class="alert alert-primary text-white font-weight-bold" role="alert">
            <small class="text-uppercase font-weight-bold">
                <asp:Label runat="server" ID="lbl_message"></asp:Label></small>
        </div>
    </div>
    <!-- End of Registration Completion -->

    <div id="ChangePw" class="hidden">
        <div class="row ">
            <div class="col-xl-4 col-lg-5 col-md-6 d-flex flex-column mx-auto">
                <div class="card card-plain">
                    <div class="card-header pb-0 text-left bg-transparent">
                        <h3 class="font-weight-bolder text-primary text-gradient">Alterar a password</h3>
                        <p class="mb-0">Introduza a sua palavra-passe atual e a sua nova palavra-passe para alterar</p>
                    </div>
                    <div class="card-body">
                        <div role="form">
                            <label>Password Atual</label>
                            <asp:RequiredFieldValidator ID="rfvpwa" runat="server" ErrorMessage="Palavra-passe Obrigatória" Text="*" ControlToValidate="tbPwOld" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                            <div class="mb-3">
                                <asp:TextBox ID="tbPwOld" oninput="validatePassword(this)" CssClass="form-control" placeholder="Password Atual" runat="server"></asp:TextBox>
                            </div>
                            <label>Nova Password</label>
                            <asp:RequiredFieldValidator ID="rfvpw" runat="server" ErrorMessage="Palavra-passe Obrigatória" Text="*" ControlToValidate="tbPwNew" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                            <div class="mb-3">
                                <asp:TextBox ID="tbPwNew" oninput="validatePassword(this)" CssClass="form-control" placeholder="Nova Password" runat="server"></asp:TextBox>
                            </div>
                            <label>Repetir a Nova Password</label>
                            <asp:RequiredFieldValidator ID="rfvpwr" runat="server" ErrorMessage="Palavra-passe Obrigatória" Text="*" ControlToValidate="tbPwNewRep" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                            <div class="mb-3">
                                <asp:TextBox ID="tbPwNewRep" oninput="validatePassword(this)" CssClass="form-control" placeholder="Repita a Password" runat="server"></asp:TextBox>
                            </div>
                            <div class="text-center col-md-12">
                                <asp:Button ID="btnChangePW" runat="server" Text="Alterar Password" class="btn bg-gradient-primary w-100 mt-4 mb-0" OnClick="btnChangePW_Click" />
                                <asp:Button ID="btnBackFromPwChange" runat="server" Text="Voltar" OnClientClick="showMainPage(); return false;" CausesValidation="false" class="btn bg-gradient-primary w-100 mt-4 mb-0" />
                            </div>
                            <div style="padding: 5px;" id="alert" class="hidden" role="alert">
                                <asp:Label runat="server" ID="Label1" CssClass="text-white"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--Javascript do Datepickr -->
    <script>
        flatpickr('#<%= tbDataNascimento.ClientID %>', {
            // Options
            dateFormat: 'd-m-Y',
            theme: 'light',
            maxDate: new Date()
        });

        flatpickr('#<%= tbDataValidade.ClientID %>', {
            // Options
            dateFormat: 'd-m-Y',
            theme: 'light',
            minDate: new Date()
        });
    </script>

    <!--Javascript para aceder aos detalhes do perfil-->
    <script>
        function showEdit(event) {
            event.preventDefault();
            // Remove 'show' class and add 'hide' class to div1
            document.getElementById('profileSinapse').classList.add('hidden');

            // Remove 'hide' class and add 'show' class to div2
            document.getElementById('registration').classList.remove('hidden');
        }
    </script>
    
    <script>
        function showMainPage() {
            event.preventDefault();

            document.getElementById('registration').classList.add('hidden');

            document.getElementById('profileSinapse').classList.remove('hidden');
        }
    </script>


    <!--Javascript para mostrar a div de mudar a pw-->
    <script>
        function showChangePw(event) {
            event.preventDefault();
            // Remove 'show' class and add 'hide' class to div1
            document.getElementById('profileSinapse').classList.add('hidden');

            // Remove 'hide' class and add 'show' class to div2
            document.getElementById('ChangePw').classList.remove('hidden');
        }
    </script>

    <!-- Javascript para mostrar a página dois do form -->
    <script>
        function showNextDiv() {
            // Check if all validators are valid
            var isValid = Page_ClientValidate('Page1');

            // If validators are valid, proceed to show next div
            if (isValid) {
                // Remove 'show' class and add 'hide' class to div1
                document.getElementById('registerCompletionpage1').classList.remove('card');
                document.getElementById('registerCompletionpage1').classList.add('hidden');

                // Remove 'hide' class and add 'show' class to div2
                document.getElementById('registerCompletionpage2').classList.remove('hidden');
                document.getElementById('registerCompletionpage2').classList.add('card');
            }
        }
    </script>

    <!-- Javascript para mostrar a página um do form se carregado o botão voltar-->
    <script>
        function showPrevDiv() {
            // Remove 'show' class and add 'hide' class to div2
            document.getElementById('registerCompletionpage2').classList.remove('card');
            document.getElementById('registerCompletionpage2').classList.add('hidden');

            // Remove 'hide' class and add 'show' class to div1
            document.getElementById('registerCompletionpage1').classList.remove('hidden');
            document.getElementById('registerCompletionpage1').classList.add('card');
        }
    </script>

    <!-- Javascript para mostrar a página anterior -->
    <script>
        function submitInfo() {
            // Check if all validators are valid
            var isValid = Page_ClientValidate('Page2');

            // If validators are valid, proceed to show next div
            if (isValid) {
                // Remove 'hide' class and add 'show' class to div2
                document.getElementById('registrationMessage').classList.remove('hidden');
            }
        }
    </script>
</asp:Content>
