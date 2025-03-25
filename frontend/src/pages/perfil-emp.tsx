import React, { useState, useEffect } from 'react';
import FileUpload from './FileUpload';
import NavbarCompany from './navbar';
import "../assets/css/perfil-emp.css";
import "../assets/css/navbar.css";

const CompanyProfile: React.FC = () => {
    const [companyData, setCompanyData] = useState({
        name: '',
        description: '',
        contactEmail: '',
        phone: '',
        location: '',
        rnc: '',
        symbol: '',
        profilePicture: '',
        isApprovedByUNPHU: false,
    });

    const [isEditingProfile, setIsEditingProfile] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const [fetchError, setFetchError] = useState<string | null>(null);
    const [showModal, setShowModal] = useState(false);

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
            } catch (err) {
                setFetchError(err instanceof Error ? err.message : 'Error desconocido');
            } finally {
                setIsLoading(false);
            }
        };

        fetchCompanyProfile();
    }, []);

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
            alert('Hubo un error al actualizar la información. Inténtalo de nuevo.');
        }
    };

    const handleProfilePictureUpload = (url: string) => {
        setCompanyData({ ...companyData, profilePicture: url });
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
                </div>
                {showModal && (
                    <div className="modal">
                        <div className="modal-content">
                            <span className="close-button" onClick={() => setShowModal(false)}>&times;</span>
                            <div className="company-edit-form">
                                <div className="company-profile-avatar">
                                    <img
                                        src={getCompanyProfilePicture()}
                                        alt="Company Profile"
                                        className="company-avatar"
                                    />
                                </div>
                                <div className="file-upload-container">
                                    <FileUpload onUploadSuccess={handleProfilePictureUpload} />
                                </div>
                                <label>Nombre:<input type="text" value={companyData.name} onChange={(e) => setCompanyData({ ...companyData, name: e.target.value })} /></label>
                                <label>Descripción:<input type="text" value={companyData.description} onChange={(e) => setCompanyData({ ...companyData, description: e.target.value })} /></label>
                                <label>Correo electrónico:<input type="email" value={companyData.contactEmail} onChange={(e) => setCompanyData({ ...companyData, contactEmail: e.target.value })} /></label>
                                <label>Teléfono:<input type="text" value={companyData.phone} onChange={(e) => setCompanyData({ ...companyData, phone: e.target.value })} /></label>
                                <label>Ubicación:<input type="text" value={companyData.location} onChange={(e) => setCompanyData({ ...companyData, location: e.target.value })} /></label>
                                <label>RNC:<input type="text" value={companyData.rnc} onChange={(e) => setCompanyData({ ...companyData, rnc: e.target.value })} /></label>
                                <button className="company-save-button" onClick={handleUpdateCompanyInfo}>Guardar Cambios</button>
                                <button className="company-cancel-button" onClick={() => setShowModal(false)}>Cancelar</button>
                            </div>
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
};

export default CompanyProfile;
