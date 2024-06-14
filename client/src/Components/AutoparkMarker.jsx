import React, { createElement, useEffect, useRef, useState } from 'react'
import { YMapMarker } from 'ymap3-components'
import { Link } from 'react-router-dom';

export default function AutoparkMarker(props) {
    const autoparkData = props.autoparkData

    let longitude = autoparkData?.address.isWest ? -autoparkData?.address.longitude : autoparkData?.address.longitude
    let latitude = autoparkData?.address.isNorth ? autoparkData?.address.latitude : -autoparkData?.address.latitude

    const coordinates = [longitude, latitude]

    return (
        <>
            <YMapMarker coordinates={coordinates} >
                <div className='bg-dark rounded-pill p-2 position-absolute translate-middle'
                    style={{ width: '1rem', height: '1rem' }}></div>
                <Link to={'/autopark/' + autoparkData.id}>
                    <div className='btn btn-outline-dark 
                    border border-3 p-2 border-dark 
                    text-center mx-0 rounded-pill
                    fw-bold d-flex flex-column'
                        style={{
                            width: '12rem',
                            position: 'absolute',
                            transform: 'translate(-50%, -130%)'
                        }}>

                        <span className='text-warning bg-dark 
                        rounded-pill px-4 py-1'
                            style={{ fontSize: '1rem' }}>
                            Автопарк №{autoparkData.id}
                            <br />
                            {autoparkData.address.name}
                        </span>
                    </div>
                </Link>

            </YMapMarker >
        </>
    )
}
