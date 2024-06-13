import React, { useCallback, useContext, useEffect, useMemo, useRef, useState } from 'react'
import ReactDOM from 'react-dom/client';
import { YMap, YMapClusterer, YMapComponentsProvider, YMapContainer, YMapControl, YMapControls, YMapDefaultFeaturesLayer, YMapDefaultMarker, YMapDefaultSchemeLayer, YMapFeature, YMapGeolocationControl, YMapListener, YMapMarker } from 'ymap3-components';
import { YMapsContext } from '../Contexts/YmapsContext';
import InputDropdown from './InputDropdown';
import StopMarker from './StopMarker';


export default function MapRoutes(props) {
    const stops = props.stops ? props.stops : [];

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
        <div>
            <div className='YMap mt-2'>
                <YMap key="map"
                    ref={(ymap) => setYmap(ymap)}
                    location={currentMapLocation}
                    mode="vector">
                    <YMapListener onUpdate={onUpdate} />
                    <YMapDefaultFeaturesLayer />
                    <YMapDefaultSchemeLayer />
                    {
                        stops.map((stop, index) => {

                            return <StopMarker
                                key={index}
                                stop={stop}
                            />
                        })
                    }
                </YMap>
            </div>
        </div >
    )
}
