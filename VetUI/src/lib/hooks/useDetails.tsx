import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent";
import { Detail } from "../types";

export const useDetails = (id?: string) => {
    const queryClient = useQueryClient();

    const {data: Details, isPending} = useQuery({
        queryKey: ['details'],
        queryFn: async () => {
          const response = await agent.get<Detail[]>('/Details');
          return response.data;
        }
    });

    const {data: details, isLoading: isLoadingDetail} = useQuery({
        queryKey: ['detail', id],
        queryFn: async () => {
          const response = await agent.get<Detail>(`/Details/${id}`);
          return response.data;
        },
        enabled: !!id
    });

    const updateDetail = useMutation({
        mutationFn: async (detail: Detail) =>{
            await agent.put('/Detail', detail);
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['details']
            })
        }
    });

    const createDetail = useMutation({
        mutationFn: async (detail: Detail) =>{
            const response = await agent.post('/Details', detail);
            return response.data;
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['details']
            });
        }
    });

    const deleteDetail = useMutation({
        mutationFn: async (id : string) => {
            await agent.delete(`/Details/${id}`);
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['details']
            });
        }
    });

    return {
        Details,
        isPending,
        updateDetail,
        createDetail,
        deleteDetail,
        details,
        isLoadingDetail
    }
} 