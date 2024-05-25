import React from 'react'

export default function AddressView(props) {
    const address = props.address ? props.address : {}

    return (
        <div className="" >
            <div className="mbr-section-head mb-2">
                <h6 className="align-center mb-0 display-6">
                    <strong>Адрес</strong>
                </h6>
            </div>
            <div className="text-wrapper align-left">
                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                    <strong>Название:</strong> <span>{address.name}</span>
                </h1>
                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                    <strong>Широта:</strong> <span>{address.latitude}</span>
                </h1>
                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                    <strong>Долгота:</strong> <span>{address.longitude}</span>
                </h1>
                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                    <strong>Запад:</strong> <span>{address.isWest ? '+' : '-'}</span>
                </h1>
                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                    <strong>Север: </strong> <span>{address.isNorth ? '+' : '-'}</span>
                </h1>
            </div>
        </div>
    )
}
