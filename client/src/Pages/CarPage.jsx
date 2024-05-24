import React, { useEffect, useState } from 'react'
import { useFetching } from '../Hooks/useFetching'
import UserService from '../Services/UserService'
import userRoles from '../Data/UserRoles.json'
import { useNavigate, useParams } from 'react-router'
import CarService from '../Services/CarService'
import carTypes from '../Data/CarTypes.json'

export default function CarPage() {

    const { carId } = useParams()

    const [carData, setCarData] = useState({
        licenseNumber: "",
        mark: "",
        range: 0, //km
        carrying: 0, //g
        tankVolume: 0, //dm3	

        capacityLength: 0, //mm	
        capacityWidth: 0, //mm	
        capacityHeight: 0, //mm

        carType: { id: null, name: '' },
        carTypeName: ''
    })

    const navigate = useNavigate()

    const [fetch, loading, error] = useFetching(async (type) => {
        switch (type) {
            case "get":
                {
                    const res = await CarService.GetById(carId)

                    let tempCarData = res.data
                    let typeCarIt = carTypes.find(v => v.id == tempCarData.carType.id)

                    tempCarData.carTypeName = typeCarIt ? typeCarIt.name : ''


                    setCarData(res.data)
                }
                break;
            case "delete":
                {
                    const res = await CarService.Delete(carId)
                    if (res) {
                        navigate(-1)
                    }
                }
                break;
        }

    })

    const handleDeleteClick = (e) => {
        e.preventDefault()
        fetch("delete")
    }

    useEffect(() => {
        fetch("get")
    }, [carId])

    return (
        <div className="container mt-5 py-5">

            <div className="row justify-content-center mt-5">
                <div className="col-12 col-md-12 col-lg">
                    <div className="text-wrapper align-left">
                        <h1 className="mbr-section-title mbr-fonts-style mb-4 display-4">
                            <strong>Машина №{carData.id}</strong>
                        </h1>

                        <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                            <strong>Номер лицензии:</strong> <span>{carData.licenseNumber}</span>
                        </h1>

                        <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                            <strong>Марка:</strong> <span>{carData.mark}</span>
                        </h1>

                        <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                            <strong>Дальность езды (км):</strong> <span>{carData.range}</span>
                        </h1>

                        <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                            <strong>Грузоподъёмность (г):</strong> <span>{carData.carrying}</span>
                        </h1>

                        <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                            <strong>Объём бака (л):</strong> <span>{carData.tankVolume}</span>
                        </h1>
                    </div>
                </div>
                <div className="col-12 col-md-12 col-lg">
                    <div className="text-wrapper align-left">
                        <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                            <strong>Длинна вместимости (мм):</strong> <span>{carData.capacityLength}</span>
                        </h1>
                        <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                            <strong>Ширина вместимости (мм):</strong> <span>{carData.capacityWidth}</span>
                        </h1>
                        <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                            <strong>Высота вместимости (мм):</strong> <span>{carData.capacityHeight}</span>
                        </h1>
                        <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                            <strong>Тип:</strong> <span>{carData.carTypeName}</span>
                        </h1>
                    </div>
                </div>
            </div>

            <div className="btn btn-danger display-3" onClick={handleDeleteClick}>Удалить</div>

            {
                error &&
                <div className="border border-danger border rounded-4 p-2 px-4 mt-2">
                    <span className="text-danger text-center h3">{typeof error === 'string' ? error : toString(error)}</span>
                </div>
            }
        </div >
    )
}
