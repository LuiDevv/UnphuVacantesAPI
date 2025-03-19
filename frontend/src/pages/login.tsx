import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios"; // Importamos Axios
import "../assets/css/login.css"; // Asegúrate de que el path sea correcto

import { Container, Form, Button, Alert } from "react-bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";

const Login: React.FC = () => {
  const navigate = useNavigate();
  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [error, setError] = useState<string>("");

  const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setError("");

    try {
      const { data } = await axios.post("http://localhost:5283/api/account/login", {
        username,
        password,
      });

      if (!data.token) throw new Error("Error en la autenticación");

      localStorage.setItem("token", data.token);
      navigate("/home");
    } catch (error) {
      if (axios.isAxiosError(error) && error.response?.data?.message) {
        setError(error.response.data.message);
      } else {
        setError("Error al iniciar sesión");
      }
    }
  };

  return (
    <div className="login-container">
      <Container className="login-box">
        <h2 className="text-center">Iniciar Sesión</h2>

        {error && <Alert variant="danger">{error}</Alert>}

        <Form onSubmit={handleLogin}>
          <Form.Group className="mb-3" controlId="formBasicEmail">
            <Form.Label>Usuario</Form.Label>
            <Form.Control
              type="text"
              placeholder="Ingrese su usuario"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              required
            />
          </Form.Group>

          <Form.Group className="mb-3" controlId="formBasicPassword">
            <Form.Label>Contraseña</Form.Label>
            <Form.Control
              type="password"
              placeholder="Ingrese su contraseña"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </Form.Group>

          <Button variant="primary" type="submit" className="w-100">
            Iniciar Sesión
          </Button>
        </Form>
      </Container>
    </div>
  );
};

export default Login;
