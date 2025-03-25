// src/pages/Dashboard.tsx
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Card, Row, Col, Button, Container } from 'react-bootstrap';
import ApexCharts from 'react-apexcharts';
import axios from 'axios';
import Sidebar from './navbar'; // Importar el nuevo Sidebar

import '../assets/css/dashboard.css';

const Dashboard: React.FC = () => {
  const [vacantes, setVacantes] = useState<any[]>([]);  // Vacantes en formato de array
  const [vacantesActivas, setVacantesActivas] = useState<number>(0);  // Vacantes activas
  const navigate = useNavigate();

  // Fetch de vacantes
  useEffect(() => {
    const fetchVacantes = async () => {
      try {
        const response = await axios.get('http://localhost:5283/api/vacant/');
        setVacantes(response.data);
        
        // Actualiza el número de vacantes activas
        const activeVacantesCount = response.data.filter((vacante: any) => vacante.status).length;
        setVacantesActivas(activeVacantesCount);  // Actualiza el número de vacantes activas
      } catch (error) {
        console.error('Error fetching vacantes:', error);
      }
    };

    fetchVacantes();
  }, []);  // Se ejecuta solo una vez cuando el componente se monta

  // Opciones para la gráfica de vacantes activas
  const chartOptions: ApexCharts.ApexOptions = {
    chart: { height: 350, type: 'area', zoom: { enabled: false } },
    dataLabels: { enabled: false },
    stroke: { curve: 'smooth' },
    title: { text: 'Vacantes Activadas', align: 'left' },
    xaxis: { categories: ['January', 'February', 'March', 'April', 'May'] },
    fill: { opacity: 0.3 },
  };

  // Series para la gráfica
  const chartSeries = [{ name: 'Vacantes Activas', data: [65, 59, 80, 81, 56] }];

  return (
    <div className="dashboard-container">
      {/* Barra lateral */}
      <Sidebar />

      {/* Contenido principal */}
      <div className="main-content">
        <div className="content-header">
          <h1>Dashboard</h1>
        </div>

        <Container fluid>
          {/* Estadísticas de vacantes */}
          <Row className="dashboard-stat-cards">
            <Col md={4}>
              <Card>
                <Card.Body>
                  <Card.Title>Vacantes Activas</Card.Title>
                  <Card.Text>{vacantesActivas}</Card.Text>
                </Card.Body>
              </Card>
            </Col>
          </Row>

          {/* Gráfico de vacantes activas */}
          <Row className="dashboard-chart-container">
            <Col>
              <h3>Estadísticas de Vacantes</h3>
              <ApexCharts options={chartOptions} series={chartSeries} type="area" height={350} />
            </Col>
          </Row>

          {/* Vacantes recientes */}
          <Row className="mb-4">
            <Col>
              <h3 className="mb-4">Vacantes Recientes</h3>
              <Container>
                <Row className="vacantes-recientes-container">
                  {vacantes.slice(0, 3).map((vacante, index) => (
                    <Col key={index} md={4} className="vacante-reciente-col">
                      <Card className="vacante-reciente-card shadow-sm">
                        <Card.Body>
                          <Card.Title>{vacante.title}</Card.Title>
                          <Card.Subtitle className="mb-2 text-muted">{vacante.area}</Card.Subtitle>
                          <Card.Text>
                            <strong>Descripción:</strong> {vacante.description} <br />
                          </Card.Text>
                          <Button variant="primary" onClick={() => navigate('/vacantes')} className="vacante-reciente-btn">
                            Ver todas las vacantes
                          </Button>
                        </Card.Body>
                      </Card>
                    </Col>
                  ))}
                </Row>
              </Container>
            </Col>
          </Row>
        </Container>
      </div>
    </div>
  );
};

export default Dashboard;
