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
import { useAppointments } from '../../../lib/hooks/useAppointments';
import { usePets } from '../../../lib/hooks/usePets';
import { Detail, MedicalAppointment} from '../../../lib/types';
import { AppointmentForm } from '../components/AppointmentForm';
import { DetailForm } from '../components/DetailForm';
import { useDiseases } from '../../../lib/hooks/useDiseases';
import { useDetails } from '../../../lib/hooks/useDetails';

export const AppointmentsTable = () => {
  const { appointments, isPending, createAppointment, updateAppointment, deleteAppointment } = useAppointments();
  const { createDetail, updateDetail } = useDetails();
  const { pets } = usePets();
  const { diseases } = useDiseases();
  const [isOpen, setIsOpen] = useState(false);
  const [isOpenDetail, setIsOpenDetail] = useState(false);
  const [selectedAppointment, setSelectedAppointment] = useState<MedicalAppointment | null>(null);
  const [selectedDetail, setSelectedDetail] = useState<Detail | null>(null);

  const handleSubmit = async (formData: Partial<MedicalAppointment>) => {
    if (selectedAppointment) {
      await updateAppointment.mutateAsync({ ...selectedAppointment, ...formData });
    } else {
      await createAppointment.mutateAsync(formData as MedicalAppointment);
    }
    setIsOpen(false);
    setSelectedAppointment(null);
  };

  const handleEdit = (appointment: MedicalAppointment) => {
    setSelectedAppointment(appointment);
    setIsOpen(true);
  };

  const handleDelete = async (id: string) => {
    if (window.confirm('¿Está seguro de eliminar esta cita?')) {
      await deleteAppointment.mutateAsync(id);
    }
  };

  const handleSubmitDetail = async (formData: Partial<Detail>) => {
    if (selectedDetail) {
      await updateDetail.mutateAsync({ ...selectedDetail, ...formData });
    } else {
      await createDetail.mutateAsync(formData as Detail);
    }
    setIsOpenDetail(false);
    setSelectedDetail(null);
  };

  const handleEditDetail = (appointment: MedicalAppointment) => {
    setSelectedAppointment(appointment);
    setIsOpenDetail(true);
  };

  const getStatusLabel = (status: number) => {
    switch (status) {
      case 0: return 'Pendiente';
      case 1: return 'Confirmada';
      case 2: return 'Cancelada';
      case 3: return 'Completada';
      default: return 'Desconocido';
    }
  };

  const getPetName = (petId: string) => {
    const pet = pets?.find(p => p.id === petId);
    return pet?.petName || 'Mascota no encontrada';
  };

  if (isPending) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
        <CircularProgress />
      </Box>
    );
  }

  const appointmentsList = Array.isArray(appointments) ? appointments : [];

  return (
    <Box sx={{ p: 3 }}>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
        <Typography variant="h4" component="h2">
          Citas Médicas
        </Typography>
        <Button
          variant="contained"
          color="primary"
          onClick={() => {
            setSelectedAppointment(null);
            setIsOpen(true);
          }}
        >
          Agregar Cita
        </Button>
      </Box>      
      <AppointmentForm
        open={isOpen}
        onClose={() => {
          setIsOpen(false);
          setSelectedAppointment(null);
        }}
        onSubmit={handleSubmit}
        pets={pets || []}
        appointment={selectedAppointment}
      />

      <DetailForm
        open={isOpenDetail}
        onClose={() => {
          setIsOpenDetail(false);
          setSelectedAppointment(null);
        }}
        onSubmit={handleSubmitDetail}
        diseases={diseases || []}
        selectedAppointment={selectedAppointment}
      />

      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Mascota</TableCell>
              <TableCell>Fecha</TableCell>
              <TableCell>Estado</TableCell>
              <TableCell>Motivo</TableCell>
              <TableCell>Acciones</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {appointmentsList.map((appointment) => (
              <TableRow key={appointment.id}>
                <TableCell>{getPetName(appointment.idPet)}</TableCell>
                <TableCell>{new Date(appointment.appointmentDate).toLocaleString()}</TableCell>
                <TableCell>{getStatusLabel(appointment.appointmentStatus)}</TableCell>
                <TableCell>{appointment.reason}</TableCell>
                <TableCell>
                  <Box sx={{ display: 'flex', gap: 1 }}>
                    <Button
                      variant="outlined"
                      size="small"
                      onClick={() => handleEdit(appointment)}
                    >
                      Editar
                    </Button>
                    <Button
                      variant="outlined"
                      size="small"
                      color='success'
                      onClick={() => handleEditDetail(appointment)}
                    >
                      Detalle
                    </Button>
                    <Button
                      variant="outlined"
                      color="error"
                      size="small"
                      onClick={() => handleDelete(appointment.id)}
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