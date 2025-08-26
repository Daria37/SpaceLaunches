import React from "react";

interface LaunchImageProps {
  imageUrl: string;
  alt: string;
  className?: string;
}

const LaunchImage: React.FC<LaunchImageProps> = ({ 
  imageUrl, 
  alt, 
  className = "" 
}) => {
  const isValidUrl = imageUrl && imageUrl.startsWith('http');
  
  if (!isValidUrl) {
    return (
      <div className={`bg-gray-200 flex items-center justify-center ${className}`}>
        <span className="text-gray-500">No image available</span>
      </div>
    );
  }

  return (
    <img 
      src={imageUrl} 
      alt={alt}
      className={`object-cover w-60 h-60 ${className}`}
      onError={(e) => {
        e.currentTarget.style.display = 'none';
      }}
    />
  );
};

export default LaunchImage;