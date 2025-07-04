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
import { usePets } from '../../../lib/hooks/usePets';
import { Pet } from '../../../lib/types';
import { FormDialog } from '../../../components/common/FormDialog';
import { validations } from '../../../lib/utils/validations';

export const PetsTable = () => {
  const { pets, isPending, createPet, updatePet, deletePet } = usePets();
  const [isOpen, setIsOpen] = useState(false);
  const [selectedPet, setSelectedPet] = useState<Pet | null>(null);

  const initialFormData = {
    petName: '',
    breed: '',
    species: '',
    gender: '',
    birthdate: '',
  };

  const formFields = [
    {
      name: 'petName',
      label: 'Nombre',
      validation: validations.name,
    },
    {
      name: 'breed',
      label: 'Raza',
      validation: validations.name,
    },
    {
      name: 'species',
      label: 'Especie',
      validation: validations.name,
    },
    {
      name: 'gender',
      label: 'Género',
      validation: validations.name,
      options: [
        { value: 'Macho', label: 'Macho' },
        { value: 'Hembra', label: 'Hembra' }
      ]
    },
    {
      name: 'birthdate',
      label: 'Fecha de Nacimiento',
      type: 'date',
      validation: validations.date,
    },
  ];

  const handleSubmit = async (formData: Partial<Pet>) => {
    if (selectedPet) {
      await updatePet.mutateAsync({ ...selectedPet, ...formData });
    } else {
      await createPet.mutateAsync(formData as Pet);
    }
    setIsOpen(false);
    setSelectedPet(null);
  };

  const handleEdit = (pet: Pet) => {
    setSelectedPet(pet);
    setIsOpen(true);
  };

  const handleDelete = async (id: string) => {
    if (window.confirm('¿Está seguro de eliminar esta mascota?')) {
      await deletePet.mutateAsync(id);
    }
  };

  if (isPending) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
        <CircularProgress />
      </Box>
    );
  }

  const petsList = Array.isArray(pets) ? pets : [];

  return (
    <Box sx={{ p: 3 }}>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
        <Typography variant="h4" component="h2">
          Mascotas
        </Typography>
        <Button
          variant="contained"
          color="primary"
          onClick={() => {
            setSelectedPet(null);
            setIsOpen(true);
          }}
        >
          Agregar Mascota
        </Button>
      </Box>

      <FormDialog
        open={isOpen}
        onClose={() => {
          setIsOpen(false);
          setSelectedPet(null);
        }}
        onSubmit={handleSubmit}
        title={selectedPet ? 'Editar Mascota' : 'Nueva Mascota'}
        fields={formFields}
        initialData={selectedPet || initialFormData}
        submitButtonText={selectedPet ? 'Actualizar' : 'Crear'}
      />

      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Nombre</TableCell>
              <TableCell>Raza</TableCell>
              <TableCell>Especie</TableCell>
              <TableCell>Género</TableCell>
              <TableCell>Fecha de Nacimiento</TableCell>
              <TableCell>Acciones</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {petsList.map((pet) => (
              <TableRow key={pet.id}>
                <TableCell>{pet.petName}</TableCell>
                <TableCell>{pet.breed}</TableCell>
                <TableCell>{pet.species}</TableCell>
                <TableCell>{pet.gender}</TableCell>
                <TableCell>{new Date(pet.birthdate).toLocaleDateString()}</TableCell>
                <TableCell>
                  <Box sx={{ display: 'flex', gap: 1 }}>
                    <Button
                      variant="outlined"
                      size="small"
                      onClick={() => handleEdit(pet)}
                    >
                      Editar
                    </Button>
                    <Button
                      variant="outlined"
                      color="error"
                      size="small"
                      onClick={() => handleDelete(pet.id)}
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