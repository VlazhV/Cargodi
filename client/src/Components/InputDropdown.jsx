import React, { useEffect, useRef, useState } from 'react'
import useActiveElement from '../Hooks/useActiveElement'

export default function InputDropdown(props) {
    const getName = props.getName ? props.getName : (option => toString(option))
    const options = props.options ? props.options : []
    const onTextChange = props.onTextChange ? props.onTextChange : (newText) => { }
    const onSelect = props.onSelect ? props.onSelect : (option) => { }
    const placeholder = props.placeholder ? props.placeholder : ''
    const value = props.value ? props.value : null;

    const [inputText, setInputText] = useState(value ? value : '')

    const textRef = useRef()
    const activeElement = useActiveElement()

    const handleChangeText = (e) => {
        setInputText(e.target.value)
        onTextChange(e.target.value)
    }

    const handleSelect = (e) => {
        const index = Number(e.target.id)
        setInputText(getName(options[index]))
        onSelect(options[index])
    }

    const [dropdownVisible, setDropdownVisible] = useState(false)

    useEffect(() => {
        setDropdownVisible(options.length > 0 && activeElement == textRef.current)
    }, [textRef, options, activeElement])

    return (
        <div>
            <input ref={textRef} name="input" type='text' data-form-field="input"
                className="form-control" placeholder={placeholder} value={inputText} onChange={handleChangeText}>

            </input>
            <div className='position-relative'>
                <div className='bg-light border p-2
                rounded w-100 top-100 
                d-flex flex-column position-absolute 
                ' style={{ zIndex: 1, visibility: dropdownVisible ? 'visible' : 'hidden' }}>
                    {
                        options.map((option, index) =>
                            <button key={index} id={index} className='btn btn-outline-dark border-0 btn-sm m-0'
                                onClick={handleSelect}>{getName(option)}</button>
                        )
                    }
                </div>
            </div>

        </div>
    )
}
