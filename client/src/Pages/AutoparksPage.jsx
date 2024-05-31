import React, { useEffect, useState } from 'react'
import { useFetching } from '../Hooks/useFetching'
import { Link } from 'react-router-dom'
import AutoparkService from '../Services/AutoparkService'
import Map from '../Components/MapAddressSelect'
import AddressEdit from '../Components/AddressEdit'
import EditYandexMap from '../Components/MapAddressSelect'

export default function AutoparksPage() {

    const [autoparksData, setAutoparksData] = useState([])

    const [newParkData, setNewParkData] = useState({
        address: {
            name: '',
            longitude: null,
            latitude: null,
            isWest: false,
            isNorth: false
        },
        capacity: null,
    })

    const [fetch, loading, error] = useFetching(async (type) => {
        switch (type) {
            case "get":
                {
                    const res = await AutoparkService.GetAll()
                    setAutoparksData(res.data)
                }
                break;
            case "create":
                {
                    const res = await AutoparkService.Create(newParkData.address, newParkData.capacity)
                    fetch("get")
                }
                break;
        }

    })

    useEffect(() => {
        fetch("get")
    }, [])

    const handleAddressChange = (newAddress) => {
        setNewParkData(oldValue => { return { capacity: oldValue.capacity, address: newAddress } })
    }

    const handleCapacityChange = (e) => {
        e.preventDefault()
        setNewParkData(oldValue => { return { capacity: e.target.value, address: oldValue.address } })
    }

    const handleCreatePark = (e) => {
        e.preventDefault()
        fetch("create")
    }

    return (
        <div className="container-fluid mt-5 p-5">
            <div className='mt-5'>
                <div className="row justify-content-center">
                    <div className="col-12 content-head">
                        <div className="mbr-section-head mb-5">
                            <h4 className="mbr-section-title mbr-fonts-style align-center mb-0 display-2">
                                <strong>Автопарки</strong>
                            </h4>
                        </div>
                    </div>
                </div>
                <div className="justify-content-center d-flex flex-row">
                    <AddressEdit address={newParkData.address} onAddressChange={handleAddressChange} />
                    <div className="">
                        <div className="col-12 form-group mb-3" data-for="textarea">
                            <input name="input" placeholder="Вместимость" type="number" data-form-field="input"
                                className="form-control" id="capacity" min={1} onChange={handleCapacityChange}></input>
                        </div>


                        <div className="col-lg-12 col-md-12 col-sm-12 align-center mbr-section-btn">
                            <button className="btn btn-primary display-7" onClick={handleCreatePark}>Создать автопарк</button>
                        </div>

                    </div>
                </div>
                {
                    error &&
                    <div className="border border-danger border rounded-4 p-2 px-4 mt-2">
                        <span className="text-danger text-center h3">{typeof error === 'string' ? error : toString(error)}</span>
                    </div>
                }
                <div className="row">

                    {
                        autoparksData.map(autoparkData =>
                            <div className="item features-without-image col-12 col-md-6 col-lg-3 item-mb active border rounded rounded-4" key={autoparkData.id}>
                                <div className="item-wrapper">
                                    <div className="item-head">
                                        <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                            <strong>Автопарк №{autoparkData.id}</strong>
                                        </h6>
                                        <h5 className="item-title mbr-fonts-style mb-0 display-7">
                                            <strong>Адрес:</strong> {autoparkData.address.name}
                                        </h5>
                                        <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                            <strong>Вместимость:</strong> {autoparkData.capacity}
                                        </h6>
                                    </div>

                                    <div className="mbr-section-btn item-footer">
                                        <Link to={"/autopark/" + autoparkData.id} className="btn item-btn btn-primary display-7">Подробнее</Link>
                                    </div>
                                </div>
                            </div>
                        )
                    }

                </div>
            </div>
            <div className="d-flex align-items-center text-warning m-4 rounded-pill px-4 py-2 display-4 fixed-bottom bg-dark" style={{ visibility: loading ? 'visible' : 'hidden' }}>
                <strong>Загрузка...</strong>
                <div className="spinner-border ms-auto" role="status" aria-hidden="true"></div>
            </div>
        </div>
    )
}
