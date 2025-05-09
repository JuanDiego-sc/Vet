import { createBrowserRouter } from "react-router";
import App from "../App";
import HomePage from "../../features/home/HomePage";
import { PetsTable } from "../../features/Pets/Dashboard/PetsTable";
import { MedicinesTable } from "../../features/Medicines/Dashboard/MedicinesTable";
import { DiseasesTable } from "../../features/Diseases/Dashboard/DiseasesTable";
import { AppointmentsTable } from "../../features/Appointments/Dashboard/AppointmentsTable";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App></App>,
        children: [
            {path: '', element: <HomePage></HomePage>},
            {path: 'pet', element: <PetsTable></PetsTable>},
            {path: 'medicine', element: <MedicinesTable></MedicinesTable>},
            {path: 'disease', element: <DiseasesTable></DiseasesTable>},
            {path: 'appointment', element: <AppointmentsTable></AppointmentsTable>},
        ]
    }
]);