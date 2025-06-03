import { useQuery } from '@tanstack/react-query';
import agent from '../api/agent'; // Asume que tienes tu agente HTTP configurado
import { AppointmentAnalyzer } from '../types';

export const useAnalyzer = (startDate: string | null, endDate: string | null) => {
  console.log('useAnalyzer called with:', { startDate, endDate });
  
  const { data: result, isLoading: isLoadingAnalyze, error } = useQuery({
    queryKey: ['results', startDate, endDate],
    queryFn: async (): Promise<AppointmentAnalyzer | null> => {
      if (!startDate || !endDate) {
        console.log('Dates are null, skipping query');
        return null;
      }
      
      console.log('Making API request with dates:', { startDate, endDate });
      
      try {
        const response = await agent.get<AppointmentAnalyzer[]>(
          `/DiseaseAnalysis/analyze?startDate=${encodeURIComponent(startDate)}&endDate=${encodeURIComponent(endDate)}`
        );
        console.log('API Response:', response.data);
        
        // Si la respuesta es un array, tomar el primer elemento
        // Si está vacío, retornar null
        if (Array.isArray(response.data) && response.data.length > 0) {
          return response.data[0];
        }
        
        return null;
      } catch (error) {
        console.error('Error in useAnalyzer:', error);
        throw error;
      }
    },
    enabled: !!startDate && !!endDate
  });

  return {
    result,
    isLoadingAnalyze,
    error
  };
};