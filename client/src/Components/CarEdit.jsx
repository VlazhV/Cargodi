import React, { useEffect, useState } from 'react'
import { useFetching } from '../Hooks/useFetching'
import CarService from '../Services/CarService'
import carTypes from '../Data/CarTypes.json'

export default function CarEdit(props) {
    const id = props.id ? props.id : null
    const onSelectCar = props.onSelectCar ? props.onSelectCar : (autoparkId) => { }

    const [carsData, setCarsData] = useState([])

    const handleSelectCar = (e) => {
        e.preventDefault()
        let index = e.target.id
        onSelectCar({ ...(carsData[index]) })
    }

    const [fetch, loading, error] = useFetching(async (type) => {
        switch (type) {
            case "get":
                {
                    const res = await CarService.GetAll()
                    setCarsData(res.data)
                }
                break;
        }

    })

    useEffect(() => {
        fetch("get")
    }, [])

    return (
        <div className="modal fade" id={id} tabIndex="-1">
            <div className="modal-dialog modal-dialog-scrollable">
                <div className="modal-content">
                    <div className="modal-header">
                        <h1 className="modal-title fs-5">Машины</h1>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>

                    </div>
                    <div className="modal-body d-flex flex-column">
                        {
                            carsData.map((carData, index) => {
                                return <button className='btn btn-outline-primary' data-bs-dismiss="modal" key={carData.id} id={index} onClick={handleSelectCar}>
                                    Машина №{carData.id}
                                    <br />
                                    {carData.mark}; {carData.licenseNumber}; {carTypes.find(v => v.id == carData.carType.id)?.name}
                                </button>
                            })
                        }
                    </div>
                    <div className="modal-footer">
                        <div className='w-100 h-100 d-flex flex-row'>
                            <div className='h-100 col d-flex flex-column'>
                                <div className="d-flex align-items-center text-warning mb-2 rounded-pill px-4 py-2 display-4 bg-dark"
                                    style={{ visibility: loading ? 'visible' : 'hidden' }}>
                                    <strong>Загрузка...</strong>
                                    <div className="spinner-border ms-auto" role="status" aria-hidden="true"></div>
                                </div>
                                <div className="border border-danger border rounded-pill p-2 px-4 w-100"
                                    style={{ visibility: error ? 'visible' : 'hidden' }}>
                                    <span className="text-danger text-center h3">{typeof error === 'string' ? error : toString(error)}</span>
                                </div>
                            </div>
                            <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Закрыть</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}
