import { useContext, useState } from "react";
import { AuthContext } from "../Contexts/AuthContext";
import { useNavigate } from "react-router-dom";

export const useFetching = (callback) => {
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState('');
    const { dispatch, user } = useContext(AuthContext)
    const navigate = useNavigate()

    const fetching = async (...args) => {
        if (user == null) {
            navigate("/login")
        }
        else {
            try {
                setError('')
                setIsLoading(true)
                await callback(...args)
            } catch (e) {
                console.log(e)
                if (e.response?.status == 401) {
                    dispatch({ type: "LOGIN_FAILURE", payload: e.response.data.ErrorMessage })
                    navigate('/login')
                }
                else {
                    if (e.response) {
                        if (e.response.data) {
                            if (e.response.data.ErrorMessage) {
                                setError(e.response.data.ErrorMessage)
                            }
                            else {
                                setError(`Статус: ${e.response.data.status}. Ошибка: ${e.response.data.title}`)
                            }
                        }
                        else {
                            setError(`Статус: ${e.response.status}. Ошибка: ${e.response.statusText}`)
                        }
                    }
                    else {
                        if (e.message) {
                            setError(e.message)
                        }
                        else {
                            setError(e)
                        }
                    }
                }
            } finally {
                setIsLoading(false)
            }
        }
    }

    return [fetching, isLoading, error]
}