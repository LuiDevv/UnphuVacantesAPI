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

      {/* Sección de Características */}
      <section className="features-section py-5">
        <Container>
          <h2 className="text-center mb-5">¿Por qué elegir UNPHU Vacantes?</h2>
          <Row className="align-items-center mb-5">
            <Col md={6} className="text-center">
              <img
                src="/images/oportunidad.png"
                alt="Oportunidades"
                className="img-fluid rounded shadow"
              />
            </Col>
            <Col md={6}>
              <h4>Oportunidades Laborales</h4>
              <p>
                Accede a miles de ofertas de trabajo en diferentes sectores y áreas profesionales.
              </p>
            </Col>
          </Row>
          <Row className="align-items-center mb-5">
            <Col md={6}>
              <h4>Conéctate con Empresas</h4>
              <p>
                Encuentra empleadores que buscan talento como el tuyo.
              </p>
            </Col>
            <Col md={6} className="text-center">
              <img
                src="/images/crecimiento.png"
                alt="Conexión"
                className="img-fluid rounded shadow"
              />
            </Col>
          </Row>
          <Row className="align-items-center">
            <Col md={6} className="text-center">
              <img
                src="/images/conexion.jpg"
                alt="Crecimiento"
                className="img-fluid rounded shadow"
              />
            </Col>
            <Col md={6}>
              <h4>Crecimiento Profesional</h4>
              <p>
                Desarrolla tu carrera con oportunidades de formación y desarrollo.
              </p>
            </Col>
          </Row>
        </Container>
      </section>

      {/* Sección de Testimonios */}
      <section className="testimonials-section py-5 bg-light">
        <Container>
          <h2 className="text-center mb-5">Lo que dicen nuestros usuarios</h2>
          <Row>
            <Col md={4} className="mb-4">
              <Card className="p-4 shadow-sm border-0">
                <Card.Body>
                  <Card.Text>
                    “Gracias a UNPHU Vacantes conseguí el empleo de mis sueños en una semana.”
                  </Card.Text>
                  <Card.Footer className="text-muted">- María Gómez</Card.Footer>
                </Card.Body>
              </Card>
            </Col>
            <Col md={4} className="mb-4">
              <Card className="p-4 shadow-sm border-0">
                <Card.Body>
                  <Card.Text>
                    “Me encantó lo fácil que fue conectar con empresas y postularme.”
                  </Card.Text>
                  <Card.Footer className="text-muted">- Juan Pérez</Card.Footer>
                </Card.Body>
              </Card>
            </Col>
            <Col md={4} className="mb-4">
              <Card className="p-4 shadow-sm border-0">
                <Card.Body>
                  <Card.Text>
                    “El proceso de aplicación es intuitivo y rápido, ¡recomendado al 100%!”
                  </Card.Text>
                  <Card.Footer className="text-muted">- Ana Rodríguez</Card.Footer>
                </Card.Body>
              </Card>
            </Col>
          </Row>
        </Container>
      </section>

      {/* Sección de CTA para Empresas */}
      <section className="cta-section py-5 text-white text-center">
        <Container>
          <h2 className="mb-4">¿Eres una empresa?</h2>
          <p className="mb-4">
            Publica tus vacantes y encuentra el mejor talento para tu equipo.
          </p>
          <Button
            variant="light"
            size="lg"
            onClick={() => navigate("/register-emp")}
          >
            Regístrate como Empresa
          </Button>
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
