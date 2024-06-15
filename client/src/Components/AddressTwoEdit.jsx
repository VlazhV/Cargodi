import React from 'react'
import MapAddressSelect from './MapAddressSelect'
import MapTwoAddressSelect from './MapTwoAddressSelect'

export default function AddressTwoEdit(props) {
    const onAddressFromChange = props.onAddressFromChange ? props.onAddressFromChange : (newAddres) => { }
    const onAddressToChange = props.onAddressToChange ? props.onAddressToChange : (newAddres) => { }
    const addressFrom = props.addressFrom ? props.addressFrom : {}
    const addressTo = props.addressTo ? props.addressTo : {}
    const name = props.name ? props.name : 'Адрес начала и конца'

    const onAddressFromSelect = (newMapAddress) => {
        let newAddress = { ...addressFrom }
        let longitude = newMapAddress.geometry.coordinates[0]
        let latitude = newMapAddress.geometry.coordinates[1]

        newAddress.longitude = Math.abs(longitude)
        newAddress.latitude = Math.abs(latitude)
        newAddress.isNorth = latitude > 0
        newAddress.isWest = longitude < 0
        newAddress.name = newMapAddress.properties.name
        onAddressFromChange(newAddress)
    }

    const onAddressToSelect = (newMapAddress) => {
        let newAddress = { ...addressTo }
        let longitude = newMapAddress.geometry.coordinates[0]
        let latitude = newMapAddress.geometry.coordinates[1]

        newAddress.longitude = Math.abs(longitude)
        newAddress.latitude = Math.abs(latitude)
        newAddress.isNorth = latitude > 0
        newAddress.isWest = longitude < 0
        newAddress.name = newMapAddress.properties.name
        onAddressToChange(newAddress)
    }

    let mapAddressFrom = null
    if (addressFrom && addressFrom.name) {
        let longitude = addressFrom.isWest ? -addressFrom.longitude : addressFrom.longitude
        let latitude = addressFrom.isNorth ? addressFrom.latitude : -addressFrom.latitude

        mapAddressFrom = {
            properties: { name: addressFrom.name },
            geometry: { coordinates: [longitude, latitude] }
        }
    }

    let mapAddressTo = null
    if (addressTo && addressTo.name) {
        let longitude = addressTo.isWest ? -addressTo.longitude : addressTo.longitude
        let latitude = addressTo.isNorth ? addressTo.latitude : -addressTo.latitude

        mapAddressTo = {
            properties: { name: addressTo.name },
            geometry: { coordinates: [longitude, latitude] }
        }
    }

    return (
        <div className="" >
            <div className="mbr-section-head mb-2">
                <h6 className="align-center mb-0 display-6">
                    <strong>{name}</strong>
                </h6>
            </div>
            <MapTwoAddressSelect mapAddressFrom={mapAddressFrom}
                mapAddressTo={mapAddressTo}
                onMapAddressFromSelect={onAddressFromSelect}
                onMapAddressToSelect={onAddressToSelect} />
        </div>
    )
}
