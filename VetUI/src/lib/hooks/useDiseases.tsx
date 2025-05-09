import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent";
import { Disease } from "../types";

export const useDiseases = (id?: string) => {
    const queryClient = useQueryClient();

    const {data: diseases, isPending} = useQuery({
        queryKey: ['diseases'],
        queryFn: async () => {
          const response = await agent.get<Disease[]>('/Disease');
          return response.data;
        }
    });

    const {data: disease, isLoading: isLoadingDisease} = useQuery({
        queryKey: ['diseases', id],
        queryFn: async () => {
          const response = await agent.get<Disease>(`/Disease/${id}`);
          return response.data;
        },
        enabled: !!id
    });

    const updateDisease = useMutation({
        mutationFn: async (disease: Disease) =>{
            await agent.put('/Disease', disease);
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['diseases']
            })
        }
    });

    const createDisease = useMutation({
        mutationFn: async (disease: Disease) =>{
            const response = await agent.post('/Disease', disease);
            return response.data;
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['diseases']
            });
        }
    });

    const deleteDisease = useMutation({
        mutationFn: async (id : string) => {
            await agent.delete(`/Disease/${id}`);
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['diseases']
            });
        }
    });

    return {
        diseases,
        isPending,
        updateDisease,
        createDisease,
        deleteDisease,
        disease,
        isLoadingDisease
    }
} 