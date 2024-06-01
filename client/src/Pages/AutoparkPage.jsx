import React, { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router'
import AutoparkService from '../Services/AutoparkService'
import { useFetching } from '../Hooks/useFetching'
import AddressView from '../Components/AddressView'
import AddressEdit from '../Components/AddressEdit'
import { Link } from 'react-router-dom'
import carTypes from '../Data/CarTypes.json'
import CarService from '../Services/CarService'
import TrailerService from '../Services/TrailerService'
import trailerTypes from '../Data/TrailerTypes.json'

export default function AutoparkPage() {
    const { parkId } = useParams()

    const navigate = useNavigate()

    const [showList, setShowList] = useState('showCars')

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

    const [newCarData, setNewCarData] = useState({
        licenseNumber: "",
        mark: "",
        range: 0, //km
        carrying: 0, //g
        tankVolume: 0, //dm3	

        capacityLength: 0, //mm	
        capacityWidth: 0, //mm	
        capacityHeight: 0, //mm

        carTypeId: 1,
        autoparkId: parkId,
        actualAutoparkId: parkId,
    })

    const [newTrailerData, setNewTrailerData] = useState({
        licenseNumber: "",
        carrying: 0, //g
        capacityLength: 0, //mm	
        capacityWidth: 0, //mm	
        capacityHeight: 0, //mm

        trailerTypeId: 1,
        autoparkId: parkId,
        actualAutoparkId: parkId,
    })

    const [fetch, loading, error] = useFetching(async (type) => {
        switch (type) {
            case "get":
                {
                    const res = await AutoparkService.GetById(parkId)

                    let autoparks = res.data
                    autoparks.drivers.forEach(d => {
                        d.employDate = new Date(d.employDate)
                        if (d.fireDate) {
                            d.fireDate = new Date(d.fireDate)
                        }
                    })

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
            case "createCar":
                {
                    const res = await CarService.Create(newCarData)
                    fetch("get")
                }
                break;
            case "createTrailer":
                {
                    const res = await TrailerService.Create(newTrailerData)
                    fetch("get")
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

    const handleCarChange = (e) => {
        e.preventDefault()
        setNewCarData((prevData) => { return { ...prevData, [e.target.id]: e.target.value } })
    }

    const handleTrailerChange = (e) => {
        e.preventDefault()
        setNewTrailerData((prevData) => { return { ...prevData, [e.target.id]: e.target.value } })
    }

    const handleCarCreate = (e) => {
        e.preventDefault()
        fetch("createCar")
    }

    const handleTrailerCreate = (e) => {
        e.preventDefault()
        fetch("createTrailer")
    }

    const switchRenderList = () => {
        switch (showList) {
            case 'showCars':
                return <>

                    <div className="justify-content-center d-flex flex-row">
                        <div className="">
                            <div className="col-12 form-group mb-3" data-for="textarea">
                                <input name="input" placeholder="Регистрационный номер" type="text" data-form-field="input"
                                    className="form-control" id="licenseNumber" onChange={handleCarChange} ></input>
                            </div>
                            <div className="col-12 form-group mb-3" data-for="textarea">
                                <input name="input" placeholder="Марка" type="text" data-form-field="input"
                                    className="form-control" id="mark" onChange={handleCarChange} ></input>
                            </div>
                            <div className="col-12 form-group mb-3" data-for="textarea">
                                <input name="input" placeholder="Дальность следования (км)" type="number" data-form-field="input"
                                    className="form-control" id="range" onChange={handleCarChange} ></input>
                            </div>
                            <div className="col-12 form-group mb-3" data-for="textarea">
                                <input name="input" placeholder="Грузоподъёмность (кг)" type="number" data-form-field="input"
                                    className="form-control" id="carrying" onChange={handleCarChange} ></input>
                            </div>
                            <div className="col-12 form-group mb-3" data-for="textarea">
                                <input name="input" placeholder="Объём бака (л)" type="number" data-form-field="input"
                                    className="form-control" id="tankVolume" onChange={handleCarChange} ></input>
                            </div>
                        </div>
                        <div className="">
                            <div className="col-12 form-group mb-3" data-for="textarea">
                                <input name="input" placeholder="Длина вместимости (см)" type="number" data-form-field="input"
                                    className="form-control" id="capacityLength" onChange={handleCarChange} ></input>
                            </div>
                            <div className="col-12 form-group mb-3" data-for="textarea">
                                <input name="input" placeholder="Ширина вместимости (см)" type="number" data-form-field="input"
                                    className="form-control" id="capacityWidth" onChange={handleCarChange} ></input>
                            </div>
                            <div className="col-12 form-group mb-3" data-for="textarea">
                                <input name="input" placeholder="Высота вместимости (см)" type="number" data-form-field="input"
                                    className="form-control" id="capacityHeight" onChange={handleCarChange} ></input>
                            </div>
                            <div className="col-12 form-group mb-3" data-for="input">

                                <select name="select" className='form-select display-7 p-3' onChange={handleCarChange} id="carTypeId">
                                    {
                                        carTypes.map(cType => <option value={cType.id} key={cType.id}>{cType.name}</option>)
                                    }
                                </select>
                            </div>
                        </div>
                    </div>
                    <div className="col-lg-12 col-md-12 col-sm-12 align-center mbr-section-btn">
                        <button className="btn btn-primary display-7" onClick={handleCarCreate}>Добавить машину</button>
                    </div>
                    <div className='w-100 d-flex flex-row'>
                        <div className='w-50 border-end border-5 p-4'>
                            <h2 className="mbr-section-title mbr-fonts-style my-4 ">
                                <strong>Текущие машины</strong> <span>(Кол-во: {autoparkData.actualCars.length})</span>
                            </h2>
                            <div className="row w-100">

                                {
                                    autoparkData.actualCars.map(carData =>
                                        <div className="item features-without-image col-14 col-md-6 col-lg-4 item-mb active border rounded rounded-4" key={carData.id}>
                                            <div className="item-wrapper">
                                                <div className="item-head">
                                                    <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                                        <strong>Машина №{carData.id}</strong>
                                                    </h6>
                                                    <h5 className="item-title mbr-fonts-style mb-0 display-7">
                                                        <strong>Марка: </strong> {carData.mark}
                                                    </h5>
                                                    <h5 className="item-title mbr-fonts-style mb-0 display-7">
                                                        <strong>Тип: </strong> {carTypes.find(v => v.id == carData.carType.id).name}
                                                    </h5>
                                                </div>

                                                <div className="mbr-section-btn item-footer">
                                                    <Link to={"/car/" + carData.id} className="btn item-btn btn-primary">Подробнее</Link>
                                                </div>
                                            </div>
                                        </div>
                                    )
                                }

                            </div>
                        </div>
                        <div className='w-50 p-4'>
                            <h2 className="mbr-section-title mbr-fonts-style my-4 ">
                                <strong>Прописанные машины</strong> <span>(Кол-во: {autoparkData.cars.length})</span>
                            </h2>
                            <div className="row w-100">

                                {
                                    autoparkData.cars.map(carData =>
                                        <div className="item features-without-image col-14 col-md-6 col-lg-4 item-mb active border rounded rounded-4" key={carData.id}>
                                            <div className="item-wrapper">
                                                <div className="item-head">
                                                    <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                                        <strong>Машина №{carData.id}</strong>
                                                    </h6>
                                                    <h5 className="item-title mbr-fonts-style mb-0 display-7">
                                                        <strong>Марка: </strong> {carData.mark}
                                                    </h5>
                                                    <h5 className="item-title mbr-fonts-style mb-0 display-7">
                                                        <strong>Тип: </strong> {carTypes.find(v => v.id == carData.carType.id).name}
                                                    </h5>
                                                </div>

                                                <div className="mbr-section-btn item-footer">
                                                    <Link to={"/car/" + carData.id} className="btn item-btn btn-primary">Подробнее</Link>
                                                </div>
                                            </div>
                                        </div>
                                    )
                                }

                            </div>
                        </div>
                    </div>
                </>
            case 'showTrailers':
                return <>

                    <div className="justify-content-center d-flex flex-row">
                        <div className="">
                            <div className="col-12 form-group mb-3" data-for="textarea">
                                <input name="input" placeholder="Регистрационный номер" type="text" data-form-field="input"
                                    className="form-control" id="licenseNumber" onChange={handleTrailerChange} ></input>
                            </div>
                            <div className="col-12 form-group mb-3" data-for="textarea">
                                <input name="input" placeholder="Грузоподъёмность (кг)" type="number" data-form-field="input"
                                    className="form-control" id="carrying" onChange={handleTrailerChange} ></input>
                            </div>
                        </div>
                        <div className="">
                            <div className="col-12 form-group mb-3" data-for="textarea">
                                <input name="input" placeholder="Длина вместимости (см)" type="number" data-form-field="input"
                                    className="form-control" id="capacityLength" onChange={handleTrailerChange} ></input>
                            </div>
                            <div className="col-12 form-group mb-3" data-for="textarea">
                                <input name="input" placeholder="Ширина вместимости (см)" type="number" data-form-field="input"
                                    className="form-control" id="capacityWidth" onChange={handleTrailerChange} ></input>
                            </div>
                            <div className="col-12 form-group mb-3" data-for="textarea">
                                <input name="input" placeholder="Высота вместимости (см)" type="number" data-form-field="input"
                                    className="form-control" id="capacityHeight" onChange={handleTrailerChange} ></input>
                            </div>
                            <div className="col-12 form-group mb-3" data-for="input">

                                <select name="select" className='form-select display-7 p-3' onChange={handleTrailerChange} id="trailerTypeId">
                                    {
                                        trailerTypes.map(tType => <option value={tType.id} key={tType.id}>{tType.name}</option>)
                                    }
                                </select>
                            </div>
                        </div>
                    </div>
                    <div className="col-lg-12 col-md-12 col-sm-12 align-center mbr-section-btn">
                        <button className="btn btn-primary display-7" onClick={handleTrailerCreate}>Добавить прицеп</button>
                    </div>
                    <div className='w-100 d-flex flex-row'>
                        <div className='w-50 border-end border-5 p-4'>
                            <h2 className="mbr-section-title mbr-fonts-style my-4">
                                <strong>Текущие прицепы</strong> <span>(Кол-во: {autoparkData.actualTrailers.length})</span>
                            </h2>
                            <div className="row w-100">

                                {
                                    autoparkData.actualTrailers.map(trailerData =>
                                        <div className="item features-without-image col-14 col-md-6 col-lg-4 item-mb active border rounded rounded-4" key={trailerData.id}>
                                            <div className="item-wrapper">
                                                <div className="item-head">
                                                    <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                                        <strong>Прицеп №{trailerData.id}</strong>
                                                    </h6>
                                                    <h5 className="item-title mbr-fonts-style mb-0 display-7">
                                                        <strong>Тип: </strong> {trailerTypes.find(v => v.id == trailerData.trailerType.id).name}
                                                    </h5>
                                                </div>

                                                <div className="mbr-section-btn item-footer">
                                                    <Link to={"/trailer/" + trailerData.id} className="btn item-btn btn-primary">Подробнее</Link>
                                                </div>
                                            </div>
                                        </div>
                                    )
                                }
                            </div>
                        </div>
                        <div className='w-50 p-4'>
                            <h2 className="mbr-section-title mbr-fonts-style my-4">
                                <strong>Прописанные прицепы</strong> <span>(Кол-во: {autoparkData.trailers.length})</span>
                            </h2>
                            <div className="row w-100">

                                {
                                    autoparkData.trailers.map(trailerData =>
                                        <div key={trailerData.id} className="item features-without-image col-14 col-md-6 col-lg-4 item-mb active border rounded rounded-4">
                                            <div className="item-wrapper">
                                                <div className="item-head">
                                                    <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                                        <strong>Прицеп №{trailerData.id}</strong>
                                                    </h6>
                                                    <h5 className="item-title mbr-fonts-style mb-0 display-7">
                                                        <strong>Тип: </strong> {trailerTypes.find(v => v.id == trailerData.trailerType.id).name}
                                                    </h5>
                                                </div>

                                                <div className="mbr-section-btn item-footer">
                                                    <Link to={"/trailer/" + trailerData.id} className="btn item-btn btn-primary">Подробнее</Link>
                                                </div>
                                            </div>
                                        </div>
                                    )
                                }
                            </div>
                        </div>
                    </div>
                </>
            case 'showDrivers':
                return <div className="row w-100">
                    {
                        autoparkData.drivers.map(driverData =>
                            <div key={driverData.id} className="item features-without-image col-4 item-mb active border rounded">
                                <div className="item-wrapper">
                                    <div className="item-head">
                                        <h6 className="item-title mbr-fonts-style mb-0 display-7">
                                            <strong>Водитель №{driverData.id}</strong>
                                        </h6>
                                        <h6 className="item-title mbr-fonts-style mb-0 display-7">
                                            <strong>ФИО: {driverData.secondName} {driverData.firstName} {driverData.middleName}</strong>
                                        </h6>
                                        <h5 className="item-subtitle mbr-fonts-style mt-0 mb-1 ">
                                            Вод. удостоверение: {driverData.license}
                                        </h5>
                                        <h5 className="item-subtitle mbr-fonts-style mt-0 mb-1 ">
                                            Время нанятия: {driverData.employDate.toLocaleString()}
                                        </h5>

                                    </div>

                                </div>
                            </div>
                        )
                    }

                </div>
        }
    }

    const handleShowListChange = (e) => {
        setShowList(e.target.id)
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
                            <div className="btn btn-danger display-3" onClick={handleDelete}>Удалить</div>
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

            <div className="btn-group d-flex flex-row justify-content-center mb-3 w-100">
                <input type="radio" className="btn-check"
                    name="btnradio" id="showCars"
                    autoComplete="off" defaultChecked={true} onClick={handleShowListChange} />
                <label className="btn btn-outline-dark" htmlFor="showCars">Машины</label>

                <input type="radio" className="btn-check"
                    name="btnradio" id="showTrailers"
                    autoComplete="off" onClick={handleShowListChange} />
                <label className="btn btn-outline-dark" htmlFor="showTrailers">Прицепы</label>

                <input type="radio" className="btn-check"
                    name="btnradio" id="showDrivers"
                    autoComplete="off" onClick={handleShowListChange} />
                <label className="btn btn-outline-dark" htmlFor="showDrivers">Водители</label>
            </div>

            {switchRenderList()}
            <div className="d-flex align-items-center text-warning m-4 rounded-pill px-4 py-2 display-4 fixed-bottom bg-dark" style={{ visibility: loading ? 'visible' : 'hidden' }}>
                <strong>Загрузка...</strong>
                <div className="spinner-border ms-auto" role="status" aria-hidden="true"></div>
            </div>
        </div >
    )
}
