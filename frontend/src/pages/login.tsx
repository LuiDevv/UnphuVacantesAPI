import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom'; // Importar useNavigate para el redireccionamiento
import '../assets/css/login.css'; // Asegúrate de que este archivo CSS esté configurado correctamente
import { Link } from 'react-router-dom';

const Login = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const navigate = useNavigate(); // Usar useNavigate para redirigir

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();

    // Prepara los datos para enviar a la API
    const data = {
      username,
      password
    };

    try {
      const response = await axios.post('http://localhost:5283/api/account/login', data);
      // Si el login es exitoso, muestra un mensaje y redirige al home
      setSuccess('Inicio de sesión exitoso');
      setError('');
      navigate('/home'); // Redirige a la página de inicio después de un inicio de sesión exitoso
    } catch (err) {
      // Si hay un error, muestra un mensaje de error
      setSuccess('');
      setError('Hubo un error al iniciar sesión. Verifica tus credenciales.');
    }
  };

  return (
    <section className="vh-100">
      <div className="container py-5 h-100">
        <div className="row d-flex justify-content-center align-items-center h-100">
          <div className="col col-xl-10">
            <div className="card" style={{ borderRadius: '1rem' }}>
              <div className="row g-0">
                <div className="col-md-6 col-lg-5 d-none d-md-block">
                  <img
                    src="/images/form-back.png"
                    alt="login form"
                    className="img-fluid login-img" 
                    style={{ borderRadius: '14.5px', width: '100%', height: '100%' }}
                  />
                </div>
                <div className="col-md-6 col-lg-7 d-flex align-items-center">
                  <div className="card-body p-4 p-lg-5 text-black">
                    <form onSubmit={handleLogin}>
                      <div className="d-flex align-items-center mb-3 pb-1">
                        <i className="fas fa-cubes fa-2x me-3" style={{ color: '#ff6219' }}></i>
                        <span className="h1 fw-bold mb-0">UNPHU | Vacantes</span>
                      </div>

                      <h5 className="fw-normal mb-3 pb-3" style={{ letterSpacing: '1px' }}>
                        Ingresa a tu cuenta
                      </h5>

                      <div data-mdb-input-init className="form-outline mb-4">
                        <input
                          type="text"
                          id="form2Example17"
                          className="form-control form-control-lg"
                          value={username}
                          onChange={(e) => setUsername(e.target.value)}
                          required
                        />
                        <label className="form-label" htmlFor="form2Example17">
                          Nombre de usuario
                        </label>
                      </div>

                      <div data-mdb-input-init className="form-outline mb-4">
                        <input
                          type="password"
                          id="form2Example27"
                          className="form-control form-control-lg"
                          value={password}
                          onChange={(e) => setPassword(e.target.value)}
                          required
                        />
                        <label className="form-label" htmlFor="form2Example27">
                          Contraseña
                        </label>
                      </div>

                      <div className="pt-1 mb-4">
                        <button
                          data-mdb-button-init
                          data-mdb-ripple-init
                          className="btn btn-dark btn-lg btn-block"
                          type="submit"
                        >
                          Login
                        </button>
                      </div>

                      <p className="mb-5 pb-lg-2" style={{ color: '#393f81' }}>
                      ¿No tienes una cuenta?{' '}
                        <Link to="/register" style={{ color: '#393f81' }}>
                          Regístrate aquí.
                        </Link>
                      </p>

                      
                      
                    </form>
                    {error && <p className="error-message">{error}</p>}
                    {success && <p className="success-message">{success}</p>}
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Login;
