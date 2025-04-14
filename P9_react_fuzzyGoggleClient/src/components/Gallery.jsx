
import React, { useEffect, useState } from 'react';
import axios from 'axios';

const Gallery = () => {
    const [photos, setPhotos] = useState([]);

    useEffect(() => {
        const fetchPhotos = async () => {
            try {
                const response = await axios.get('https://api.pexels.com/v1/curated', {
                    headers: {
                        Authorization: 'A18L6UPAOtZeFZ4vLDzj2fO4wTeto2iIb2aqtyo2EA3agRXRdEN6YFRV', // Replace with your Pexels API key
                    },
                });
                console.log(response.data)
                setPhotos(response.data.photos);
            } catch (error) {
                console.error('Error fetching photos:', error);
            }
        };

        fetchPhotos();
    }, []);
    return (
        <div style={{ fontFamily: 'Arial, sans-serif', padding: '20px', backgroundColor: '#f8f8f8' }}>
            <h1 style={{ textAlign: 'center', color: '#333', marginBottom: '20px' }}>Gallery</h1>
            <div
                style={{
                    display: 'grid',
                    gridTemplateColumns: 'repeat(auto-fit, minmax(250px, 1fr))',
                    gap: '15px',
                    justifyContent: 'center',
                }}
            >
                {photos.map((photo) => (
                    <div
                        key={photo.id}
                        style={{
                            position: 'relative',
                            overflow: 'hidden',
                            borderRadius: '10px',
                            boxShadow: '0 4px 6px rgba(0, 0, 0, 0.1)',
                            backgroundColor: '#fff',
                        }}
                    >
                        <img
                            src={photo.src.medium}
                            alt={photo.photographer}
                            style={{
                                width: '100%',
                                height: 'auto',
                                display: 'block',
                                transition: 'transform 0.3s ease',
                            }}
                            onMouseOver={(e) => (e.target.style.transform = 'scale(1.05)')}
                            onMouseOut={(e) => (e.target.style.transform = 'scale(1)')}
                        />
                        <div
                            style={{
                                position: 'absolute',
                                bottom: '0',
                                left: '0',
                                right: '0',
                                backgroundColor: 'rgba(0, 0, 0, 0.6)',
                                color: '#fff',
                                padding: '10px',
                                fontSize: '14px',
                                textAlign: 'center',
                            }}
                        >
                            {photo.photographer}
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default Gallery;