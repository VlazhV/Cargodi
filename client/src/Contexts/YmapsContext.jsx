import { createContext, useEffect, useReducer, useState } from "react"
import { AuthReducer, INITIAL_STATE } from "../Redux/AuthReducer"
import { YMapComponentsProvider } from "ymap3-components"


export const YMapsContext = createContext(INITIAL_STATE)

export const YMapsContextProvider = ({ children }) => {
    const [ymaps, setYmaps] = useState(null)

    useEffect(() => {
    }, [ymaps])

    const onMapsLoad = (params) => {
        setYmaps(params.ymaps)
    }

    return (
        <YMapComponentsProvider apiKey='b18e0a22-f14a-4d61-8b5f-6da9260a1876' lang='ru_RU' onLoad={onMapsLoad}>
            <YMapsContext.Provider
                value={{
                    ymaps
                }}>
                {children}
            </YMapsContext.Provider>
        </YMapComponentsProvider>
    )
}