import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom'; // Importa useNavigate para la redirección
import axios from 'axios'; // Importa axios
import '../assets/css/register.css'; // Asegúrate de que el archivo CSS esté configurado

const Register = () => {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [repeatPassword, setRepeatPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState(''); // Para mostrar los errores
  const [loading, setLoading] = useState(false); // Para mostrar el estado de carga

  // Redirección después del registro
  const navigate = useNavigate();

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault();

    // Lógica para manejar el registro
    if (password !== repeatPassword) {
      alert('Las contraseñas no coinciden');
      return;
    }

    setLoading(true); // Activa el estado de carga
    setErrorMessage(''); // Limpia cualquier mensaje de error previo

    try {
      const response = await axios.post('http://localhost:5283/api/account/register', {
        username: name, // Se usa 'username' en lugar de 'name' para que coincida con el backend
        email: email,
        password: password,
      });

      // Verificamos si la respuesta fue exitosa
      if (response.status === 200) {
        alert('¡Registro exitoso! Ahora puedes iniciar sesión.');
        navigate('/login'); // Redirige a la página de inicio de sesión
      } else {
        setErrorMessage('Error al registrar el usuario.');
      }
    } catch (error: any) {
      // Maneja los errores de la API
      if (error.response) {
        // Si hay respuesta de error desde la API
        setErrorMessage(error.response.data.errors || error.response.data || 'Error desconocido');
      } else {
        // Si no hay respuesta (problema con la red, por ejemplo)
        setErrorMessage('Hubo un problema con la solicitud, por favor inténtalo nuevamente.');
      }
    } finally {
      setLoading(false); // Desactiva el estado de carga
    }
  };

  return (
    <section className="vh-100" style={{ backgroundColor: '#eee' }}>
      <div className="container h-100">
        <div className="row d-flex justify-content-center align-items-center h-100">
          <div className="col-lg-12 col-xl-11">
            <div className="card text-black" style={{ borderRadius: '25px' }}>
              <div className="card-body p-md-5">
                <div className="row justify-content-center">
                  <div className="col-md-10 col-lg-6 col-xl-5 order-2 order-lg-1">
                    <p className="text-center h1 fw-bold mb-5 mx-1 mx-md-4 mt-4">Registro</p>

                    <form className="mx-1 mx-md-4" onSubmit={handleRegister}>
                      {/* Nombre */}
                      <div className="d-flex flex-row align-items-center mb-4">
                        <i className="fas fa-user fa-lg me-3 fa-fw"></i>
                        <div className="form-outline flex-fill mb-0">
                          <input
                            type="text"
                            id="form3Example1c"
                            className="form-control"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            required
                          />
                          <label className="form-label" htmlFor="form3Example1c">
                            Tu matrícula
                          </label>
                        </div>
                      </div>

                      {/* Correo */}
                      <div className="d-flex flex-row align-items-center mb-4">
                        <i className="fas fa-envelope fa-lg me-3 fa-fw"></i>
                        <div className="form-outline flex-fill mb-0">
                          <input
                            type="email"
                            id="form3Example3c"
                            className="form-control"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                          />
                          <label className="form-label" htmlFor="form3Example3c">
                            Tu correo
                          </label>
                        </div>
                      </div>

                      {/* Contraseña */}
                      <div className="d-flex flex-row align-items-center mb-4">
                        <i className="fas fa-lock fa-lg me-3 fa-fw"></i>
                        <div className="form-outline flex-fill mb-0">
                          <input
                            type="password"
                            id="form3Example4c"
                            className="form-control"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                          />
                          <label className="form-label" htmlFor="form3Example4c">
                            Contraseña
                          </label>
                        </div>
                      </div>

                      {/* Repetir contraseña */}
                      <div className="d-flex flex-row align-items-center mb-4">
                        <i className="fas fa-key fa-lg me-3 fa-fw"></i>
                        <div className="form-outline flex-fill mb-0">
                          <input
                            type="password"
                            id="form3Example4cd"
                            className="form-control"
                            value={repeatPassword}
                            onChange={(e) => setRepeatPassword(e.target.value)}
                            required
                          />
                          <label className="form-label" htmlFor="form3Example4cd">
                            Repite tu contraseña
                          </label>
                        </div>
                      </div>

                      {/* Mensaje de error */}
                      {errorMessage && <div className="alert alert-danger">{errorMessage}</div>}

                      {/* Botón de registro */}
                      <div className="d-flex justify-content-center mx-4 mb-3 mb-lg-4">
                        <button
                          type="submit"
                          className="btn btn-primary btn-lg"
                          disabled={loading} // Desactiva el botón mientras está cargando
                        >
                          {loading ? 'Registrando...' : 'Registrarme'}
                        </button>
                      </div>
                    </form>
                  </div>

                  {/* Imagen */}
                  <div className="col-md-10 col-lg-6 col-xl-7 d-flex align-items-center order-1 order-lg-2">
                    <img
                      src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-registration/draw1.webp"
                      className="img-fluid"
                      alt="Sample image"
                    />
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

export default Register;
