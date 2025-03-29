// src/pages/Dashboard.tsx
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Card, Row, Col, Button, Container } from 'react-bootstrap';
import ApexCharts from 'react-apexcharts';
import axios from 'axios';
import Sidebar from './navbar'; // Importar el Sidebar (corregido)
import '../assets/css/dashboard.css';

interface Vacante {
    id: number;
    title: string;
    area: string;
    description: string;
    status: boolean;
    createdAt: string;
}

interface Postulante {
    id: number;
    applicantName: string;
    cvUrl: string;
    applicationDate: string;
}

const Dashboard: React.FC = () => {
    const [vacantes, setVacantes] = useState<Vacante[]>([]); // Vacantes en formato de array
    const [vacantesActivas, setVacantesActivas] = useState<number>(0); // Vacantes activas
    const [numeroPostulantes, setNumeroPostulantes] = useState<number>(0); // Numero de postulantes
    const [chartData, setChartData] = useState<{ categories: string[]; data: number[] }>({
        categories: [],
        data: [],
    });
    const navigate = useNavigate();

    // Fetch de vacantes y postulantes
    useEffect(() => {
        const fetchData = async () => {
            try {
                // Obtener vacantes
                const vacantesResponse = await axios.get('http://localhost:5283/api/vacant/');
                setVacantes(vacantesResponse.data);

                // Actualiza el número de vacantes activas
                const activeVacantesCount = vacantesResponse.data.filter((vacante: { status: any; }) => vacante.status).length;
                setVacantesActivas(activeVacantesCount);

                // Procesar datos para la gráfica (por área)
                const vacantesPorArea = processVacantesForChart(vacantesResponse.data);
                setChartData(vacantesPorArea);

                // Obtener numero de postulantes
                const postulantesResponse = await axios.get('http://localhost:5283/api/Vacant/applications');
                setNumeroPostulantes(postulantesResponse.data.length);
            } catch (error) {
                console.error('Error fetching vacantes:', error);
            }
        };

        fetchData();
    }, []);

    // Procesar datos de vacantes para la gráfica (por área)
    const processVacantesForChart = (vacantes: Vacante[]) => {
        const vacantesPorArea: { [key: string]: number } = {};

        vacantes.forEach((vacante) => {
            const area = vacante.area;

            if (!vacantesPorArea[area]) {
                vacantesPorArea[area] = 0;
            }
            vacantesPorArea[area]++;
        });

        const categories = Object.keys(vacantesPorArea);
        const data = Object.values(vacantesPorArea);

        return { categories, data };
    };

    // Opciones para la gráfica de barras
    const chartOptions: ApexCharts.ApexOptions = {
        chart: {
            height: 350,
            type: 'bar',
            zoom: { enabled: false },
            toolbar: { show: false },
        },
        plotOptions: {
            bar: {
                horizontal: false,
                borderRadius: 5,
                columnWidth: '30%',

            },
        },
        dataLabels: { enabled: false },
        stroke: { curve: 'smooth', width: 2 },
        title: { text: 'Vacantes por Área', align: 'left', style: { fontSize: '18px', fontWeight: 'bold' } },
        xaxis: {
            categories: chartData.categories,
            title: {
                text: 'Área',
                style: {
                    fontSize: '14px',
                    fontWeight: 'normal',
                }
            }
        },
        yaxis: {
            title: {
                text: 'Número de Vacantes',
                style: {
                    fontSize: '14px',
                    fontWeight: 'normal',
                }
            }
        },
        fill: { opacity: 1, type: 'solid', colors: ['#005B8D'] },
        tooltip: { enabled: true, theme: 'dark' },
        grid: {
            borderColor: '#e7e7e7',
            row: {
                colors: ['#f3f3f3', 'transparent'], // alternar colores de fila
                opacity: 0.5
            },
        },
    };

    // Series para la gráfica
    const chartSeries = [{ name: 'Vacantes', data: chartData.data }];

    return (
        <div className="dashboard-containerr">
            {/* Barra lateral */}
            <Sidebar />... {/* Contenido principal */}
            <div className="main-content">
                <div className="content-header">
                    <h1>Dashboard</h1>
                </div>

                <Container fluid>
                    {/* Estadísticas de vacantes y postulantes */}
                    <Row className="dashboard-stat-cards">
                        <Col md={6}>
                            <Card className="shadow-sm">
                                <Card.Body>
                                    <Card.Title>Vacantes Activas</Card.Title>
                                    <Card.Text className="display-4">{vacantesActivas}</Card.Text>
                                </Card.Body>
                            </Card>
                        </Col>
                        <Col md={6}>
                            <Card className="shadow-sm">
                                <Card.Body>
                                    <Card.Title>Postulantes</Card.Title>
                                    <Card.Text className="display-4">{numeroPostulantes}</Card.Text>
                                </Card.Body>
                            </Card>
                        </Col>
                    </Row>

                    {/* Gráfico de vacantes por área */}
                    <Row className="dashboard-chart-container">
                        <Col>
                            <Card className="shadow-sm">
                                <Card.Body>
                                    <h3 className="mb-4">Estadísticas de Vacantes</h3>
                                    {chartData.categories.length > 0 ? (
                                        <ApexCharts options={chartOptions} series={chartSeries} type="bar" height={350} />
                                    ) : (
                                        <p>No hay datos para mostrar en la gráfica.</p>
                                    )}
                                </Card.Body>
                            </Card>
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
                                                    <Button
                                                        variant="primary"
                                                        onClick={() => navigate('/all-vacantes')}
                                                        className="vacante-reciente-btn"
                                                    >
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
