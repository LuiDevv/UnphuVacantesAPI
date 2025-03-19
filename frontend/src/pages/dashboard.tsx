import React, { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import { Container, Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const Dashboard: React.FC = () => {
  const auth = useContext(AuthContext);
  const navigate = useNavigate();

  const handleLogout = () => {
    auth?.logout();
    navigate("/login");
  };

  return (
    <Container className="mt-5">
      <h2 className="text-center">Panel de Control</h2>
      <p className="text-center">Bienvenido al área protegida de UNPHU Vacantes.</p>

      <div className="text-center mt-4">
        <Button variant="danger" onClick={handleLogout}>
          Cerrar Sesión
        </Button>
      </div>
    </Container>
  );
};

export default Dashboard;
