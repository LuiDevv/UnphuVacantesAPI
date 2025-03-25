import React, { useEffect, useState } from 'react';
import { Card, Button, Spinner, Container, Row, Col } from 'react-bootstrap';
import axios from 'axios';
import '../assets/css/vacantes.css'; // Asegúrate de que este archivo CSS contenga los estilos
import Navbar from './navbar'; // Asegúrate de que la ruta sea correcta

const AllVacantes: React.FC = () => {
  const [vacantes, setVacantes] = useState<any[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string>('');

  useEffect(() => {
    const fetchVacantes = async () => {
      try {
        const response = await axios.get('http://localhost:5283/api/vacant'); // Obtiene todas las vacantes
        setVacantes(response.data);
      } catch (err) {
        setError('Hubo un error al cargar las vacantes.');
      } finally {
        setLoading(false);
      }
    };

    fetchVacantes();
  }, []);

  return (
    <div className="vacantes-container">
      <Navbar /> {/* Asegúrate de que este componente Navbar sea el que quieres usar */}
      <div className="main-content">
        <div className="content-header">
          <h1>Todas las Vacantes</h1>
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
              {vacantes.length > 0 ? (
                vacantes.map((vacante: any, index: number) => (
                  <Col key={index} md={4} className="mb-4">
                    <Card className="vacante-card-container">
                      <Card.Body>
                        <Card.Title className="vacante-card-title">{vacante.title}</Card.Title>
                        <Card.Subtitle className="vacante-card-subtitle">
                          <strong>{vacante.area}</strong>
                        </Card.Subtitle>
                        <Card.Text className="vacante-card-text">
                          <strong>Modalidad:</strong> {vacante.modality} <br />
                          <strong>Salario:</strong> {vacante.salary} <br />
                          <strong>Estado:</strong> {vacante.status ? 'Activo' : 'Inactivo'} <br />
                          <strong>Descripción:</strong> {vacante.description}
                        </Card.Text>
                      </Card.Body>
                    </Card>
                  </Col>
                ))
              ) : (
                <p>No hay vacantes disponibles</p>
              )}
            </Row>
          </Container>
        )}
      </div>
    </div>
  );
};

export default AllVacantes;
