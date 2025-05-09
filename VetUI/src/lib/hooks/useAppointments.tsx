import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent";
import { MedicalAppointment } from "../types";

export const useAppointments = (id?: string) => {
    const queryClient = useQueryClient();

    const {data: appointments, isPending} = useQuery({
        queryKey: ['appointments'],
        queryFn: async () => {
          const response = await agent.get<MedicalAppointment[]>('/MedicalAppointment');
          return response.data;
        }
    });

    const {data: appointment, isLoading: isLoadingAppointment} = useQuery({
        queryKey: ['appointments', id],
        queryFn: async () => {
          const response = await agent.get<MedicalAppointment>(`/MedicalAppointment/${id}`);
          return response.data;
        },
        enabled: !!id
    });

    const updateAppointment = useMutation({
        mutationFn: async (appointment: MedicalAppointment) =>{
            await agent.put('/MedicalAppointment', appointment);
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['appointments']
            })
        }
    });

    const createAppointment = useMutation({
        mutationFn: async (appointment: MedicalAppointment) =>{
            const response = await agent.post('/MedicalAppointment', appointment);
            return response.data;
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['appointments']
            });
        }
    });

    const deleteAppointment = useMutation({
        mutationFn: async (id : string) => {
            await agent.delete(`/MedicalAppointment/${id}`);
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['appointments']
            });
        }
    });

    return {
        appointments,
        isPending,
        updateAppointment,
        createAppointment,
        deleteAppointment,
        appointment,
        isLoadingAppointment
    }
} 