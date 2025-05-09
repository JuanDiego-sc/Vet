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
import { useMedicines } from '../../../lib/hooks/useMedicines';
import { Medicine } from '../../../lib/types';
import { FormDialog } from '../../../components/common/FormDialog';
import { validations } from '../../../lib/utils/validations';

export const MedicinesTable = () => {
  const { medicines, isPending, createMedicine, updateMedicine, deleteMedicine } = useMedicines();
  const [isOpen, setIsOpen] = useState(false);
  const [selectedMedicine, setSelectedMedicine] = useState<Medicine | null>(null);

  const initialFormData = {
    name: '',
    stock: 0,
    description: '',
  };

  const formFields = [
    {
      name: 'name',
      label: 'Nombre',
      validation: validations.name,
    },
    {
      name: 'stock',
      label: 'Stock',
      type: 'number',
      validation: validations.positiveNumber,
    },
    {
      name: 'description',
      label: 'Descripción',
      validation: validations.description,
    },
  ];

  const handleSubmit = async (formData: Partial<Medicine>) => {
    if (selectedMedicine) {
      await updateMedicine.mutateAsync({ ...selectedMedicine, ...formData });
    } else {
      await createMedicine.mutateAsync(formData as Medicine);
    }
    setIsOpen(false);
    setSelectedMedicine(null);
  };

  const handleEdit = (medicine: Medicine) => {
    setSelectedMedicine(medicine);
    setIsOpen(true);
  };

  const handleDelete = async (id: string) => {
    if (window.confirm('¿Está seguro de eliminar este medicamento?')) {
      await deleteMedicine.mutateAsync(id);
    }
  };

  if (isPending) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
        <CircularProgress />
      </Box>
    );
  }

  const medicinesList = Array.isArray(medicines) ? medicines : [];

  return (
    <Box sx={{ p: 3 }}>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
        <Typography variant="h4" component="h2">
          Medicamentos
        </Typography>
        <Button
          variant="contained"
          color="primary"
          onClick={() => {
            setSelectedMedicine(null);
            setIsOpen(true);
          }}
        >
          Agregar Medicamento
        </Button>
      </Box>

      <FormDialog
        open={isOpen}
        onClose={() => {
          setIsOpen(false);
          setSelectedMedicine(null);
        }}
        onSubmit={handleSubmit}
        title={selectedMedicine ? 'Editar Medicamento' : 'Nuevo Medicamento'}
        fields={formFields}
        initialData={selectedMedicine || initialFormData}
        submitButtonText={selectedMedicine ? 'Actualizar' : 'Crear'}
      />

      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Nombre</TableCell>
              <TableCell>Stock</TableCell>
              <TableCell>Descripción</TableCell>
              <TableCell>Acciones</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {medicinesList.map((medicine) => (
              <TableRow key={medicine.id}>
                <TableCell>{medicine.name}</TableCell>
                <TableCell>{medicine.stock}</TableCell>
                <TableCell>{medicine.description}</TableCell>
                <TableCell>
                  <Box sx={{ display: 'flex', gap: 1 }}>
                    <Button
                      variant="outlined"
                      size="small"
                      onClick={() => handleEdit(medicine)}
                    >
                      Editar
                    </Button>
                    <Button
                      variant="outlined"
                      color="error"
                      size="small"
                      onClick={() => handleDelete(medicine.id)}
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