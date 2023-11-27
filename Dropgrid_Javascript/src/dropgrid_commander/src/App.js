import logo from './logo.svg';
import MapContainer from './Map';
import './App.css';
const mapOptions = {
  zoom: 13,
  center: { lat: -34.397, lng: 150.644 },
  mapTypeId: 'satellite',

  scaleControl: true,
};
function App() {
  return (
    
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
      <MapContainer location={mapOptions} zoomLevel={17} />
    </div>
  );
}

export default App;
