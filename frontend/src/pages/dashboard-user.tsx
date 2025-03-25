// DashboardUser.tsx
import { useState } from "react";
import NavBar from "../pages/navbar-user"; // Asegúrate de que el path sea correcto
import VacantesUser from "../pages/vacantesuser"; // Importa el nuevo componente VacantesUser

const DashboardUser: React.FC = () => {
  const [activeTab, setActiveTab] = useState("home");

  // Función para manejar el cambio de tab
  const handleTabClick = (tab: string) => {
    setActiveTab(tab); // Cambiar la pestaña activa
  };

  return (
    <div className="dashboard-container">
      {/* Barra de navegación */}
      <NavBar />

      {/* Contenido dinámico */}
      <div className="dashboard-content">
        {activeTab === "home" && (
          <div className="dashboard-card">
            <h2>Bienvenido al Dashboard</h2>
            <p>Aquí puedes acceder a tus opciones de usuario.</p>
          </div>
        )}

        {activeTab === "profile" && (
          <div className="dashboard-card">
            <h2>Perfil</h2>
            <p>Información del usuario.</p>
          </div>
        )}

        {activeTab === "vacancies" && <VacantesUser />} {/* Aquí usamos el componente VacantesUser */}

      </div>
    </div>
  );
};

export default DashboardUser;
