<%@ Page Title="Turnos" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Turnos.aspx.cs" Inherits="Presentación.Turnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css" />

    <style>
        .tabla-turnos th {
            background-color: #f8f9fa;
            color: #495057;
            font-weight: 600;
        }

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
                <h2 class="fw-bold mb-1">Administración de Turnos</h2>
                <p class="text-muted mb-0">Gestión, reprogramación y consulta de turnos médicos.</p>
            </div>

            <a href="NuevoTurno.aspx" class="btn btn-primary d-flex align-items-center gap-2">
                <i class="bi bi-calendar-plus-fill"></i>Nuevo Turno
            </a>
        </div>

        <div class="row g-3 mb-4">
            <div class="col-md-4">
                <label class="form-label fw-semibold text-muted small">Buscar Paciente o Médico</label>
                <div class="input-group">
                    <span class="input-group-text bg-white border-end-0"><i class="bi bi-search text-muted"></i></span>
                    <asp:TextBox ID="txtFiltroBusqueda" runat="server" CssClass="form-control border-start-0" placeholder="Nombre, apellido o DNI..." AutoPostBack="true" OnTextChanged="txtFiltroBusqueda_TextChanged" />
                </div>
            </div>
            <div class="col-md-3">
                <label class="form-label fw-semibold text-muted small">Filtrar por Estado</label>
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">

                    <%-- Foreach que recorra los estados --%>
                    <asp:ListItem Text="Todos los estados" Value="0" />
                    <asp:ListItem Text="Pendiente" Value="1" />
                    <asp:ListItem Text="Confirmado" Value="2" />
                    <asp:ListItem Text="Cancelado" Value="3" />
                    <asp:ListItem Text="Reprogramado" Value="4" />
                    <asp:ListItem Text="No Asistió" Value="5" />
                    <asp:ListItem Text="Finalizado" Value="6" />
                </asp:DropDownList>
            </div>
            <div class="col-md-3">
                <label class="form-label fw-semibold text-muted small">
                    Filtrar por Fecha
                </label>

                <asp:TextBox
                    ID="txtFechaFiltro"
                    runat="server"
                    ClientIDMode="Static"
                    placeholder="Seleccione una fecha..."
                    CssClass="form-control"
                    AutoPostBack="true"
                    OnTextChanged="txtFechaFiltro_TextChanged" />
            </div>
            
            <div class="col-md-2 d-flex align-items-end">
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary btn-sm w-100" OnClick="btnLimpiar_Click" />
            </div>
        </div>


        <div class="table-responsive">
            <asp:GridView ID="dgvTurnos" runat="server"
                CssClass="table table-hover align-middle tabla-turnos"
                AutoGenerateColumns="false"
                GridLines="None"
                DataKeyNames="Id"
                OnRowCommand="dgvTurnos_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="Código" DataField="Codigo" ItemStyle-CssClass="fw-bold text-primary" />

                    <asp:TemplateField HeaderText="Paciente">
                        <ItemTemplate>
                            <span class="fw-semibold"><%# Eval("Paciente.Usuario.Apellido") %>, <%# Eval("Paciente.Usuario.Nombre") %></span>
                            <div class="text-muted small">DNI: <%# Eval("Paciente.DNI") %></div>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Médico">
                        <ItemTemplate>
                            Dr. <%# Eval("Medico.Usuario.Apellido") %>, <%# Eval("Medico.Usuario.Nombre") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha y Hora">
                        <ItemTemplate>
                            <i class="bi bi-calendar-event text-muted me-1"></i>
                            <%# Eval("FechaHora", "{0:dd/MM/yyyy HH:mm}") %> hs
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class='<%# ObtenerClaseBadge(Eval("EstadoTurno.Nombre").ToString()) %>'>
                                <%# Eval("EstadoTurno.Nombre") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
    <ItemTemplate>
        <div class="d-flex gap-2">
            <!-- Botón Modificar (siempre visible) -->
            <asp:LinkButton ID="btnModificar" runat="server" 
                CssClass="btn btn-sm btn-outline-primary" 
                ToolTip="Modificar turno" 
                CommandName="Modificar" 
                CommandArgument='<%# Eval("Id") %>'>
                <i class="bi bi-pencil"></i> Modificar
            </asp:LinkButton>
            <!-- Botón Cancelar -->
            <asp:LinkButton ID="btnCancelar" runat="server" 
                CssClass="btn btn-sm btn-outline-danger" 
                ToolTip="Cancelar turno" 
                CommandName="Cancelar" 
                CommandArgument='<%# Eval("Id") %>'
                OnClientClick="return confirm('¿Está seguro de que desea cancelar este turno?');"
                Visible='<%# Eval("EstadoTurno.Nombre").ToString() == "Pendiente" || Eval("EstadoTurno.Nombre").ToString() == "Confirmado" %>'>
                <i class="bi bi-x-circle"></i> Cancelar
            </asp:LinkButton>
        </div>
    </ItemTemplate>
</asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <script src="https://cdn.jsdelivr.net/npm/flatpickr/dist/l10n/es.js"></script>

    <!-- Script JS para deshabilitar fines de semana y feriados en el filtro de fechas -->
    <script type="text/javascript">
        // Feriados nacionales fijos en formato "MM-DD"
        const feriadosNacionales = [
            "01-01", // Año Nuevo
            "03-24", // Día de la Memoria
            "04-02", // Veteranos de Malvinas
            "05-01", // Día del Trabajador
            "05-25", // Revolución de Mayo
            "06-17", // Gral. Güemes
            "06-20", // Gral. Belgrano
            "07-09", // Día de la Independencia
            "08-17", // Gral. San Martín
            "10-12", // Diversidad Cultural
            "11-20", // Soberanía Nacional
            "12-08", // Inmaculada Concepción
            "12-25"  // Navidad
        ];

        document.addEventListener("DOMContentLoaded", function () {
            flatpickr("#txtFechaFiltro", {
                locale: "es",
                dateFormat: "Y-m-d",
                // Permitimos ver fechas pasadas (no usamos minDate: "today") 
                // para que el usuario pueda consultar turnos anteriores
                disable: [
                    function (date) {
                        // 1. Deshabilitar Sábados (6) y Domingos (0)
                        if (date.getDay() === 0 || date.getDay() === 6) {
                            return true;
                        }

                        // 2. Deshabilitar feriados
                        const mesDia = String(date.getMonth() + 1).padStart(2, '0') + "-" + String(date.getDate()).padStart(2, '0');
                        return feriadosNacionales.includes(mesDia);
                    }
                ],
                onChange: function (selectedDates, dateStr, instance) {
                    // Disparar el evento de cambio nativo para que ASP.NET realice el postback automáticamente
                    const el = document.getElementById('txtFechaFiltro');
                    if (el) {
                        if (typeof el.onchange === 'function') {
                            el.onchange();
                        } else {
                            el.dispatchEvent(new Event('change'));
                        }
                    }
                }
            });
        });
    </script>

</asp:Content>
