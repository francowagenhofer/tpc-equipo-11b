<%@ Page Title="Nuevo Turno" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="NuevoTurno.aspx.cs" Inherits="Presentacion.NuevoTurno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .turno-page {
            max-width: 800px;
            margin: 0 auto;
        }

        .turno-header {
            display: flex;
            justify-content: space-between;
            gap: 1rem;
            align-items: flex-start;
            margin-bottom: 1.25rem;
        }

        .turno-panel {
            border: 1px solid #e7eaf0;
            border-radius: .75rem;
            background: #fff;
            box-shadow: 0 10px 30px rgba(15, 23, 42, .06);
        }

            .turno-panel + .turno-panel {
                margin-top: 1rem;
            }

        .turno-panel-header {
            padding: 1rem 1.25rem;
            border-bottom: 1px solid #eef1f5;
            display: flex;
            justify-content: space-between;
            gap: 1rem;
            align-items: center;
        }

        .turno-panel-body {
            padding: 1.25rem;
        }

        .turno-step {
            width: 2rem;
            height: 2rem;
            border-radius: 50%;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            background: #0d6efd;
            color: #fff;
            font-weight: 700;
            margin-right: .5rem;
        }

        .pacientes-lista {
            border: 1px solid #edf1f5;
            border-radius: .65rem;
            overflow: hidden;
        }

        .pacientes-scroll {
            max-height: 190px;
            overflow-y: auto;
            overflow-x: hidden;
            border-radius: .7rem;
        }

            .pacientes-scroll::-webkit-scrollbar {
                width: 8px;
            }

            .pacientes-scroll::-webkit-scrollbar-thumb {
                background: #cbd5e1;
                border-radius: 999px;
            }

            .pacientes-scroll::-webkit-scrollbar-track {
                background: transparent;
            }

        .paciente-row {
            width: 100%;
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: .75rem;
            padding: .75rem .9rem;
            border: 0;
            border-bottom: 1px solid #edf1f5;
            background: #fff;
            color: inherit;
            text-align: left;
            text-decoration: none;
        }

            .paciente-row:last-child {
                border-bottom: 0;
            }

            .paciente-row:hover,
            .paciente-row:focus {
                background: #f8fafc;
                color: inherit;
            }

            .paciente-row.is-selected {
                background: #f2f7ff;
                box-shadow: inset 3px 0 0 #0d6efd;
            }

        .paciente-meta {
            display: flex;
            flex-wrap: wrap;
            gap: .4rem .85rem;
            color: #6c757d;
            font-size: .875rem;
        }

        .paciente-resumen {
            background: #f8fafc;
            border: 1px solid #edf1f5;
            border-radius: .65rem;
            padding: .75rem .9rem;
            display: grid;
            grid-template-columns: 1.4fr .8fr .7fr 1.1fr;
            gap: .75rem;
        }

            .paciente-resumen small {
                color: #6c757d;
                display: block;
                margin-bottom: .15rem;
            }

        .subsection-title {
            font-size: .92rem;
            font-weight: 700;
            color: #495057;
            margin: 0 0 .75rem;
        }

        .medicos-grid {
            display: grid;
            grid-template-columns: repeat(2, minmax(0, 1fr));
            gap: .65rem;
        }

        .medico-card {
            width: 100%;
            text-align: left;
            border: 1px solid #dee5ef;
            border-radius: .65rem;
            background: #fff;
            padding: .75rem .85rem;
            color: inherit;
            text-decoration: none;
            transition: border-color .15s ease, background-color .15s ease;
        }

            .medico-card:hover,
            .medico-card:focus {
                border-color: #b9c9df;
                background: #f8fafc;
                color: inherit;
            }

            .medico-card.is-selected {
                border-color: #0d6efd;
                background: #f2f7ff;
                box-shadow: inset 0 0 0 1px #0d6efd;
            }

        .seleccion-linea {
            background: #f8fafc;
            border: 1px solid #edf1f5;
            border-radius: .65rem;
            padding: .65rem .8rem;
        }

        .horarios-grid {
            display: grid;
            grid-template-columns: repeat(4, minmax(86px, 1fr));
            gap: .5rem;
            justify-content: center;
            align-items: center;
        }

        #rblHora input[type="radio"] {
            position: absolute;
            opacity: 0;
            pointer-events: none;
        }

        #rblHora span {
            display: block;
            min-width: 0;
        }

        #rblHora label {
            width: 100%;
            min-height: 36px;
            border: 1px solid #d9e1ea;
            border-radius: 999px;
            padding: .45rem .65rem;
            display: flex;
            align-items: center;
            justify-content: center;
            text-align: center;
            cursor: pointer;
            background: #fff;
            font-weight: 600;
            font-size: .9rem;
            line-height: 1;
            white-space: nowrap;
        }

        #rblHora input[type="radio"]:checked + label {
            background: #0d6efd;
            color: #fff;
            border-color: #0d6efd;
        }

        #rblHora input[type="radio"]:disabled + label {
            color: #9aa3af;
            background: #f1f3f5;
            cursor: not-allowed;
            text-decoration: line-through;
        }

        .empty-state {
            border: 1px dashed #cfd7e3;
            border-radius: .7rem;
            padding: 1.25rem;
            background: #f8fafc;
            color: #6c757d;
            text-align: center;
        }


        .fecha-card,
        .horarios-card {
            height: 100%;
            border: 1px solid #edf1f5;
            border-radius: .75rem;
            background: #fafbfd;
            padding: 1.25rem;
        }

        .fecha-card {
            display: flex;
            flex-direction: column;
        }

        .horarios-card {
            display: flex;
            flex-direction: column;
        }

        .horarios-grid {
            flex: 1;
        }

        @media (max-width: 768px) {
            .turno-header,
            .turno-panel-header {
                display: block;
            }

            .paciente-row {
                align-items: flex-start;
                flex-direction: column;
            }

            .paciente-resumen,
            .medicos-grid,
            .horarios-grid {
                grid-template-columns: 1fr;
            }

            .horario-box {
                max-width: none;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">
    <div class="turno-page">
        <div class="turno-header">
            <div>
                <h2 class="fw-bold mb-1">
                    <asp:Literal ID="litTitulo" runat="server" Text="Solicitar turno" />
                </h2>
                <p class="text-muted mb-0">Busque el paciente, elija la especialidad y seleccione un horario disponible.</p>
            </div>
            <a href="Turnos.aspx" class="btn btn-outline-secondary">
                <i class="bi bi-arrow-left me-2"></i>Volver
            </a>
        </div>

        <asp:Label ID="lblMensaje" runat="server" CssClass="alert d-block" Visible="false" />

        <section class="turno-panel">
            <div class="turno-panel-header">
                <h5 class="fw-bold mb-0"><span class="turno-step">1</span>Paciente</h5>
                <asp:Label ID="lblResultadoPacientes" runat="server" CssClass="text-muted small" />
            </div>
            <div class="turno-panel-body">
                <div class="row g-3 align-items-end">
                    <div class="col-lg-5">
                        <label class="form-label fw-semibold" for="<%= txtBuscarPaciente.ClientID %>">Buscar paciente</label>
                        <asp:TextBox ID="txtBuscarPaciente" runat="server" CssClass="form-control" placeholder="DNI, apellido o nombre" />
                    </div>
                    <div class="col-lg-4">
                        <label class="form-label fw-semibold" for="<%= ddlFiltroObraSocial.ClientID %>">Obra social</label>
                        <asp:DropDownList ID="ddlFiltroObraSocial" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroObraSocial_SelectedIndexChanged" />
                    </div>
                    <div class="col-lg-3 d-flex gap-2">
                        <asp:Button ID="btnLimpiarPaciente" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary flex-fill" OnClick="btnLimpiarPaciente_Click" />
                    </div>
                </div>

                <asp:DropDownList ID="ddlPaciente" runat="server" CssClass="d-none" AutoPostBack="true" OnSelectedIndexChanged="ddlPaciente_SelectedIndexChanged" />

                <div class="mt-3">
                    <div class="d-flex justify-content-between align-items-center mb-2">
                        <label class="form-label fw-semibold mb-0">Pacientes encontrados</label>
                        <small class="text-muted">Seleccione un paciente de la lista</small>
                    </div>

                    <div class="pacientes-scroll">

                        <asp:Repeater ID="rptPacientes" runat="server" OnItemCommand="rptPacientes_ItemCommand" OnItemDataBound="rptPacientes_ItemDataBound">
                            <HeaderTemplate>
                                <div class="pacientes-lista">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSeleccionarPaciente" runat="server"
                                    CssClass="paciente-row"
                                    CommandName="SeleccionarPaciente"
                                    CommandArgument='<%# Eval("Id") %>'
                                    CausesValidation="false">
                                <div>
                                    <div class="fw-semibold"><%# Eval("Usuario.Apellido") %>, <%# Eval("Usuario.Nombre") %></div>
                                    <div class="paciente-meta">
                                        <span>DNI <%# Eval("DNI") %></span>
                                        <span><%# Eval("ObraSocial.Nombre") %> <%# Eval("ObraSocial.TipoPlan") %></span>
                                    </div>
                                </div>
                                <span class="btn btn-sm btn-outline-primary">Seleccionar</span>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <FooterTemplate>
                                </div>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>

                </div>

                <div class="paciente-resumen mt-3">
                    <div>
                        <small>Paciente</small>
                        <asp:Label ID="lblPacienteSeleccionado" runat="server" Text="-" CssClass="fw-semibold" />
                    </div>
                    <div>
                        <small>DNI</small>
                        <asp:Label ID="lblDNI" runat="server" Text="-" CssClass="fw-semibold" />
                    </div>
                    <div>
                        <small>Edad</small>
                        <asp:Label ID="lblEdad" runat="server" Text="-" CssClass="fw-semibold" />
                    </div>
                    <div>
                        <small>Obra social</small>
                        <asp:Label ID="lblObraSocial" runat="server" Text="-" CssClass="fw-semibold" />
                    </div>
                </div>
            </div>
        </section>

        <section class="turno-panel">
            <div class="turno-panel-header">
                <h5 class="fw-bold mb-0"><span class="turno-step">2</span>Especialidad y profesional</h5>
                <asp:Label ID="lblCantidadMedicos" runat="server" CssClass="text-muted small" Text="Seleccione paciente y especialidad" />
            </div>
            <div class="turno-panel-body">
                <div class="row g-3 align-items-end">
                    <div class="col-lg-7">
                        <label class="form-label fw-semibold" for="<%= ddlEspecialidad.ClientID %>">Especialidad</label>
                        <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged" />
                    </div>
                    <div class="col-lg-3">
                        <asp:Button ID="btnLimpiarEspecialidad" runat="server" Text="Limpiar seleccion" CssClass="btn btn-outline-secondary w-100" OnClick="btnLimpiarEspecialidad_Click" />
                    </div>
                </div>

                <div class="seleccion-linea mt-3">
                    <div class="row g-2">
                        <div class="col-md-5">
                            <small class="text-muted d-block">Especialidad</small>
                            <asp:Label ID="lblEspecialidadSeleccionada" runat="server" Text="-" CssClass="fw-semibold" />
                        </div>
                        <div class="col-md-7">
                            <small class="text-muted d-block">Profesional</small>
                            <asp:Label ID="lblMedicoSeleccionado" runat="server" Text="Sin seleccionar" CssClass="fw-semibold" />
                        </div>
                    </div>
                </div>

                <div class="mt-4">
                    <h6 class="subsection-title">Profesionales disponibles</h6>

                    <asp:Repeater ID="rptMedicos" runat="server" OnItemCommand="rptMedicos_ItemCommand" OnItemDataBound="rptMedicos_ItemDataBound">
                        <HeaderTemplate>
                            <div class="medicos-grid">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnSeleccionarMedico" runat="server"
                                CssClass="medico-card"
                                CommandName="SeleccionarMedico"
                                CommandArgument='<%# Eval("Id") %>'
                                CausesValidation="false">
                                <div class="d-flex justify-content-between gap-2">
                                    <div>
                                        <div class="fw-bold">Dr. <%# Eval("Usuario.Apellido") %>, <%# Eval("Usuario.Nombre") %></div>
                                        <div class="text-muted small"><%# Eval("Especialidad.Nombre") %></div>
                                    </div>
                                         <div class="small text-secondary mt-1">Matrícula: <%# Eval("Matricula") %></div>
                                </div>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>

                    <asp:Panel ID="pnlSinMedicos" runat="server" CssClass="empty-state" Visible="false">
                        No hay profesionales para la especialidad y obra social seleccionadas.
                    </asp:Panel>
                </div>

                <asp:HiddenField ID="hfMedicoSeleccionado" runat="server" ClientIDMode="Static" />
            </div>
        </section>

        <section class="turno-panel">

            <div class="turno-panel-header">
                <h5 class="fw-bold mb-0">
                    <span class="turno-step">3</span>
                    Fecha y horario
                </h5>
            </div>

            <div class="turno-panel-body">

                <div class="row g-4 align-items-stretch">
                    <!-- FECHA -->
                    <div class="col-lg-6">

                        <div class="fecha-card h-100">

                            <h6 class="fw-semibold mb-3">
                                <i class="bi bi-calendar-event me-2"></i>
                                Fecha
                            </h6>

                            <asp:TextBox
                                ID="txtFecha"
                                runat="server"
                                ClientIDMode="Static"
                                TextMode="Date"
                                CssClass="form-control"
                                OnTextChanged="txtFecha_TextChanged" />

                            <div id="ayudaFecha" class="form-text mt-2">
                                Seleccione un profesional para consultar su agenda.
                            </div>
                            <div class="mt-4">
                                <div class="border rounded p-3 bg-white">
                                    <small class="text-muted d-block mb-1">Próximo turno disponible</small>
                                    <asp:Label
                                        ID="lblProximoTurno"
                                        runat="server"
                                        CssClass="fw-bold d-block"
                                        Text="Seleccione un profesional." />
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- HORARIOS -->
                    <div class="col-lg-6">
                        <div class="horarios-card h-100">
                            <h6 class="fw-semibold mb-3">
                                <i class="bi bi-clock me-2"></i>
                                Horarios disponibles
                            </h6>
                            <asp:RadioButtonList
                                ID="rblHora"
                                runat="server"
                                ClientIDMode="Static"
                                CssClass="horarios-grid"
                                RepeatLayout="Flow"
                                RepeatColumns="3">
                            </asp:RadioButtonList>

                            <asp:HiddenField
                                ID="hfHoraSeleccionada"
                                runat="server"
                                ClientIDMode="Static" />

                            <div id="avisoHorarios"
                                class="alert alert-warning mt-3 d-none">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <div class="d-flex justify-content-end gap-2 mt-4">
            <a href="Turnos.aspx" class="btn btn-outline-secondary px-4">Cancelar</a>
            <asp:Button ID="btnGuardar" runat="server" Text="Confirmar turno" CssClass="btn btn-primary px-4" OnClick="btnGuardar_Click" />
        </div>
    </div>

    <script type="text/javascript">
        (function () {
            const fecha = document.getElementById("txtFecha");
            const medico = document.getElementById("hfMedicoSeleccionado");
            const aviso = document.getElementById("avisoHorarios");
            const ayudaFecha = document.getElementById("ayudaFecha");
            const horaHidden = document.getElementById("hfHoraSeleccionada");
            const idTurnoActual = <%= Request.QueryString["id"] != null ? Request.QueryString["id"] : "0" %>;

            function hoyIso() {
                const hoy = new Date();
                hoy.setMinutes(hoy.getMinutes() - hoy.getTimezoneOffset());
                return hoy.toISOString().slice(0, 10);
            }

            function mostrarAviso(texto) {
                aviso.textContent = texto;
                aviso.classList.remove("d-none");
            }

            function ocultarAviso() {
                aviso.textContent = "";
                aviso.classList.add("d-none");
            }

            function setHoraSeleccionada() {
                const seleccion = document.querySelector("#rblHora input[type='radio']:checked");
                horaHidden.value = seleccion ? seleccion.value : "";
            }

            function actualizarHorarios() {
                ocultarAviso();
                setHoraSeleccionada();

                if (!fecha || !medico || !medico.value || !fecha.value) {
                    return;
                }

                fetch("NuevoTurno.aspx/ObtenerHorasNoDisponiblesAjax", {
                    method: "POST",
                    headers: { "Content-Type": "application/json; charset=utf-8" },
                    body: JSON.stringify({
                        idMedico: parseInt(medico.value, 10),
                        fecha: fecha.value,
                        idTurnoActual: idTurnoActual
                    })
                })
                    .then(function (response) { return response.json(); })
                    .then(function (data) {
                        const bloqueadas = data.d || [];
                        const opciones = document.querySelectorAll("#rblHora input[type='radio']");
                        let disponibles = 0;

                        opciones.forEach(function (opcion) {
                            const label = document.querySelector("label[for='" + opcion.id + "']");
                            const bloqueada = bloqueadas.indexOf(opcion.value) >= 0;

                            opcion.disabled = bloqueada;
                            if (label) {
                                label.textContent = bloqueada ? opcion.value + " no disponible" : opcion.value;
                            }

                            if (bloqueada && opcion.checked) {
                                opcion.checked = false;
                            }

                            if (!bloqueada) {
                                disponibles++;
                            }
                        });

                        setHoraSeleccionada();

                        if (disponibles === 0) {
                            mostrarAviso("El medico no tiene horarios disponibles para ese dia. Proba con otra fecha.");
                        }
                    })
                    .catch(function () {
                        mostrarAviso("No se pudieron consultar los horarios disponibles.");
                    });
            }

            if (fecha) {
                fecha.min = hoyIso();
                fecha.addEventListener("change", actualizarHorarios);
            }

            document.addEventListener("change", function (event) {
                if (event.target && event.target.name && event.target.name.indexOf("rblHora") >= 0) {
                    setHoraSeleccionada();
                }
            });

            if (medico && medico.value) {
                ayudaFecha.textContent = "Seleccione una fecha para ver que horarios siguen disponibles.";
            }

            actualizarHorarios();
        })();
    </script>
</asp:Content>
