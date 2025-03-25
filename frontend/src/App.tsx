import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { AuthProvider } from "./context/AuthContext";
import PrivateRoute from "./routes/PrivateRoute";
import Home from "./pages/home";
import Login from "./pages/login";
import CompanyLogin from "./pages/company-login";
import Dashboard from "./pages/dashboard";
import Register from "./pages/register";
import RegisterEmp from "./pages/register-emp";
import Vacantes from "./pages/vacantes";
import VacancyDetail from "./pages/VacancyDetail";
import Perfil from "./pages/perfil";
import VacantesUser from "./pages/vacantesuser";
import DashboardUserHome from "./pages/dashboard-user-home";
import AllVacantes from "./pages/all-vacantes"; 
import PerfilEmp from "./pages/perfil-emp"


const App: React.FC = () => {
    return (
        <AuthProvider>
            <Router>
                <Routes>
                    <Route path="/login" element={<Login />} />
                    <Route path="/company-login" element={<CompanyLogin />} />
                    <Route path="/register" element={<Register />} />
                    <Route path="/register-emp" element={<RegisterEmp />} />
                    
                    <Route element={<PrivateRoute />}>
                        <Route path="/" element={<Home />} />
                        <Route path="/dashboard" element={<Dashboard />} />
                        <Route path="/vacantes" element={<Vacantes />} />
                        <Route path="/dashboard-user" element={<DashboardUserHome />} /> {/* Use DashboardUserHome here */}
                        <Route path="/Vacant/:id" element={<VacancyDetail />} />
                        <Route path="/perfil" element={<Perfil />} />
                        <Route path="/vacantes-user" element={<VacantesUser />} />
                        <Route path="/all-vacantes" element={<AllVacantes />} />
                        <Route path="/perfil-emp" element={<PerfilEmp />} />
                    </Route>
                </Routes>
            </Router>
        </AuthProvider>
    );
};

export default App;
