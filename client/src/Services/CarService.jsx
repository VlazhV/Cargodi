import api from "../http/index"

export default class CarService {
    static async GetById(id) {
        return await api.get('/cars/' + id)
    }
    static async GetAll() {
        return await api.get('/cars')
    }
    static async Create(carData) {
        return await api.post('/cars', carData)
    }
    static async Delete(id) {
        return await api.delete('/cars/' + id)
    }
    static async Update(id, carData) {
        return await api.put('/cars/' + id, carData)
    }
}