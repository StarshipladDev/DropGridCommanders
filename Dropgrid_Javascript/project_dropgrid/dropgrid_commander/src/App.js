import logo from './logo.svg';
import MapContainer from './Map';
import './App.css';
import GridRenderer from './GridRenderer';
import GridLine from './UI/GridLine';
import { useState } from 'react';

const mapOptions = {
  zoom: 13,
  center: { lat: -34.397, lng: 150.644 },
  mapTypeId: 'satellite',

  scaleControl: true,
};

//Here is enough to count as a commit
function createGrids(numberOfGrids) {
  let returnedGridArray = [];
  const percentPerGrid = 100 / numberOfGrids;
  for (let xAxisCounter = 0; xAxisCounter < numberOfGrids; xAxisCounter++) {
    const newGridLine = new GridLine(percentPerGrid * xAxisCounter, 0 , percentPerGrid * xAxisCounter, 100);
    console.log("newGridLine is ", newGridLine);
    returnedGridArray.push(newGridLine);
  }

  for (let yAxisCounter = 0; yAxisCounter < numberOfGrids; yAxisCounter++) {
    returnedGridArray.push(new GridLine(0, percentPerGrid * yAxisCounter , 100, percentPerGrid * yAxisCounter));
  }
  return returnedGridArray;
}


function App() {
  const mapLines = createGrids(20);
  const [disabledState, setDisabledState] = useState(true);
  console.log("mapLines is ",mapLines);
  return (

    <div className="App">
      <div className='grid-svg'>
        <MapContainer location={mapOptions} zoomLevel={17} />
      </div>
      <GridRenderer  onclickFunction={(x,y) => {console.log(`received x ${x} and y ${y}`);alert(`clicked with co-ords ${x},  and , ${y}`)}} gridLines = {mapLines} disabled = {disabledState} />
      <button className='swap-button' onClick = {() => {setDisabledState(!disabledState)}}>
        InteractWithMap
      </button>
    </div>
  );
}

export default App;
