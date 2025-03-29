import React from "react";
import { Link, useNavigate } from "react-router-dom";
import '../assets/css/navbar-user.css';

const NavbarUser: React.FC = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    // Eliminar token y datos del usuario del localStorage
    localStorage.clear();

    // Redirigir al home
    navigate('/');

    // Recargar la página para limpiar cualquier estado de sesión
    window.location.reload();
  };

  return (
    <nav className="dashboard-navbar">
      <div className="dashboard-navbar-content">
        <span className="dashboard-title">Dashboard</span>
        <div className="dashboard-buttons">
          <Link to="/dashboard-user" className="dashboard-btn">
            Home
          </Link>
          <Link to="/perfil" className="dashboard-btn">
            Perfil
          </Link>
          <Link to="/vacantes-user" className="dashboard-btn">
            Vacantes
          </Link>
        </div>
        <button className="dashboard-logout" onClick={handleLogout}>Salir</button>
      </div>
    </nav>
  );
};

export default NavbarUser;
