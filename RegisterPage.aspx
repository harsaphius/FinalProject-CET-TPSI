<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="FinalProject.RegisterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .registration-form {
            margin: 20px auto;
            padding: 20px;
            background-color: #eeefef;
            border: 1px solid #dee2e6;
            border-radius: 5px;
            max-width: 1000px;
        }

            .registration-form h2 {
                text-align: center;
                margin-bottom: 20px;
            }

        .formR {
            padding: 0 !important;
        }

        .form-group label {
            font-weight: bold;
        }

        .form-control {
            width: 100%;
            padding: 10px;
            font-size: 16px;
            border: 1px solid #ced4da;
            border-radius: 4px;
        }

        @media (min-width: 576px) {
            .form-group {
                margin-bottom: 15px;
                /* Increased margin between form groups on larger screens */
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container registration-form">
        <h2>Registration Form</h2>

        <div class="row formR">
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblNome" runat="server" AssociatedControlID="txtNome">Nome</asp:Label>
                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome" ErrorMessage="Nome é obrigatório" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblSexo" runat="server" AssociatedControlID="ddlSexo">Sexo</asp:Label>
                    <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvSexo" runat="server" ControlToValidate="ddlSexo" InitialValue="" ErrorMessage="Sexo é obrigatório" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblDataNascimento" runat="server" AssociatedControlID="txtDataNascimento">Data de Nascimento</asp:Label>
                    <asp:TextBox ID="txtDataNascimento" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDataNascimento" runat="server" ControlToValidate="txtDataNascimento" ErrorMessage="Data de Nascimento é obrigatória" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="row formR">
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblCC" runat="server" AssociatedControlID="txtCC">CC</asp:Label>
                    <asp:TextBox ID="txtCC" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCC" runat="server" ControlToValidate="txtCC" ErrorMessage="CC é obrigatório" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblDataValidadeCC" runat="server" AssociatedControlID="txtDataValidadeCC">Data de Validade do CC</asp:Label>
                    <asp:TextBox ID="txtDataValidadeCC" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDataValidadeCC" runat="server" ControlToValidate="txtDataValidadeCC" ErrorMessage="Data de Validade do CC é obrigatória" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblNIF" runat="server" AssociatedControlID="txtNIF">NIF</asp:Label>
                    <asp:TextBox ID="txtNIF" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNIF" runat="server" ControlToValidate="txtNIF" ErrorMessage="NIF é obrigatório" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="row formR">
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblMorada" runat="server" AssociatedControlID="txtMorada">Morada</asp:Label>
                    <asp:TextBox ID="txtMorada" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvMorada" runat="server" ControlToValidate="txtMorada" ErrorMessage="Morada é obrigatória" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblCodPais" runat="server" AssociatedControlID="ddlCodPais">Código do País</asp:Label>
                    <asp:DropDownList ID="ddlCodPais" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvCodPais" runat="server" ControlToValidate="ddlCodPais" InitialValue="" ErrorMessage="Código do País é obrigatório" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblCodCodPostal" runat="server" AssociatedControlID="txtCodCodPostal">Código do Código Postal</asp:Label>
                    <asp:TextBox ID="txtCodCodPostal" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCodCodPostal" runat="server" ControlToValidate="txtCodCodPostal" ErrorMessage="Código do Código Postal é obrigatório" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="row formR">
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblCodEstadoCivil" runat="server" AssociatedControlID="ddlCodEstadoCivil">Código do Estado Civil</asp:Label>
                    <asp:DropDownList ID="ddlCodEstadoCivil" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvCodEstadoCivil" runat="server" ControlToValidate="ddlCodEstadoCivil" InitialValue="" ErrorMessage="Código do Estado Civil é obrigatório" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblNrSegSocial" runat="server" AssociatedControlID="txtNrSegSocial">Número de Segurança Social</asp:Label>
                    <asp:TextBox ID="txtNrSegSocial" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNrSegSocial" runat="server" ControlToValidate="txtNrSegSocial" ErrorMessage="Número de Segurança Social é obrigatório" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblIBAN" runat="server" AssociatedControlID="txtIBAN">IBAN</asp:Label>
                    <asp:TextBox ID="txtIBAN" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvIBAN" runat="server" ControlToValidate="txtIBAN" ErrorMessage="IBAN é obrigatório" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="row formR">
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblCodNaturalidade" runat="server" AssociatedControlID="ddlCodNaturalidade">Código da Naturalidade</asp:Label>
                    <asp:DropDownList ID="ddlCodNaturalidade" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvCodNaturalidade" runat="server" ControlToValidate="ddlCodNaturalidade" InitialValue="" ErrorMessage="Código da Naturalidade é obrigatório" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblCodNacionalidade" runat="server" AssociatedControlID="ddlCodNacionalidade">Código da Nacionalidade</asp:Label>
                    <asp:DropDownList ID="ddlCodNacionalidade" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvCodNacionalidade" runat="server" ControlToValidate="ddlCodNacionalidade" InitialValue="" ErrorMessage="Código da Nacionalidade é obrigatório" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblCodPrefixo" runat="server" AssociatedControlID="txtCodPrefixo">Código do Prefixo</asp:Label>
                    <asp:TextBox ID="txtCodPrefixo" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCodPrefixo" runat="server" ControlToValidate="txtCodPrefixo" ErrorMessage="Código do Prefixo é obrigatório" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="row formR">
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblTelemovel" runat="server" AssociatedControlID="txtTelemovel">Telemóvel</asp:Label>
                    <asp:TextBox ID="txtTelemovel" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTelemovel" runat="server" ControlToValidate="txtTelemovel" ErrorMessage="Telemóvel é obrigatório" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail">Email</asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email é obrigatório" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblFoto" runat="server" AssociatedControlID="fuFoto">Foto</asp:Label>
                    <asp:FileUpload ID="fuFoto" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvFoto" runat="server" ControlToValidate="fuFoto" ErrorMessage="Foto é obrigatória" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="row formR">
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblCodGrauAcademico" runat="server" AssociatedControlID="ddlCodGrauAcademico">Código do Grau Académico</asp:Label>
                    <asp:DropDownList ID="ddlCodGrauAcademico" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvCodGrauAcademico" runat="server" ControlToValidate="ddlCodGrauAcademico" InitialValue="" ErrorMessage="Código do Grau Académico é obrigatório" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblCodSituacaoProfissional" runat="server" AssociatedControlID="ddlCodSituacaoProfissional">Código da Situação Profissional</asp:Label>
                    <asp:DropDownList ID="ddlCodSituacaoProfissional" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvCodSituacaoProfissional" runat="server" ControlToValidate="ddlCodSituacaoProfissional" InitialValue="" ErrorMessage="Código da Situação Profissional é obrigatório" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblAnexo" runat="server" AssociatedControlID="fuAnexo">Anexo</asp:Label>
                    <asp:FileUpload ID="fuAnexo" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvAnexo" runat="server" ControlToValidate="fuAnexo" ErrorMessage="Anexo é obrigatório" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn" />
    </div>

</asp:Content>
