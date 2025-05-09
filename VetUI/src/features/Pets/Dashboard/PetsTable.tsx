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
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Typography,
  Box,
  CircularProgress,
} from '@mui/material';
import { usePets } from '../../../lib/hooks/usePets';
import { Pet } from '../../../lib/types';

export const PetsTable = () => {
  const { pets, isPending, createPet, updatePet, deletePet } = usePets();
  const [isOpen, setIsOpen] = useState(false);
  const [selectedPet, setSelectedPet] = useState<Pet | null>(null);
  const [formData, setFormData] = useState<Partial<Pet>>({
    petName: '',
    breed: '',
    species: '',
    gender: '',
    birthdate: '',
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (selectedPet) {
      await updatePet.mutateAsync({ ...selectedPet, ...formData });
    } else {
      await createPet.mutateAsync(formData as Pet);
    }
    setIsOpen(false);
    setSelectedPet(null);
    setFormData({
      petName: '',
      breed: '',
      species: '',
      gender: '',
      birthdate: '',
    });
  };

  const handleEdit = (pet: Pet) => {
    setSelectedPet(pet);
    setFormData({
      petName: pet.petName,
      breed: pet.breed,
      species: pet.species,
      gender: pet.gender,
      birthdate: pet.birthdate,
    });
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
  console.log(pets);

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
            setFormData({
              petName: '',
              breed: '',
              species: '',
              gender: '',
              birthdate: '',
            });
            setIsOpen(true);
          }}
        >
          Agregar Mascota
        </Button>
      </Box>

      <Dialog open={isOpen} onClose={() => setIsOpen(false)} maxWidth="sm" fullWidth>
        <DialogTitle>{selectedPet ? 'Editar Mascota' : 'Nueva Mascota'}</DialogTitle>
        <form onSubmit={handleSubmit}>
          <DialogContent>
            <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
              <TextField
                label="Nombre"
                value={formData.petName}
                onChange={(e: React.ChangeEvent<HTMLInputElement>) => 
                  setFormData({ ...formData, petName: e.target.value })}
                required
                fullWidth
              />
              <TextField
                label="Raza"
                value={formData.breed}
                onChange={(e: React.ChangeEvent<HTMLInputElement>) => 
                  setFormData({ ...formData, breed: e.target.value })}
                required
                fullWidth
              />
              <TextField
                label="Especie"
                value={formData.species}
                onChange={(e: React.ChangeEvent<HTMLInputElement>) => 
                  setFormData({ ...formData, species: e.target.value })}
                required
                fullWidth
              />
              <TextField
                label="Género"
                value={formData.gender}
                onChange={(e: React.ChangeEvent<HTMLInputElement>) => 
                  setFormData({ ...formData, gender: e.target.value })}
                required
                fullWidth
              />
              <TextField
                label="Fecha de Nacimiento"
                type="date"
                value={formData.birthdate}
                onChange={(e: React.ChangeEvent<HTMLInputElement>) => 
                  setFormData({ ...formData, birthdate: e.target.value })}
                required
                fullWidth
                InputLabelProps={{ shrink: true }}
              />
            </Box>
          </DialogContent>
          <DialogActions>
            <Button onClick={() => setIsOpen(false)}>Cancelar</Button>
            <Button type="submit" variant="contained" color="primary">
              {selectedPet ? 'Actualizar' : 'Crear'}
            </Button>
          </DialogActions>
        </form>
      </Dialog>

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