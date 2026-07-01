<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="HistoriaClinicaDetalle.aspx.cs" Inherits="Presentación.HistoriaClinicaDetalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .pie-documento,
        .firmas-documento {
            display: none;
        }

        @media print {

            .pie-documento {
                display: block;
            }

            .firmas-documento {
                display: flex;
            }

            /* Ocultar elementos del Master */
            .menu-lateral,
            .barra-superior,
            .footer {
                display: none !important;
            }

            /* Sacar márgenes del layout */
            .contenido {
                padding: 0 !important;
                margin: 0 !important;
                background: #fff !important;
            }

            .contenido-principal {
                margin: 0 !important;
                padding: 0 !important;
            }

            /* Ocultar botones */
            .acciones-impresion {
                display: none !important;
            }

            /* Dejar la historia ocupando toda la hoja */
            #historiaImprimible {
                width: 100%;
                margin: 0;
                box-shadow: none !important;
                border: none !important;
            }

            .card {
                box-shadow: none !important;
            }
        }

        @page {
            size: A4;
            margin: 1.5cm;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <div id="historiaImprimible" class="card card-custom p-3 shadow-sm border-0">

        <!-- Encabezado -->
        <div class="d-flex justify-content-between align-items-start mb-4">
            <div class="d-flex align-items-center">
                <asp:Image
                    ID="imgLogo"
                    runat="server"
                    ImageUrl="~/Assets/IMG/logo-clinica.png"
                    CssClass="me-3"
                    Height="75" />

                <div>
                    <h2 class="fw-bold mb-1">Historia Clínica</h2>
                    <div class="text-muted mb-2">
                        Sistema Clínica
                    </div>
                    <div>
                        <span class="text-muted me-2">Referencia:</span>
                        <span class="badge bg-secondary fs-6">
                            <asp:Label ID="lblIdHistoria" runat="server" />
                        </span>
                    </div>
                </div>

            </div>

            <div class="d-flex gap-2 acciones-impresion">

                <asp:LinkButton
                    ID="btnImprimir_pdf"
                    runat="server"
                    CssClass="btn btn-outline-dark"
                    OnClientClick="window.print(); return false;">
                    <i class="bi bi-printer"></i>
                    Imprimir / PDF
                </asp:LinkButton>

                <asp:LinkButton
                    ID="btnVolverHistorialClinica"
                    runat="server"
                    CssClass="btn btn-primary"
                    OnClick="btnVolver_Click">
                    <i class="bi bi-arrow-left"></i>
                    Volver
                </asp:LinkButton>
            </div>
        </div>

        <!-- Resumen -->
        <div class="card bg-light border-0 mb-3">
            <div class="card-body py-3">

                <div class="row text-center">

                    <div class="col-md-3">
                        <small class="text-muted d-block">Código Turno</small>
                        <div class="fw-semibold">
                            <asp:Label ID="lblCodigo" runat="server" />
                        </div>
                    </div>

                    <div class="col-md-3">
                        <small class="text-muted d-block">Fecha</small>
                        <div class="fw-semibold">
                            <asp:Label ID="lblFecha" runat="server" />
                        </div>
                    </div>

                    <div class="col-md-3">
                        <small class="text-muted d-block">Hora</small>
                        <div class="fw-semibold">
                            <asp:Label ID="lblHora" runat="server" />
                        </div>
                    </div>

                    <div class="col-md-3">
                        <small class="text-muted d-block">Especialidad</small>
                        <div class="fw-semibold">
                            <asp:Label ID="lblEspecialidad" runat="server" />
                        </div>
                    </div>

                </div>

            </div>
        </div>

        <!-- Paciente -->

        <div class="card mb-3">

            <div class="card-header fw-bold">
                Paciente
            </div>

            <div class="card-body">

                <div class="row g-3">

                    <div class="col-md-3">
                        <small class="text-muted">Nombre</small>
                        <div class="fw-semibold">
                            <asp:Label ID="lblPaciente" runat="server" />
                        </div>
                    </div>

                    <div class="col-md-3">
                        <small class="text-muted">Género</small>
                        <div class="fw-semibold">
                            <asp:Label ID="lblGenero" runat="server" />
                        </div>
                    </div>

                    <div class="col-md-3">
                        <small class="text-muted">DNI</small>
                        <div class="fw-semibold">
                            <asp:Label ID="lblDni" runat="server" />
                        </div>
                    </div>

                    <div class="col-md-3">
                        <small class="text-muted">Obra Social</small>
                        <div class="fw-semibold">
                            <asp:Label ID="lblObraSocial" runat="server" />
                        </div>
                    </div>

                </div>

            </div>

        </div>

        <!-- Profesional -->

        <div class="card mb-3">

            <div class="card-header fw-bold">
                Profesional
            </div>

            <div class="card-body">

                <div class="row g-3">

                    <div class="col-md-8">
                        <small class="text-muted">Médico</small>
                        <div class="fw-semibold">
                            <asp:Label ID="lblMedico" runat="server" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <small class="text-muted">Matrícula</small>
                        <div class="fw-semibold">
                            <asp:Label ID="lblMatricula" runat="server" />
                        </div>
                    </div>

                </div>

            </div>

        </div>

        <!-- Atención -->

        <div class="card">

            <div class="card-header fw-bold">
                Atención Médica
            </div>

            <div class="card-body">

                <div class="mb-3">
                    <label class="form-label fw-semibold mb-1">Diagnóstico</label>
                    <div class="border rounded bg-light p-3" style="min-height: 80px;">
                        <asp:Label ID="lblDiagnostico" runat="server" />
                    </div>
                </div>

                <div class="mb-3">
                    <label class="form-label fw-semibold mb-1">Tratamiento</label>
                    <div class="border rounded bg-light p-3" style="min-height: 80px;">
                        <asp:Label ID="lblTratamiento" runat="server" />
                    </div>
                </div>

                <div class="mb-4">
                    <label class="form-label fw-semibold mb-1">Observaciones</label>
                    <div class="border rounded bg-light p-3" style="min-height: 80px;">
                        <asp:Label ID="lblObservaciones" runat="server" />
                    </div>
                </div>

            </div>

        </div>

        <asp:Panel ID="pnlFirmas" runat="server" CssClass="pie-documento">
            <div class="row mt-5">
                <div class="firmas-documento">

                    <div class="col-6 text-center">
                        ___________________________
                    <br />
                        Firma del Profesional
                    <br />
                        Matrícula:
                    <asp:Label ID="lblMatriculaFirma" runat="server" />
                    </div>
                    <div class="col-6 text-center">
                        ___________________________
                    <br />
                        Firma del Paciente
                    </div>
                </div>
            </div>
            <hr />

            <div class="text-center text-muted small mt-3">
                Documento generado automáticamente por Sistema Clínica.
                  <br />
                Fecha de impresión:
                  <%= DateTime.Now.ToString("dd/MM/yyyy HH:mm") %>
            </div>
        </asp:Panel>

    </div>

</asp:Content>
