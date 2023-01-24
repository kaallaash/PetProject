import React, { useState } from "react";
import Constants from "./utilities/Constants";
import LoginForm from "./Components/LoginForm";
import RegisterForm from "./Components/RegisterForm";
import DrawingCreateForm from "./Components/DrawingCreateForm";
import DrawingUpdateForm from "./Components/DrawingUpdateForm";

export default function App() {
  const [drawings, setDrawings] = useState([]);
  const [showingLoginForm, setShowingLoginForm] = useState(false);
  const [showingRegisterForm, setShowingRegisterForm] = useState(false);
  const [showingCreateNewDrawingForm, setShowingCreateNewDrawingForm] = useState(false);
  const [drawingCurrentlyBeingUpdated, setDrawingCurrentlyBeingUpdated] = useState(null);

  function getDrawings() {
    const url = Constants.API_URL_GET_ALL_DRAWINGS;

    fetch(url, {
      method: 'GET',
      headers: {
        'Authorization': sessionStorage.getItem('Authorization'),
      },
    })
      .then(response => response.json())
      .then(drawingFromServer => {
        console.log(drawingFromServer);
        setDrawings(drawingFromServer);
      })
      .catch((error) => {
        console.log(error);
        alert(error);
      })
  }

  function deleteDrawing(drawingId){
    const url = Constants.API_URL_DELETE_DRAWING + `/${drawingId}`;

    fetch(url, {
      method: 'DELETE',
      headers: {
        'Authorization': sessionStorage.getItem('Authorization'),
      },
    })
      .then(response => response.json())
      .then(drawingFromServer => {
        console.log(drawingFromServer);
        onDrawingDeleted(drawingId);
      })
      .catch((error) => {
        console.log(error);
        alert(error);
      })
  }

  return (  
    <div className="container">
      <div className="row min-vh-100">
        <div className="col d-flex flex-column justify-content-center align-items-center">
          {(showingLoginForm === false
           && showingRegisterForm === false
           && showingCreateNewDrawingForm === false
           && drawingCurrentlyBeingUpdated === null) && (
            <div>
              <div className="mt-5">
                {
                  ((sessionStorage.getItem('Authorization') !== null)
                  && (
                    <button onClick={
                      () => {
                        if (window.confirm(`Are you sure you want to logOut?`)){
                          onLogout();}
                      }
                    } 
                    className="btn btn-secondary btn-lg w-100 mt-4">
                      LogOut
                    </button>
                  ))
                  ||(
                    <div>
                    <button onClick={() => setShowingLoginForm(true)} className="btn btn-secondary btn-lg w-100 mt-4">
                      LogIn
                    </button>
                    <button onClick={() => setShowingRegisterForm(true)} className="btn btn-secondary btn-lg w-100 mt-4">
                      Register
                    </button>
                    </div>)
                }
                <button onClick={getDrawings} className="btn btn-dark btn-lg w-100">
                  Get Drawings from server
                </button>
                <button onClick={() => setShowingCreateNewDrawingForm(true)} className="btn btn-secondary btn-lg w-100 mt-4">
                  Create New Drawing
                </button>
              </div>
            </div>
          )}

          {(drawings.length > 0
            && showingLoginForm === false
            && showingRegisterForm === false
            && showingCreateNewDrawingForm === false            
            && drawingCurrentlyBeingUpdated === null) 
            && renderDrawingsTable()}

          {showingLoginForm && <LoginForm onLogin={onLogin} />}

          {showingRegisterForm && <RegisterForm onRegister={onRegister} />}

          {showingCreateNewDrawingForm && <DrawingCreateForm onDrawingCreated={onDrawingCreated} />}

          {drawingCurrentlyBeingUpdated !== null
            && <DrawingUpdateForm drawing={drawingCurrentlyBeingUpdated}
              onDrawingUpdated={onDrawingUpdated} />}
        </div>
      </div>
    </div>  
  );

  function renderDrawingsTable() {
    return (
      <div className="table-responsive mt-5">
        <table className="table table-bordered border-dark">
          <thead>
            <tr>
              <th scope="col">Id</th>
              <th scope="col">Text</th>
              <th scope="col">Points</th>
              <th scope="col">DescriptionPhotoLink</th>
              <th scope="col">DrawingPhotoLink</th>
              <th scope="col">CRUD Operations</th>
            </tr>
          </thead>
          <tbody>
            {drawings.map((drawing) => (
              <tr>
                <td scope="row">{drawing.id}</td>
                <td>{drawing.description.text}</td>
                <td>{drawing.description.points}</td>
                <td>{drawing.description.descriptionPhotoLink}</td>
                <td>{drawing.drawingPhotoLink}</td>
                <td>
                  <button onClick={() => setDrawingCurrentlyBeingUpdated(drawing)} className="btn btn-dark btn-lg mx-3 my-3">Update</button>
                  <button onClick={() => {
                    if (window.confirm(`Are you sure you want to delete the drawing with id:${drawing.id}?`))
                    {
                      deleteDrawing(drawing.id);
                    }
                  }} className="btn btn-seconadry btn-lg">Delete</button>
                </td>
              </tr>))}
          </tbody>
        </table>

        <button onClick={() => setDrawings([])} className="btn btn-dark btn-lf w-100">
          Empty drawings array
        </button>
      </div>
    )
  }

  function SetDefaultParameters(){
    setDrawings([]);
    setShowingLoginForm(false);
    setShowingRegisterForm(false);
    setShowingCreateNewDrawingForm(false);
    setDrawingCurrentlyBeingUpdated(null);
  }

  function onLogin(token) {
    setShowingLoginForm(false);

    if (token !== null) {
      alert(`We've gotten a token`);
    }
    
    getDrawings();
  }

  function onLogout() {
    sessionStorage.clear();
    SetDefaultParameters();    
  }

  function onRegister(name) {
    setShowingRegisterForm(false);

    if (name !== null) {
      alert(`Hello ${name}`);
    }
    
    getDrawings();
  }

  function onDrawingCreated(createdDrawing) {
    setShowingCreateNewDrawingForm(false);

    if (createdDrawing !== null) {
      alert(`Drawing is successfully created`);
    }
    else
    {
      alert(`Drawing isn't created`);
    }
    
    getDrawings();
  }

  function onDrawingUpdated(updatedDrawing) {
    setDrawingCurrentlyBeingUpdated(null);

    if (updatedDrawing === null) {
      return;
    }

    let drawingsCopy = [...drawings];

    const index = drawingsCopy.findIndex((drawingsCopyDrawing, currentIndex) => {
      if (drawingsCopyDrawing.id === updatedDrawing.id) {
        return true;
      }
    });

    if (index !== -1) {
      drawingsCopy[index] = updatedDrawing;
    }

    setDrawings(drawingsCopy);

    alert(`Drawing successfully updated`);
  }
  
  function onDrawingDeleted(deletedDrawingId) {
    let drawingsCopy = [...drawings];

    const index = drawingsCopy.findIndex((drawingsCopyDrawing, currentIndex) => {
      if (drawingsCopyDrawing.id === deletedDrawingId) {
        return true;
      }
    });

    if (index !== -1) {
      drawingsCopy.splice(index, 1);
    }

    setDrawings(drawingsCopy);

    alert(`Drawing successfully deleted`);
  }
}
