import { useState } from 'react';
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Button,
  Typography,
  Box,
  CircularProgress,
} from '@mui/material';
import { useDetails } from '../../../lib/hooks/useDetails';
import { useDiseases } from '../../../lib/hooks/useDiseases';
import { Detail, Treatment } from '../../../lib/types';
import { TreatmentForm } from '../../Appointments/components/TreatmentForm';
import { useMedicines } from '../../../lib/hooks/useMedicines';
import { useTreatment } from '../../../lib/hooks/useTreatments';

export const DetailTable = () => {
  const { Details: details, isPending, deleteDetail } = useDetails();
  const { createTreatment, updateTreatment } = useTreatment();
  const { diseases } = useDiseases();
  const { medicines } = useMedicines();
  const [isOpenTreatment, setIsOpenTreatment] = useState(false);
  const [selectedDetail, setSelectedDetail] = useState<Detail | null>(null);
  const [selectedTreatment, setSelectedTreatment] = useState<Treatment | null>(null);

  const handleDelete = async (id: string) => {
    if (window.confirm('¿Está seguro de eliminar este detalle?')) {
      await deleteDetail.mutateAsync(id);
    }
  };

  const handleAddTreatment = (detail: Detail) => {
    setSelectedDetail(detail);
    setIsOpenTreatment(true);
  };

  const handleSubmitTreatment = async (formData: Partial<Treatment>) => {
    if (selectedTreatment) {
      await updateTreatment.mutateAsync({ ...selectedTreatment, ...formData });
    } else {
      await createTreatment.mutateAsync(formData as Treatment);
    }
    setIsOpenTreatment(false);
    setSelectedTreatment(null);
  };

  const getDiseaseName = (diseaseId: string) => {
    const disease = diseases?.find(d => d.id === diseaseId);
    return disease?.name || 'Enfermedad no encontrada';
  };

  if (isPending) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
        <CircularProgress />
      </Box>
    );
  }

  const detailsList = Array.isArray(details) ? details : [];

  return (
    <Box sx={{ p: 3 }}>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
        <Typography variant="h4" component="h2">
          Detalles Médicos
        </Typography>
      </Box>      

      <TreatmentForm
        open={isOpenTreatment}
        onClose={() => {
          setIsOpenTreatment(false);
          setSelectedDetail(null);
        }}
        onSubmit={handleSubmitTreatment}
        medicines={medicines || []}
        selectedDetail={selectedDetail}
      />

      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Enfermedad</TableCell>
              <TableCell>Diagnóstico</TableCell>
              <TableCell>Observaciones</TableCell>
              <TableCell>Acciones</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {detailsList.map((detail) => (
              <TableRow key={detail.id}>
                <TableCell>{getDiseaseName(detail.idDisease)}</TableCell>
                <TableCell>{detail.diagnostic}</TableCell>
                <TableCell>{detail.observation}</TableCell>
                <TableCell>
                  <Box sx={{ display: 'flex', gap: 1 }}>
                    <Button
                      variant="outlined"
                      size="small"
                      color="success"
                      onClick={() => handleAddTreatment(detail)}
                    >
                      Agregar Tratamiento
                    </Button>
                    <Button
                      variant="outlined"
                      color="error"
                      size="small"
                      onClick={() => handleDelete(detail.id)}
                    >
                      Eliminar
                    </Button>
                  </Box>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Box>
  );
};