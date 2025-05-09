export const validations = {
    // Validación para nombres (solo letras, espacios y algunos caracteres especiales)
    name: (value: string) => {
        const regex = /^[a-zA-ZÀ-ÿ\u00f1\u00d1\s\-']+$/;
        if (!value) return 'Este campo es requerido';
        if (!regex.test(value)) return 'Solo se permiten letras, espacios y guiones';
        return '';
    },

    // Validación para números positivos
    positiveNumber: (value: string) => {
        const regex = /^[0-9]+$/;
        if (!value) return 'Este campo es requerido';
        if (!regex.test(value)) return 'Solo se permiten números positivos';
        return '';
    },

    // Validación para descripciones (letras, números, espacios y algunos caracteres especiales)
    description: (value: string) => {
        const regex = /^[a-zA-ZÀ-ÿ\u00f1\u00d1\s\-'.,;:()0-9]+$/;
        if (!value) return 'Este campo es requerido';
        if (!regex.test(value)) return 'Caracteres especiales no permitidos';
        return '';
    },

    // Validación para fechas
    date: (value: string) => {
        if (!value) return 'Este campo es requerido';
        const date = new Date(value);
        if (isNaN(date.getTime())) return 'Fecha inválida';
        return '';
    }
}; 