import React, { useState } from "react";
import styles from './CSS/style.css';
import Constants from "./utilities/Constants";
import DrawingCreateForm from "./Components/DrawingCreateForm";
import DrawingUpdateForm from "./Components/DrawingUpdateForm";

export default function App() {

  const initialPagedList = {
    collection: [],
    totalPages: 0,
  }
  const initialPagedParameters = {
    page: 1,
    count: 5,
  }
  const [pagedList, setPagedList] = useState(initialPagedList);
  const [pagedParameters, setPagedParameters] = useState(initialPagedList.totalPages);
  const [showingLoginForm, setShowingLoginForm] = useState(false);
  const [showingRegisterForm, setShowingRegisterForm] = useState(false);
  const [showingCreateNewDrawingForm, setShowingCreateNewDrawingForm] = useState(false);
  const [drawingCurrentlyBeingUpdated, setDrawingCurrentlyBeingUpdated] = useState(null);

  function getPagedList() {
    const url = Constants.API_URL_GET_DRAWING_BY_PARAMETERS;

    fetch(url, {
      method: 'GET'
    })
      .then(response => response.json())
      .then(pagedListromServer => {
        console.log(pagedListromServer);
        setPagedList(pagedListromServer);
      })
      .catch((error) => {
        console.log(error);
        alert(error);
      })
  }

  function deleteDrawing(drawingId){
    const url = Constants.API_URL_DELETE_DRAWING + `/${drawingId}`;

    alert(`we are here`);

    fetch(url, {
      method: 'DELETE'
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
          {(showingCreateNewDrawingForm === false && drawingCurrentlyBeingUpdated === null) && (
            <div>
              <h1> ASP.NET Core React Tutorial</h1>

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
                <button onClick={getPagedList} className="btn btn-dark btn-lg w-100">
                  Get Drawings from server
                </button>
                <button onClick={() => setShowingCreateNewDrawingForm(true)} className="btn btn-secondary btn-lg w-100 mt-4">
                  Create New Drawing
                </button>
              </div>
            </div>
          )}

          {(pagedList.collection.length > 0
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
            {pagedList.collection.map((drawing) => (
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

        {
          GetEachPage(pagedList.totalPages).map((page) => (
            <button className="btn btn-dark btn-lg mx-1 my-3" onClick="">{page}</button>))
        }

        <button onClick={() => setPagedList([])} className="btn btn-dark btn-lf w-100">
          Empty drawings array
        </button>
      </div>
    )
  }

  function GetEachPage(totalPages){
    return Array.from(Array(totalPages), (_, index) => index + 1);
  }

  function SetDefaultParameters(){
    setPagedList(initialPagedList);
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
    
    getPagedList();
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
    
    getPagedList();
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
    
    getPagedList();
  }

  function onDrawingUpdated(updatedDrawing) {
    setDrawingCurrentlyBeingUpdated(null);

    // if (updatedDrawing === null) {
    //   return;
    // }

    // let drawingsCopy = [...drawings];

    // const index = drawingsCopy.findIndex((drawingsCopyDrawing, currentIndex) => {
    //   if (drawingsCopyDrawing.id === updatedDrawing.id) {
    //     return true;
    //   }
    // });

    // if (index !== -1) {
    //   drawingsCopy[index] = updatedDrawing;
    // }

    //setDrawings(drawingsCopy);

    alert(`Drawing successfully updated`);
  }
  
  function onDrawingDeleted(deletedDrawingId) {
    // let drawingsCopy = [...drawings];

    // const index = drawingsCopy.findIndex((drawingsCopyDrawing, currentIndex) => {
    //   if (drawingsCopyDrawing.id === deletedDrawingId) {
    //     return true;
    //   }
    // });

    // if (index !== -1) {
    //   drawingsCopy.splice(index, 1);
    // }

    //setPagedList(drawingsCopy);

    alert(`Drawing successfully deleted`);
  }
}
