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
import { useDiseases } from '../../../lib/hooks/useDiseases';
import { Disease } from '../../../lib/types';
import { FormDialog } from '../../../components/common/FormDialog';
import { validations } from '../../../lib/utils/validations';

export const DiseasesTable = () => {
  const { diseases, isPending, createDisease, updateDisease, deleteDisease } = useDiseases();
  const [isOpen, setIsOpen] = useState(false);
  const [selectedDisease, setSelectedDisease] = useState<Disease | null>(null);

  const initialFormData = {
    name: '',
    type: '',
    description: '',
  };

  const formFields = [
    {
      name: 'name',
      label: 'Nombre',
      validation: validations.name,
    },
    {
      name: 'type',
      label: 'Tipo',
      validation: validations.name,
      options: [
        { value: 'Viral', label: 'Viral' },
        { value: 'Bacteriana', label: 'Bacteriana' },
        { value: 'Fúngica', label: 'Fúngica' },
        { value: 'Parasitaria', label: 'Parasitaria' },
        { value: 'Otra', label: 'Otra' }
      ]
    },
    {
      name: 'description',
      label: 'Descripción',
      validation: validations.description,
    },
  ];

  const handleSubmit = async (formData: Partial<Disease>) => {
    if (selectedDisease) {
      await updateDisease.mutateAsync({ ...selectedDisease, ...formData });
    } else {
      await createDisease.mutateAsync(formData as Disease);
    }
    setIsOpen(false);
    setSelectedDisease(null);
  };

  const handleEdit = (disease: Disease) => {
    setSelectedDisease(disease);
    setIsOpen(true);
  };

  const handleDelete = async (id: string) => {
    if (window.confirm('¿Está seguro de eliminar esta enfermedad?')) {
      await deleteDisease.mutateAsync(id);
    }
  };

  if (isPending) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
        <CircularProgress />
      </Box>
    );
  }

  const diseasesList = Array.isArray(diseases) ? diseases : [];

  return (
    <Box sx={{ p: 3 }}>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
        <Typography variant="h4" component="h2">
          Enfermedades
        </Typography>
        <Button
          variant="contained"
          color="primary"
          onClick={() => {
            setSelectedDisease(null);
            setIsOpen(true);
          }}
        >
          Agregar Enfermedad
        </Button>
      </Box>

      <FormDialog
        open={isOpen}
        onClose={() => {
          setIsOpen(false);
          setSelectedDisease(null);
        }}
        onSubmit={handleSubmit}
        title={selectedDisease ? 'Editar Enfermedad' : 'Nueva Enfermedad'}
        fields={formFields}
        initialData={selectedDisease || initialFormData}
        submitButtonText={selectedDisease ? 'Actualizar' : 'Crear'}
      />

      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Nombre</TableCell>
              <TableCell>Tipo</TableCell>
              <TableCell>Descripción</TableCell>
              <TableCell>Acciones</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {diseasesList.map((disease) => (
              <TableRow key={disease.id}>
                <TableCell>{disease.name}</TableCell>
                <TableCell>{disease.type}</TableCell>
                <TableCell>{disease.description}</TableCell>
                <TableCell>
                  <Box sx={{ display: 'flex', gap: 1 }}>
                    <Button
                      variant="outlined"
                      size="small"
                      onClick={() => handleEdit(disease)}
                    >
                      Editar
                    </Button>
                    <Button
                      variant="outlined"
                      color="error"
                      size="small"
                      onClick={() => handleDelete(disease.id)}
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