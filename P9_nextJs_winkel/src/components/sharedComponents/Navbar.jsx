
import Link from 'next/link';

const Navbar = () => {


    const username  = "irfan"

  return (
    <nav style={styles.navbar}>
      <div style={styles.logo}>
        <Link href="/" style={styles.logoLink}>MyWebsite</Link>
      </div>

    <div>
        Welcome {username}
    </div>


      <ul style={styles.navLinks}>
        <li><Link href="/" style={styles.link}>Home</Link></li>
        <li><Link href="/home/about" style={styles.link}>About</Link></li>
        <li><Link href="/home/services" style={styles.link}>Services</Link></li>
        <li><Link href="/home/contact" style={styles.link}>Contact</Link></li>
      </ul>
    </nav>
  );
};

const styles = {
  navbar: {
    backgroundColor: '#1a1a1a',
    padding: '15px 30px',
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    color: 'white',
    position: 'sticky',
    top: 0,
    zIndex: 1000
  },
  logo: {
    fontSize: '24px',
    fontWeight: 'bold'
  },
  logoLink: {
    color: 'white',
    textDecoration: 'none'
  },
  navLinks: {
    listStyle: 'none',
    display: 'flex',
    gap: '20px',
    margin: 0,
    padding: 0
  },
  link: {
    color: 'white',
    textDecoration: 'none',
    fontSize: '16px'
  }
};

export default Navbar;
