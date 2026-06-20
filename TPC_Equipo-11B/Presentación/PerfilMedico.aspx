<%@ Page Title="Perfil Médico" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="PerfilMedico.aspx.cs" Inherits="Presentación.PerfilMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .perfil-header {
            background: white;
            border-radius: 12px;
            padding: 2rem;
            margin-bottom: 1.5rem;
        }

        .avatar-medico {
            width: 80px;
            height: 80px;
            background: #0d6efd;
            color: white;
            border-radius: 50%;
            font-size: 2rem;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .card-info {
            border: none;
            border-radius: 12px;
        }

        .dato-label {
            font-size: .85rem;
            color: #6c757d;
            margin-bottom: .2rem;
        }

        .dato-valor {
            font-weight: 600;
            margin-bottom: 1rem;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <div class="container-fluid">

        <!-- CABECERA -->

        <div class="perfil-header shadow-sm">

            <div class="d-flex align-items-center">

                <div class="avatar-medico me-4">
                    <i class="bi bi-person-fill"></i>
                </div>

                <div>

                    <h2 class="fw-bold mb-1">
                        <asp:Label ID="lblNombreCompleto" runat="server" />
                    </h2>

                    <p class="text-muted mb-1">
                        <asp:Label ID="lblEspecialidad" runat="server" />
                    </p>

                    <span class="badge bg-success">
                        <asp:Label ID="lblEstado" runat="server" />
                    </span>

                </div>

            </div>

        </div>

        <div class="row">

            <!-- DATOS PERSONALES -->

            <div class="col-md-6 mb-4">

                <div class="card card-info shadow-sm">

                    <div class="card-header bg-white">
                        <h5 class="mb-0">
                            <i class="bi bi-person-vcard me-2"></i>
                            Datos Personales
                        </h5>
                    </div>

                    <div class="card-body">

                        <div class="dato-label">Nombre</div>
                        <div class="dato-valor">
                            <asp:Label ID="lblNombre" runat="server" />
                        </div>

                        <div class="dato-label">Apellido</div>
                        <div class="dato-valor">
                            <asp:Label ID="lblApellido" runat="server" />
                        </div>

                        <div class="dato-label">Email</div>
                        <div class="dato-valor">
                            <asp:Label ID="lblEmail" runat="server" />
                        </div>

                        <div class="dato-label">Teléfono</div>
                        <div class="dato-valor">
                            <asp:Label ID="lblTelefono" runat="server" />
                        </div>

                    </div>

                </div>

            </div>

            <!-- DATOS PROFESIONALES -->

            <div class="col-md-6 mb-4">

                <div class="card card-info shadow-sm">

                    <div class="card-header bg-white">
                        <h5 class="mb-0">
                            <i class="bi bi-heart-pulse me-2"></i>
                            Datos Profesionales
                        </h5>
                    </div>

                    <div class="card-body">

                        <div class="dato-label">Matrícula</div>
                        <div class="dato-valor">
                            <asp:Label ID="lblMatricula" runat="server" />
                        </div>

                        <div class="dato-label">Especialidad</div>
                        <div class="dato-valor">
                            <asp:Label ID="lblEspecialidadDetalle" runat="server" />
                        </div>

                        <div class="dato-label">Estado</div>
                        <div class="dato-valor">
                            <asp:Label ID="lblEstadoDetalle" runat="server" />
                        </div>

                    </div>

                </div>

            </div>

        </div>

        <!-- BOTONES -->

        <div class="d-flex justify-content-end gap-2">

            <asp:Button
                ID="btnVolver"
                runat="server"
                Text="Volver"
                CssClass="btn btn-outline-secondary"
                PostBackUrl="~/Medicos.aspx" />

            <asp:Button
                ID="btnEditar"
                runat="server"
                Text="Editar Médico"
                CssClass="btn btn-primary" />

        </div>

    </div>

</asp:Content>