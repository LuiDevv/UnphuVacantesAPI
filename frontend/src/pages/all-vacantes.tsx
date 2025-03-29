// src/pages/AllVacantes.tsx
import React, { useEffect, useState } from 'react';
import { Card, Button, Spinner, Container, Row, Col } from 'react-bootstrap';
import axios from 'axios';
import '../assets/css/vacantes.css';
import { ReactComponent as SearchIcon } from '../icons/search.svg';
import Navbar from './navbar';

const AllVacantes: React.FC = () => {
    const [vacantes, setVacantes] = useState<any[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string>('');
    const [searchTerm, setSearchTerm] = useState<string>(''); // Estado para el término de búsqueda
    const [areas, setAreas] = useState<string[]>([]); // Estado para las áreas únicas

    useEffect(() => {
        const fetchVacantes = async () => {
            try {
                const response = await axios.get('http://localhost:5283/api/vacant');
                setVacantes(response.data);
            } catch (err) {
                setError('Hubo un error al cargar las vacantes.');
            } finally {
                setLoading(false);
            }
        };

        fetchVacantes();
    }, []);

    // Extraer áreas únicas de las vacantes
    useEffect(() => {
        if (vacantes.length > 0) {
            const uniqueAreas = Array.from(new Set(vacantes.map((vacante) => vacante.area)));
            setAreas(uniqueAreas);
        }
    }, [vacantes]);

    // Filtrar vacantes basadas en el término de búsqueda
    const filteredVacantes = vacantes.filter((vacante) =>
        vacante.area.toLowerCase().includes(searchTerm.toLowerCase())
    );

    return (
        <div className="vacantes-container1">
            <Navbar />
            <div className="main-content">
            <div className="header-content">
            <h1>Todas las Vacantes</h1>
            <div className="search-bar">
                <input
                    type="text"
                    placeholder="Buscar por área..."
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                    className="search-input" // Clase para el input
                />
                <button type="submit" className="search-button">
                    <SearchIcon className="search-icon" /> {/* Usa el componente SVG */}
                </button>
            </div>
        </div>

                {error && <p className="error-message">{error}</p>}

                {loading ? (
                    <div className="loading-spinner">
                        <Spinner animation="border" variant="primary" />
                        <span>Cargando vacantes...</span>
                    </div>
                ) : (
                    <Container>
                        <Row className="vacantes-grid">
                            {filteredVacantes.length > 0 ? (
                                filteredVacantes.map((vacante: any, index: number) => (
                                    <Col key={index} md={4} className="mb-4">
                                        <Card className="vacante-card-container">
                                            <Card.Body>
                                                <Card.Title className="vacante-card-title">{vacante.title}</Card.Title>
                                                <Card.Subtitle className="vacante-card-subtitle">
                                                    <strong>{vacante.area}</strong>
                                                    <br></br>
                                                    <strong>Empresa: </strong> {vacante.companyName}
                                                </Card.Subtitle>
                                                <Card.Text className="vacante-card-text">
                                                    <strong>Modalidad:</strong> {vacante.modality} <br />
                                                    <strong>Salario:</strong> {vacante.salary} <br />
                                                    <strong>Estado:</strong> {vacante.status ? 'Activo' : 'Inactivo'} <br />
                                                    <strong>Descripción:</strong> {vacante.description}
                                                    <br></br>
                                                    <strong>Empresa: </strong> {vacante.companyName}
                                                </Card.Text>
                                            </Card.Body>
                                        </Card>
                                    </Col>
                                ))
                            ) : (
                                <p>No hay vacantes disponibles en esta área.</p>
                            )}
                        </Row>
                    </Container>
                )}
            </div>
        </div>
    );
};

export default AllVacantes;
