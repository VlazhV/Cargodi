import api from "../http/index"

export default class ClientService {
    static async GetAll() {
        return await api.get('/clients')
    }
}