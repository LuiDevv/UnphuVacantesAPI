import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Container, Button, Row, Col, Card } from "react-bootstrap";
import "../assets/css/unphu.css";
import { useNavigate } from "react-router-dom";
import NavbarHome from "./navbar-home";

const Home: React.FC = () => {
  const navigate = useNavigate();

  return (
    <>
      {/* Navbar */}
      <NavbarHome />

      {/* Hero Section */}
      <section className="hero-section d-flex align-items-center text-white text-center text-md-start">
        <Container>
          <Row className="align-items-center">
            <Col md={6}>
              <h1 className="fw-bold mb-4">Encuentra el trabajo ideal con UNPHU Vacantes</h1>
              <p className="lead mb-4">
                Conéctate con las mejores empresas y comienza a construir tu futuro profesional hoy mismo.
              </p>
              <Button
                variant="warning"
                size="lg"
                onClick={() => navigate("/register")}
                className="me-3"
              >
                ¡Regístrate Ahora!
              </Button>
              <Button
                variant="outline-light"
                size="lg"
                onClick={() => navigate("/login")}
              >
                Iniciar Sesión
              </Button>
            </Col>
            <Col md={6} className="text-center">
              <img
                src="/images/trabajo.jpg"
                alt="Trabajo"
                className="img-fluid rounded shadow"
              />
            </Col>
          </Row>
        </Container>
      </section>
      


      

      

      {/* Footer */}
      <footer className="py-4 bg-dark text-white text-center">
        <Container>
          <p className="mb-0">© 2025 UNPHU Vacantes. Todos los derechos reservados.</p>
        </Container>
      </footer>
    </>
  );
};

export default Home;
