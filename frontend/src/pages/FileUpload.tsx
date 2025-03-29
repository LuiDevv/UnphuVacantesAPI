import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import React, { useState } from 'react';

interface FileUploadProps {
  onUploadSuccess: (url: string) => void; // Callback para notificar cuando la subida sea exitosa
}

const FileUpload: React.FC<FileUploadProps> = ({ onUploadSuccess }) => {
  const [isUploading, setIsUploading] = useState(false); // Estado para manejar la carga
  const [error, setError] = useState<string | null>(null); // Estado para manejar errores

  const handleFileChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0]; // Obtener el archivo seleccionado
    if (!file) {
      toast('Por favor, selecciona un archivo.');
      return;
    }

    setIsUploading(true);
    setError(null);

    try {
      // Crear un FormData para enviar el archivo al backend
      const formData = new FormData();
      formData.append('file', file);

      // Enviar el archivo al backend
      const response = await fetch('http://localhost:5283/api/fileupload/upload', { // Reemplaza con la URL de tu endpoint
        method: 'POST',
        body: formData,
      });

      if (!response.ok) {
        throw new Error('Error al subir el archivo.');
      }

      const data = await response.json();

      // Verificar si la respuesta contiene la URL del archivo
      if (data && data.url) {
        onUploadSuccess(data.url); // Notificar la URL del archivo subido
      } else {
        throw new Error('La respuesta del servidor no contiene la URL del archivo.');
      }
    } catch (err) {
      setError('Hubo un problema al subir el archivo. Inténtalo de nuevo.');
      console.error(err);
    } finally {
      setIsUploading(false);
    }
  };

  return (
    <div>
      <h3>Subir Archivo</h3>
      <input type="file" onChange={handleFileChange} disabled={isUploading} />
      {isUploading && <p>Subiendo archivo...</p>}
      {error && <p style={{ color: 'red' }}>{error}</p>}

      <ToastContainer position="top-right" />
    </div>
  );
};

export default FileUpload;
