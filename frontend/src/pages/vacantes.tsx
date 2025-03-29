import { toast, ToastPosition } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import React, { useEffect, useState } from 'react';
import { Card, Button, Spinner, Modal, Form, Container, Row, Col, ToastContainer } from 'react-bootstrap';
import axios from 'axios';
import '../assets/css/vacantes.css';
import { ReactComponent as PlusIcon } from '../icons/plus-icon.svg';
import Navbar from './navbar';
import { useNavigate } from 'react-router-dom'; // Importamos useNavigate para la navegación

const Vacantes: React.FC = () => {
  const [vacantes, setVacantes] = useState<any[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string>('');
  const [showModal, setShowModal] = useState<boolean>(false);
  const [showCreateModal, setShowCreateModal] = useState<boolean>(false);
  const [vacanteToEdit, setVacanteToEdit] = useState<any>(null);
  const [newVacante, setNewVacante] = useState<any>({
    title: '',
    description: '',
    salary: '',
    area: '',
    modality: '',
  });

  const navigate = useNavigate(); // Usamos useNavigate para redirigir a otra página

  const getCompanyIdFromToken = () => {
    const token = localStorage.getItem('token');
    if (token) {
      const base64Url = token.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      const decoded = JSON.parse(atob(base64));
      return decoded.CompanyId;
    }
    return null;
  };

  const getAuthHeaders = () => {
    const token = localStorage.getItem('token');
    return token ? { Authorization: `Bearer ${token}` } : {};
  };

  useEffect(() => {
    const fetchVacantes = async () => {
      const companyId = getCompanyIdFromToken();
      if (!companyId) {
        setError('No se pudo obtener la compañía desde el token');
        setLoading(false);
        return;
      }

      try {
        const response = await axios.get(`http://localhost:5283/api/Companies/company/${companyId}`, {
          headers: getAuthHeaders(),
        });

        console.log('Vacantes response:', response);

        setVacantes(response.data || []); // Cambiado según la estructura del API
      } catch (err) {
        setError('Hubo un error al cargar las vacantes.');
      } finally {
        setLoading(false);
      }
    };

    fetchVacantes();
  }, []);

  const handleCreateVacante = async () => {
    const companyId = getCompanyIdFromToken();
    if (!companyId) {
      toast('No se pudo obtener la compañía desde el token');
      return;
    }

    try {
      await axios.post(
        'http://localhost:5283/api/Vacant',
        { ...newVacante, companyId },
        { headers: getAuthHeaders() }
      );
      toast('Vacante creada');
      setShowCreateModal(false);
      setNewVacante({ title: '', description: '', salary: '', area: '', modality: '' });

      const response = await axios.get(`http://localhost:5283/api/Companies/company/${companyId}`, {
        headers: getAuthHeaders(),
      });
      setVacantes(response.data || []); // Actualizamos las vacantes
    } catch (err) {
      toast('Hubo un error al crear la vacante');
    }
  };

  const handleDeleteVacante = async (id: number) => {
    if (window.confirm('¿Estás seguro de que deseas borrar esta vacante?')) {
      try {
        await axios.delete(`http://localhost:5283/api/vacant/${id}`, {
          headers: getAuthHeaders(),
        });
        setVacantes(vacantes.filter((vacante) => vacante.id !== id));
        toast('Vacante eliminada');
      } catch (err) {
        toast('Hubo un error al eliminar la vacante');
      }
    }
  };

  const handleUpdateVacante = (vacante: any) => {
    setVacanteToEdit(vacante);
    setShowModal(true);
  };

  const handleModalClose = () => {
    setShowModal(false);
    setVacanteToEdit(null);
  };

  const handleSaveChanges = async () => {
    if (vacanteToEdit) {
      try {
        await axios.put(
          `http://localhost:5283/api/vacant/${vacanteToEdit.id}`,
          vacanteToEdit,
          { headers: getAuthHeaders() }
        );
        setVacantes(prevVacantes =>
          prevVacantes.map(v => v.id === vacanteToEdit.id ? vacanteToEdit : v)
        );
        toast('Vacante actualizada');
        handleModalClose();
      } catch (err) {
        toast('Hubo un error al actualizar la vacante');
      }
    }
  };

  const handleVerPostulantes = (vacanteId: number) => {
    navigate(`/postulantes/${vacanteId}`); // Redirige al componente de postulantes con el id de la vacante
  };

  return (
    <div className="vacantes-container1">
      <Navbar />
      <div className="main-content">
        <div className="content-header">
          <h1>Vacantes</h1>
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
                        <Card.Subtitle className="vacante-card-subtitle">{vacante.area}</Card.Subtitle>
                        <Card.Text className="vacante-card-text">
                          <strong>Modalidad:</strong> {vacante.modality} <br />
                          <strong>Salario:</strong> {vacante.salary} <br />
                          <strong>Estado:</strong> {vacante.status ? 'Activo' : 'Inactivo'} <br />
                          <strong>Descripción:</strong> {vacante.description}
                        </Card.Text>
                        <div className="vacante-card-buttons">
                          <Button className="vacante-btn" variant="warning" onClick={() => handleUpdateVacante(vacante)}>Actualizar</Button>
                          <Button className="vacante-btn" variant="danger" onClick={() => handleDeleteVacante(vacante.id)}>Borrar</Button>
                          <Button className="vacante-btn" variant="info" onClick={() => handleVerPostulantes(vacante.id)}>Ver Postulantes</Button>
                        </div>
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

        <Button variant="primary" className="create-vacante-btn" onClick={() => setShowCreateModal(true)}>
          <PlusIcon />
        </Button>

        {/* Modal para Crear Vacante */}
        <Modal className="custom-modal" show={showCreateModal} onHide={() => setShowCreateModal(false)} style={{ zIndex: 1050 }}>
          <Modal.Header closeButton>
            <Modal.Title>Crear Vacante</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form>
              <Form.Group controlId="formTitle">
                <Form.Label>Título</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Título de la vacante"
                  value={newVacante.title}
                  onChange={(e) => setNewVacante({ ...newVacante, title: e.target.value })}
                />
              </Form.Group>

              <Form.Group controlId="formDescription">
                <Form.Label>Descripción</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Descripción de la vacante"
                  value={newVacante.description}
                  onChange={(e) => setNewVacante({ ...newVacante, description: e.target.value })}
                />
              </Form.Group>

              <Form.Group controlId="formSalary">
                <Form.Label>Salario</Form.Label>
                <Form.Control
                  type="number"
                  placeholder="Salario"
                  value={newVacante.salary}
                  onChange={(e) => setNewVacante({ ...newVacante, salary: e.target.value })}
                />
              </Form.Group>

              <Form.Group controlId="formArea">
                <Form.Label>Área</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Área de la vacante"
                  value={newVacante.area}
                  onChange={(e) => setNewVacante({ ...newVacante, area: e.target.value })}
                />
              </Form.Group>
              <Form.Group controlId="formModality">
                <Form.Label>Modalidad</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Modalidad de trabajo"
                  value={newVacante.modality}
                  onChange={(e) => setNewVacante({ ...newVacante, modality: e.target.value })}
                />
              </Form.Group>
            </Form>
          </Modal.Body>
          <Modal.Footer>
            <Button className="custom-modal-button" variant="secondary" onClick={() => setShowCreateModal(false)}>
              Cerrar
            </Button>
            <Button className="btn-crear" variant="primary" onClick={handleCreateVacante}>
              Crear Vacante
            </Button>
          </Modal.Footer>
        </Modal>

        {/* Modal para Editar Vacante */}
        <Modal show={showModal} onHide={handleModalClose} style={{ zIndex: 1050 }}>
          <Modal.Header closeButton>
            <Modal.Title>Editar Vacante</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form>
              <Form.Group controlId="formTitle">
                <Form.Label>Título</Form.Label>
                <Form.Control
                  type="text"
                  value={vacanteToEdit?.title || ''}
                  onChange={(e) => setVacanteToEdit({ ...vacanteToEdit, title: e.target.value })}
                />
              </Form.Group>

              <Form.Group controlId="formDescription">
                <Form.Label>Descripción</Form.Label>
                <Form.Control
                  type="text"
                  value={vacanteToEdit?.description || ''}
                  onChange={(e) => setVacanteToEdit({ ...vacanteToEdit, description: e.target.value })}
                />
              </Form.Group>

              <Form.Group controlId="formSalary">
                <Form.Label>Salario</Form.Label>
                <Form.Control
                  type="number"
                  value={vacanteToEdit?.salary || ''}
                  onChange={(e) => setVacanteToEdit({ ...vacanteToEdit, salary: e.target.value })}
                />
              </Form.Group>

              <Form.Group controlId="formArea">
                <Form.Label>Área</Form.Label>
                <Form.Control
                  type="text"
                  value={vacanteToEdit?.area || ''}
                  onChange={(e) => setVacanteToEdit({ ...vacanteToEdit, area: e.target.value })}
                />
              </Form.Group>

              <Form.Group controlId="formModality">
                <Form.Label>Modalidad</Form.Label>
                <Form.Control
                  type="text"
                  value={vacanteToEdit?.modality || ''}
                  onChange={(e) => setVacanteToEdit({ ...vacanteToEdit, modality: e.target.value })}
                />
              </Form.Group>
            </Form>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={handleModalClose}>
              Cerrar
            </Button>
            <Button className="btn-save" variant="primary" onClick={handleSaveChanges}>
              Guardar cambios
            </Button>
          </Modal.Footer>
        </Modal>
      </div>
      <ToastContainer position="top-end" />
    </div>
  );
};

export default Vacantes;
