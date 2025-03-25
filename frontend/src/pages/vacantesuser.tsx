// VacantesUser.tsx
import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import "../assets/css/vacantes-user.css"; // Asegúrate de que el CSS esté bien configurado
import NavBar from "../pages/navbar-user"; // Importar el NavbarUser

const VacantesUser: React.FC = () => {
  const [vacancies, setVacancies] = useState<any[]>([]);

  useEffect(() => {
    fetch("http://localhost:5283/api/Vacant")
      .then((response) => response.json())
      .then((data) => setVacancies(data))
      .catch((error) => console.error("Error fetching vacancies:", error));
  }, []);

  return (
    <div className="vacancies-container">
      {/* Aquí rendereamos el NavbarUser */}
      <NavBar />

      <h2>Vacantes Disponibles</h2>
      <div className="vacancy-grid">
        {vacancies.length > 0 ? (
          vacancies.map((vacant, index) => (
            <div key={index} className="vacancy-card">
              <h3>{vacant.title}</h3>
              <p><strong>Descripción: </strong> {vacant.description.slice(0, 100)}...</p>
              <Link to={`/Vacant/${vacant.id}`} className="view-more-btn">Ver más</Link>
            </div>
          ))
        ) : (
          <p>No hay vacantes disponibles.</p>
        )}
      </div>
    </div>
  );
};

export default VacantesUser;
