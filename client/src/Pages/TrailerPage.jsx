import React, { useEffect, useState } from 'react'
import { useFetching } from '../Hooks/useFetching'
import { useNavigate, useParams } from 'react-router'
import TrailerService from '../Services/TrailerService'
import carTypes from '../Data/CarTypes.json'
import trailerTypes from '../Data/TrailerTypes.json'

export default function TrailerPage() {

    const { trailerId } = useParams()

    const [editing, setEditing] = useState(false)

    const [trailerData, setTrailerData] = useState({
        licenseNumber: "",
        carrying: 0, //g

        capacityLength: 0, //mm	
        capacityWidth: 0, //mm	
        capacityHeight: 0, //mm

        trailerType: { id: null, name: '' },
        trailerTypeName: '',
        trailerTypeId: null,

        autoparkId: null,
        actualAutoparkId: null
    })

    const navigate = useNavigate()

    const [fetch, loading, error] = useFetching(async (type) => {
        switch (type) {
            case "get":
                {
                    const res = await TrailerService.GetById(trailerId)

                    let tempTrailerData = res.data
                    let trailerTypeIt = trailerTypes.find(v => v.id == tempTrailerData.trailerType.id)

                    tempTrailerData.trailerTypeName = trailerTypeIt ? trailerTypeIt.name : ''
                    tempTrailerData.trailerTypeId = tempTrailerData.trailerType.id

                    setTrailerData(res.data)
                }
                break;
            case "delete":
                {
                    const res = await TrailerService.Delete(trailerId)
                    if (res) {
                        navigate(-1)
                    }
                }
                break;
            case "update":
                {
                    const res = await TrailerService.Update(trailerId, trailerData)

                    let tempTrailerData = res.data
                    let trailerTypeIt = trailerTypes.find(v => v.id == tempTrailerData.trailerType.id)

                    tempTrailerData.trailerTypeName = trailerTypeIt ? trailerTypeIt.name : ''
                    tempTrailerData.trailerTypeId = tempTrailerData.trailerType.id

                    setTrailerData(res.data)
                    setEditing(false)
                }
                break;
        }

    })

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
        setTrailerData(prev => ({ ...prev, [e.target.id]: e.target.value }))
    }

    useEffect(() => {
        fetch("get")
    }, [trailerId])

    return (
        <div className="container mt-5 py-5">

            {
                !editing &&
                <div className="row justify-content-center mt-5">
                    <div className="col-12 col-md-12 col-lg">
                        <div className="text-wrapper align-left">
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-4">
                                <strong>Прицеп №{trailerData.id}</strong>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Регистрационный номер:</strong> <span>{trailerData.licenseNumber}</span>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Грузоподъёмность (г):</strong> <span>{trailerData.carrying}</span>
                            </h1>
                        </div>
                    </div>
                    <div className="col-12 col-md-12 col-lg">
                        <div className="text-wrapper align-left">
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Длинна вместимости (мм):</strong> <span>{trailerData.capacityLength}</span>
                            </h1>
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Ширина вместимости (мм):</strong> <span>{trailerData.capacityWidth}</span>
                            </h1>
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Высота вместимости (мм):</strong> <span>{trailerData.capacityHeight}</span>
                            </h1>
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Тип:</strong> <span>{trailerData.trailerTypeName}</span>
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
                                <strong>Прицеп №{trailerData.id}</strong>
                            </h1>

                            <h1 className="mb-4 input-group">
                                <strong className='input-group-text display-7'>Регистрационный номер:</strong>
                                <input name="input" value={trailerData.licenseNumber} type='text'
                                    className="form-control mx-2" id="licenseNumber" onChange={handleCarChange}></input>
                            </h1>

                            <h1 className="mb-4 input-group">
                                <strong className='input-group-text display-7'>Грузоподъёмность (г):</strong>
                                <input name="input" value={trailerData.carrying} type='text'
                                    className="form-control mx-2" id="carrying" onChange={handleCarChange}></input>
                            </h1>
                        </div>
                    </div>
                    <div className="col-12 col-md-12 col-lg">
                        <div className="text-wrapper align-left">
                            <h1 className="mb-4 input-group">
                                <strong className='input-group-text display-7'>Длинна вместимости (мм):</strong>
                                <input name="input" value={trailerData.capacityLength} type='text'
                                    className="form-control mx-2" id="capacityLength" onChange={handleCarChange}></input>
                            </h1>
                            <h1 className="mb-4 input-group">
                                <strong className='input-group-text display-7'>Ширина вместимости (мм):</strong>
                                <input name="input" value={trailerData.capacityWidth} type='text'
                                    className="form-control mx-2" id="capacityWidth" onChange={handleCarChange}></input>
                            </h1>
                            <h1 className="mb-4 input-group">
                                <strong className='input-group-text display-7'>Высота вместимости (мм):</strong>
                                <input name="input" value={trailerData.capacityHeight} type='text'
                                    className="form-control mx-2" id="capacityHeight" onChange={handleCarChange}></input>
                            </h1>
                            <h1 className="mb-4 input-group">
                                <div className="col-12 form-group mb-3" data-for="input">

                                    <select name="select" className='form-select display-7 p-3' value={trailerData.trailerTypeId} onChange={handleCarChange} id="trailerTypeId">
                                        {
                                            trailerTypes.map(tType => <option value={tType.id} key={tType.id}>{tType.name}</option>)
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
                </>
            }
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
