import React, { useState } from 'react'
import Constants from '../utilities/Constants'

export default function DrawingUpdateForm(props) {

  // const initialFormData = Object.freeze({
  //   drawingPhotoLink: props.drawing.drawingPhotoLink,
  //   descriptionText: props.drawing.description.text,
  //   descriptionPoints: props.drawing.description.points,
  //   descriptionPhotoLink: props.drawing.description.descriptionPhotoLink,
  // });
  const initialFormData = Object.freeze({
    drawingPhotoLink: "",
    descriptionText: "",
    descriptionPoints: "",
    descriptionPhotoLink: "",
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

    const drawingToUpdate = {
      drawingPhotoLink: formData.drawingPhotoLink,
      description: {
        text: formData.descriptionText,
        points: formData.descriptionPoints,
        descriptionPhotoLink: formData.descriptionPhotoLink
      }
    };

    const url = Constants.API_URL_UPDATE_DRAWING + `/${props.drawing.id}`;

    fetch(url, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': sessionStorage.getItem('Authorization'),
      },
      body: JSON.stringify(drawingToUpdate)
    })
      .then(response => response.json())
      .then(responseFromServer => {
        console.log(responseFromServer);
      })
      .catch((error) => {
        console.log(error);
        alert(error);
      });

    props.onDrawingUpdated(drawingToUpdate);
  }

  return (
    <div>
      <form className="w-100 px-5">
        <h1 className="mt-5"> Updating the drawing</h1>

        <div className="mt-5">

          <label className="h3 form-label">Text</label>
          <input
            value={FormData.descriptionText}
            name="descriptionText"
            type="text"
            className="form-control"
            onChange={handleChange} />

          <label className="h3 form-label">Points</label>
          <input
            value={FormData.descriptionPoints}
            name="descriptionPoints"
            type="text"
            className="form-control"
            onChange={handleChange} />

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
            onChange={handleChange} />

          <button onClick={handleSubmit} className="btn btn-dark brn-lg w-100 mt-5">
            Submit
          </button>
          <button onClick={() => props.onDrawingUpdated(null)} className="btn btn-secondary brn-lg w-100 mt-5">
            Cancel
          </button>
        </div>
      </form>
    </div>
  )
}
