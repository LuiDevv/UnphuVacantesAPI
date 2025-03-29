import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import React, { useState, useEffect } from 'react';
import FileUpload from './FileUpload';
import NavbarUser from './navbar-user';
import "../assets/css/perfil.css";


const Perfil: React.FC = () => {
    const [user, setUser] = useState({
        userName: '',
        email: '',
        firstName: '',
        lastName: '',
        phoneNumber: '',
        profilePicture: '',
        cv: '',
    });

    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [showModal, setShowModal] = useState(false);

    const defaultProfilePicture = 'https://res.cloudinary.com/devgzya8r/image/upload/v1742693601/muvtwqkuvyy8lpiwmwhz.webp';

    const getProfilePicture = () => {
        return user.profilePicture === 'string' ? defaultProfilePicture : user.profilePicture;
    };

    useEffect(() => {
        const fetchUserData = async () => {
            try {
                const response = await fetch('http://localhost:5283/api/account/current-user', {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${localStorage.getItem('token')}`,
                    },
                });

                if (!response.ok) {
                    throw new Error(`Error al obtener los datos del usuario: ${response.status} ${response.statusText}`);
                }

                const data = await response.json();
                setUser({
                    userName: data.userName,
                    email: data.email,
                    firstName: data.firstName,
                    lastName: data.lastName,
                    phoneNumber: data.phoneNumber,
                    profilePicture: data.profilePicture,
                    cv: data.cv || '',
                });
            } catch (err) {
                setError(err instanceof Error ? err.message : 'Error desconocido');
            } finally {
                setLoading(false);
            }
        };

        fetchUserData();
    }, []);

    const handleUpdateInfo = async (cvFile: string | null = null) => {
        try {
            const payload = {
                userName: user.userName,
                email: user.email,
                firstName: user.firstName,
                lastName: user.lastName,
                phoneNumber: user.phoneNumber,
                profilePicture: user.profilePicture,
                cv: cvFile !== null ? cvFile : user.cv,
            };

            const response = await fetch('http://localhost:5283/api/account/update-profile', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem('token')}`,
                },
                body: JSON.stringify(payload),
            });

            if (!response.ok) {
                const errorData = await response.json();
                const errorMessage = errorData?.message || `Error al actualizar la información del usuario: ${response.status} ${response.statusText}`;
                throw new Error(errorMessage);
            }

            console.log('Información actualizada correctamente');
            setShowModal(false);

            setUser(prevUser => ({
                ...prevUser,
                firstName: user.firstName,
                lastName: user.lastName,
                phoneNumber: user.phoneNumber,
                userName: user.userName,
                email: user.email,
                profilePicture: user.profilePicture,
                cv: cvFile !== null ? cvFile : user.cv,
            }));
        } catch (err) {
            const errorMessage = err instanceof Error ? err.message : 'Error desconocido al actualizar la información.';
            toast(errorMessage);
        }
    };

    const handleUploadCV = async (url: string) => {
        await handleUpdateInfo(url);
    };

    const handleUploadSuccess = (url: string) => {
        setUser(prevUser => ({ ...prevUser, profilePicture: url }));
    };

    if (loading) return <p>Cargando datos...</p>;
    if (error) return <p>Error: {error}</p>;

    return (
        <div>
            <NavbarUser />
            <div className="profile-container">
                <div className="profile-header">
                    <div className="profile-avatar">
                        <img
                            src={getProfilePicture()}
                            alt="Profile"
                            className="avatar"
                            style={{ cursor: 'pointer' }}
                            onClick={() => console.log("Actualizar imagen")}
                        />
                    </div>
                    <div className="profile-info">
                        <h1>{user.firstName} {user.lastName}</h1>
                        <p>{user.email}</p>
                        <button className="edit-button" onClick={() => setShowModal(true)}>
                            Editar Información
                        </button>
                    </div>
                </div>
                <div className="profile-details">
                    <div className="profile-about">
                        <h2>Acerca de mí</h2>
                        <p>Nombre de usuario: {user.userName}</p>
                        <p>Nombre: {user.firstName} {user.lastName}</p>
                        <p>Correo electrónico: {user.email}</p>
                        <p>Teléfono: {user.phoneNumber}</p>
                    </div>
                </div>
                
                <div className="upload-container">
                    <h2>Subir CV</h2>
                    <FileUpload onUploadSuccess={handleUploadCV} />
                    
                    {user.cv && (
                        <div>
                            <h3>Vista previa del CV:</h3>
                            <iframe 
                                src={`https://docs.google.com/gview?url=${encodeURIComponent(user.cv)}&embedded=true`} 
                                width="100%" 
                                height="500px" 
                                style={{ border: "none" }}
                            ></iframe>
                            <a href={user.cv} download="CV">Descargar CV</a>
                        </div>
                    )}
                </div>

                {/* Modal */}
                {showModal && (
                    <div className="modal">
                        <div className="modal-content">
                            <span className="close" onClick={() => setShowModal(false)}>&times;</span>
                            <h2>Editar Información</h2>
                            <div className="edit-form">
                                <label>
                                    Nombre de usuario:
                                    <input
                                        type="text"
                                        value={user.userName}
                                        onChange={(e) => setUser({ ...user, userName: e.target.value })}
                                    />
                                </label>
                                <label>
                                    Correo electrónico:
                                    <input
                                        type="email"
                                        value={user.email}
                                        onChange={(e) => setUser({ ...user, email: e.target.value })}
                                    />
                                </label>
                                <label>
                                    Nombre:
                                    <input
                                        type="text"
                                        value={user.firstName}
                                        onChange={(e) => setUser({ ...user, firstName: e.target.value })}
                                    />
                                </label>
                                <label>
                                    Apellido:
                                    <input
                                        type="text"
                                        value={user.lastName}
                                        onChange={(e) => setUser({ ...user, lastName: e.target.value })}
                                    />
                                </label>
                                <label>
                                    Teléfono:
                                    <input
                                        type="text"
                                        value={user.phoneNumber}
                                        onChange={(e) => setUser({ ...user, phoneNumber: e.target.value })}
                                    />
                                </label>
                                <label>
                                    Imagen de perfil:
                                    <FileUpload onUploadSuccess={handleUploadSuccess} />
                                </label>
                                <button className="save-button" onClick={() => handleUpdateInfo()}>
                                    Guardar Cambios
                                </button>
                            </div>
                        </div>
                    </div>
                )}
            </div>
            <ToastContainer position="top-right" />
        </div>
    );
};

export default Perfil;
