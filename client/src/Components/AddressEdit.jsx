import React from 'react'

export default function AddressEdit(props) {
    const onAddressChange = props.onAddressChange ? props.onAddressChange : (newAddres) => { }
    const address = props.address ? props.address : {}

    const handleAddressChange = (e) => {
        e.preventDefault()
        let newAddress = address
        newAddress = { ...newAddress, [e.target.id]: e.target.value }
        newAddress.isWest = Boolean(newAddress.isWest)
        newAddress.isNorth = Boolean(newAddress.isNorth)
        onAddressChange(newAddress)
    }

    return (
        <div className="" >
            <div className="mbr-section-head mb-2">
                <h6 className="align-center mb-0 display-6">
                    <strong>Адресс</strong>
                </h6>
            </div>
            <div className="col-12 form-group mb-3" data-for="textarea">
                <input name="input" placeholder="Название" type="text" data-form-field="input"
                    className="form-control" id="name" value={address.name} onChange={handleAddressChange}></input>
            </div>

            <div className="col-12 form-group mb-3" data-for="textarea">
                <input name="input" placeholder="Широта" type='text' data-form-field="input"
                    className="form-control" id="latitude" value={address.latitude} onChange={handleAddressChange}></input>
            </div>

            <div className="col-12 form-group mb-3" data-for="input">
                <input name="input" placeholder="Долгота" type='text' data-form-field="input"
                    className="form-control" id="longitude" value={address.longitude} onChange={handleAddressChange}></input>
            </div>

            <div className='px-4 display-4'>
                <div className="col-12 form-check mb-3" data-for="input">
                    <input name="input" type='checkbox' data-form-field="input"
                        className="form-check-input" id="isWest" value={address.isWest} onChange={handleAddressChange}></input>
                    <label className='form-check-label' htmlFor='isWest'>Запад</label>
                </div>
            </div>

            <div className='px-4 display-4'>
                <div className="col-12 form-check mb-3" data-for="input">
                    <input name="input" type='checkbox' data-form-field="input"
                        className="form-check-input" id="isNorth" value={address.isNorth} onChange={handleAddressChange}></input>
                    <label className='form-check-label' htmlFor='isNorth'>Север</label>
                </div>
            </div>
        </div>
    )
}
