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
import { Detail, Disease, MedicalAppointment } from '../../../lib/types';

interface DetailFormProps {
  open: boolean;
  onClose: () => void;
  onSubmit: (detail: Partial<Detail>) => void;
  diseases: Disease[];
  selectedAppointment: MedicalAppointment | null;
  detail?: Detail | null;
}

export const DetailForm = ({ open, onClose, onSubmit, diseases, selectedAppointment, detail }: DetailFormProps) => {
  const [formData, setFormData] = useState<Partial<Detail>>({
    diagnostic: '',
    observation: '',
    idDisease: '',
    idAppointment: selectedAppointment?.id || ''
  });
  useEffect(() => {
    if (detail) {
      // Si estamos editando un detalle existente
      setFormData({
        ...detail,
      });
    } else if (selectedAppointment) {
      // Si estamos creando un nuevo detalle para una cita existente
      setFormData({
        diagnostic: '',
        observation: '',
        idDisease: '',
        idAppointment: selectedAppointment.id
      });
    } else {
      // Estado inicial/reseteo
      setFormData({
        diagnostic: '',
        observation: '',
        idDisease: '',
        idAppointment: ''
      });
    }
  }, [detail, selectedAppointment, open]);

const handleChange = (
    e: React.ChangeEvent<HTMLInputElement> | React.ChangeEvent<{ name?: string; value: unknown }> | SelectChangeEvent<string>
  ) => {
    const name = e.target.name as keyof Detail;
    const value = e.target.value;

    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };
  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!selectedAppointment) {
      console.error('No appointment selected');
      return;
    }
    
    const submissionData: Partial<Detail> = {
      ...formData,
      idDisease: String(formData.idDisease),
      idAppointment: selectedAppointment.id
    };

    onSubmit(submissionData);
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <form onSubmit={handleSubmit}>
        <DialogTitle>
          {detail ? 'Editar Detalle' : 'Nuevo Detalle'}
        </DialogTitle>
        <DialogContent>
          <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2, pt: 2 }}>
            <FormControl fullWidth>
              <InputLabel>Enfermedad</InputLabel>
              <Select
                name="idDisease"
                value={formData.idDisease || ''}
                onChange={handleChange}
                label="Enfermedad"
                required
              >
                {diseases.map((disease) => (
                  <MenuItem key={disease.id} value={disease.id}>
                    {disease.name}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>

            <TextField
              name="diagnostic"
              label="Diagnóstico"
              value={formData.diagnostic || ''}
              onChange={handleChange}
              multiline
              rows={3}
              fullWidth
              required
            />

            <TextField
              name="observation"
              label="Observaciones"
              value={formData.observation || ''}
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
            {detail ? 'Actualizar' : 'Crear'}
          </Button>
        </DialogActions>
      </form>
    </Dialog>
  );
};
