import React, { useEffect, useState } from 'react'
import { useFetching } from '../Hooks/useFetching'
import UserService from '../Services/UserService'
import userRoles from '../Data/UserRoles.json'
import { useNavigate, useParams } from 'react-router'
import CarService from '../Services/CarService'
import carTypes from '../Data/CarTypes.json'
import AutoparkEdit from '../Components/AutoparkEdit'

export default function CarPage() {

    const { carId } = useParams()

    const [editing, setEditing] = useState(false)

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
        carTypeName: '',
        carTypeId: null,

        autoparkId: null,
        actualAutoparkId: null
    })

    const navigate = useNavigate()

    const [fetch, loading, error] = useFetching(async (type, data) => {
        switch (type) {
            case "get":
                {
                    const res = await CarService.GetById(carId)

                    let tempCarData = res.data
                    let typeCarIt = carTypes.find(v => v.id == tempCarData.carType.id)

                    tempCarData.carTypeName = typeCarIt ? typeCarIt.name : ''
                    tempCarData.carTypeId = tempCarData.carType.id

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
            case "update":
                {
                    const res = await CarService.Update(carId, carData)

                    let tempCarData = res.data
                    let typeCarIt = carTypes.find(v => v.id == tempCarData.carType.id)

                    tempCarData.carTypeName = typeCarIt ? typeCarIt.name : ''
                    tempCarData.carTypeId = tempCarData.carType.id

                    setCarData(res.data)
                    setEditing(false)
                }
                break;
            case "updateAutopark":
                {
                    let updateCarData = carData
                    updateCarData.autoparkId = data

                    const res = await CarService.Update(carId, updateCarData)

                    let tempCarData = res.data
                    let typeCarIt = carTypes.find(v => v.id == tempCarData.carType.id)

                    tempCarData.carTypeName = typeCarIt ? typeCarIt.name : ''
                    tempCarData.carTypeId = tempCarData.carType.id

                    setCarData(res.data)
                }
                break;
            case "updateActualAutopark":
                {
                    let updateCarData = carData
                    updateCarData.actualAutoparkId = data

                    const res = await CarService.Update(carId, updateCarData)

                    let tempCarData = res.data
                    let typeCarIt = carTypes.find(v => v.id == tempCarData.carType.id)

                    tempCarData.carTypeName = typeCarIt ? typeCarIt.name : ''
                    tempCarData.carTypeId = tempCarData.carType.id

                    setCarData(res.data)
                }
                break;
        }

    })

    const handleAutoparkChange = (newAutoparkData) => {
        fetch("updateAutopark", newAutoparkData.id)
    }

    const handleActualAutoparkChange = (newAutoparkData) => {
        fetch("updateActualAutopark", newAutoparkData.id)
    }

    const handleDeleteClick = (e) => {
        e.preventDefault()
        fetch("delete")
    }

    const handleEditingStart = (e) => {
        e.preventDefault()
        setEditing(true)
    }

    const handleEditingCancel = (e) => {
        e.preventDefault()
        setEditing(false)
        fetch("get")
    }

    const handleEditingSave = (e) => {
        e.preventDefault()
        fetch("update")
    }

    const handleCarChange = (e) => {
        e.preventDefault()
        setCarData(prev => ({ ...prev, [e.target.id]: e.target.value }))
    }

    useEffect(() => {
        fetch("get")
    }, [carId])

    return (
        <div className="container mt-5 py-5">

            {
                !editing &&
                <div className="row justify-content-center mt-5">
                    <div className="col-12 col-md-12 col-lg">
                        <div className="text-wrapper align-left">
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-4">
                                <strong>Машина №{carData.id}</strong>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Регистрационный номер:</strong> <span>{carData.licenseNumber}</span>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Марка:</strong> <span>{carData.mark}</span>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Дальность следования (км):</strong> <span>{carData.range}</span>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Грузоподъёмность (кг):</strong> <span>{carData.carrying}</span>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Объём бака (л):</strong> <span>{carData.tankVolume}</span>
                            </h1>
                        </div>
                    </div>
                    <div className="col-12 col-md-12 col-lg">
                        <div className="text-wrapper align-left">
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Длина вместимости (см):</strong> <span>{carData.capacityLength}</span>
                            </h1>
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Ширина вместимости (см):</strong> <span>{carData.capacityWidth}</span>
                            </h1>
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Высота вместимости (см):</strong> <span>{carData.capacityHeight}</span>
                            </h1>
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Тип:</strong> <span>{carData.carTypeName}</span>
                            </h1>
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Приписка:</strong> <span>Автопарк №{carData.autoparkId}</span>
                            </h1>
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Текущий автопарк:</strong> <span>Автопарк №{carData.actualAutoparkId}</span>
                            </h1>
                        </div>
                    </div>
                </div>
            }

            {
                editing &&
                <div className="row justify-content-center mt-5">
                    <div className="col-12 col-md-12 col-lg">
                        <div className="text-wrapper align-left">
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-4">
                                <strong>Машина №{carData.id}</strong>
                            </h1>

                            <h1 className="mb-4 input-group">
                                <strong className='input-group-text display-7'>Регистрационный номер:</strong>
                                <input name="input" value={carData.licenseNumber} type='text'
                                    className="form-control mx-2" id="licenseNumber" onChange={handleCarChange}></input>
                            </h1>

                            <h1 className="mb-4 input-group">
                                <strong className='input-group-text display-7'>Марка:</strong>
                                <input name="input" value={carData.mark} type='text'
                                    className="form-control mx-2" id="mark" onChange={handleCarChange}></input>
                            </h1>

                            <h1 className="mb-4 input-group">
                                <strong className='input-group-text display-7'>Дальность следования (км):</strong>
                                <input name="input" value={carData.range} type='text'
                                    className="form-control mx-2" id="range" onChange={handleCarChange}></input>
                            </h1>

                            <h1 className="mb-4 input-group">
                                <strong className='input-group-text display-7'>Грузоподъёмность (кг):</strong>
                                <input name="input" value={carData.carrying} type='text'
                                    className="form-control mx-2" id="carrying" onChange={handleCarChange}></input>
                            </h1>

                            <h1 className="mb-4 input-group">
                                <strong className='input-group-text display-7'>Объём бака (л):</strong>
                                <input name="input" value={carData.tankVolume} type='text'
                                    className="form-control mx-2" id="tankVolume" onChange={handleCarChange}></input>
                            </h1>
                        </div>
                    </div>
                    <div className="col-12 col-md-12 col-lg">
                        <div className="text-wrapper align-left">
                            <h1 className="mb-4 input-group">
                                <strong className='input-group-text display-7'>Длина вместимости (см):</strong>
                                <input name="input" value={carData.capacityLength} type='text'
                                    className="form-control mx-2" id="capacityLength" onChange={handleCarChange}></input>
                            </h1>
                            <h1 className="mb-4 input-group">
                                <strong className='input-group-text display-7'>Ширина вместимости (см):</strong>
                                <input name="input" value={carData.capacityWidth} type='text'
                                    className="form-control mx-2" id="capacityWidth" onChange={handleCarChange}></input>
                            </h1>
                            <h1 className="mb-4 input-group">
                                <strong className='input-group-text display-7'>Высота вместимости (см):</strong>
                                <input name="input" value={carData.capacityHeight} type='text'
                                    className="form-control mx-2" id="capacityHeight" onChange={handleCarChange}></input>
                            </h1>
                            <h1 className="mb-4 input-group">
                                <div className="col-12 form-group mb-3" data-for="input">

                                    <select name="select" className='form-select display-7 p-3' value={carData.carTypeId} onChange={handleCarChange} id="carTypeId">
                                        {
                                            carTypes.map(cType => <option value={cType.id} key={cType.id}>{cType.name}</option>)
                                        }
                                    </select>
                                </div>
                            </h1>
                        </div>
                    </div>
                </div>
            }

            {
                !editing &&
                <>
                    <div className="btn btn-primary display-3" onClick={handleEditingStart}>Изменить</div>
                    <div className="btn btn-danger display-3" onClick={handleDeleteClick}>Удалить</div>
                    <button type="button" className="btn btn-primary display-3"
                        data-bs-toggle="modal" data-bs-target="#carAutoparkId">Назначить приписку</button>
                    <button type="button" className="btn btn-primary display-3"
                        data-bs-toggle="modal" data-bs-target="#carActualAutoparkId">Назначить текущий автопарк</button>
                </>
            }
            <AutoparkEdit id="carAutoparkId" onSelectAutopark={handleAutoparkChange} />
            <AutoparkEdit id="carActualAutoparkId" onSelectAutopark={handleActualAutoparkChange} />
            {
                editing &&
                <>
                    <div className="btn btn-primary display-3" onClick={handleEditingSave}>Сохранить</div>
                    <div className="btn btn-secondary display-3" onClick={handleEditingCancel}>Отмена</div>
                </>}

            {
                error &&
                <div className="border border-danger border rounded-4 p-2 px-4 mt-2">
                    <span className="text-danger text-center h3">{typeof error === 'string' ? error : toString(error)}</span>
                </div>
            }
            <div className="d-flex align-items-center text-warning m-4 rounded-pill px-4 py-2 display-4 fixed-bottom bg-dark" style={{ visibility: loading ? 'visible' : 'hidden' }}>
                <strong>Загрузка...</strong>
                <div className="spinner-border ms-auto" role="status" aria-hidden="true"></div>
            </div>
        </div >
    )
}
