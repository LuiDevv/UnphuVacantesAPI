import React from "react";
import { Navbar, Nav, Container } from "react-bootstrap";
import "../assets/css/navbar-home.css"; // Verifica que este archivo exista y esté cargado correctamente

const NavbarHome: React.FC = () => {
  return (
    <Navbar className="unphu-navbar" bg="light" expand="lg">
      <Container>
        <Navbar.Brand className="unphu-brand" href="/">UNPHU Vacantes</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="ms-auto">
          <Nav.Link className="nav-link-emp" href="/company-login">Inicia sesion como empresa</Nav.Link>
            <Nav.Link className="nav-link-emp" href="/register-emp">Registrate como empresa</Nav.Link>
            <Nav.Link className="nav-link-login" href="/login">Iniciar Sesión</Nav.Link>
            <Nav.Link className="nav-link-register" href="/register">Registrarse</Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default NavbarHome;
