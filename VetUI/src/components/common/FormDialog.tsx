import { useState, useEffect } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  Box,
  Alert,
  Select,
  MenuItem,
  FormControl,
  InputLabel,
} from '@mui/material';

interface FormField {
  name: string;
  label: string;
  type?: string;
  validation: (value: string) => string;
  options?: { value: string | number; label: string }[];
}

interface FormDialogProps<T extends Record<string, string | number>> {
  open: boolean;
  onClose: () => void;
  onSubmit: (formData: T) => void;
  title: string;
  fields: FormField[];
  initialData: T;
  submitButtonText: string;
}

export const FormDialog = <T extends Record<string, string | number>>({
  open,
  onClose,
  onSubmit,
  title,
  fields,
  initialData,
  submitButtonText,
}: FormDialogProps<T>) => {
  const [formData, setFormData] = useState<T>(initialData);
  const [errors, setErrors] = useState<Record<string, string>>({});
  const [formError, setFormError] = useState<string>('');

  useEffect(() => {
    setFormData(initialData);
    setErrors({});
    setFormError('');
  }, [initialData, open]);

  const validateField = (name: string, value: string | number) => {
    const field = fields.find(f => f.name === name);
    if (field) {
      return field.validation(String(value));
    }
    return '';
  };

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement> | { target: { name: string; value: string | number } }
  ) => {
    const name = e.target.name;
    const value = e.target.value;
    setFormData(prev => ({ ...prev, [name]: value }));
    
    const error = validateField(name, value);
    setErrors(prev => ({
      ...prev,
      [name]: error
    }));
  };

  const validateForm = () => {
    const newErrors: Record<string, string> = {};
    let isValid = true;

    fields.forEach(field => {
      const error = validateField(field.name, formData[field.name] || '');
      if (error) {
        newErrors[field.name] = error;
        isValid = false;
      }
    });

    setErrors(newErrors);
    return isValid;
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setFormError('');

    if (validateForm()) {
      try {
        onSubmit(formData);
      } catch (error) {
        setFormError(error+' Ha ocurrido un error al procesar el formulario');
      }
    }
  };

  const renderField = (field: FormField) => {
    if (field.options) {
      return (
        <FormControl fullWidth error={!!errors[field.name]} required>
          <InputLabel>{field.label}</InputLabel>
          <Select
            name={field.name}
            value={formData[field.name] || ''}
            onChange={handleChange}
            label={field.label}
          >
            {field.options.map(option => (
              <MenuItem key={option.value} value={option.value}>
                {option.label}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
      );
    }

    return (
      <TextField
        key={field.name}
        name={field.name}
        label={field.label}
        type={field.type || 'text'}
        value={formData[field.name] || ''}
        onChange={handleChange}
        error={!!errors[field.name]}
        helperText={errors[field.name]}
        required
        fullWidth
        InputLabelProps={field.type === 'date' ? { shrink: true } : undefined}
      />
    );
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogTitle>{title}</DialogTitle>
      <form onSubmit={handleSubmit}>
        <DialogContent>
          {formError && (
            <Alert severity="error" sx={{ mb: 2 }}>
              {formError}
            </Alert>
          )}
          <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
            {fields.map((field) => renderField(field))}
          </Box>
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose}>Cancelar</Button>
          <Button type="submit" variant="contained" color="primary">
            {submitButtonText}
          </Button>
        </DialogActions>
      </form>
    </Dialog>
  );
}; 