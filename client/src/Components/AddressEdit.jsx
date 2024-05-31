import React from 'react'
import MapAddressSelect from './MapAddressSelect'

export default function AddressEdit(props) {
    const onAddressChange = props.onAddressChange ? props.onAddressChange : (newAddres) => { }
    const address = props.address ? props.address : {}
    const name = props.name ? props.name : 'Адрес'

    const onAddressSelect = (newMapAddress) => {
        let newAddress = address
        let longitude = newMapAddress.geometry.coordinates[0]
        let latitude = newMapAddress.geometry.coordinates[1]

        newAddress.longitude = Math.abs(longitude)
        newAddress.latitude = Math.abs(latitude)
        newAddress.isNorth = latitude > 0
        newAddress.isWest = longitude < 0
        newAddress.name = newMapAddress.properties.name
        onAddressChange(newAddress)
    }

    let mapAddress = null
    if (address && address.name) {
        let longitude = address.isWest ? -address.longitude : address.longitude
        let latitude = address.isNorth ? address.latitude : -address.latitude

        mapAddress = {
            properties: { name: address.name },
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
            <MapAddressSelect mapAddress={mapAddress} onMapAddressSelect={onAddressSelect} />
        </div>
    )
}
