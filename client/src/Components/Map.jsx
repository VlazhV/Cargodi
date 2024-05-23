import React, { useEffect, useMemo, useRef, useState } from 'react'
import ReactDOM from 'react-dom';
import { YMap, YMapDefaultFeaturesLayer, YMapDefaultSchemeLayer } from 'ymap3-components';

export default function Map() {

    return (
        <YMap location={{
            center: [37.588144, 55.733842],
            zoom: 10
        }}>
            <YMapDefaultSchemeLayer />
            <YMapDefaultFeaturesLayer />
        </YMap>
    )
}
