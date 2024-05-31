import api from "../http/index"

export default class ShipService {
    static async GetById(id) {
        return await api.get('/ships/' + id)
    }
    static async GetAll() {
        return await api.get('/ships')
    }
    static async GetAllOfDriver() {
        return await api.get('/ships/my')
    }
    static async Create() {
        return await api.post('/ships', {})
    }
    static async Delete(id) {
        return await api.delete('/ships/' + id)
    }
    static async Update(id, updateShipDto) {
        return await api.put('/ships/' + id, updateShipDto)
    }
    static async Mark(id) {
        return await api.patch('/ships/' + id)
    }
}