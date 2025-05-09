export type Pet ={
    id: string;
    petName: string;
    breed: string;
    species: string;
    gender: string;
    birthdate: string;
    createAt: string;
    updateAt: string;
}


export type Medicine ={
    id: string
    name: string
    stock: number
    description: string
    createAt: string
    updateAt: string
}

export type Disease ={
    id: string
    name: string
    type: string
    description: string
    createAt: string
    updateAt: string
}

export type MedicalAppointment = {
    id: string
    appointmentDate: string
    appointmentStatus: number
    reason: string
    createAt: string
    updateAt: string
    idPet: string
}

