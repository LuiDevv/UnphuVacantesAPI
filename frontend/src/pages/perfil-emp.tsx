import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import React, { useState, useEffect } from 'react';
import FileUpload from './FileUpload';
import NavbarCompany from './navbar';
import "../assets/css/perfil-emp.css";
import "../assets/css/navbar.css";
import { Card, Container, Row, Col, ToastContainer } from 'react-bootstrap';
import { Bar } from 'react-chartjs-2';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
} from 'chart.js';

ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
);

interface CompanyData {
    name: string;
    description: string;
    contactEmail: string;
    phone: string;
    location: string;
    rnc: string;
    symbol: string;
    profilePicture: string;
    isApprovedByUNPHU: boolean;
    id: number; // Add the id property
}

interface Vacant {
    id: number;
    title: string;
    // Add other properties as needed
}

const CompanyProfile: React.FC = () => {
    const [companyData, setCompanyData] = useState<CompanyData>({
        name: '',
        description: '',
        contactEmail: '',
        phone: '',
        location: '',
        rnc: '',
        symbol: '',
        profilePicture: '',
        isApprovedByUNPHU: false,
        id: 0
    });

    const [isEditingProfile, setIsEditingProfile] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const [fetchError, setFetchError] = useState<string | null>(null);
    const [showModal, setShowModal] = useState(false);
    const [applicationCount, setApplicationCount] = useState(0);
    const [vacancyCount, setVacancyCount] = useState(0);
    const [companyId, setCompanyId] = useState<number | null>(null);

    const defaultProfilePicture = 'https://res.cloudinary.com/devgzya8r/image/upload/v1742693601/muvtwqkuvyy8lpiwmwhz.webp';

    const getCompanyProfilePicture = () => {
        return companyData.profilePicture || defaultProfilePicture;
    };

    useEffect(() => {
        const fetchCompanyProfile = async () => {
            try {
                const response = await fetch('http://localhost:5283/api/Companies/current-company', {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${localStorage.getItem('token')}`,
                    },
                });

                if (!response.ok) {
                    throw new Error('Error al obtener el perfil de la empresa');
                }

                const data = await response.json();
                setCompanyData(data);
                setCompanyId(data.id); // Set the company ID
            } catch (err) {
                setFetchError(err instanceof Error ? err.message : 'Error desconocido');
            } finally {
                setIsLoading(false);
            }
        };

        const fetchApplicationCount = async () => {
            try {
                const response = await fetch('http://localhost:5283/api/Vacant/applications', {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${localStorage.getItem('token')}`,
                    },
                });

                if (!response.ok) {
                    throw new Error('Error al obtener el número de aplicaciones');
                }

                const applications = await response.json();
                setApplicationCount(applications.length);
            } catch (err) {
                console.error("Error fetching application count:", err);
            }
        };

        const fetchVacancyCount = async (companyId: number) => {
            try {
                const response = await fetch(`http://localhost:5283/api/Companies/company/${companyId}`, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${localStorage.getItem('token')}`,
                    },
                });

                if (!response.ok) {
                    throw new Error('Error al obtener el número de vacantes de la empresa');
                }

                const vacancies: Vacant[] = await response.json();
                setVacancyCount(vacancies.length);
            } catch (err) {
                console.error("Error fetching vacancy count:", err);
            }
        };
        
        fetchCompanyProfile();
        fetchApplicationCount();

        if (companyId) {
            fetchVacancyCount(companyId); // Fetch vacancies after company ID is available
        }
    }, [companyId]);

    const handleUpdateCompanyInfo = async () => {
        try {
            const response = await fetch('http://localhost:5283/api/Companies/update-profile', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem('token')}`,
                },
                body: JSON.stringify(companyData),
            });

            if (!response.ok) {
                throw new Error('Error al actualizar el perfil de la empresa');
            }

            console.log('Perfil de la empresa actualizado correctamente');
            setIsEditingProfile(false);
            setShowModal(false);
            window.location.reload();
        } catch (err) {
            toast('Hubo un error al actualizar la información. Inténtalo de nuevo.');
        }
    };

    const handleProfilePictureUpload = (url: string) => {
        setCompanyData({ ...companyData, profilePicture: url });
    };

    const chartData = {
        labels: ['Vacantes', 'Aplicaciones'],
        datasets: [
            {
                label: 'Cantidad',
                data: [vacancyCount, applicationCount],
                backgroundColor: [
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                ],
                borderColor: [
                    'rgba(54, 162, 235, 1)',
                    'rgba(75, 192, 192, 1)',
                ],
                borderWidth: 1,
            },
        ],
    };

    const chartOptions = {
        responsive: true,
        plugins: {
            title: {
                display: true,
                text: 'Vacantes vs Aplicaciones',
            },
        },
    };

    if (isLoading) return <p>Cargando datos...</p>;
    if (fetchError) return <p>Error: {fetchError}</p>;

    return (
        <div>
            <NavbarCompany />
            <div className="company-profile-container">
                <div className="company-profile-card">
                    <div className="company-profile-header">
                        <div className="company-profile-avatar">
                            <img
                                src={getCompanyProfilePicture()}
                                alt="Company Profile"
                                className="company-avatar"
                            />
                        </div>
                        <div className="company-profile-info">
                            <h1>{companyData.name}</h1>
                            <p>{companyData.contactEmail}</p>
                            <button className="company-edit-button" onClick={() => setShowModal(true)}>
                                Editar Información
                            </button>
                        </div>
                    </div>
                    <div className="company-profile-details">
                        <h2>Acerca de la empresa</h2>
                        <p>Nombre: {companyData.name}</p>
                        <p>Descripción: {companyData.description}</p>
                        <p>Correo electrónico: {companyData.contactEmail}</p>
                        <p>Teléfono: {companyData.phone}</p>
                        <p>Ubicación: {companyData.location}</p>
                        <p>RNC: {companyData.rnc}</p>
                    </div>
                      {/* Vacancy Activity Chart */}
                      <div className="vacancy-activity">
                          <h2>Vacantes vs Aplicaciones</h2>
                          <Bar data={chartData} options={chartOptions} />
                      </div>
                    <div className="company-social-links">
                        <a href="#"><i className="fab fa-linkedin"></i></a>
                        <a href="#"><i className="fab fa-twitter"></i></a>
                        <a href="#"><i className="fab fa-facebook"></i></a>
                        <a href="#"><i className="fab fa-instagram"></i></a>
                    </div>
                    <div className="company-application-count">
                        Total de Aplicaciones Recibidas: {applicationCount}
                    </div>
                </div>
            </div>
            <ToastContainer position="top-end" />
        </div>
    );
};

export default CompanyProfile;
