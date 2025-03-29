// src/components/Sidebar.tsx
import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import '../assets/css/navbar.css';

const Sidebar: React.FC = () => {
  const navigate = useNavigate();

  // Lógica para cerrar sesión
  const handleLogout = () => {
    // Aquí puedes agregar la lógica de cierre de sesión, por ejemplo, eliminar el token del almacenamiento local.
    localStorage.removeItem('authToken');  // Suponiendo que usas localStorage para almacenar el token

    // Redirigir al Home después de cerrar sesión
    navigate('/');
  };

  return (
    <div className="sidebar">
      <h2 className="sidebar-title">UNPHU Vacantes | Empresa</h2>
      <ul className="sidebar-links">
        <li><Link to="/dashboard">Dashboard</Link></li>
        <li><Link to="/vacantes">Mis vacantes</Link></li>
        <li><Link to="/all-vacantes">Vacantes</Link></li>
        <li><Link to="/perfil-emp">Mi perfil</Link></li>
        
      </ul>
      {/* Botón de Cerrar Sesión */}
      <Button variant="danger" className="logout-button" onClick={handleLogout}>
        Cerrar Sesión
      </Button>
    </div>
  );
};

export default Sidebar;
