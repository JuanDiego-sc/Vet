import { useState } from "react";
import { 
  Box, 
  Paper, 
  TextField, 
  Typography, 
  CircularProgress,
  List,
  ListItem,
  ListItemText,
  Alert,
  Button,
  Chip
} from "@mui/material";
import { useAnalyzer } from "../../../lib/hooks/useAnalyzer";

export default function AppointmentAnalyzer() {
  const [startDateUI, setStartDateUI] = useState(new Date().toISOString().split('T')[0]);
  const [endDateUI, setEndDateUI] = useState(new Date().toISOString().split('T')[0]);
  
  const formatDateForAPI = (dateString: string, isEndDate: boolean = false) => {
    const time = isEndDate ? "T23:59:59" : "T00:00:00";
    return `${dateString}${time}`;
  };
  
  const startDate = formatDateForAPI(startDateUI);
  const endDate = formatDateForAPI(endDateUI, true);
  const [shouldFetch, setShouldFetch] = useState(false);
  const { result, isLoadingAnalyze, error } = useAnalyzer(
    shouldFetch ? startDate : null,
    shouldFetch ? endDate : null
  );

  const handleAnalyze = () => {
    console.log('Analyze button clicked with dates:', { 
      startDateUI, 
      endDateUI, 
      startDateFormatted: startDate, 
      endDateFormatted: endDate 
    });
    setShouldFetch(true);
  };

  return (
    <Box>
      <Typography variant="h4" gutterBottom align="center">
        Analizador de Citas
      </Typography>
      <Box display="flex" alignItems="center" justifyContent="center" mb={4}>
        <TextField
          name="startDate"
          label="Fecha Inicial"
          type="date"
          value={startDateUI}
          onChange={(e) => setStartDateUI(e.target.value)}
          sx={{ mx: 1 }}
          InputLabelProps={{ shrink: true }}
        />
        <TextField
          name="endDate"
          label="Fecha Final"
          type="date"
          value={endDateUI}
          onChange={(e) => setEndDateUI(e.target.value)}
          sx={{ mx: 1 }}
          InputLabelProps={{ shrink: true }}
        />
        <Button 
          variant="contained" 
          color="primary" 
          onClick={handleAnalyze}
          sx={{ mx: 1 }}
        >
          Analizar
        </Button>
      </Box>

      <Paper sx={{ p: 3, mt: 2 }}>
        {error && (
          <Alert severity="error" sx={{ mb: 2 }}>
            Error al obtener los datos: {error.message}
          </Alert>
        )}
        
        {isLoadingAnalyze ? (
          <Box display="flex" justifyContent="center">
            <CircularProgress />
          </Box>
        ) : result ? (
          <Box>
            <Box mb={3}>
              {result.diseaseName && (
                <>
                  <Typography variant="h6" gutterBottom>
                    Enfermedad: {result.diseaseName}
                  </Typography>
                  <Typography variant="subtitle1">
                    Número de casos: {result.caseCount || 0}
                  </Typography>
                </>
              )}
            </Box>
            
            <Box mb={3}>
              <Typography variant="h6" gutterBottom>
                Medicamentos Utilizados:
              </Typography>
              <List>
                {result.usedMedicines && result.usedMedicines.length > 0 ? (
                  result.usedMedicines.map((medicine, index) => (
                    <ListItem key={index}>
                      <ListItemText primary={medicine} />
                    </ListItem>
                  ))
                ) : (
                  <ListItem>
                    <ListItemText primary="No hay medicamentos registrados" />
                  </ListItem>
                )}
              </List>
            </Box>

            {result.hasStockIssues && result.stockAlerts && result.stockAlerts.length > 0 && (
              <Box>
                <Typography variant="h6" gutterBottom color="error">
                  Alertas de Stock:
                </Typography>
                {result.stockAlerts.map((alert, index) => (
                  <Alert severity="warning" key={index} sx={{ mt: 1 }}>
                    <Typography variant="body2">
                      <strong>{alert.medicineName}</strong>
                    </Typography>
                    <Box display="flex" alignItems="center" gap={1} mt={1}>
                      <Chip 
                        size="small" 
                        label={`Stock actual: ${alert.currentStock}`} 
                        color="error" 
                        variant="outlined"
                      />
                      <Chip 
                        size="small" 
                        label={`Stock requerido: ${alert.requiredStock}`} 
                        color="warning" 
                        variant="outlined"
                      />
                      <Chip 
                        size="small" 
                        label={`Faltante: ${alert.requiredStock - alert.currentStock}`} 
                        color="error"
                      />
                    </Box>
                  </Alert>
                ))}
              </Box>
            )}
          </Box>
        ) : (
          <Typography align="center" color="textSecondary">
            Seleccione un rango de fechas y presione Analizar para ver los resultados
          </Typography>
        )}
      </Paper>
    </Box>
  );
}