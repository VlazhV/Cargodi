import React, { createElement, useEffect, useRef, useState } from 'react'
import { YMapMarker } from 'ymap3-components'
import ReactDOM from 'react-dom/client';
import { Link } from 'react-router-dom';

export default function StopMarker(props) {
    const stop = props.stop

    let longitude = stop?.address.isWest ? -stop?.address.longitude : stop?.address.longitude
    let latitude = stop?.address.isNorth ? stop?.address.latitude : -stop?.address.latitude

    const coordinates = [longitude, latitude]


    return (
        <>
            <YMapMarker coordinates={coordinates} >
                <Link to={'/order/' + stop.order.id}>
                    <div className='btn btn-outline-dark border border-3 p-2 border-dark rounded-pill translate-middle text-center fw-bold d-flex flex-column' style={{ width: '8rem' }}>
                        <span className='text-info bg-dark rounded-pill px-4 py-1' style={{ fontSize: '1.5rem' }}>№{stop.number}</span>
                        Заказ №{stop.order.id}
                    </div>
                </Link>

            </YMapMarker>
        </>
    )
}
