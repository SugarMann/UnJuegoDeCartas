import plotly.figure_factory as ff
import datetime

# --- Task Definitions ---
# (End dates adjusted so 1-day milestones are visible)
df = [
    dict(Task="Redacción Ficha de Trabajo", Start='2025-10-18', Finish='2025-10-18', Resource='PEC 1: Planificación'),
    dict(Task="Redacción Apartado 1 (Introducción)", Start='2025-10-18', Finish='2025-10-19', Resource='PEC 1: Planificación'),
    dict(Task="HITO: Entrega PEC 1", Start='2025-10-19', Finish='2025-10-19', Resource='HITO'),

    dict(Task="Gestión y Setup (Feedback, GitHub, Unity)", Start='2025-10-20', Finish='2025-10-23', Resource='PEC 2: Estado del Arte'),
    dict(Task="Documentación (Estado del Arte, Apartado 3)", Start='2025-10-20', Finish='2025-11-10', Resource='PEC 2: Estado del Arte'),
    dict(Task="Desarrollo (Arquitectura Base, Sistema Cartas)", Start='2025-10-24', Finish='2025-11-14', Resource='PEC 2: Estado del Arte'),
    dict(Task="Entregables (Vídeo, Revisión Memoria)", Start='2025-11-14', Finish='2025-11-16', Resource='PEC 2: Estado del Arte'),
    dict(Task="HITO: Entrega PEC 2", Start='2025-11-16', Finish='2025-11-16', Resource='HITO'),

    dict(Task="Desarrollo (Core Loop, Mapa, Campamento, IA)", Start='2025-11-17', Finish='2025-12-05', Resource='PEC 3: Versión Jugable'),
    dict(Task="Documentación (Apartado 4 - Diseño Técnico)", Start='2025-11-25', Finish='2025-12-05', Resource='PEC 3: Versión Jugable'),
    dict(Task="Entregables (Build Alpha, Vídeo Gameplay)", Start='2025-12-05', Finish='2025-12-07', Resource='PEC 3: Versión Jugable'),
    dict(Task="HITO: Entrega PEC 3", Start='2025-12-07', Finish='2025-12-07', Resource='HITO'),

    dict(Task="Desarrollo (Pulido, Bugfixing, Balanceo, UI/UX)", Start='2025-12-08', Finish='2025-12-30', Resource='PEC 4: Gold Master'),
    dict(Task="Documentación (Apartados 5 y 6, Revisión final)", Start='2025-12-15', Finish='2025-12-30', Resource='PEC 4: Gold Master'),
    dict(Task="Entregables Finales (Tráiler, Vídeo Defensa, Build GM)", Start='2025-12-28', Finish='2026-01-04', Resource='PEC 4: Gold Master'),
    dict(Task="HITO: Entrega PEC 4", Start='2026-01-04', Finish='2026-01-04', Resource='HITO'),

    dict(Task="Coordinación de fecha", Start='2026-01-05', Finish='2026-01-10', Resource='PEC 5: Defensa'),
    dict(Task="Preparación (Pitch, Repaso)", Start='2026-01-10', Finish='2026-01-15', Resource='PEC 5: Defensa'),
    dict(Task="HITO: Acto de Defensa", Start='2026-01-16', Finish='2026-01-16', Resource='HITO')
]

# --- Colors for each phase ---
colors = {
    'PEC 1: Planificación': 'rgb(220, 57, 18)',
    'PEC 2: Estado del Arte': 'rgb(51, 102, 204)',
    'PEC 3: Versión Jugable': 'rgb(16, 150, 24)',
    'PEC 4: Gold Master': 'rgb(255, 153, 0)',
    'PEC 5: Defensa': 'rgb(153, 0, 153)',
    'HITO': 'rgb(255, 0, 0)' # Red for milestones
}

# --- Create the base chart ---
fig = ff.create_gantt(df, 
                      colors=colors, 
                      index_col='Resource', 
                      show_colorbar=True,
                      group_tasks=True, 
                      title='Plan de Trabajo TFM - Carlos Castellano')

# --- Add vertical lines for Delivery Milestones ---
milestones = [
    dict(date='2025-10-19', label='Entrega PEC 1'),
    dict(date='2025-11-16', label='Entrega PEC 2'),
    dict(date='2025-12-07', label='Entrega PEC 3'),
    dict(date='2026-01-04', label='Entrega PEC 4'),
]

for milestone in milestones:
    # Plotly Gantt uses POSIX timestamp * 1000 (milliseconds)
    date_timestamp_ms = datetime.datetime.strptime(milestone['date'], '%Y-%m-%d').timestamp() * 1000
    
    fig.add_vline(
        x=date_timestamp_ms, 
        line_width=2, 
        line_dash="dash", 
        line_color="red",
        # Add a label to the line
        annotation_text=milestone['label'], 
        annotation_position="top left",
        annotation_font_size=10,
        annotation_font_color="red"
    )

# --- **NOTE: The line fig.update_yaxes(autorange="reversed") has been REMOVED.** ---
# This will make the chart display chronologically by default (oldest at bottom, newest at top).

# --- Save chart to HTML file ---
output_filename = 'mi_planificacion_gantt.html'
fig.write_html(output_filename)

print(f"Success! Your updated Gantt chart has been generated at: {output_filename}")
print("Open it in your browser to see the new order.")