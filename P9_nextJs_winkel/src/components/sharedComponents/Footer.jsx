
import Link from 'next/link';

const Footer = () => {
  return (
    <footer style={styles.footer}>
      <div style={styles.container}>
        <div style={styles.left}>
          <p>&copy; {new Date().getFullYear()} MyWebsite. All rights reserved.</p>
        </div>
        <div style={styles.right}>
          <Link href="/privacy-policy" style={styles.link}>Privacy Policy</Link>
          <span style={styles.separator}>|</span>
          <Link href="/terms" style={styles.link}>Terms of Service</Link>
          <span style={styles.separator}>|</span>
          <Link href="/contact" style={styles.link}>Contact</Link>
        </div>
      </div>
    </footer>
  );
};

const styles = {
  footer: {
    backgroundColor: '#1a1a1a',
    color: '#fff',
    padding: '20px 0',
    marginTop: 'auto',
    fontSize: '14px'
  },
  container: {
    maxWidth: '1200px',
    margin: '0 auto',
    padding: '0 30px',
    display: 'flex',
    justifyContent: 'space-between',
    flexWrap: 'wrap',
    alignItems: 'center'
  },
  left: {
    marginBottom: '10px'
  },
  right: {
    display: 'flex',
    gap: '15px',
    alignItems: 'center',
    flexWrap: 'wrap'
  },
  link: {
    color: '#ccc',
    textDecoration: 'none'
  },
  separator: {
    color: '#555'
  }
};

export default Footer;
