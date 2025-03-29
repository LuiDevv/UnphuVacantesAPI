import React, { useState, useEffect, ChangeEvent } from "react";
import { useParams } from "react-router-dom";
import NavbarUser from "../pages/navbar-user";
import "../assets/css/vacancydetail.css";
import axios, { AxiosResponse } from 'axios';
import { toast, ToastContainer } from "react-toastify";

interface Vacancy {
    id: number;
    title: string;
    description: string;
    modality: string;
    salary: number;
    status: boolean;
}

const VacancyDetailPage: React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const [vacancy, setVacancy] = useState<Vacancy | null>(null);
    const [cvUrl, setCvUrl] = useState<string | null>(null);
    const [uploading, setUploading] = useState<boolean>(false);
    const [applicationSent, setApplicationSent] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const [applicantName, setApplicantName] = useState<string>("");

    useEffect(() => {
        // Decodifica el token para obtener firstName y lastName
        const fetchUserDataFromToken = () => {
            const token = localStorage.getItem('token');
            if (!token) {
                console.error("Token no encontrado");
                return;
            }

            try {
                const base64Url = token.split('.')[1];
                const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
                const jsonPayload = JSON.parse(atob(base64));
                const { firstName, lastName } = jsonPayload;

                if (firstName && lastName) {
                    setApplicantName(`${firstName} ${lastName}`);
                } else {
                    console.error("No se encontraron firstName y lastName en el token");
                }
            } catch (error) {
                console.error("Error al decodificar el token:", error);
            }
        };

        fetchUserDataFromToken();
    }, []);

    useEffect(() => {
        const fetchVacancy = async () => {
            try {
                const response: AxiosResponse<Vacancy> = await axios.get(`http://localhost:5283/api/Vacant/${id}`);
                setVacancy(response.data);
            } catch (err) {
                setError("Error al cargar los detalles de la vacante.");
                console.error("Error fetching vacancy details:", err);
            }
        };

        fetchVacancy();
    }, [id]);

    const getAuthHeaders = () => {
        const token = localStorage.getItem('token');
        return token ? { Authorization: `Bearer ${token}` } : {};
    };

    const handleCvUpload = async (e: ChangeEvent<HTMLInputElement>) => {
        if (e.target.files && e.target.files[0]) {
            const file = e.target.files[0];
            setUploading(true);

            try {
                const formData = new FormData();
                formData.append('file', file);

                const response: AxiosResponse<{ url: string }> = await axios.post(
                    'http://localhost:5283/api/FileUpload/upload',
                    formData,
                    {
                        headers: {
                            ...getAuthHeaders(),
                            'Content-Type': 'multipart/form-data',
                        },
                    }
                );

                setCvUrl(response.data.url);
                toast('CV subido exitosamente!');
            } catch (err) {
                setError("Error al subir el CV. Inténtalo de nuevo.");
                console.error('Error uploading CV:', err);
            } finally {
                setUploading(false);
            }
        }
    };

    const handleApply = async () => {
        if (!cvUrl) {
            toast("Por favor, sube tu CV antes de aplicar.");
            return;
        }

        try {
            const applicationData = {
                cvUrl,
                applicantName,
            };

            await axios.post(
                `http://localhost:5283/api/Vacant/${id}/apply`,
                applicationData,
                {
                    headers: {
                        ...getAuthHeaders(),
                        'Content-Type': 'application/json',
                    },
                }
            );

            setApplicationSent(true);
            toast("Aplicación enviada exitosamente!");
        } catch (err) {
            setError("Error al aplicar a la vacante. Inténtalo de nuevo.");
            console.error("Error applying to vacancy:", err);
        }
    };

    return (
        <div className="vacancy-detail-page">
            <NavbarUser />
            {error && <p className="error-message">{error}</p>}
            {vacancy ? (
                <div className="vacancy-detail-card">
                    <h3>{vacancy.title}</h3>
                    <p><strong>Descripción Completa:</strong> {vacancy.description}</p>
                    <p><strong>Modalidad:</strong> {vacancy.modality}</p>
                    <p><strong>Salario:</strong> {vacancy.salary}</p>
                    <p><strong>Estado:</strong> {vacancy.status ? 'Activa' : 'Inactiva'}</p>

                    <label htmlFor="cv-upload">Sube tu CV actualizado</label>
                    <input
                        type="file"
                        id="cv-upload"
                        onChange={handleCvUpload}
                        disabled={uploading}
                        style={{ display: 'none' }}
                    />
                    {uploading && <p className="uploading-message">Subiendo CV...</p>}

                    <button
                        onClick={handleApply}
                        
                        className="apply-button"
                    >
                        {applicationSent ? "Aplicación Enviada" : "Aplicar a la Vacante"}
                    </button>
                </div>
            ) : (
                <p>Cargando detalles de la vacante...</p>
            )}
            <ToastContainer position="top-right" />
        </div>
    );
};

export default VacancyDetailPage;
