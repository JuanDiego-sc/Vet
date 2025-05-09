import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './app/index.css'
import { router } from './app/router/routes'
import { RouterProvider } from 'react-router'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { ReactQueryDevtools } from '@tanstack/react-query-devtools'

const queryClient = new QueryClient();

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <QueryClientProvider client={queryClient}>
      <ReactQueryDevtools></ReactQueryDevtools>
      <RouterProvider router={router}></RouterProvider>
    </QueryClientProvider>
    
  </StrictMode>,
)
