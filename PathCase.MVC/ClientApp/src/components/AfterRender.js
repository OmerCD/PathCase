import React from "react";
function AfterRender({action}){
  return (
    <img
      className="d-none"
      src=""
      onError={action}
    />
  );
};

export default AfterRender;
