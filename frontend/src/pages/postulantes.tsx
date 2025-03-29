import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import Navbar from './navbar'; // Importación del Navbar
import '../assets/css/postulantes.css';

interface Application {
    id: number;
    applicantName: string;
    cvUrl: string;
    applicationDate: string;
}

interface Vacant {
    id: number;
    title: string;
    companyName: string; // Suponiendo que este campo existe en la respuesta
}

const Postulantes: React.FC = () => {
    const { vacantId } = useParams<{ vacantId: string }>();
    const [applications, setApplications] = useState<Application[]>([]);
    const [filteredApplications, setFilteredApplications] = useState<Application[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const [vacantInfo, setVacantInfo] = useState<Vacant | null>(null);
    const [searchTerm, setSearchTerm] = useState<string>('');
    const [sortBy, setSortBy] = useState<string>('');

    // Cargar los datos cuando cambia vacantId
    useEffect(() => {
        const fetchVacantAndApplications = async () => {
            try {
                if (!vacantId) {
                    throw new Error('ID de vacante no proporcionado');
                }

                // Obtener el token del localStorage
                const token = localStorage.getItem('token');
                if (!token) {
                    throw new Error('Token no encontrado en el localStorage');
                }

                // Obtener los datos de la vacante
                const vacantApiUrl = `http://localhost:5283/api/Vacant/${vacantId}`;
                const vacantResponse = await axios.get<Vacant>(vacantApiUrl, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                        'Content-Type': 'application/json',
                    },
                });

                setVacantInfo(vacantResponse.data);

                // Obtener las aplicaciones relacionadas con la vacante
                const applicationsApiUrl = `http://localhost:5283/api/Vacant/${vacantId}/applications`;
                const applicationsResponse = await axios.get<Application[]>(applicationsApiUrl, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                        'Content-Type': 'application/json',
                    },
                });

                // Validar que la respuesta sea un array
                const apps = Array.isArray(applicationsResponse.data) ? applicationsResponse.data : [];
                setApplications(apps);
                setFilteredApplications(apps); // Inicialmente todas las aplicaciones están visibles
            } catch (err: any) {
                setError('Error al cargar los datos');
                console.error(err.message);
                console.error(err.response ? err.response.data : err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchVacantAndApplications();
    }, [vacantId]);

    // Filtrar y ordenar las aplicaciones
    useEffect(() => {
        let filtered = [...applications];

        // Aplicar el filtro de búsqueda
        if (searchTerm.trim() !== '') {
            filtered = filtered.filter((application) =>
                application.applicantName.toLowerCase().includes(searchTerm.toLowerCase())
            );
        }

        // Aplicar el ordenamiento
        if (sortBy === 'name') {
            filtered.sort((a, b) => a.applicantName.localeCompare(b.applicantName));
        } else if (sortBy === 'date') {
            filtered.sort((a, b) => new Date(a.applicationDate).getTime() - new Date(b.applicationDate).getTime());
        }

        // Actualizar el estado de las aplicaciones filtradas
        setFilteredApplications(filtered);
    }, [searchTerm, sortBy, applications]);

    if (loading) return <div className="postulantes-loading">Cargando...</div>;
    if (error) return <div className="postulantes-error">{error}</div>;

    return (
        <div className="postulantes-background">
            <Navbar /> {/* Agregamos el Navbar */}
            <div className="postulantes-header">
                {/* Barra de búsqueda */}
                <input
                    type="text"
                    placeholder="Buscar postulantes por nombre..."
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                    className="postulantes-search-bar"
                />
                {/* Dropdown para ordenar */}
                <select
                    value={sortBy}
                    onChange={(e) => setSortBy(e.target.value)}
                    className="postulantes-dropdown"
                >
                    <option value="">Ordenar por</option>
                    <option value="name">Orden alfabético</option>
                    <option value="date">Fecha de envío</option>
                </select>
                {/* Título */}
                <h1 className="postulantes-title">
                    {vacantInfo ? `${vacantInfo.title} ` : 'Cargando información...'}
                </h1>
            </div>
            <div className="postulantes-container">
                {filteredApplications.length > 0 ? (
                    <ul className="postulantes-list">
                        {filteredApplications.map((application) => (
                            <li key={application.id} className="postulantes-item">
                                <h3 className="postulantes-applicant-name">{application.applicantName}</h3>
                                <p className="postulantes-cv">
                                    CV:{' '}
                                    <a
                                        href={application.cvUrl}
                                        target="_blank"
                                        rel="noopener noreferrer"
                                        className="postulantes-cv-link"
                                    >
                                        Ver CV
                                    </a>
                                </p>
                                <p className="postulantes-date">
                                    Fecha de Aplicación:{' '}
                                    {new Date(application.applicationDate).toLocaleDateString()}
                                </p>
                            </li>
                        ))}
                    </ul>
                ) : (
                    <div className="postulantes-no-applications">
                        <h2>No hay postulantes para esta vacante</h2>
                        <p>Actualmente no se han recibido solicitudes para esta posición.</p>
                    </div>
                )}
            </div>
        </div>
    );
};

export default Postulantes;
