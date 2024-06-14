import React, { useCallback, useContext, useEffect, useMemo, useRef, useState } from 'react'
import ReactDOM from 'react-dom';
import { YMap, YMapComponentsProvider, YMapContainer, YMapControl, YMapControls, YMapDefaultFeaturesLayer, YMapDefaultMarker, YMapDefaultSchemeLayer, YMapFeature, YMapGeolocationControl, YMapListener } from 'ymap3-components';
import { YMapsContext } from '../Contexts/YmapsContext';
import InputDropdown from './InputDropdown';


export default function MapAddressSelect(props) {
    const onMapAddressSelect = props.onMapAddressSelect ? props.onMapAddressSelect : (newMapAddress) => { }
    const mapAddress = props.mapAddress ? props.mapAddress : null;

    const { ymaps } = useContext(YMapsContext)
    const [ymap, setYmap] = useState(null);

    const [currentMapLocation, setCurrentMapLocation]
        = useState({ center: [27.561831, 53.902284], zoom: 10 })

    const onUpdate = useCallback(({ location, mapInAction }) => {
        if (!mapInAction) {
            setCurrentMapLocation(location);
        }
    }, []);


    const [searchResponse, setSearchResponse] = useState([])
    const [selectedAddress, setSelectedAddress] = useState(mapAddress)

    const onAddressTextChange = async (newText) => {
        const res = await ymaps.search({ text: newText })
        setSearchResponse(res)
    }

    const onAddressSelect = (sResponse) => {
        setSelectedAddress(sResponse)
    }

    useEffect(() => {
        if (selectedAddress) {
            const newLocation = {
                zoom: currentMapLocation.zoom,
                center: selectedAddress.geometry.coordinates
            }
            setCurrentMapLocation(newLocation)

            onMapAddressSelect(selectedAddress)
        }

    }, [selectedAddress])

    return (
        <div>
            <InputDropdown options={searchResponse}
                value={selectedAddress?.properties?.name}
                onTextChange={onAddressTextChange}
                onSelect={onAddressSelect}
                getName={(option) => option?.properties?.name}
                placeholder='Введите адрес' />
            <div className='YMap mt-2'>
                <YMap key="map"
                    ref={(ymap) => setYmap(ymap)}
                    location={currentMapLocation}
                    mode="vector">
                    <YMapListener onUpdate={onUpdate} />
                    <YMapDefaultFeaturesLayer />
                    <YMapDefaultSchemeLayer />
                    {
                        selectedAddress &&
                        <>
                            <YMapDefaultMarker coordinates={selectedAddress.geometry.coordinates}></YMapDefaultMarker>
                        </>
                    }
                    {props.children}
                </YMap>
            </div>
        </div>
    )
}
