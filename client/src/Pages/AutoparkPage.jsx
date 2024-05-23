import React, { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router'
import AutoparkService from '../Services/AutoparkService'
import { useFetching } from '../Hooks/useFetching'
import AddressView from '../Components/AddressView'
import AddressEdit from '../Components/AddressEdit'

export default function AutoparkPage() {
    const { parkId } = useParams()

    const navigate = useNavigate()

    const [editing, setEditing] = useState(false)
    const [autoparkData, setAutoparkData] = useState({
        id: null,
        address: {
            id: null,
            name: null,
            latitude: null,
            longitude: null,
            isWest: false,
            isNorth: false
        },
        capacity: null,
        cars: [],
        trailers: [],
        drivers: [],
        actualCars: [],
        actualTrailers: [],
        actualDrivers: []
    })

    const [fetch, loading, error] = useFetching(async (type) => {
        switch (type) {
            case "get":
                {
                    const res = await AutoparkService.GetById(parkId)
                    setAutoparkData(res.data)
                }
                break;
            case "update":
                {
                    const res = await AutoparkService.Update(autoparkData.id, autoparkData.address, autoparkData.capacity)
                    fetch("get")
                    setEditing(false)
                }
                break;
            case "delete":
                {
                    const res = await AutoparkService.Delete(parkId)
                    navigate(-1)
                }
                break;

        }

    })

    useEffect(() => {
        fetch("get")
    }, [])

    const handleAddressChange = (newAddress) => {
        setAutoparkData((prevData) => { return { ...prevData, address: newAddress } })
    }

    const handleChange = (e) => {
        e.preventDefault()
        setAutoparkData((prevData) => { return { ...prevData, [e.target.id]: e.target.value } })
    }

    const handleStartEdit = (e) => {
        e.preventDefault()
        setEditing(true)
    }

    const handleCancelEdit = (e) => {
        e.preventDefault()
        setEditing(false)
        fetch("get")
    }

    const handleSaveEdit = (e) => {
        e.preventDefault()
        fetch("update")
    }

    const handleDelete = (e) => {
        e.preventDefault()
        fetch("delete")
    }

    return (
        <div className="container mt-5 py-5 d-flex flex-column align-items-center">
            <h1 className="mbr-section-title mbr-fonts-style my-4 display-5">
                <strong>Автопарк №</strong> <span>{autoparkData.id}</span>
            </h1>
            <div className="row justify-content-center mt-5 w-100">
                {
                    !editing &&
                    <>
                        <div className="col-12 col-md-12 col-lg">
                            <AddressView address={autoparkData.address} />
                        </div>
                        <div className="col-12 col-md-12 col-lg">
                            <div className="text-wrapper align-left">
                                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                    <strong>Вместимость:</strong> <span>{autoparkData.capacity}</span>
                                </h1>

                            </div>
                        </div>
                        <div className='d-flex flex-row justify-content-center w-100'>
                            <div className="btn btn-primary display-3" onClick={handleStartEdit}>Редактировать</div>
                            <div className="btn btn-primary display-3" onClick={handleDelete}>Удалить</div>
                        </div>
                    </>
                }

                {
                    editing &&
                    <>
                        <div className="col-12 col-md-12 col-lg">
                            <AddressEdit address={autoparkData.address} onAddressChange={handleAddressChange} />
                        </div>
                        <div className="col-12 col-md-12 col-lg">
                            <div className="text-wrapper align-left">
                                <h1 className="mb-4 input-group">
                                    <strong className='input-group-text display-7'>Вместимость:</strong>
                                    <input name="input" value={autoparkData.capacity} type='number'
                                        className="form-control mx-2" id="capacity" onChange={handleChange}></input>
                                </h1>
                            </div>
                        </div>
                        <div className='d-flex flex-row justify-content-center w-100'>
                            <div className="btn btn-primary display-3" onClick={handleCancelEdit}>Отмена</div>
                            <div className="btn btn-primary display-3" onClick={handleSaveEdit}>Сохранить</div>
                        </div>
                    </>
                }


            </div>

            {
                error &&
                <div className="border border-danger border rounded-4 p-2 px-4 mt-2">
                    <span className="text-danger text-center h3">{typeof error === 'string' ? error : toString(error)}</span>
                </div>
            }
        </div >
    )
}
