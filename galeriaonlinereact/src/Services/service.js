import axios from "axios"







const apiPorta = "5173"

const apiLocal = `http://:${apiPorta}/api/`;

const api = axios.create({
    baseURL: apiLocal
});

export default api;