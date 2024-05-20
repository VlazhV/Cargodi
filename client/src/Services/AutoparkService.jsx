import api from "../http/index"

export default class AutoparkService {
    static async GetById(id) {
        return await api.get('/autoparks/' + id)
    }
    static async GetAll() {
        return await api.get('/autoparks')
    }
    static async Create(address, capacity) {
        return await api.post('/autoparks', { address, capacity })
    }
    static async Delete(id) {
        return await api.delete('/autoparks/' + id)
    }
    static async Update(id, address, capacity) {
        return await api.put('/autoparks/' + id, { address, capacity })
    }
}