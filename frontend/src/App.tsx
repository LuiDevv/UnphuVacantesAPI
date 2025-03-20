import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { AuthProvider } from "./context/AuthContext";
import PrivateRoute from "./routes/PrivateRoute"; // Asegúrate de que PrivateRoute esté bien configurado
import Home from "./pages/home";
import Login from "./pages/login";
import Dashboard from "./pages/dashboard"; // Página protegida
import Register from "./pages/register"; // Página de registro

const App: React.FC = () => {
  return (
    <AuthProvider>
      <Router>
        <Routes>
          {/* Ruta pública para login */}
          <Route path="/login" element={<Login />} />

          {/* Ruta pública para registro */}
          <Route path="/register" element={<Register />} />

          {/* Rutas protegidas por PrivateRoute */}
          <Route element={<PrivateRoute />}>
            <Route path="/" element={<Home />} />
            <Route path="/dashboard" element={<Dashboard />} />
          </Route>
        </Routes>
      </Router>
    </AuthProvider>
  );
};

export default App;
