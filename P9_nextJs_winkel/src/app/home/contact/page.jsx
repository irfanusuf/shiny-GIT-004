


import ContactForm from '@/components/clientComponents/ContactForm';
import React  from 'react';

const ContactPage = () => {
 

  return (
    <div style={styles.container}>
      <h1 style={styles.heading}>Contact Us</h1>
      <p style={styles.text}>We’re here to help! Whether you have a question, feedback, or need support, feel free to reach out to us.</p>

      <div style={styles.contactInfo}>
        <p><strong>Email:</strong> youremail@example.com</p>
        <p><strong>Phone:</strong> (123) 456-7890</p>
        <p><strong>Address:</strong> 123 Main Street, Your City, Country</p>
      </div>

      <div style={styles.hours}>
        <p><strong>Business Hours:</strong></p>
        <p>Monday – Friday: 9:00 AM – 6:00 PM</p>
        <p>Saturday – Sunday: Closed</p>
      </div>


        {/* client component can be imported in server components   */}

        <ContactForm/> 


      
    </div>
  );
};

const styles = {
  container: {
    maxWidth: '800px',
    margin: '40px auto',
    padding: '30px',
    backgroundColor: '#fff',
    boxShadow: '0 0 10px rgba(0, 0, 0, 0.1)',
    fontFamily: 'Arial, sans-serif'
  },
  heading: {
    marginBottom: '20px',
    color: '#333'
  },
  text: {
    marginBottom: '10px',
    color: '#555'
  },
  contactInfo: {
    marginTop: '20px'
  },
  hours: {
    marginTop: '20px'
  },
  form: {
    marginTop: '30px'
  },
  label: {
    display: 'block',
    marginBottom: '5px',
    fontWeight: 'bold'
  },
  input: {
    width: '100%',
    padding: '10px',
    marginBottom: '15px',
    border: '1px solid #ccc',
    borderRadius: '4px'
  },
  textarea: {
    width: '100%',
    padding: '10px',
    marginBottom: '15px',
    border: '1px solid #ccc',
    borderRadius: '4px'
  },
  button: {
    backgroundColor: '#007bff',
    color: '#fff',
    padding: '10px 20px',
    border: 'none',
    borderRadius: '4px',
    cursor: 'pointer'
  }
};

export default ContactPage;
