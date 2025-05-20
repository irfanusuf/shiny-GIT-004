import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";
import Navbar from "@/components/sharedComponents/Navbar";
import Footer from "@/components/sharedComponents/Footer";
import { ToastContainer } from "react-toastify";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata = {
  title: "Winkel | Home",
  description: "Modernized social media",
};



export default function RootLayout({ children }) {
  return (

    <html lang="en">
      <body className={`${geistSans.variable} ${geistMono.variable}`}>
        
      {/* // navbar patial */}

        <ToastContainer/>

        <Navbar/>  
        <div style={{minHeight : "90vh"}}>

        {children}
        </div>
      
        <Footer/>

      </body>
    </html>
  );
}
