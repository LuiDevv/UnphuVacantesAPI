import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import "../assets/css/vacantes-user.css";
import NavBar from "../pages/navbar-user";

const VacantesUser: React.FC = () => {
    const [vacancies, setVacancies] = useState<any[]>([]);
    const [searchTerm, setSearchTerm] = useState<string>('');
    const [areaFilter, setAreaFilter] = useState<string>('');
    const [companyFilter, setCompanyFilter] = useState<string>('');

    useEffect(() => {
        fetch("http://localhost:5283/api/Vacant")
            .then((response) => response.json())
            .then((data) => setVacancies(data))
            .catch((error) => console.error("Error fetching vacancies:", error));
    }, []);

    // Función para manejar el cambio en el input de búsqueda
    const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSearchTerm(event.target.value);
    };

    // Función para manejar el cambio en el filtro de área
    const handleAreaFilterChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setAreaFilter(event.target.value);
    };

    // Función para manejar el cambio en el filtro de empresa
    const handleCompanyFilterChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setCompanyFilter(event.target.value);
    };

    // Filtrar las vacantes basadas en el término de búsqueda y los filtros
    const filteredVacancies = vacancies.filter(vacant => {
        const searchMatch = vacant.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
                            vacant.description.toLowerCase().includes(searchTerm.toLowerCase());
        const areaMatch = areaFilter === '' || vacant.area === areaFilter;
        const companyMatch = companyFilter === '' || vacant.companyName === companyFilter;

        return searchMatch && areaMatch && companyMatch;
    });

    // Obtener todas las áreas y empresas únicas para los filtros
    const uniqueAreas = [...new Set(vacancies.map(vacant => vacant.area))];
    const uniqueCompanies = [...new Set(vacancies.map(vacant => vacant.companyName))];

    return (
        <div className="vacancies-container">
            <NavBar />

            <h2>Vacantes Disponibles</h2>

            <div className="search-filter-container">
                {/* Barra de búsqueda (lado izquierdo) */}
                <input
                    type="text"
                    placeholder="Buscar por título o descripción"
                    className="search-bar"
                    value={searchTerm}
                    onChange={handleSearchChange}
                />

                {/* Contenedor para los filtros (lado derecho) */}
                <div className="filter-container">
                    <select className="filter-select" value={areaFilter} onChange={handleAreaFilterChange}>
                        <option value="">Todas las áreas</option>
                        {uniqueAreas.map(area => (
                            <option key={area} value={area}>{area}</option>
                        ))}
                    </select>

                    <select className="filter-select" value={companyFilter} onChange={handleCompanyFilterChange}>
                        <option value="">Todas las empresas</option>
                        {uniqueCompanies.map(company => (
                            <option key={company} value={company}>{company}</option>
                        ))}
                    </select>
                </div>
            </div>

            <div className="vacancy-grid">
                {filteredVacancies.length > 0 ? (
                    filteredVacancies.map((vacant, index) => (
                        <div key={index} className="vacancy-card">
                            <h3>{vacant.title}</h3>
                            <h5>Empresa: {vacant.companyName}</h5>
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
