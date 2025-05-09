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
import { MedicalAppointment } from '../../../lib/types';
import { FormDialog } from '../../../components/common/FormDialog';
import { validations } from '../../../lib/utils/validations';

export const AppointmentsTable = () => {
  const { appointments, isPending, createAppointment, updateAppointment, deleteAppointment } = useAppointments();
  const { pets } = usePets();
  const [isOpen, setIsOpen] = useState(false);
  const [selectedAppointment, setSelectedAppointment] = useState<MedicalAppointment | null>(null);

  const initialFormData = {
    appointmentDate: '',
    appointmentStatus: 0,
    reason: '',
    idPet: '',
  };

  const formFields = [
    {
      name: 'idPet',
      label: 'Mascota',
      validation: validations.name,
      options: pets?.map(pet => ({
        value: pet.id,
        label: pet.petName
      })) || []
    },
    {
      name: 'appointmentDate',
      label: 'Fecha de Cita',
      type: 'datetime-local',
      validation: validations.date,
    },
    {
      name: 'appointmentStatus',
      label: 'Estado',
      validation: validations.name,
      options: [
        { value: 0, label: 'Pendiente' },
        { value: 1, label: 'Confirmada' },
        { value: 2, label: 'Cancelada' },
        { value: 3, label: 'Completada' }
      ]
    },
    {
      name: 'reason',
      label: 'Motivo',
      validation: validations.description,
    },
  ];

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

      <FormDialog
        open={isOpen}
        onClose={() => {
          setIsOpen(false);
          setSelectedAppointment(null);
        }}
        onSubmit={handleSubmit}
        title={selectedAppointment ? 'Editar Cita' : 'Nueva Cita'}
        fields={formFields}
        initialData={selectedAppointment || initialFormData}
        submitButtonText={selectedAppointment ? 'Actualizar' : 'Crear'}
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