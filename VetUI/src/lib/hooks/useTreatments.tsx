import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent";
import {Treatment } from "../types";

export const useTreatment = (id?: string) => {
    const queryClient = useQueryClient();

    const {data: Treatment, isPending} = useQuery({
        queryKey: ['treatments'],
        queryFn: async () => {
          const response = await agent.get<Treatment[]>('/Treatment');
          return response.data;
        }
    });

    const {data: treatments, isLoading: isLoadingTreatment} = useQuery({
        queryKey: ['treatments', id],
        queryFn: async () => {
          const response = await agent.get<Treatment>(`/Treatment/${id}`);
          return response.data;
        },
        enabled: !!id
    });

    const updateTreatment = useMutation({
        mutationFn: async (treatment: Treatment) =>{
            await agent.put('/Treatment', treatment);
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['treatments']
            })
        }
    });

    const createTreatment = useMutation({
        mutationFn: async (treatment: Treatment) =>{
            const response = await agent.post('/Treatment', treatment);
            return response.data;
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['Treatment']
            });
        }
    });

    const deleteTreatment = useMutation({
        mutationFn: async (id : string) => {
            await agent.delete(`/Treatment/${id}`);
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['treatments']
            });
        }
    });

    return {
        Treatment,
        isPending,
        updateTreatment,
        createTreatment,
        deleteTreatment,
        treatments,
        isLoadingTreatment
    }
} 