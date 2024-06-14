import React, { useCallback, useContext, useEffect, useMemo, useRef, useState } from 'react'
import ReactDOM from 'react-dom/client';
import { YMap, YMapClusterer, YMapComponentsProvider, YMapContainer, YMapControl, YMapControls, YMapCustomClusterer, YMapDefaultFeaturesLayer, YMapDefaultMarker, YMapDefaultSchemeLayer, YMapFeature, YMapGeolocationControl, YMapListener, YMapMarker } from 'ymap3-components';
import { YMapsContext } from '../Contexts/YmapsContext';
import InputDropdown from './InputDropdown';
import StopMarker from './StopMarker';
import ClusterMarker from './ClusterMarker';


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

    const stopMarker = useCallback(
        (feature) => {
            return (
                <StopMarker stop={feature.stop} />
            )
        },
        []
    );

    const cluster = useCallback(
        (coordinates, features) => (
            <ClusterMarker coordinates={coordinates} count={features.length} />
        ),
        []
    );

    const features = stops.map((stop, index) => {

        let longitude = stop?.address.isWest ? -stop?.address.longitude : stop?.address.longitude
        let latitude = stop?.address.isNorth ? stop?.address.latitude : -stop?.address.latitude

        const coordinates = [longitude, latitude]

        return {
            type: "Feature",
            id: index,
            stop: stop,
            geometry: { coordinates, type: "Point" }
        }
    })

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
                    <YMapCustomClusterer
                        marker={stopMarker}
                        cluster={cluster}
                        gridSize={200}
                        features={features}
                    />
                    {props.children}
                </YMap>
            </div>
        </div >
    )
}
