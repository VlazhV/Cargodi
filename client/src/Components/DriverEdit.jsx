import React, { useEffect, useState } from 'react'
import { useFetching } from '../Hooks/useFetching'
import UserService from '../Services/UserService'
import DriverService from '../Services/DriverService'

export default function DriverEdit(props) {
    const id = props.id ? props.id : null
    const onSelectDriver = props.onSelectDriver ? props.onSelectDriver : (newDriverData) => { }
    const nullable = props.nullable

    const [driversData, setDriversData] = useState([])

    const handleSelectDriver = (e) => {
        e.preventDefault()
        let index = e.target.id
        if (index == -1) {
            onSelectDriver(null)
        }
        else {
            onSelectDriver({ ...(driversData[index]) })
        }
    }

    const [fetch, loading, error] = useFetching(async (type) => {
        switch (type) {
            case "get":
                {
                    const res = await DriverService.GetAll()

                    let drivers = res.data
                    drivers.forEach(dr => {
                        dr.employDate = new Date(dr.employDate)
                        if (dr.fireDate) {
                            dr.fireDate = new Date(dr.fireDate)
                        }
                    })

                    setDriversData(drivers)
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
                        <h1 className="modal-title fs-5">Водители</h1>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div className="modal-body d-flex flex-column">
                        {
                            driversData.map((driverData, index) => {
                                return <button className='btn btn-outline-primary' data-bs-dismiss="modal" key={driverData.id} id={index} onClick={handleSelectDriver}>
                                    Водитель №{driverData.id}
                                    <br />
                                    ФИО: {driverData.secondName} {driverData.firstName} {driverData.middleName}
                                    <br />
                                    Вод. удостоверение: {driverData.license}
                                    <br />
                                    Время нанятия: {driverData.employDate.toLocaleString()}
                                </button>
                            })
                        }
                        {
                            nullable &&
                            <button className='btn btn-outline-secondary' data-bs-dismiss="modal" id={-1} onClick={handleSelectDriver}>
                                Убрать водителя
                            </button>
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
