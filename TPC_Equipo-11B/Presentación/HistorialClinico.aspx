<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="HistorialClinico.aspx.cs" Inherits="Presentación.HistorialClinico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <div class="card card-custom p-4 shadow-sm border-0">

        <!-- Encabezado -->

        <div class="d-flex justify-content-between align-items-center mb-4">

            <div>
                <h2 class="fw-bold mb-1">Historia Clínica</h2>
                <p class="text-muted mb-0">
                    Consultá las historias clínicas registradas.
                </p>
            </div>

        </div>

        <!-- Tarjetas resumen -->

        <div class="row g-3 mb-4">

            <div class="col-md-4">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-body">
                        <small class="text-muted">Total Historias
                        </small>

                        <h3 class="fw-bold text-primary mb-0">
                            <asp:Label
                                ID="lblTotalHistorias"
                                runat="server"
                                Text="0" />
                        </h3>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-body">
                        <small class="text-muted">Este Mes
                        </small>

                        <h3 class="fw-bold text-success mb-0">
                            <asp:Label
                                ID="lblEsteMes"
                                runat="server"
                                Text="0" />
                        </h3>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-body">
                        <small class="text-muted">Última Consulta
                        </small>

                        <h3 class="fw-bold text-info mb-0">
                            <asp:Label
                                ID="lblUltimaConsulta"
                                runat="server"
                                Text="-" />
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
                    Buscar
                </label>

                <div class="input-group">

                    <span class="input-group-text bg-white border-end-0">
                        <i class="bi bi-search text-muted"></i>
                    </span>

                    <asp:TextBox
                        ID="txtBuscar"
                        runat="server"
                        CssClass="form-control border-start-0"
                        placeholder="Paciente..."
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
                ID="dgvHistoriaClinica"
                runat="server"
                CssClass="table table-hover align-middle tabla-personalizada"
                AutoGenerateColumns="false"
                GridLines="None"
                DataKeyNames="Id"
                AllowPaging="true"
                PageSize="10"
                PagerStyle-CssClass="table-pager"
                OnRowCommand="dgvHistoriaClinica_RowCommand"
                OnPageIndexChanging="dgvHistoriaClinica_PageIndexChanging">

                <Columns>
                    <asp:TemplateField HeaderText="Fecha">
                        <ItemTemplate>
                            <%# Eval("Fecha","{0:dd/MM/yyyy}") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Hora">
                        <ItemTemplate>
                            <%# Eval("Fecha","{0:HH:mm}") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Paciente">
                        <ItemTemplate>
                            <%# Eval("Paciente.Usuario.Apellido") %>,
                            <%# Eval("Paciente.Usuario.Nombre") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Médico">
                        <ItemTemplate>
                            Dr.
                            <%# Eval("Medico.Usuario.Apellido") %>,
                            <%# Eval("Medico.Usuario.Nombre") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Especialidad">
                        <ItemTemplate>
                            <%# Eval("Medico.Especialidad.Nombre") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Diagnóstico">
                        <ItemTemplate>
                            <%#
                                Eval("Diagnostico").ToString().Length > 40
                                ? Eval("Diagnostico").ToString().Substring(0,40) + "..." : Eval("Diagnostico")
                            %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton
                                ID="btnDetalle"
                                runat="server"
                                CssClass="btn btn-sm btn-outline-primary"
                                CommandName="Detalle"
                                CommandArgument='<%# Eval("Id") %>'>
                                <i class="bi bi-eye"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>

            </asp:GridView>

        </div>

    </div>

</asp:Content>
