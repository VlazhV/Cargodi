import api from "../http/index"

export default class TrailerService {
    static async GetById(id) {
        return await api.get('/trailers/' + id)
    }
    static async GetAll() {
        return await api.get('/trailers')
    }
    static async Create(trailerData) {
        return await api.post('/trailers', trailerData)
    }
    static async Delete(id) {
        return await api.delete('/trailers/' + id)
    }
    static async Update(id, trailerData) {
        return await api.put('/trailers/' + id, trailerData)
    }
}