import React from "react";
import { useNavigate } from "react-router-dom"; // Importa useNavigate
import "bootstrap/dist/css/bootstrap.min.css";
import { Navbar, Nav, Container, Button, Carousel, Row, Col } from "react-bootstrap";
import "../assets/css/unphu.css";

const Home: React.FC = () => {
  const navigate = useNavigate(); // Hook para la navegación

  return (
    <>
      {/* Navbar */}
      <Navbar variant="dark" expand="lg" className="navbar-custom">
        <Container>
          <Navbar.Brand href="#">UNPHU Vacantes</Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="ms-auto">
              <Button variant="outline-light" className="me-2" onClick={() => navigate("/login")}>
                Iniciar Sesión
              </Button>
              <Button variant="primary" onClick={() => navigate("/register")}>
                Registrarse
              </Button>
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>

      {/* Sección principal con Grid */}
      <Container className="mt-5">
        <Row className="align-items-center">
          {/* Carrusel */}
          <Col md={6} lg={5}>
            <Carousel>
              <Carousel.Item>
                <img className="d-block w-100" src="/images/unphu-1.jpeg" alt="First slide" />
                <Carousel.Caption>
                  <h3>Encuentra tu empleo ideal</h3>
                  <p>Conéctate con empresas líderes en la UNPHU.</p>
                </Carousel.Caption>
              </Carousel.Item>
              <Carousel.Item>
                <img className="d-block w-100" src="/images/unphu-2.jpg" alt="Second slide" />
                <Carousel.Caption>
                  <h3>Impulsa tu carrera</h3>
                  <p>Aplica a oportunidades que se ajusten a tu perfil.</p>
                </Carousel.Caption>
              </Carousel.Item>
            </Carousel>
          </Col>

          {/* Sección de información */}
          <Col md={6} lg={7}>
            <div className="info-box">
              <h2 className="text-center">¿Cómo funciona UNPHU Vacantes?</h2>
              <p className="text-center">
                Conéctate con empresas, aplica a empleos y haz crecer tu carrera profesional.
              </p>
            </div>
          </Col>
        </Row>
      </Container>
    </>
  );
};

export default Home;
