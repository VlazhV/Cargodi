import React, { useContext, useState } from 'react';
import { AuthContext } from "../Contexts/AuthContext";
import AuthService from "../Services/AuthService";
import { useNavigate } from "react-router-dom";
import userRoles from '../Data/UserRoles.json';
import AutoparkEdit from '../Components/AutoparkEdit';

function RegisterOtherPage(props) {
    const [credentials, setCredentials] = useState({
        userName: '',
        role: 'admin',
        phoneNumber: '',
        email: '',
        updateClientDto:{
            name:'',
        } ,
        updateDriverDto:{
            license:'', 
            secondName:'',
            firstName:'', 
            middleName:'', 
            autoparkId:null, 
            actualAutoparkId:null 
        } ,
        updateOperatorDto:{
            secondName:'',
            firstName:'' ,
            middleName:'' ,
            autoparkId:null ,
        }
    })

    const checkCredentials = () =>{
       const c1 = credentials.email && credentials.userName && credentials.phoneNumber && credentials.role;

       if(!c1){
        return false;
       }

       const c2 = credentials.updateClientDto && !credentials.updateDriverDto && !credentials.updateOperatorDto;

       if(c2){
            if(credentials.updateClientDto.name){
                return true;
            }
       }

       const c3 =!credentials.updateClientDto && credentials.updateDriverDto && !credentials.updateOperatorDto;

       if(c3){
            if( credentials.updateDriverDto.actualAutoparkId &&
                credentials.updateDriverDto.autoparkId &&
                credentials.updateDriverDto.firstName &&
                credentials.updateDriverDto.license &&
                credentials.updateDriverDto.middleName &&
                credentials.updateDriverDto.secondName
            ){
                return true;
            }
       }

       const c4 = !credentials.updateClientDto && !credentials.updateDriverDto && credentials.updateOperatorDto;

       if(c4){
            if( credentials.updateOperatorDto.autoparkId &&
                credentials.updateOperatorDto.firstName &&
                credentials.updateOperatorDto.middleName &&
                credentials.updateOperatorDto.secondName
            ){
                return true;
            }
       }

       return false;
    }

    const { loading, error, dispatch } = useContext(AuthContext)

    const navigate = useNavigate()

    const handleChange = (e) => {
        setCredentials(prev => ({ ...prev, [e.target.id]: e.target.value }))
    }

    const handleClientChange = (e) => {
        setCredentials(prev => ({ ...prev, updateClientDto: {...prev.updateClientDto, [e.target.id]: e.target.value } }))
    }

    const handleDriverChange = (e) => {
        setCredentials(prev => ({ ...prev, updateDriverDto: {...prev.updateDriverDto, [e.target.id]: e.target.value } }))
    }

    const handleOperatorChange = (e) => {
        setCredentials(prev => ({ ...prev, updateOperatorDto: {...prev.updateOperatorDto, [e.target.id]: e.target.value } }))
    }

    const changeRole = (role)=>{
        switch(role){
            case "manager":
            case "admin" :
                credentials.updateClientDto = null
                credentials.updateDriverDto = null
                credentials.updateOperatorDto = {
                    secondName:'',
                    firstName:'' ,
                    middleName:'' ,
                    autoparkId:null ,
                }
            break;
            case "driver": 
                credentials.updateClientDto = null
                credentials.updateDriverDto = {
                    license:'', 
                    secondName:'',
                    firstName:'', 
                    middleName:'', 
                    autoparkId:null, 
                    actualAutoparkId:null 
                }
                credentials.updateOperatorDto = null
            break;
            case "client":
                credentials.updateClientDto = {
                    name:'',
                }
                credentials.updateDriverDto = null
                credentials.updateOperatorDto = null
            break;
        }
        setCredentials(prev => ({ ...prev }))
    }

    let role = credentials.role;
    

    const handleChangeRole = (e) => {
        role = e.target.value
        changeRole(role)
        handleChange(e)   
    }

    const handleOperatorAutoparkChange = (newAutoparkId) => {
        setCredentials(prev => ({ ...prev, updateOperatorDto: {...prev.updateOperatorDto, autoparkId: newAutoparkId } }))
        //fetch("updateAutopark", newAutoparkId)
    }

    const handleDriverAutoparkChange = (newAutoparkId) => {
        setCredentials(prev => ({ ...prev, updateDriverDto: {...prev.updateDriverDto, autoparkId: newAutoparkId } }))
        //fetch("updateAutopark", newAutoparkId)
    }

    const handleActualAutoparkChange = (newAutoparkId) => {
        setCredentials(prev => ({ ...prev, updateDriverDto: {...prev.updateDriverDto, actualAutoparkId: newAutoparkId } }))
        //fetch("updateActualAutopark", newAutoparkId)
    }

    const handleClick = async (e) => {
        e.preventDefault()
        console.log(credentials)
        if (checkCredentials()) {
            try {
                console.log(credentials)
                const res = await AuthService.Register(credentials)
                navigate("/")
            } catch (err) {
                console.log(err)
            }
        }
    }

    const switchRole = (role) =>{
        switch(role){
            case "manager":
            case "admin" :
                return(
                    <div>

                        <div className="col-12 form-group mb-3" data-for="textarea">
                            <input name="input" placeholder="Фамилия" type="text" data-form-field="input"
                                className="form-control" id="secondName" onChange={handleOperatorChange}></input>
                        </div>

                        <div className="col-12 form-group mb-3" data-for="textarea">
                            <input name="input" placeholder="Имя" type="text" data-form-field="input"
                                className="form-control" id="firstName" onChange={handleOperatorChange}></input>
                        </div>

                        <div className="col-12 form-group mb-3" data-for="textarea">
                            <input name="input" placeholder="Отчество" type="text" data-form-field="input"
                                className="form-control" id="middleName" onChange={handleOperatorChange}></input>
                        </div>

                        <button type="button" className="btn btn-primary display-3"
                            data-bs-toggle="modal" data-bs-target="#carAutoparkId">Назначить прописку</button>
             
                        <AutoparkEdit id="carAutoparkId" onSelectAutopark={handleOperatorAutoparkChange} />
                    </div>
                )
            case "driver":
                return(
                    
                    <div>
                        <div className="col-12 form-group mb-3" data-for="textarea">
                            <input name="input" placeholder="Лицензия" type="text" data-form-field="input"
                                className="form-control" id="license" onChange={handleDriverChange}></input>
                        </div>

                        <div className="col-12 form-group mb-3" data-for="textarea">
                            <input name="input" placeholder="Фамилия" type="text" data-form-field="input"
                                className="form-control" id="secondName" onChange={handleDriverChange}></input>
                        </div>

                        <div className="col-12 form-group mb-3" data-for="textarea">
                            <input name="input" placeholder="Имя" type="text" data-form-field="input"
                                className="form-control" id="firstName" onChange={handleDriverChange}></input>
                        </div>

                        <div className="col-12 form-group mb-3" data-for="textarea">
                            <input name="input" placeholder="Отчество" type="text" data-form-field="input"
                                className="form-control" id="middleName" onChange={handleDriverChange}></input>
                        </div>

                        <button type="button" className="btn btn-primary display-3"
                        data-bs-toggle="modal" data-bs-target="#carAutoparkId">Назначить прописку</button>
                        <button type="button" className="btn btn-primary display-3"
                            data-bs-toggle="modal" data-bs-target="#carActualAutoparkId">Назначить текущий автопарк</button>
             
                        <AutoparkEdit id="carAutoparkId" onSelectAutopark={handleDriverAutoparkChange} />
                        <AutoparkEdit id="carActualAutoparkId" onSelectAutopark={handleActualAutoparkChange} />
                    </div>
                )
            case "client":
                return(
                    <div className="col-12 form-group mb-3" data-for="textarea">
                        <input name="input" placeholder="Имя" type="text" data-form-field="input"
                            className="form-control" id="name" onChange={handleClientChange}></input>
                    </div>
                )
        }
    }


    return (
        <div>
            <section data-bs-version="5.1" className="form5 cid-ud3KesHTYC" id="contact-form-2-ud3KesHTYC">


                <div className="container">
                    <div className="row justify-content-center">
                        <div className="col-12 content-head">
                            <div className="mbr-section-head mb-5 mt-5">
                                <h3 className="mbr-section-title mbr-fonts-style align-center mb-0 display-2">
                                    <strong>Регистрация стороннего пользователя</strong>
                                </h3>

                            </div>
                        </div>
                    </div>
                    <div className="row justify-content-center">
                        <div className="col-lg-8 mx-auto mbr-form" data-form-type="formoid">
                            <form className="mbr-form form-with-styler"
                                data-form-title="Form Name"><input type="hidden" name="email" data-form-email="true"
                                    value="Ky1MAY5B1zYpUkuQChRmHcefA594xSl1d4+lCVG/6tGeUasfRT25pdpYagbLk3yLlZsKdIhi2GKQ3VFtT+pZ5GAPn22ntoC/7vSCC672JeZx+w/sbr+Bn4RWWFA3Hsux" />
                                <div className="dragArea row">
                                    <div className="col-12 form-group mb-3" data-for="textarea">
                                        <input name="input" placeholder="Почта" type="email" data-form-field="input"
                                            className="form-control" id="email" onChange={handleChange}></input>
                                    </div>

                                    <div className="col-12 form-group mb-3" data-for="textarea">
                                        <input name="input" placeholder="Никнейм" type='text' data-form-field="input"
                                            className="form-control" id="userName" onChange={handleChange}></input>
                                    </div>

                                    <div className="col-12 form-group mb-3" data-for="input">
                                        <input name="input" placeholder="Номер телефона" data-form-field="input"
                                            className="form-control" id="phoneNumber" onChange={handleChange}></input>
                                    </div>

                                    <div className="col-12 form-group mb-3" data-for="input">

                                        <select name="select" className='form-select display-7 p-3' onChange={handleChangeRole} id="role">
                                            {
                                                userRoles.map(role => <option value={role.value} key={role.value}>{role.name}</option>)
                                            }
                                        </select>
                                    </div>

                                    <div>
                                        {
                                            switchRole(role)
                                        }
                                    </div>

                                    <div className="col-lg-12 col-md-12 col-sm-12 align-center mbr-section-btn">
                                        <button className="btn btn-primary display-7" onClick={handleClick}>Зарегестрировать</button>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </section>

            {error &&
                <div className="border border-danger border rounded-4 p-2 px-4 mt-2">
                    <span className="text-danger text-center h3">{typeof error === 'string' ? error : toString(error)}</span>
                </div>}
        </div >
    );
}

export default RegisterOtherPage;