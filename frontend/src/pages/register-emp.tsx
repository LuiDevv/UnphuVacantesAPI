import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import '../assets/css/register-emp.css';
import { Link } from 'react-router-dom';
import questionIcon from '../icons/question.svg';

const RegisterEmp = () => {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [contactEmail, setContactEmail] = useState('');
    const [phone, setPhone] = useState('');
    const [location, setLocation] = useState('');
    const [rnc, setRnc] = useState('');
    const [symbol, setSymbol] = useState('');
    const [isApprovedByUNPHU, setIsApprovedByUNPHU] = useState(true);
    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const navigate = useNavigate();
    const [helpVisible, setHelpVisible] = useState(false);

    const handleRegister = async (e: React.FormEvent) => {
        e.preventDefault();

        if (!isApprovedByUNPHU) {
            setError("La empresa no está aprobada por la UNPHU. Por favor, contacta con la UNPHU para obtener la aprobación.");
            setSuccess("");
            return;
        }

        if (password !== confirmPassword) {
            setError("Las contraseñas no coinciden.");
            setSuccess("");
            return;
        }

        const data = {
            name,
            description,
            contactEmail,
            phone,
            location,
            rnc,
            symbol,
            isApprovedByUNPHU,
            password // <--- Incluye la contraseña aquí
        };

        try {
            await axios.post('http://localhost:5283/api/companies', data);
            setSuccess('Registro de empresa exitoso. Redirigiendo...');
            setError('');
            setTimeout(() => navigate('/company-login'), 2000);
        } catch (err) {
            setSuccess('');
            setError('Error en el registro. Verifica los datos ingresados.');
        }
    };

    return (
        <section className="vh-100">
            <div className="container py-5 h-100">
                <div className="row d-flex justify-content-center align-items-center h-100">
                    <div className="col col-xl-10">
                        <div className="login-card">
                            <div className="row g-0">
                                {/* FORMULARIO A LA IZQUIERDA */}
                                <div className="col-md-6 col-lg-7 d-flex align-items-center order-1 order-lg-2">
                                    <div className="card-body p-4 p-lg-5 text-black register-form">
                                        <form onSubmit={handleRegister}>
                                            <div className="d-flex align-items-center mb-3 pb-1">
                                                <i className="fas fa-building fa-2x me-3" style={{ color: '#ff6219' }}></i>
                                                <span className="h1 fw-bold mb-0">UNPHU | Registro de Empresa</span>
                                            </div>

                                            <h5 className="fw-normal mb-3 pb-3">Crea la cuenta de tu empresa</h5>

                                            {/* Sección de inputs divididos en columnas */}
                                            <div className="row">
                                                {/* Columna 1 */}
                                                <div className="col-12 col-md-6 mb-4">
                                                    <div className="form-outline">
                                                        <input
                                                            type="text"
                                                            id="name"
                                                            className="form-control form-control-lg"
                                                            value={name}
                                                            onChange={(e) => setName(e.target.value)}
                                                            required
                                                        />
                                                        <label className="form-label" htmlFor="name">
                                                            Nombre de la empresa
                                                        </label>
                                                    </div>

                                                    <div className="form-outline">
                                                        <input
                                                            type="text"
                                                            id="description"
                                                            className="form-control form-control-lg"
                                                            value={description}
                                                            onChange={(e) => setDescription(e.target.value)}
                                                            required
                                                        />
                                                        <label className="form-label" htmlFor="description">
                                                            Descripción de la empresa
                                                        </label>
                                                    </div>

                                                    <div className="form-outline">
                                                        <input
                                                            type="email"
                                                            id="contactEmail"
                                                            className="form-control form-control-lg"
                                                            value={contactEmail}
                                                            onChange={(e) => setContactEmail(e.target.value)}
                                                            required
                                                        />
                                                        <label className="form-label" htmlFor="contactEmail">
                                                            Correo electrónico de contacto
                                                        </label>
                                                    </div>

                                                    <div className="form-outline">
                                                        <input
                                                            type="text"
                                                            id="phone"
                                                            className="form-control form-control-lg"
                                                            value={phone}
                                                            onChange={(e) => setPhone(e.target.value)}
                                                            required
                                                        />
                                                        <label className="form-label" htmlFor="phone">
                                                            Teléfono
                                                        </label>
                                                    </div>

                                                    <div className="form-outline">
                                                        <input
                                                            type="text"
                                                            id="location"
                                                            className="form-control form-control-lg"
                                                            value={location}
                                                            onChange={(e) => setLocation(e.target.value)}
                                                            required
                                                        />
                                                        <label className="form-label" htmlFor="location">
                                                            Ubicación
                                                        </label>
                                                    </div>
                                                </div>

                                                {/* Columna 2 */}
                                                <div className="col-12 col-md-6 mb-4">
                                                    <div className="form-outline">
                                                        <input
                                                            type="text"
                                                            id="rnc"
                                                            className="form-control form-control-lg"
                                                            value={rnc}
                                                            onChange={(e) => setRnc(e.target.value)}
                                                            required
                                                        />
                                                        <label className="form-label" htmlFor="rnc">
                                                            RNC
                                                        </label>
                                                    </div>

                                                    <div className="form-outline">
                                                        <input
                                                            type="text"
                                                            id="symbol"
                                                            className="form-control form-control-lg"
                                                            value={symbol}
                                                            onChange={(e) => setSymbol(e.target.value)}
                                                            required
                                                        />
                                                        <label className="form-label" htmlFor="symbol">
                                                            Símbolo de la empresa
                                                        </label>
                                                        <span
                                                            className="help-icon"
                                                            onClick={() => setHelpVisible(!helpVisible)}
                                                            style={{ cursor: "pointer", marginLeft: "10px" }}
                                                        >
                                                            <img src={questionIcon} alt="Help icon" style={{ width: '30x', height: '30px' }} />

                                                        </span>
                                                        {helpVisible && (
                                                            <div className="help-message">
                                                                Si la empresa tiene más de 5 letras, el símbolo debe ser la abreviación del nombre. Ejemplo: Altice = ALTC
                                                            </div>
                                                        )}
                                                    </div>

                                                    <div className="form-outline">
                                                        <label className="form-label" htmlFor="isApprovedByUNPHU">
                                                            ¿Está aprobada por UNPHU?
                                                        </label>
                                                        <select
                                                            id="isApprovedByUNPHU"
                                                            className="form-control form-control-lg"
                                                            value={isApprovedByUNPHU ? 'true' : 'false'}
                                                            onChange={(e) => setIsApprovedByUNPHU(e.target.value === 'true')}
                                                            required
                                                        >
                                                            <option value="true">Sí</option>
                                                            <option value="false">No</option>
                                                        </select>
                                                    </div>

                                                    {/* Campos de contraseña */}
                                                    <div className="form-outline">
                                                        <input
                                                            type="password"
                                                            id="password"
                                                            className="form-control form-control-lg"
                                                            value={password}
                                                            onChange={(e) => setPassword(e.target.value)}
                                                            required
                                                        />
                                                        <label className="form-label" htmlFor="password">
                                                            Contraseña
                                                        </label>
                                                    </div>

                                                    <div className="form-outline">
                                                        <input
                                                            type="password"
                                                            id="confirmPassword"
                                                            className="form-control form-control-lg"
                                                            value={confirmPassword}
                                                            onChange={(e) => setConfirmPassword(e.target.value)}
                                                            required
                                                        />
                                                        <label className="form-label" htmlFor="confirmPassword">
                                                            Confirmar contraseña
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div className="pt-1 mb-4">
                                                <button className="btn btn-dark btn-lg btn-block" type="submit">
                                                    Registrarme
                                                </button>
                                            </div>

                                            <p className="mb-5 pb-lg-2" style={{ color: '#393f81' }}>
                                                ¿Ya tienes una cuenta de empresa?{' '}
                                                <Link to="/company-login" style={{ color: '#393f81' }}>
                                                    Inicia sesión aquí.
                                                </Link>
                                            </p>
                                        </form>
                                        {error && <p className="error-message">{error}</p>}
                                        {success && <p className="success-message">{success}</p>}
                                    </div>
                                </div>

                                {/* IMAGEN A LA DERECHA */}
                                <div className="col-md-6 col-lg-5 d-flex align-items-center order-2 order-lg-1">
                                    <img
                                        src="/images/empresa.jpg"
                                        alt="register form"
                                        className="img-fluid login-img"
                                    />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default RegisterEmp;
