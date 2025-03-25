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
        profilePicture: '', // Inicialmente vacío
        cvUrl: '',
    });

    const [isEditing, setIsEditing] = useState(false);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const defaultProfilePicture = 'https://res.cloudinary.com/devgzya8r/image/upload/v1742693601/muvtwqkuvyy8lpiwmwhz.webp'; // Imagen por defecto

    // Función para obtener la imagen de perfil
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
                    throw new Error('Error al obtener los datos del usuario');
                }

                const data = await response.json();
                setUser({
                    userName: data.userName,
                    email: data.email,
                    firstName: data.firstName,
                    lastName: data.lastName,
                    phoneNumber: data.phoneNumber,
                    profilePicture: data.profilePicture, // No asignamos la imagen por defecto aquí
                    cvUrl: data.cvUrl || '',
                });
            } catch (err) {
                setError(err instanceof Error ? err.message : 'Error desconocido');
            } finally {
                setLoading(false);
            }
        };

        fetchUserData();
    }, []);

    // Actualizar solo la información del usuario
    const handleUpdateInfo = async () => {
        try {
            const response = await fetch('http://localhost:5283/api/account/update-profile', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem('token')}`,
                },
                body: JSON.stringify({
                    userName: user.userName,
                    email: user.email,
                    firstName: user.firstName,
                    lastName: user.lastName,
                    phoneNumber: user.phoneNumber,
                    profilePicture: user.profilePicture, // Incluimos la imagen aquí
                }),
            });

            if (!response.ok) {
                throw new Error('Error al actualizar la información del usuario');
            }

            console.log('Información actualizada correctamente');
            setIsEditing(false);
        } catch (err) {
            alert('Hubo un error al actualizar la información. Inténtalo de nuevo.');
        }
    };

    // Subir CV
    const handleUploadCV = async (url: string) => {
        try {
            const response = await fetch('http://localhost:5283/api/account/update-cv', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem('token')}`,
                },
                body: JSON.stringify({ cvUrl: url }),
            });

            if (!response.ok) {
                throw new Error('Error al subir el CV');
            }

            console.log('CV subido correctamente');
            setUser({ ...user, cvUrl: url });
        } catch (err) {
            alert('Hubo un error al subir el CV. Inténtalo de nuevo.');
        }
    };

    const handleUploadSuccess = (url: string) => {
        setUser({ ...user, profilePicture: url });
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
                         <button className="edit-button" onClick={() => setIsEditing(true)}>
                            Editar Información
                        </button>
                    </div>
                </div>

                <div className="profile-details">
                    {isEditing ? (
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

                            <button className="save-button" onClick={handleUpdateInfo}>
                                Guardar Cambios
                            </button>
                            <button className="cancel-button" onClick={() => setIsEditing(false)}>
                                Cancelar
                            </button>
                        </div>
                    ) : (
                        <div className="profile-about">
                            <h2>Acerca de mí</h2>
                            <p>Nombre de usuario: {user.userName}</p>
                            <p>Nombre: {user.firstName} {user.lastName}</p>
                            <p>Correo electrónico: {user.email}</p>
                            <p>Teléfono: {user.phoneNumber}</p>
                        </div>
                    )}
                </div>

                <div className="upload-container">
                    <h2>Subir CV</h2>
                    <FileUpload onUploadSuccess={handleUploadCV} />
                    {user.cvUrl && (
                        <p>
                            <a href={user.cvUrl} target="_blank" rel="noopener noreferrer">
                                Ver CV
                            </a>
                        </p>
                    )}
                </div>
            </div>
        </div>
    );
};

export default Perfil;
