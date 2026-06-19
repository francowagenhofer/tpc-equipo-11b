<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="Presentación.ConfiguracionHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <div class="card card-custom shadow-sm border-0 p-4">

        <div class="mb-4">
            <h2 class="fw-bold mb-1">
                <i class="bi bi-gear-fill text-primary me-2"></i>
                Configuración del Sistema
            </h2>

            <p class="text-muted mb-0">
                Administración de catálogos y configuraciones generales de la clínica.       
            </p>
        </div>

        <!-- Buscador -->

        <div class="row mb-5">

            <div class="col-md-5">

                <div class="input-group">

                    <span class="input-group-text bg-white border-end-0">
                        <i class="bi bi-search text-muted"></i>
                    </span>

                    <input type="text"
                        class="form-control border-start-0"
                        placeholder="Buscar configuración..." />
                </div>

            </div>

        </div>

        <!-- Catálogos -->

        <h5 class="fw-bold mb-3">Catálogos</h5>

        <div class="row g-4">

            <!-- Obras Sociales -->
            <div class="col-xl-3 col-lg-4 col-md-6">
                <div class="card h-100 border-0 shadow-sm">
                    <div class="card-body text-center">

                        <i class="bi bi-hospital fs-1 text-primary"></i>

                        <h5 class="mt-3">Obras Sociales</h5>

                        <p class="text-muted small">
                            Administración de obras sociales.
                        </p>

                        <a runat="server" href="~/Configuracion/ObrasSociales.aspx"
                            class="btn btn-outline-primary btn-sm">Gestionar </a>

                    </div>
                </div>
            </div>

            <!-- Roles -->
            <div class="col-xl-3 col-lg-4 col-md-6">
                <div class="card h-100 border-0 shadow-sm">
                    <div class="card-body text-center">

                        <i class="bi bi-people-fill fs-1 text-primary"></i>

                        <h5 class="mt-3">Roles
                        </h5>

                        <p class="text-muted small">
                            Administración de roles.                   
                        </p>

                        <a runat="server" href="~/Configuracion/Roles.aspx"
                            class="btn btn-outline-primary btn-sm">Gestionar</a>

                    </div>
                </div>
            </div>

            <!-- Estados -->
            <div class="col-xl-3 col-lg-4 col-md-6">
                <div class="card h-100 border-0 shadow-sm">
                    <div class="card-body text-center">
                        <i class="bi bi-calendar-check fs-1 text-primary"></i>

                        <h5 class="mt-3">Estados de Turno</h5>

                        <p class="text-muted small">
                            Administración de estados.
                        </p>

                        <a runat="server" href="~/Configuracion/EstadosTurno.aspx"
                            class="btn btn-outline-primary btn-sm">Gestionar</a>

                    </div>
                </div>
            </div>

            <!-- Géneros -->
            <div class="col-xl-3 col-lg-4 col-md-6">
                <div class="card h-100 border-0 shadow-sm">
                    <div class="card-body text-center">

                        <i class="bi bi-gender-ambiguous fs-1 text-primary"></i>

                        <h5 class="mt-3">Géneros
                        </h5>

                        <p class="text-muted small">
                            Administración de géneros.
                        </p>

                        <a runat="server" href="~/Configuracion/Generos.aspx"
                            class="btn btn-outline-primary btn-sm">Gestionar</a>

                    </div>
                </div>
            </div>

        </div>

    </div>

</asp:Content>
