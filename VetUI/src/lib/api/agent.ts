import axios from "axios";

const agent = axios.create({
    baseURL: "https://localhost:5002/api"
});

export default agent