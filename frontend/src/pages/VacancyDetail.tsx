// src/pages/VacancyDetailPage.tsx
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import NavbarUser from "../pages/navbar-user"; // Importamos el NavbarUser
import "../assets/css/vacancydetail.css";

const VacancyDetailPage: React.FC = () => {
  const { id } = useParams<{ id: string }>(); // Obtener el ID de la vacante de la URL
  const [vacancy, setVacancy] = useState<any | null>(null);

  useEffect(() => {
    fetch(`http://localhost:5283/api/Vacant/${id}`)
      .then((response) => response.json())
      .then((data) => setVacancy(data))
      .catch((error) => console.error("Error fetching vacancy details:", error));
  }, [id]);

  const [cv, setCv] = useState<File | null>(null);

  const handleCvUpload = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files[0]) {
      setCv(e.target.files[0]);
    }
  };

  const handleApply = () => {
    if (cv) {
      alert(`CV ${cv.name} subido exitosamente para la vacante: ${vacancy?.title}`);
    } else {
      alert("Por favor, sube tu CV antes de aplicar.");
    }
  };

  return (
    <div className="vacancy-detail-page">
      <NavbarUser /> {/* Usamos el NavbarUser */}
      {vacancy ? (
        <div className="vacancy-detail-card">
          <h3>{vacancy.title}</h3>
          <p><strong>Descripción Completa:</strong> {vacancy.description}</p>
          <p><strong>Modalidad:</strong> {vacancy.modality}</p>
          <p><strong>Salario:</strong> {vacancy.salary}</p>
          <p><strong>Estado:</strong> {vacancy.status ? 'Activa' : 'Inactiva'}</p>

          <input type="file" onChange={handleCvUpload} />
          <button onClick={handleApply}>Aplicar</button>
        </div>
      ) : (
        <p>Cargando detalles de la vacante...</p>
      )}
    </div>
  );
};

export default VacancyDetailPage;
