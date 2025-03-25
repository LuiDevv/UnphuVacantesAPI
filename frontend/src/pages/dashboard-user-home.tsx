import React, { useState, useEffect } from "react";
import axios from "axios";
import NavBar from "../pages/navbar-user";
import '../assets/css/dashboard-user-home.css';

const DashboardUserHome: React.FC = () => {
  const [user, setUser] = useState<{ firstName: string } | null>(null);
  const [companies, setCompanies] = useState<{ rnc: string; name: string; description: string; contactEmail: string; phone: string; location: string }[]>([]);
  const [ads, setAds] = useState<string[]>([]);
  const [testimonials, setTestimonials] = useState<
    { name: string; role: string; text: string }[]
  >([]);
  const [stats, setStats] = useState<{ jobsPosted: number; usersActive: number }>({
    jobsPosted: 0,
    usersActive: 0,
  });

  const token = localStorage.getItem("token");
  const axiosConfig = { headers: { Authorization: `Bearer ${token}` } };

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const response = await axios.get("http://localhost:5283/api/account/current-user", axiosConfig);
        setUser(response.data);
      } catch (error) {
        console.error("Error al obtener la información del usuario:", error);
      }
    };

    const fetchCompanies = async () => {
      try {
        const response = await axios.get("http://localhost:5283/api/Companies", axiosConfig);
        setCompanies(response.data);
      } catch (error) {
        console.error("Error al obtener las compañías:", error);
      }
    };

    // Datos de ejemplo para anuncios y testimonios
    const exampleAds = [
      "¡Nuevas ofertas de empleo disponibles!",
      "Descuentos exclusivos en cursos de desarrollo profesional.",
      "Únete a nuestra comunidad y encuentra el trabajo de tus sueños.",
    ];
    setAds(exampleAds);

    const exampleTestimonials = [
      {
        name: "María Rodríguez",
        role: "Desarrolladora Web",
        text: "Gracias a esta plataforma, encontré el trabajo de mis sueños en una empresa increíble.",
      },
      {
        name: "Carlos Pérez",
        role: "Ingeniero de Software",
        text: "Las oportunidades de empleo son variadas y se adaptan a mis habilidades. ¡Muy recomendado!",
      },
    ];
    setTestimonials(exampleTestimonials);

    // Datos de ejemplo para estadísticas
    setStats({ jobsPosted: 120, usersActive: 350 });

    fetchUser();
    fetchCompanies();
  }, []);

   const handleViewVacancies = (companyRnc: string) => {
        // You can implement your logic here, e.g., redirecting to a page with company vacancies
        console.log(`Viewing vacancies for company RNC: ${companyRnc}`);
        // Example: window.location.href = `/vacancies/${companyRnc}`;
    };

  return (
    <div className="min-h-screen bg-gray-100">
      <NavBar />
      <div className="dashboard-content-container"> {/* Contenedor de fondo que cubre toda la página */}
        <div className="container mx-auto p-6"> {/* Contenedor interno para centrar el contenido */}
          {/* Header de Bienvenida */}
          <div className="welcome-header">
            <h2 className="text-3xl font-bold text-gray-800">
              {user ? `¡Bienvenido, ${user.firstName}!` : "Bienvenido al Dashboard"}
            </h2>
            <p className="text-gray-600 mt-2">Explora las mejores oportunidades para ti.</p>
          </div>

          {/* Anuncios */}
          <div className="ads-section">
            <h3 className="text-2xl font-semibold text-gray-800 mb-4">Anuncios</h3>
            <ul>
              {ads.map((ad, index) => (
                <li key={index} className="text-gray-600">{ad}</li>
              ))}
            </ul>
          </div>

          {/* Empresas Destacadas */}
          <div className="companies-section">
            <h3 className="text-2xl font-semibold text-gray-800 mb-4">Empresas destacadas</h3>
            <div className="companies-grid">
              {companies.length > 0 ? (
                companies.map((company) => (
                  <div key={company.rnc} className="company-card">
                    <h3 className="text-xl font-semibold text-gray-800">{company.name}</h3>
                    <p className="text-gray-600 mt-2">{company.description}</p>
                    <div className="contact-info">
                      <p>Email: {company.contactEmail}</p>
                      <p>Teléfono: {company.phone}</p>
                      <p>Ubicación: {company.location}</p>
                    </div>
                    <button
                      className="view-vacancies-button"
                      onClick={() => handleViewVacancies(company.rnc)}
                    >
                      Ver Vacantes de Compañía
                    </button>
                  </div>
                ))
              ) : (
                <p className="text-gray-600">No hay compañías disponibles.</p>
              )}
            </div>
          </div>

          

        </div>
      </div>
    </div>
  );
};

export default DashboardUserHome;
