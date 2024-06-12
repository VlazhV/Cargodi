import React, { useCallback, useContext, useEffect, useMemo, useRef, useState } from 'react'
import ReactDOM from 'react-dom';
import { YMap, YMapComponentsProvider, YMapContainer, YMapControl, YMapControls, YMapDefaultFeaturesLayer, YMapDefaultMarker, YMapDefaultSchemeLayer, YMapFeature, YMapGeolocationControl, YMapListener } from 'ymap3-components';
import { YMapsContext } from '../Contexts/YmapsContext';
import InputDropdown from './InputDropdown';


export default function MapTwoAddressSelect(props) {
    const onMapAddressFromSelect = props.onMapAddressFromSelect ? props.onMapAddressFromSelect : (newMapAddress) => { }
    const onMapAddressToSelect = props.onMapAddressToSelect ? props.onMapAddressToSelect : (newMapAddress) => { }
    const mapAddressFrom = props.mapAddressFrom ? props.mapAddressFrom : null;
    const mapAddressTo = props.mapAddressTo ? props.mapAddressTo : null;

    const { ymaps } = useContext(YMapsContext)
    const [ymap, setYmap] = useState(null);

    const [currentMapLocation, setCurrentMapLocation]
        = useState({ center: [27.561831, 53.902284], zoom: 10 })

    const onUpdate = useCallback(({ location, mapInAction }) => {
        if (!mapInAction) {
            setCurrentMapLocation(location);
        }
    }, []);


    const [searchFromResponse, setSearchFromResponse] = useState([])
    const [searchToResponse, setSearchToResponse] = useState([])
    const [selectedAddressFrom, setSelectedAddressFrom] = useState(mapAddressFrom)
    const [selectedAddressTo, setSelectedAddressTo] = useState(mapAddressTo)

    const onAddressFromTextChange = async (newText) => {
        const res = await ymaps.search({ text: newText })
        setSearchFromResponse(res)
    }
    const onAddressToTextChange = async (newText) => {
        const res = await ymaps.search({ text: newText })
        setSearchToResponse(res)
    }

    const onAddressFromSelect = (sResponse) => {
        setSelectedAddressFrom(sResponse)
    }
    const onAddressToSelect = (sResponse) => {
        setSelectedAddressTo(sResponse)
    }

    useEffect(() => {
        if (selectedAddressFrom) {
            const newLocation = {
                zoom: currentMapLocation.zoom,
                center: selectedAddressFrom.geometry.coordinates
            }
            setCurrentMapLocation(newLocation)
            onMapAddressFromSelect(selectedAddressFrom)
        }

    }, [selectedAddressFrom])

    useEffect(() => {
        if (selectedAddressTo) {
            const newLocation = {
                zoom: currentMapLocation.zoom,
                center: selectedAddressTo.geometry.coordinates
            }
            setCurrentMapLocation(newLocation)
            onMapAddressToSelect(selectedAddressTo)
        }

    }, [selectedAddressTo])

    return (
        <div>
            <InputDropdown options={searchFromResponse}
                value={selectedAddressFrom?.properties?.name}
                onTextChange={onAddressFromTextChange}
                onSelect={onAddressFromSelect}
                getName={(option) => option?.properties?.name}
                placeholder='Адрес загрузки' />
            <div className='m-2'></div>
            <InputDropdown options={searchToResponse}
                value={selectedAddressTo?.properties?.name}
                onTextChange={onAddressToTextChange}
                onSelect={onAddressToSelect}
                getName={(option) => option?.properties?.name}
                placeholder='Адрес разгрузки' />
            <div className='YMap mt-2'>
                <YMap key="map"
                    ref={(ymap) => setYmap(ymap)}
                    location={currentMapLocation}
                    mode="vector">
                    <YMapListener onUpdate={onUpdate} />
                    <YMapDefaultFeaturesLayer />
                    <YMapDefaultSchemeLayer />
                    {
                        selectedAddressFrom &&
                        <>
                            <YMapDefaultMarker color='green' coordinates={selectedAddressFrom.geometry.coordinates}></YMapDefaultMarker>
                        </>
                    }
                    {
                        selectedAddressTo &&
                        <>
                            <YMapDefaultMarker color='red' coordinates={selectedAddressTo.geometry.coordinates}></YMapDefaultMarker>
                        </>
                    }
                </YMap>
            </div>
        </div>
    )
}
