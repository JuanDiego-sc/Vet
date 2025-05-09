import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent";
import { Pet } from "../types";

export const usePets = (id?: string) => {

    const queryClient = useQueryClient();

    const {data: pets, isPending} = useQuery({
        queryKey: ['pets'],
        queryFn: async () => {
          const response = await agent.get<Pet[]>('/Pet');
          return response.data;
        }
      });

      const {data: pet, isLoading: isLoadingPet} = useQuery({
        queryKey: ['pets', id],
        queryFn: async () => {
          const response = await agent.get<Pet>(`/Pet/${id}`);
          return response.data;
        },
        enabled: !!id //if id exists, get an activity going to work
      });

    const updatePet = useMutation({
        mutationFn: async (activty: Pet) =>{
            await agent.put('/Pet', activty);
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['pets']
            })
        }
    });

    const createPet = useMutation({
        mutationFn: async (pet: Pet) =>{
            const response = await agent.post('/Pet', pet);
            return response.data;
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['pets']
            });
        }
    });

    const deletePet = useMutation({
        mutationFn: async (id : string) => {
            await agent.delete(`/Pet/${id}`);
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['pets']
            });
        }
    });
    

      return{
        pets,
        isPending,
        updatePet,
        createPet,
        deletePet,
        pet,
        isLoadingPet
      }
}