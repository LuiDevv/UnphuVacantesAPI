import React from "react";
import { Link, useNavigate } from "react-router-dom";
import '../assets/css/navbar-user.css';

const NavbarUser: React.FC = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    // Eliminar token y cualquier información de usuario del localStorage
    localStorage.removeItem('token');
    localStorage.removeItem('userName');
    localStorage.removeItem('email');
    localStorage.removeItem('firstName');
    localStorage.removeItem('lastName');

    // Redirigir al home
    navigate('/');
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
