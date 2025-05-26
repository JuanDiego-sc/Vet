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
import { Treatment, Medicine, Detail } from '../../../lib/types';

interface TreatmentFormProps {
  open: boolean;
  onClose: () => void;
  onSubmit: (treatment: Partial<Treatment>) => void;
  medicines: Medicine[];
  treatment?: Treatment | null;
  selectedDetail: Detail | null;
}

export const TreatmentForm = ({ open, onClose, onSubmit, medicines, treatment, selectedDetail }: TreatmentFormProps) => {
  const [formData, setFormData] = useState<Partial<Treatment>>({
    duration: 0,
    dose: '',
    contraindications: '',
    idMedicine: '',
    idDetail: selectedDetail?.id || ''
  });
  useEffect(() => {
    if (treatment) {
      // Si estamos editando un tratamiento existente
      setFormData({
        ...treatment,
      });
    } else if (selectedDetail) {
      // Si estamos creando un nuevo tratamiento para un detalle existente
      setFormData({
        duration: 0,
        dose: '',
        contraindications: '',
        idMedicine: '',
        idDetail: selectedDetail.id
      });
    } else {
      // Estado inicial/reseteo
      setFormData({
        duration: 0,
        dose: '',
        contraindications: '',
        idMedicine: '',
        idDetail: ''
      });
    }
  }, [treatment, selectedDetail, open]);
  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement> | React.ChangeEvent<{ name?: string; value: unknown }> | SelectChangeEvent<string>
  ) => {
    const name = e.target.name as keyof Treatment;
    let value = e.target.value;

    // Convertir duration a número
    if (name === 'duration') {
      value = Number(value);
    }

    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };
  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!selectedDetail) {
      console.error('No detail selected');
      return;
    }
    
    const submissionData: Partial<Treatment> = {
      ...formData,
      duration: Number(formData.duration),
      idMedicine: String(formData.idMedicine),
      idDetail: selectedDetail.id
    };

    onSubmit(submissionData);
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <form onSubmit={handleSubmit}>
        <DialogTitle>
          {treatment ? 'Editar Tratamiento' : 'Nuevo Tratamiento'}
        </DialogTitle>
        <DialogContent>
          <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2, pt: 2 }}>
            <FormControl fullWidth>
              <InputLabel>Medicamento</InputLabel>
              <Select
                name="idMedicine"
                value={formData.idMedicine || ''}
                onChange={handleChange}
                label="Medicamento"
                required
              >
                {medicines.map((medicine) => (
                  <MenuItem key={medicine.id} value={medicine.id}>
                    {medicine.name}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>

            <TextField
              name="duration"
              label="Duración (días)"
              type="number"
              value={formData.duration || ''}
              onChange={handleChange}
              fullWidth
              required
              inputProps={{ min: 1 }}
            />

            <TextField
              name="dose"
              label="Dosis"
              value={formData.dose || ''}
              onChange={handleChange}
              multiline
              rows={2}
              fullWidth
              required
            />

            <TextField
              name="contraindications"
              label="Contraindicaciones"
              value={formData.contraindications || ''}
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
            {treatment ? 'Actualizar' : 'Crear'}
          </Button>
        </DialogActions>
      </form>
    </Dialog>
  );
};
