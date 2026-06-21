<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmarTurno.aspx.cs" Inherits="Presentación.ConfirmarTurno" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Confirmación de Turno - Clínica Médica</title>
    <!-- Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Icons -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css" />
    <style>
        body {
            background-color: #f8f9fa;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }
        .confirmation-card {
            background: white;
            border-radius: 16px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.05);
            max-width: 500px;
            width: 100%;
            padding: 2.5rem;
            text-align: center;
        }
        .success-icon {
            font-size: 4rem;
            color: #198754;
            margin-bottom: 1.5rem;
        }
        .error-icon {
            font-size: 4rem;
            color: #dc3545;
            margin-bottom: 1.5rem;
        }
        .turno-details {
            background-color: #f8f9fa;
            border-radius: 12px;
            padding: 1.5rem;
            margin: 1.5rem 0;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container d-flex justify-content-center">
            <div class="confirmation-card">
                
                <asp:Panel ID="pnlExito" runat="server" Visible="false">
                    <i class="bi bi-check-circle-fill success-icon"></i>
                    <h2 class="fw-bold mb-2">¡Turno Confirmado!</h2>
                    <p class="text-muted">Tu cita médica ha sido confirmada con éxito.</p>
                    
                    <div class="turno-details border-start border-success border-4 shadow-sm">
                        <p class="mb-2"><strong>Código:</strong> <asp:Label ID="lblCodigo" runat="server" CssClass="text-primary fw-bold" /></p>
                        <p class="mb-2"><strong>Paciente:</strong> <asp:Label ID="lblPaciente" runat="server" /></p>
                        <p class="mb-2"><strong>Médico:</strong> <asp:Label ID="lblMedico" runat="server" /></p>
                        <p class="mb-0"><strong>Fecha y Hora:</strong> <asp:Label ID="lblFechaHora" runat="server" CssClass="fw-semibold" /></p>
                    </div>
                    
                    <a href="Login.aspx" class="btn btn-primary px-4 py-2 mt-2 w-100 rounded-pill">Iniciar Sesión</a>
                </asp:Panel>

                <asp:Panel ID="pnlError" runat="server" Visible="false">
                    <i class="bi bi-exclamation-triangle-fill error-icon"></i>
                    <h2 class="fw-bold mb-2">Error de Confirmación</h2>
                    <p class="text-muted"><asp:Label ID="lblMensajeError" runat="server" /></p>
                    
                    <a href="Login.aspx" class="btn btn-outline-secondary px-4 py-2 mt-4 w-100 rounded-pill">Ir al Inicio</a>
                </asp:Panel>

            </div>
        </div>
    </form>
</body>
</html>
