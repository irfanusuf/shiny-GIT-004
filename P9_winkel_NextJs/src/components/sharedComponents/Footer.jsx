import Link from 'next/link';

const Footer = () => {
  return (
    <footer className="bg-purple-900 text-white text-sm py-5 mt-auto">


      <div className="max-w-screen-xl mx-auto px-6 flex justify-between items-center flex-wrap">
        
        
        <div className="mb-2">
          <p>&copy; {new Date().getFullYear()} MyWebsite. All rights reserved.</p>
        </div>



        <div className="flex items-center flex-wrap gap-4 text-gray-400">
          <Link href="/privacy-policy" className="hover:underline">Privacy Policy</Link>
          <span className="text-gray-600">|</span>
          <Link href="/terms" className="hover:underline">Terms of Service</Link>
          <span className="text-gray-600">|</span>
          <Link href="/contact" className="hover:underline">Contact</Link>
        </div>
      </div>


    </footer>
  );
};

export default Footer;
