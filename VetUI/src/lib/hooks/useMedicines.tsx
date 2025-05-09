import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent";
import { Medicine } from "../types";

export const useMedicines = (id?: string) => {
    const queryClient = useQueryClient();

    const {data: medicines, isPending} = useQuery({
        queryKey: ['medicines'],
        queryFn: async () => {
          const response = await agent.get<Medicine[]>('/Medicine');
          return response.data;
        }
    });

    const {data: medicine, isLoading: isLoadingMedicine} = useQuery({
        queryKey: ['medicines', id],
        queryFn: async () => {
          const response = await agent.get<Medicine>(`/Medicine/${id}`);
          return response.data;
        },
        enabled: !!id
    });

    const updateMedicine = useMutation({
        mutationFn: async (medicine: Medicine) =>{
            await agent.put('/Medicine', medicine);
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['medicines']
            })
        }
    });

    const createMedicine = useMutation({
        mutationFn: async (medicine: Medicine) =>{
            const response = await agent.post('/Medicine', medicine);
            return response.data;
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['medicines']
            });
        }
    });

    const deleteMedicine = useMutation({
        mutationFn: async (id : string) => {
            await agent.delete(`/Medicine/${id}`);
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['medicines']
            });
        }
    });

    return {
        medicines,
        isPending,
        updateMedicine,
        createMedicine,
        deleteMedicine,
        medicine,
        isLoadingMedicine
    }
} 