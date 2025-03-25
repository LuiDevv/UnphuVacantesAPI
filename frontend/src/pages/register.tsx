import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import '../assets/css/register.css';
import { Link } from 'react-router-dom';

const Register = () => {
  const [matricula, setMatricula] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const [isLoading, setIsLoading] = useState(false); // Estado para el indicador de carga
  const navigate = useNavigate();

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault();

    if (password !== confirmPassword) {
      setError('Las contraseñas no coinciden.');
      setSuccess('');
      return;
    }

    const data = {
      matricula,
      firstName,
      lastName,
      username,
      email,
      password,
    };

    setIsLoading(true); // Activar el indicador de carga
    setError(''); // Limpiar errores previos

    try {
      await axios.post('http://localhost:5283/api/account/register', data);
      setSuccess('Registro exitoso. Redirigiendo al login...');
      setTimeout(() => navigate('/login'), 2000); // Redirigir después de 2 segundos
    } catch (err) {
      setSuccess('');
      setError('Error en el registro. Verifica los datos ingresados.');
    } finally {
      setIsLoading(false); // Desactivar el indicador de carga
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
                  <div className="card-body p-4 p-lg-5 text-black">
                    <form onSubmit={handleRegister}>
                      <div className="d-flex align-items-center mb-3 pb-1">
                        <i className="fas fa-cubes fa-2x me-3" style={{ color: '#ff6219' }}></i>
                        <span className="h1 fw-bold mb-0">UNPHU | Registro</span>
                      </div>

                      <h5 className="fw-normal mb-3 pb-3">Crea tu cuenta</h5>

                      {/* GRID LAYOUT */}
                      <div
                        style={{
                          display: 'grid',
                          gridTemplateColumns: '1fr 1fr',
                          gap: '20px',
                        }}
                      >
                        {/* Primera columna */}
                        <div>
                          <div className="form-outline mb-4">
                            <input
                              type="text"
                              id="matricula"
                              className="form-control form-control-lg"
                              value={matricula}
                              onChange={(e) => setMatricula(e.target.value)}
                              required
                            />
                            <label className="form-label" htmlFor="matricula">
                              Matrícula
                            </label>
                          </div>

                          <div className="form-outline mb-4">
                            <input
                              type="text"
                              id="firstName"
                              className="form-control form-control-lg"
                              value={firstName}
                              onChange={(e) => setFirstName(e.target.value)}
                              required
                            />
                            <label className="form-label" htmlFor="firstName">
                              Primer Nombre
                            </label>
                          </div>

                          <div className="form-outline mb-4">
                            <input
                              type="text"
                              id="lastName"
                              className="form-control form-control-lg"
                              value={lastName}
                              onChange={(e) => setLastName(e.target.value)}
                              required
                            />
                            <label className="form-label" htmlFor="lastName">
                              Apellido
                            </label>
                          </div>

                          <div className="form-outline mb-4">
                            <input
                              type="text"
                              id="username"
                              className="form-control form-control-lg"
                              value={username}
                              onChange={(e) => setUsername(e.target.value)}
                              required
                            />
                            <label className="form-label" htmlFor="username">
                              Nombre de usuario
                            </label>
                          </div>
                        </div>

                        {/* Segunda columna */}
                        <div>
                          <div className="form-outline mb-4">
                            <input
                              type="email"
                              id="email"
                              className="form-control form-control-lg"
                              value={email}
                              onChange={(e) => setEmail(e.target.value)}
                              required
                            />
                            <label className="form-label" htmlFor="email">
                              Correo electrónico
                            </label>
                          </div>

                          <div className="form-outline mb-4">
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

                          <div className="form-outline mb-4">
                            <input
                              type="password"
                              id="confirmPassword"
                              className="form-control form-control-lg"
                              value={confirmPassword}
                              onChange={(e) => setConfirmPassword(e.target.value)}
                              required
                            />
                            <label className="form-label" htmlFor="confirmPassword">
                              Repite tu contraseña
                            </label>
                          </div>
                        </div>
                      </div>

                      {/* Botón de registro */}
                      <div className="pt-1 mb-4">
                        <button
                          className="btn btn-dark btn-lg btn-block"
                          type="submit"
                          disabled={isLoading} // Deshabilitar el botón durante la carga
                        >
                          {isLoading ? (
                            <>
                              <span
                                className="spinner-border spinner-border-sm"
                                role="status"
                                aria-hidden="true"
                              ></span>{' '}
                              Cargando...
                            </>
                          ) : (
                            'Registrarme'
                          )}
                        </button>
                      </div>

                      {/* Mensajes de éxito y error */}
                      {success && (
                        <div className="alert alert-success d-flex align-items-center" role="alert">
                          <i className="fas fa-check-circle me-2"></i>
                          {success}
                        </div>
                      )}
                      {error && (
                        <div className="alert alert-danger d-flex align-items-center" role="alert">
                          <i className="fas fa-exclamation-circle me-2"></i>
                          {error}
                        </div>
                      )}

                      {/* Enlaces adicionales */}
                      <p className="mb-5 pb-lg-2" style={{ color: '#393f81' }}>
                        ¿Ya tienes una cuenta?{' '}
                        <Link to="/login" style={{ color: '#393f81' }}>
                          Inicia sesión aquí.
                        </Link>
                      </p>

                      {/* Nuevo Link */}
                      <p className="mb-5 pb-lg-2" style={{ color: '#393f81' }}>
                        ¿Eres una empresa?{' '}
                        <Link to="/register-emp" style={{ color: '#393f81' }}>
                          ¡Regístrate aquí!
                        </Link>
                      </p>
                    </form>
                  </div>
                </div>

                {/* IMAGEN A LA DERECHA */}
                <div className="col-md-6 col-lg-5 d-flex align-items-center order-2 order-lg-1">
                  <img
                    src="/images/register-img.jpg"
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

export default Register;
