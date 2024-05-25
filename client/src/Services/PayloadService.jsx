import api from "../http/index"

export default class PayloadService {
    static async GetById(id) {
        return await api.get('/payloads/' + id)
    }
    static async GetAll() {
        return await api.get('/payloads')
    }
    static async Delete(id) {
        return await api.delete('/payloads/' + id)
    }
    static async Update(id, payloadData) {
        return await api.put('/payloads/' + id, payloadData)
    }
}