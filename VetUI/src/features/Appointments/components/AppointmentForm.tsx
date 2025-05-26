import { useState, useEffect } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  Box,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  SelectChangeEvent,
} from '@mui/material';
import { Pet, MedicalAppointment } from '../../../lib/types';

interface AppointmentFormProps {
  open: boolean;
  onClose: () => void;
  onSubmit: (appointment: Partial<MedicalAppointment>) => void;
  pets: Pet[];
  appointment?: MedicalAppointment | null;
}

const STATUS_OPTIONS = [
  { value: 0, label: 'Pendiente' },
  { value: 1, label: 'Confirmada' },
  { value: 2, label: 'Cancelada' },
  { value: 3, label: 'Completada' }
];

export const AppointmentForm = ({ open, onClose, onSubmit, pets, appointment }: AppointmentFormProps) => {
  const [formData, setFormData] = useState<Partial<MedicalAppointment>>({
    appointmentDate: '',
    appointmentStatus: 0,
    reason: '',
    idPet: ''
  });

  useEffect(() => {
    if (appointment) {
      const appointmentDate = new Date(appointment.appointmentDate)
        .toISOString()
        .split('T')[0]; 

      setFormData({
        ...appointment,
        appointmentDate
      });
    } else {
      // Si es creación, resetear el formulario
      setFormData({
        appointmentDate: '',
        appointmentStatus: 0,
        reason: '',
        idPet: ''
      });
    }
  }, [appointment, open]);

  const handleChange = (
      e: React.ChangeEvent<HTMLInputElement> | React.ChangeEvent<{ name?: string; value: unknown }> | SelectChangeEvent<string | number>
    )=> {
    const name = e.target.name as keyof MedicalAppointment;
    let value = e.target.value;

    // Asegurarse de que appointmentStatus sea número
    if (name === 'appointmentStatus') {
      value = Number(value);
    }

    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    // Asegurarse de que los tipos sean correctos antes de enviar
    const submissionData: Partial<MedicalAppointment> = {
      ...formData,
      appointmentStatus: Number(formData.appointmentStatus),
      idPet: String(formData.idPet)
    };

    onSubmit(submissionData);
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <form onSubmit={handleSubmit}>
        <DialogTitle>
          {appointment ? 'Editar Cita Médica' : 'Nueva Cita Médica'}
        </DialogTitle>
        <DialogContent>
          <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2, pt: 2 }}>
            <FormControl fullWidth>
              <InputLabel>Mascota</InputLabel>
              <Select
                name="idPet"
                value={formData.idPet || ''}
                onChange={handleChange}
                label="Mascota"
                required
              >
                {pets.map((pet) => (
                  <MenuItem key={pet.id} value={pet.id}>
                    {pet.petName}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>

            <TextField
              name="appointmentDate"
              label="Fecha de Cita"
              type="datetime-local"
              value={formData.appointmentDate || ''}
              onChange={handleChange}
              fullWidth
              required
              InputLabelProps={{ shrink: true }}
            />

            <FormControl fullWidth>
              <InputLabel>Estado</InputLabel>
              <Select
                name="appointmentStatus"
                value={formData.appointmentStatus}
                onChange={handleChange}
                label="Estado"
                required
              >
                {STATUS_OPTIONS.map((status) => (
                  <MenuItem key={status.value} value={status.value}>
                    {status.label}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>

            <TextField
              name="reason"
              label="Motivo" 
              value={formData.reason || ''}
              onChange={handleChange}
              multiline
              rows={3}
              fullWidth
              required
            />
          </Box>
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose}>Cancelar</Button>
          <Button type="submit" variant="contained" color="primary">
            {appointment ? 'Actualizar' : 'Crear'}
          </Button>
        </DialogActions>
      </form>
    </Dialog>
  );
};
