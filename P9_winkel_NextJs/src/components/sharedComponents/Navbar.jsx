import Link from 'next/link';

const Navbar = () => {
  return (
    <nav className="bg-purple-900 text-white px-30 py-2 flex justify-between items-center sticky top-0 z-50 ">



      <div className="text-4xl font-bold">
        <Link href="/" className="text-white no-underline">Winkel</Link>
      </div>


      <ul className="flex gap-6 list-none m-0 p-0">
        <li>
          <Link href="/" className="text-white text-base no-underline hover:underline">Home</Link>
        </li>
        <li>
          <Link href="/home/about" className="text-white text-base no-underline hover:underline">About</Link>
        </li>
        <li>
          <Link href="/home/services" className="text-white text-base no-underline hover:underline">Services</Link>
        </li>
        <li>
          <Link href="/home/contact" className="text-white text-base no-underline hover:underline">Contact</Link>
        </li>
      </ul>
    </nav>
  );
};

export default Navbar;
