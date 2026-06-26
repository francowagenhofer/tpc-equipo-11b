<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="MiAgenda.aspx.cs" Inherits="Presentación.MiAgenda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .estado-pendiente {
            background-color: #fff3cd;
            color: #856404;
            padding: 0.35em 0.65em;
            border-radius: 50rem;
            font-size: 0.85em;
            font-weight: 600;
        }

        .estado-confirmado {
            background-color: #d4edda;
            color: #155724;
            padding: 0.35em 0.65em;
            border-radius: 50rem;
            font-size: 0.85em;
            font-weight: 600;
        }

        .estado-cancelado {
            background-color: #f8d7da;
            color: #721c24;
            padding: 0.35em 0.65em;
            border-radius: 50rem;
            font-size: 0.85em;
            font-weight: 600;
        }

        .estado-reprogramado {
            background-color: #cce5ff;
            color: #004085;
            padding: 0.35em 0.65em;
            border-radius: 50rem;
            font-size: 0.85em;
            font-weight: 600;
        }

        .estado-finalizado {
            background-color: #e2e3e5;
            color: #383d41;
            padding: 0.35em 0.65em;
            border-radius: 50rem;
            font-size: 0.85em;
            font-weight: 600;
        }

        .estado-noasistio {
            background-color: #f5c6cb;
            color: #721c24;
            padding: 0.35em 0.65em;
            border-radius: 50rem;
            font-size: 0.85em;
            font-weight: 600;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <div class="card card-custom p-4 shadow-sm border-0">

        <div class="d-flex justify-content-between align-items-center mb-4">
            <div>
                <h2 class="fw-bold mb-1">Mi Agenda</h2>
                <p class="text-muted mb-0">
                    Consultá y administrá los turnos asignados.
                </p>
            </div>
        </div>

        <!-- Tarjetas resumen -->

        <div class="row g-3 mb-4">

            <div class="col-md-3">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-body">
                        <small class="text-muted">Turnos de hoy</small>
                        <h3 class="fw-bold text-primary mb-0">
                            <asp:Label ID="lblTurnosHoy" runat="server" Text="0" />
                        </h3>
                    </div>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-body">
                        <small class="text-muted">Pendientes</small>
                        <h3 class="fw-bold text-warning mb-0">
                            <asp:Label ID="lblPendientes" runat="server" Text="0" />
                        </h3>
                    </div>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-body">
                        <small class="text-muted">Confirmados</small>
                        <h3 class="fw-bold text-info mb-0">
                            <asp:Label ID="lblConfirmados" runat="server" Text="0" />
                        </h3>
                    </div>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-body">
                        <small class="text-muted">Finalizados</small>
                        <h3 class="fw-bold text-success mb-0">
                            <asp:Label ID="lblFinalizados" runat="server" Text="0" />
                        </h3>
                    </div>
                </div>
            </div>

        </div>

        <!-- Filtros -->

        <div class="row g-3 mb-4">

            <div class="col-md-3">
                <label class="form-label fw-semibold text-muted small">
                    Fecha
                </label>

                <asp:TextBox
                    ID="txtFecha"
                    runat="server"
                    CssClass="form-control"
                    TextMode="Date"
                    AutoPostBack="true"
                    OnTextChanged="txtFecha_TextChanged" />
            </div>

            <div class="col-md-3">

                <label class="form-label fw-semibold text-muted small">
                    Estado
                </label>

                <asp:DropDownList
                    ID="ddlEstado"
                    runat="server"
                    CssClass="form-select"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">

                    <asp:ListItem Text="Todos los estados" Value="0" />

                </asp:DropDownList>

            </div>

            <div class="col-md-4">

                <label class="form-label fw-semibold text-muted small">
                    Buscar paciente
                </label>

                <div class="input-group">

                    <span class="input-group-text bg-white border-end-0">
                        <i class="bi bi-search text-muted"></i>
                    </span>

                    <asp:TextBox
                        ID="txtBuscar"
                        runat="server"
                        CssClass="form-control border-start-0"
                        placeholder="Nombre o apellido..."
                        AutoPostBack="true"
                        OnTextChanged="txtBuscar_TextChanged" />

                </div>

            </div>

            <div class="col-md-2 d-flex align-items-end">

                <asp:Button
                    ID="btnLimpiar"
                    runat="server"
                    Text="Limpiar"
                    CssClass="btn btn-outline-secondary w-100"
                    OnClick="btnLimpiar_Click" />

            </div>

        </div>

        <!-- Tabla -->

        <div class="table-responsive">

            <asp:GridView
                ID="dgvAgenda"
                runat="server"
                CssClass="table table-hover align-middle tabla-personalizada"
                AutoGenerateColumns="false"
                GridLines="None"
                DataKeyNames="Id"
                AllowPaging="true"
                PageSize="10"
                PagerStyle-CssClass="table-pager"
                OnRowCommand="dgvAgenda_RowCommand"
                OnPageIndexChanging="dgvAgenda_PageIndexChanging">

                <Columns>
                    <asp:TemplateField HeaderText="Fecha">
                        <ItemTemplate>
                            <%# ((DateTime)Eval("FechaHora")).ToString("dd/MM/yyyy") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Hora">
                        <ItemTemplate>
                            <%# ((DateTime)Eval("FechaHora")).ToString("HH:mm") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Paciente">
                        <ItemTemplate>
                            <%# Eval("Paciente.Usuario.Nombre") %> <%# Eval("Paciente.Usuario.Apellido") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class='badge <%# ObtenerClaseBadge(Eval("EstadoTurno.Nombre").ToString()) %>'>
                                <%# Eval("EstadoTurno.Nombre") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="d-flex gap-2">
                                <asp:LinkButton
                                    ID="btnVer"
                                    runat="server"
                                    CssClass="btn btn-sm btn-outline-secondary"
                                    CommandName="Ver"
                                    CommandArgument='<%# Eval("Id") %>'>
                                    <i class="bi bi-eye"></i>
                                </asp:LinkButton>
                                <asp:LinkButton
                                    ID="btnAtender"
                                    runat="server"
                                    CssClass="btn btn-sm btn-primary"
                                    CommandName="Atender"
                                    CommandArgument='<%# Eval("Id") %>'
                                    Visible='<%# PuedeAtender(Eval("EstadoTurno.Nombre").ToString()) %>'>
                                    <i class="bi bi-clipboard2-pulse"></i>
                                </asp:LinkButton>
                                <asp:LinkButton
                                    ID="btnHistoria"
                                    runat="server"
                                    CssClass="btn btn-sm btn-outline-success"
                                    CommandName="Historia"
                                    CommandArgument='<%# Eval("Id") %>'
                                    Visible='<%# Eval("EstadoTurno.Nombre").ToString() == "Finalizado" %>'>
                                     <i class="bi bi-file-earmark-medical"></i>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>

    </div>


    <!-- Modal Resumen del Turno -->
    <div class="modal fade"
        id="modalResumenTurno"
        runat="server"
        clientidmode="Static"
        tabindex="-1"
        aria-hidden="true">

        <div class="modal-dialog modal-lg modal-dialog-centered">

            <div class="modal-content border-0 shadow">

                <!-- Encabezado -->

                <div class="modal-header bg-light">

                    <div>

                        <h5 class="modal-title fw-bold mb-1">
                            <i class="bi bi-calendar-check me-2"></i>
                            Resumen del Turno
                        </h5>

                        <small class="text-muted">Información general del turno seleccionado
                        </small>

                    </div>

                    <button
                        type="button"
                        class="btn-close"
                        data-bs-dismiss="modal">
                    </button>

                </div>

                <div class="modal-body">

                    <!-- Tarjeta resumen -->

                    <div class="card border-0 bg-light mb-4">

                        <div class="card-body">

                            <div class="row text-center">

                                <div class="col-md-4">

                                    <small class="text-muted d-block">Código
                                    </small>

                                    <h5 class="fw-bold mb-0">

                                        <asp:Label
                                            ID="lblCodigo"
                                            runat="server"
                                            Text="T0000" />

                                    </h5>

                                </div>

                                <div class="col-md-4">

                                    <small class="text-muted d-block">Fecha
                                    </small>

                                    <h5 class="fw-bold mb-0">

                                        <asp:Label
                                            ID="lblFecha"
                                            runat="server" />

                                    </h5>

                                </div>

                                <div class="col-md-4">

                                    <small class="text-muted d-block">Estado
                                    </small>

                                    <asp:Literal
                                        ID="litEstado"
                                        runat="server" />

                                </div>

                            </div>

                        </div>

                    </div>

                    <!-- Datos -->

                    <div class="row g-4">

                        <div class="col-md-6">

                            <label class="form-label text-muted">
                                <i class="bi bi-clock me-1"></i>
                                Hora
                            </label>

                            <div class="form-control bg-light">

                                <asp:Label
                                    ID="lblHora"
                                    runat="server" />

                            </div>

                        </div>

                        <div class="col-md-6">

                            <label class="form-label text-muted">
                                <i class="bi bi-heart-pulse me-1"></i>
                                Especialidad
                            </label>

                            <div class="form-control bg-light">

                                <asp:Label
                                    ID="lblEspecialidad"
                                    runat="server" />

                            </div>

                        </div>

                        <div class="col-md-12">

                            <label class="form-label text-muted">
                                <i class="bi bi-person me-1"></i>
                                Paciente
                            </label>

                            <div class="form-control bg-light">

                                <asp:Label
                                    ID="lblPaciente"
                                    runat="server" />

                            </div>

                        </div>

                        <div class="col-md-6">

                            <label class="form-label text-muted">
                                <i class="bi bi-person-vcard me-1"></i>
                                DNI
                            </label>

                            <div class="form-control bg-light">

                                <asp:Label
                                    ID="lblDni"
                                    runat="server" />

                            </div>

                        </div>

                        <div class="col-md-6">

                            <label class="form-label text-muted">
                                <i class="bi bi-hospital me-1"></i>
                                Obra Social
                            </label>

                            <div class="form-control bg-light">

                                <asp:Label
                                    ID="lblObraSocial"
                                    runat="server" />

                            </div>

                        </div>

                        <div class="col-md-8">

                            <label class="form-label text-muted">
                                <i class="bi bi-person-badge me-1"></i>
                                Médico
                            </label>

                            <div class="form-control bg-light">

                                <asp:Label
                                    ID="lblMedico"
                                    runat="server" />

                            </div>

                        </div>

                        <div class="col-md-4">

                            <label class="form-label text-muted">
                                <i class="bi bi-patch-check me-1"></i>
                                Matrícula
                            </label>

                            <div class="form-control bg-light">

                                <asp:Label
                                    ID="lblMatricula"
                                    runat="server" />

                            </div>

                        </div>

                    </div>

                </div>

                <div class="modal-footer">

                    <button
                        type="button"
                        class="btn btn-outline-secondary"
                        data-bs-dismiss="modal">

                        <i class="bi bi-x-circle me-1"></i>
                        Cerrar

                    </button>

                </div>
            </div>

        </div>
    </div>

</asp:Content>
