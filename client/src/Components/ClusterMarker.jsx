import React, { createElement, useEffect, useRef, useState } from 'react'
import { YMapMarker } from 'ymap3-components'
import ReactDOM from 'react-dom/client';
import { Link } from 'react-router-dom';

export default function ClusterMarker(props) {
    const coordinates = props.coordinates
    const count = props.count

    return (
        <>
            <YMapMarker coordinates={coordinates}>
                <div className='bg-dark rounded-circle text-white
                text-center d-flex align-items-center justify-content-center'
                    style={{ width: '3rem', height: '3rem' }}>
                    {count}
                </div>
            </YMapMarker>
        </>
    )
}
