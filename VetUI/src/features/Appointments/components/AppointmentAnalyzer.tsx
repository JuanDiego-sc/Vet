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
  Chip,
  IconButton,
  Snackbar
} from "@mui/material";
import { Edit as EditIcon, Save as SaveIcon, Cancel as CancelIcon } from "@mui/icons-material";
import { useAnalyzer } from "../../../lib/hooks/useAnalyzer";
import { useMedicines } from "../../../lib/hooks/useMedicines";

export default function AppointmentAnalyzer() {
  const [startDateUI, setStartDateUI] = useState(new Date().toISOString().split('T')[0]);
  const [endDateUI, setEndDateUI] = useState(new Date().toISOString().split('T')[0]);
  
  // Estados para manejar la edición de stock
  const [editingStock, setEditingStock] = useState<{ [key: string]: boolean }>({});
  const [tempStockValues, setTempStockValues] = useState<{ [key: string]: string }>({});
  const [updateSuccess, setUpdateSuccess] = useState<string | null>(null);
  
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
  const { medicines, isPending, updateMedicine } = useMedicines();

  const handleAnalyze = () => {
    console.log('Analyze button clicked with dates:', { 
      startDateUI, 
      endDateUI, 
      startDateFormatted: startDate, 
      endDateFormatted: endDate 
    });
    setShouldFetch(true);
  };

  // Función para encontrar la medicina por nombre
  const findMedicineByName = (medicineName: string) => {
    return medicines?.find(medicine => 
      medicine.name.toLowerCase() === medicineName.toLowerCase()
    );
  };

  // Iniciar edición de stock - MODIFICADO para inicializar en 0
  const handleStartEdit = (medicineName: string, currentStock: number) => {
    setEditingStock(prev => ({ ...prev, [medicineName]: true }));
    // Cambio: inicializar en "0" en lugar del stock actual para agregar cantidad
    setTempStockValues(prev => ({ ...prev, [medicineName]: "0" }));
  };

  // Cancelar edición
  const handleCancelEdit = (medicineName: string) => {
    setEditingStock(prev => ({ ...prev, [medicineName]: false }));
    setTempStockValues(prev => {
      const newValues = { ...prev };
      delete newValues[medicineName];
      return newValues;
    });
  };

  // Guardar cambios de stock - MODIFICADO para sumar en lugar de reemplazar
  const handleSaveStock = async (medicineName: string) => {
    const medicine = findMedicineByName(medicineName);
    const stockToAdd = parseInt(tempStockValues[medicineName]);
    
    if (!medicine || isNaN(stockToAdd) || stockToAdd < 0) {
      alert('Valor de stock a agregar inválido');
      return;
    }

    // Cambio principal: sumar el nuevo stock al stock existente
    const newTotalStock = medicine.stock + stockToAdd;

    try {
      await updateMedicine.mutateAsync({
        ...medicine,
        stock: newTotalStock // Usar el total calculado
      });
      
      setEditingStock(prev => ({ ...prev, [medicineName]: false }));
      setTempStockValues(prev => {
        const newValues = { ...prev };
        delete newValues[medicineName];
        return newValues;
      });
      
      // Mensaje más descriptivo mostrando la suma
      setUpdateSuccess(`Se agregaron ${stockToAdd} unidades a ${medicineName}. Stock total: ${newTotalStock}`);
      
      // Opcional: Refrescar el análisis para obtener datos actualizados
      if (shouldFetch) {
        setShouldFetch(false);
        setTimeout(() => setShouldFetch(true), 100);
      }
      
    } catch (error) {
      console.error('Error updating medicine stock:', error);
      alert('Error al actualizar el stock');
    }
  };

  // Manejar cambio en el input de stock
  const handleStockChange = (medicineName: string, value: string) => {
    setTempStockValues(prev => ({ ...prev, [medicineName]: value }));
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
                {result.stockAlerts.map((alert, index) => {
                  const medicine = findMedicineByName(alert.medicineName);
                  const isEditing = editingStock[alert.medicineName];
                  const currentStock = medicine?.stock ?? alert.currentStock;
                  
                  return (
                    <Alert severity="warning" key={index} sx={{ mt: 1 }}>
                      <Box width="100%">
                        <Typography variant="body2" gutterBottom>
                          <strong>{alert.medicineName}</strong>
                        </Typography>
                        
                        <Box display="flex" alignItems="center" gap={1} mb={2}>
                          <Chip 
                            size="small" 
                            label={`Stock actual: ${currentStock}`} 
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
                            label={`Faltante: ${alert.requiredStock - currentStock}`} 
                            color="error"
                          />
                        </Box>

                        {/* Campo de actualización de stock */}
                        <Box display="flex" alignItems="center" gap={1}>
                          {isEditing ? (
                            <>
                              <TextField
                                size="small"
                                type="number"
                                label="Cantidad a Agregar"
                                value={tempStockValues[alert.medicineName] || ''}
                                onChange={(e) => handleStockChange(alert.medicineName, e.target.value)}
                                InputProps={{
                                  inputProps: { min: 0 }
                                }}
                                sx={{ width: 140 }}
                              />
                              <IconButton
                                size="small"
                                color="primary"
                                onClick={() => handleSaveStock(alert.medicineName)}
                                disabled={isPending}
                              >
                                <SaveIcon />
                              </IconButton>
                              <IconButton
                                size="small"
                                color="secondary"
                                onClick={() => handleCancelEdit(alert.medicineName)}
                              >
                                <CancelIcon />
                              </IconButton>
                            </>
                          ) : (
                            <>
                              <Typography variant="body2" color="textSecondary">
                                Agregar stock:
                              </Typography>
                              <IconButton
                                size="small"
                                color="primary"
                                onClick={() => handleStartEdit(alert.medicineName, currentStock)}
                                disabled={!medicine || isPending}
                              >
                                <EditIcon />
                              </IconButton>
                              {!medicine && (
                                <Typography variant="caption" color="error">
                                  Medicina no encontrada en el sistema
                                </Typography>
                              )}
                            </>
                          )}
                        </Box>
                      </Box>
                    </Alert>
                  );
                })}
              </Box>
            )}
          </Box>
        ) : (
          <Typography align="center" color="textSecondary">
            Seleccione un rango de fechas y presione Analizar para ver los resultados
          </Typography>
        )}
      </Paper>

      {/* Snackbar para mostrar éxito */}
      <Snackbar
        open={!!updateSuccess}
        autoHideDuration={4000}
        onClose={() => setUpdateSuccess(null)}
        message={updateSuccess}
      />
    </Box>
  );
}