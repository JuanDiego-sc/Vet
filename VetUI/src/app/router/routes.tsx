import { createBrowserRouter } from "react-router";
import App from "../App";
import HomePage from "../../features/home/HomePage";
import { PetsTable } from "../../features/Pets/Dashboard/PetsTable";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App></App>,
        children: [
            {path: '', element: <HomePage></HomePage>},
            {path: 'pet', element: <PetsTable></PetsTable>},
        ]
    }
]);