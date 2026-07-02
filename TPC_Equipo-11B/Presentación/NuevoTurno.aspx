<%@ Page Title="Nuevo Turno" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="NuevoTurno.aspx.cs" Inherits="Presentación.NuevoTurno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- CSS de Flatpickr para el calendario moderno -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <div class="card card-custom p-4 shadow-sm border-0 mx-auto" style="max-width: 600px;">
        <h2 class="fw-bold mb-1">
            <asp:Literal ID="litTitulo" runat="server" Text="Registrar Nuevo Turno" /></h2>
        <p class="text-muted mb-4">Selecciona paciente, médico y la fecha del turno.</p>

        <asp:Label ID="lblMensaje" runat="server" CssClass="alert d-block text-center" Visible="false"></asp:Label>

        <div class="row g-3">

            <!-- Paciente con Autocompletado Premium (Select Oculto) -->
            <div class="col-12 position-relative">
                <label class="form-label fw-semibold">Paciente</label>
                <div class="input-group">
                    <span class="input-group-text"><i class="bi bi-person-fill"></i></span>
                    <input type="text" id="txtPacienteBusqueda" class="form-control" autocomplete="off" placeholder="Escribe DNI, apellido o nombre del paciente..." oninput="filtrarOpciones('Paciente')" onfocus="mostrarOpciones('Paciente')" />
                </div>
                <!-- El DropDownList real queda oculto y sincronizado -->
                <asp:DropDownList ID="ddlPaciente"
                    runat="server"
                    ClientIDMode="Static"
                    Style="display: none;">
                </asp:DropDownList>

                <!-- Lista de sugerencias dinámica -->
                <ul id="sugerenciasPaciente" class="list-group position-absolute w-100 mt-1 shadow-lg" style="display: none; z-index: 1000; max-height: 200px; overflow-y: auto;">
                </ul>
            </div>

            <!-- Médico con Autocompletado Premium (Select Oculto) -->
            <div class="col-12 position-relative">
                <label class="form-label fw-semibold">Médico</label>
                <div class="input-group">
                    <span class="input-group-text"><i class="bi bi-heart-pulse-fill"></i></span>
                    <input type="text" id="txtMedicoBusqueda" class="form-control" autocomplete="off" placeholder="Escribe matrícula, apellido o nombre del médico..." oninput="filtrarOpciones('Medico')" onfocus="mostrarOpciones('Medico')" />
                </div>
                <!-- El DropDownList real queda oculto y sincronizado -->
                <asp:DropDownList ID="ddlMedico"
                    runat="server"
                    ClientIDMode="Static"
                    Style="display: none;">
                </asp:DropDownList>

                <!-- Lista de sugerencias dinámica -->
                <ul id="sugerenciasMedico" class="list-group position-absolute w-100 mt-1 shadow-lg" style="display: none; z-index: 1000; max-height: 200px; overflow-y: auto;">
                </ul>
            </div>

            <!-- Fecha (Flatpickr) y Hora por Separado -->
            <div class="col-md-6">
                <label class="form-label fw-semibold">Fecha</label>
                <asp:TextBox
                    ID="txtFecha"
                    runat="server"
                    ClientIDMode="Static"
                    placeholder="Seleccione una fecha..."
                    CssClass="form-control" />
            </div>

            <div class="col-md-6">
                <label class="form-label fw-semibold">Hora (Turnos de 60 min)</label>
                <asp:DropDownList
                    ID="ddlHora"
                    runat="server"
                    ClientIDMode="Static"
                    CssClass="form-select">
                </asp:DropDownList>
            </div>

            <div class="col-12 d-flex justify-content-end gap-2 mt-4">
                <a href="Turnos.aspx" class="btn btn-outline-secondary">Cancelar</a>
                <asp:Button ID="btnGuardar" runat="server" Text="Confirmar Turno" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            </div>
        </div>
    </div>

    <!-- JS de Flatpickr -->
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <script src="https://cdn.jsdelivr.net/npm/flatpickr/dist/l10n/es.js"></script>

    <!-- Script JS para autocompletados y control del calendario -->
    <script type="text/javascript">
        let pacientes = [];
        let medicos = [];

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
            // Inicializar el selector de fechas Flatpickr en Español
            flatpickr("#txtFecha", {
                locale: "es",
                dateFormat: "Y-m-d",
                minDate: "today" // Evita reservar turnos en el pasado
            });



            // Cargar datos de pacientes y médicos
            inicializarDatos('Paciente', 'ddlPaciente', 'txtPacienteBusqueda');
            inicializarDatos('Medico', 'ddlMedico', 'txtMedicoBusqueda');

            document.addEventListener("click", function (e) {
                if (!e.target.closest('#txtPacienteBusqueda') && !e.target.closest('#sugerenciasPaciente')) {
                    document.getElementById('sugerenciasPaciente').style.display = 'none';
                }
                if (!e.target.closest('#txtMedicoBusqueda') && !e.target.closest('#sugerenciasMedico')) {
                    document.getElementById('sugerenciasMedico').style.display = 'none';
                }
            });
        });

        function inicializarDatos(tipo, selectId, inputId) {
            const select = document.getElementById(selectId);
            const input = document.getElementById(inputId);
            const items = [];

            for (let i = 0; i < select.options.length; i++) {
                const option = select.options[i];
                if (option.value === "") continue;

                items.push({
                    value: option.value,
                    text: option.text
                });

                if (option.selected) {
                    input.value = option.text;
                }
            }

            if (tipo === 'Paciente') pacientes = items;
            if (tipo === 'Medico') medicos = items;
        }

        function mostrarOpciones(tipo) {
            filtrarOpciones(tipo);
        }

        function filtrarOpciones(tipo) {
            const inputId = tipo === 'Paciente' ? 'txtPacienteBusqueda' : 'txtMedicoBusqueda';
            const selectId = tipo === 'Paciente' ? 'ddlPaciente' : 'ddlMedico';
            const listId = tipo === 'Paciente' ? 'sugerenciasPaciente' : 'sugerenciasMedico';

            const input = document.getElementById(inputId);
            const select = document.getElementById(selectId);
            const list = document.getElementById(listId);
            const data = tipo === 'Paciente' ? pacientes : medicos;
            const filter = input.value.toLowerCase().trim();

            if (filter === "") {
                select.value = "";
            }

            list.innerHTML = "";

            const filtrados = data.filter(item => item.text.toLowerCase().includes(filter));

            if (filtrados.length === 0) {
                const li = document.createElement('li');
                li.className = "list-group-item text-muted small";
                li.textContent = "No se encontraron coincidencias";
                list.appendChild(li);
            } else {
                filtrados.forEach(item => {
                    const li = document.createElement('li');
                    li.className = "list-group-item list-group-item-action py-2";
                    li.style.cursor = "pointer";
                    li.textContent = item.text;
                    li.onclick = function () {
                        seleccionarOpcion(tipo, item.value, item.text);
                    };
                    list.appendChild(li);
                });
            }

            list.style.display = "block";
        }

        function seleccionarOpcion(tipo, value, text) {
            const inputId = tipo === 'Paciente' ? 'txtPacienteBusqueda' : 'txtMedicoBusqueda';
            const selectId = tipo === 'Paciente' ? 'ddlPaciente' : 'ddlMedico';
            const listId = tipo === 'Paciente' ? 'sugerenciasPaciente' : 'sugerenciasMedico';

            const input = document.getElementById(inputId);
            const select = document.getElementById(selectId);
            const list = document.getElementById(listId);

            input.value = text;
            select.value = value;
            list.style.display = "none";
        }

        // ID del turno actual si estamos editando (0 si es nuevo)
        const idTurnoActual = <%= Request.QueryString["id"] != null ? Request.QueryString["id"] : "0" %>;

        // Guarda el value de la hora seleccionada al cargar (útil al editar)
        let horaPreseleccionada = document.getElementById('ddlHora').value;

        function actualizarHorariosDisponibles() {
            const idMedico = document.getElementById('ddlMedico').value;
            const fecha = document.getElementById('txtFecha').value;
            const ddlHora = document.getElementById('ddlHora');

            if (!idMedico || !fecha) return;

            fetch('NuevoTurno.aspx/ObtenerHorasOcupadasAjax', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json; charset=utf-8' },
                body: JSON.stringify({
                    idMedico: parseInt(idMedico),
                    fecha: fecha,
                    idTurnoActual: idTurnoActual
                })
            })
                .then(response => response.json())
                .then(data => {
                    const ocupadas = data.d; // PageMethods devuelve el array dentro de "d"
                    const valorActual = ddlHora.value;
                    let disponibles = 0;

                    for (let i = 0; i < ddlHora.options.length; i++) {
                        const opt = ddlHora.options[i];
                        if (opt.value === "") continue; // saltar el placeholder
                        const estaOcupada = ocupadas.includes(opt.value);
                        opt.disabled = estaOcupada;
                        opt.textContent = estaOcupada ? opt.value + " (No disponible)" : opt.value;
                        if (!estaOcupada) disponibles++;
                    }

                    // Si la hora que tenía seleccionada quedó ocupada/no disponible, la deselecciona
                    if (ocupadas.includes(valorActual)) {
                        ddlHora.value = "";
                    }

                    // Mostrar aviso si no quedan horarios disponibles ese día
                    const avisoId = 'avisoSinHorarios';
                    let aviso = document.getElementById(avisoId);
                    if (disponibles === 0) {
                        if (!aviso) {
                            aviso = document.createElement('div');
                            aviso.id = avisoId;
                            aviso.className = 'alert alert-warning mt-2';
                            aviso.textContent = 'El médico no tiene disponibilidad configurada o está ausente en la fecha seleccionada. Probá con otra fecha o médico.';
                            ddlHora.closest('.col-md-6').appendChild(aviso);
                        }
                    } else if (aviso) {
                        aviso.remove();
                    }
                })
                .catch(err => console.error("Error al consultar horarios ocupados:", err));
        }

        // Disparar el chequeo cuando cambia el médico (vía selección del autocompletado)
        const ddlMedicoOriginal = document.getElementById('ddlMedico');
        const observerMedico = new MutationObserver(actualizarHorariosDisponibles);
        observerMedico.observe(ddlMedicoOriginal, { attributes: true, attributeFilter: ['value'] });

        // Disparar también cuando cambia la fecha (Flatpickr dispara "change" sobre el input real)
        document.getElementById('txtFecha').addEventListener('change', actualizarHorariosDisponibles);

        // También conviene reforzar el disparo justo después de elegir médico en el autocompletado:
        const seleccionarOpcionOriginal = seleccionarOpcion;
        seleccionarOpcion = function (tipo, value, text) {
            seleccionarOpcionOriginal(tipo, value, text);
            if (tipo === 'Medico') {
                actualizarHorariosDisponibles();
            }
        };

        // Si estamos editando un turno, correr el chequeo apenas carga la página
        document.addEventListener("DOMContentLoaded", function () {
            if (idTurnoActual > 0) {
                setTimeout(actualizarHorariosDisponibles, 300);
            }
        });

    </script>

</asp:Content>
