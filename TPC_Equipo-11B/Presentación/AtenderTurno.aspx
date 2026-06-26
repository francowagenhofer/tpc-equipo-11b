<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="AtenderTurno.aspx.cs" Inherits="Presentación.AtenderTurno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <div class="card card-custom p-4 shadow-sm border-0">

        <div class="mb-4">

            <h2 class="fw-bold mb-1">Atender Turno</h2>

            <p class="text-muted mb-0">
                Complete la información correspondiente a la atención médica.
            </p>

        </div>

        <!-- Resumen -->

        <div class="card border-0 bg-light shadow-sm mb-4">

            <div class="card-body">

                <div class="row align-items-center">

                    <div class="col-md-4">

                        <small class="text-muted d-block">Paciente</small>

                        <h5 class="fw-bold mb-0">

                            <asp:Label
                                ID="lblPacienteResumen"
                                runat="server"
                                Text="Juan Pérez" />

                        </h5>

                        <small class="text-muted">

                            <asp:Label
                                ID="lblEdad"
                                runat="server"
                                Text="35 años" />

                            ·

                        <asp:Label
                            ID="lblGenero"
                            runat="server"
                            Text="Masculino" />

                        </small>

                    </div>

                    <div class="col-md-4">

                        <small class="text-muted d-block">Turno</small>

                        <h6 class="fw-semibold mb-0">

                            <asp:Label
                                ID="lblTurnoResumen"
                                runat="server"
                                Text="26/06/2026 - 15:30" />

                        </h6>

                    </div>

                    <div class="col-md-4">

                        <small class="text-muted d-block">Especialidad</small>

                        <h6 class="fw-semibold mb-0">

                            <asp:Label
                                ID="lblEspecialidadResumen"
                                runat="server"
                                Text="Cardiología" />

                        </h6>

                    </div>

                </div>

            </div>

        </div>


        <div class="row g-4">

            <!-- PANEL IZQUIERDO -->

            <div class="col-lg-3">

                <div class="card shadow-sm border-0 mb-4">

                    <div class="card-header bg-light">

                        <h5 class="mb-0">Datos del Paciente
                        </h5>

                    </div>

                    <div class="card-body">

                        <div class="mb-3">

                            <label class="form-label text-muted">DNI</label>

                            <div class="form-control bg-light">

                                <asp:Label
                                    ID="lblDni"
                                    runat="server" />

                            </div>

                        </div>

                        <div class="mb-3">

                            <label class="form-label text-muted">
                                Obra Social
                            </label>

                            <div class="form-control bg-light">

                                <asp:Label
                                    ID="lblObraSocial"
                                    runat="server" />

                            </div>

                        </div>

                        <div class="mb-3">

                            <label class="form-label text-muted">
                                Teléfono
                            </label>

                            <div class="form-control bg-light">

                                <asp:Label
                                    ID="lblTelefono"
                                    runat="server" />

                            </div>

                        </div>

                        <div class="mb-3">

                            <label class="form-label text-muted">
                                Código Turno
                            </label>

                            <div class="form-control bg-light">

                                <asp:Label
                                    ID="lblCodigo"
                                    runat="server" />

                            </div>

                        </div>

                        <div>

                            <label class="form-label text-muted">
                                Estado
                            </label>

                            <div class="form-control bg-light">

                                <asp:Label
                                    ID="lblEstado"
                                    runat="server" />

                            </div>

                        </div>

                    </div>

                </div>


                <!-- Últimas consultas -->

                <div class="card shadow-sm border-0">

                    <div class="card-header bg-light">

                        <h6 class="mb-0">Últimas Consultas
                        </h6>

                    </div>

                    <div class="card-body">

                        <small class="text-muted">Se mostrarán las últimas atenciones del paciente.

                        </small>

                        <hr />

                        <asp:Repeater
                            ID="repUltimasConsultas"
                            runat="server">

                            <ItemTemplate>

                                <div class="mb-3">

                                    <strong>
                                        <%# Eval("Fecha", "{0:dd/MM/yyyy}") %>
                                    </strong>

                                    <br />

                                    <small>
                                        <%# Eval("Diagnostico") %>
                                    </small>

                                </div>

                            </ItemTemplate>

                        </asp:Repeater>
                    </div>
                </div>
            </div>


            <!-- HISTORIA CLÍNICA -->

            <div class="col-lg-9">

                <div class="card shadow-sm border-0">

                    <div class="card-header bg-light">

                        <h5 class="mb-0">Historia Clínica
                        </h5>

                    </div>

                    <div class="card-body">

                        <div class="mb-3">

                            <label class="form-label fw-semibold">
                                Diagnóstico <span class="text-danger">*</span>

                            </label>

                            <asp:TextBox
                                ID="txtDiagnostico"
                                runat="server"
                                CssClass="form-control"
                                TextMode="MultiLine"
                                Rows="4"
                                MaxLength="500"
                                placeholder="Ingrese el diagnóstico..." />

                        </div>


                        <div class="mb-3">

                            <label class="form-label fw-semibold">
                                Tratamiento

                            </label>

                            <asp:TextBox
                                ID="txtTratamiento"
                                runat="server"
                                CssClass="form-control"
                                TextMode="MultiLine"
                                Rows="3"
                                MaxLength="500"
                                placeholder="Tratamiento indicado..." />

                        </div>


                        <div>

                            <label class="form-label fw-semibold">
                                Observaciones

                            </label>

                            <asp:TextBox
                                ID="txtObservaciones"
                                runat="server"
                                CssClass="form-control"
                                TextMode="MultiLine"
                                Rows="3"
                                MaxLength="500"
                                placeholder="Observaciones adicionales..." />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- BOTONES -->
        <div class="d-flex justify-content-end gap-2 mt-4">
            <asp:Button
                ID="btnCancelar"
                runat="server"
                Text="Cancelar"
                CssClass="btn btn-outline-secondary"
                OnClick="btnCancelar_Click" />

            <asp:Button
                ID="btnFinalizar"
                runat="server"
                Text="Finalizar Atención"
                CssClass="btn btn-success"
                OnClick="btnFinalizar_Click" />
        </div>
    </div>

</asp:Content>
