import React, { useCallback, useContext, useState } from 'react'
import { YMapsContext } from '../Contexts/YmapsContext';
import { YMap, YMapDefaultFeaturesLayer, YMapDefaultSchemeLayer, YMapListener } from 'ymap3-components';
import AddressMarker from './AddressMarker';

export default function AddressView(props) {
    const address = props.address ? props.address : {}

    const { ymaps } = useContext(YMapsContext)
    const [ymap, setYmap] = useState(null);

    const [currentMapLocation, setCurrentMapLocation]
        = useState({ center: [27.561831, 53.902284], zoom: 10 })

    const onUpdate = useCallback(({ location, mapInAction }) => {
        if (!mapInAction) {
            setCurrentMapLocation(location);
        }
    }, []);

    return (
        <div className="" >
            <div className="mbr-section-head mb-2">
                <h6 className="align-center mb-0 display-6">
                    <strong>Адрес</strong>
                </h6>
            </div>
            <div className='YMap mt-2'>
                <YMap key="map"
                    ref={(ymap) => setYmap(ymap)}
                    location={currentMapLocation}
                    mode="vector">
                    <YMapListener onUpdate={onUpdate} />
                    <YMapDefaultFeaturesLayer />
                    <YMapDefaultSchemeLayer />
                    {props.children}
                    <AddressMarker address={address} />
                </YMap>
            </div>
        </div>
    )
}
