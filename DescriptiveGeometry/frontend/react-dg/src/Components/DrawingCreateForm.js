import React, { useState } from 'react'
import Constants from '../utilities/Constants'

export default function DrawingCreateForm(props) {
  
  const initialFormData = Object.freeze({
    drawingPhotoLink: "DrawingPhotoLink",
    descriptionText : "Text",
    descriptionPoints: "Points",
    descriptionPhotoLink: "DescriptionPhotoLink"
  });

  const [formData, setFormData] = useState(initialFormData);

  const handleChange = (e => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  });

  const handleSubmit = (e) => {
    e.preventDefault();

    const drawingToCreate = {
      drawingPhotoLink: formData.drawingPhotoLink,
      description: {
        text: formData.descriptionText,
        points: formData.descriptionPoints,
        descriptionPhotoLink: formData.descriptionPhotoLink
      }
    };

    const url = Constants.API_URL_CREATE_DRAWING

    fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': sessionStorage.getItem('Authorization'),
      },
      body: JSON.stringify(drawingToCreate)
    })
      .then(response => {
        response.json()

        console.log(sessionStorage.getItem('Bearer'));

        if(response.status !== 200)
        {
          throw new URIError(`Response status of server: ${response.status}`);
        }

        props.onDrawingCreated(drawingToCreate)
      })
      .then(responseFromServer => {
        console.log(responseFromServer);
      })
      .catch((error) => {
        console.log(error);
        alert(error);
        props.onDrawingCreated(null);
      });
  }

  return (
    <div>
      <form className="w-100 px-5">
        <h1 className="mt-5"> Create new drawing</h1>

        <div className="mt-5">

          <label className="h3 form-label"> DrawingPhotoLink</label>
          <input
            value={FormData.drawingPhotoLink}
            name="drawingPhotoLink"
            type="text"
            className="form-control"
            onChange={handleChange} />

          <label className="h3 form-label"> DescriptionPhotoLink</label>
          <input
            value={FormData.descriptionPhotoLink}
            name="descriptionPhotoLink"
            type="text"
            className="form-control"
            onChange={handleChange} 
            />

          <label className="h3 form-label">Points</label>
          <input
            value={FormData.descriptionPoints}
            name="descriptionPoints"
            type="text"
            className="form-control"
            onChange={handleChange} />

          <label className="h3 form-label">Text</label>
          <input
            value={FormData.descriptionText}
            name="descriptionText"
            type="text"
            className="form-control"
            onChange={handleChange} />

          <button onClick={handleSubmit} className="btn btn-dark brn-lg w-100 mt-5">
            Submit
          </button>
          <button onClick={() => props.onDrawingCreated(null)} className="btn btn-secondary brn-lg w-100 mt-5">
            Cancel
          </button>
        </div>
      </form>
    </div>
  )
}
