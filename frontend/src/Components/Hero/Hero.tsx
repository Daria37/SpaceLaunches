import React from "react";
import { Link } from "react-router-dom";
import hero from "./hero.png";
import "./Hero.css";

interface Props {}

const Hero = (props: Props) => {
  return (
    <section id="hero">
      <div className="container flex flex-col-reverse mx-auto p-8 lg:flex-row">
        <div className="flex flex-col space-y-10 mb-44 m-10 lg:m-10 xl:m-20 lg:mt:16 lg:w-1/2 xl:mb-52">
          <h1 className="text-5xl font-bold text-center lg:text-6xl lg:max-w-md lg:text-left">
            Space Launches analytics
          </h1>
          <p className="text-2xl text-center text-gray-400 lg:max-w-md lg:text-left">
            Watch space launches in live
          </p>
          <div className="mx-auto lg:mx-0">
            <Link
              to="/search"
              className="py-5 px-10 text-2xl font-bold text-white bg-darkBlue rounded lg:py-4 hover:opacity-70 mr-64"
            >
              Get Started
            </Link>
          </div>
        </div>
        <div className="lg:w flex items-center justify-center">
          <img src={hero} alt="Space launches app" 
          className="max-h-[600px] mr-32 w-auto object-contain animate-float"
          />
        </div>
      </div>
    </section>
  );
};

export default Hero;