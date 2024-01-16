import { Map, InfoWindow, Marker, GoogleApiWrapper } from 'google-maps-react';
import { ApiKey } from './Config';
import React from 'react';
export class MapContainer extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      // Your initial state values go here
      selectedPlace: {
        name: "Test Name"
      },
      mapOptions: {
        zoom: 13,
        center: { lat: -34.397, lng: 150.644 },
        mapTypeId: "satellite",

        scaleControl: true,
      }
    };
  }
  render() {
    return (
      <Map google={this.props.google} mapTypeId={this.state.mapOptions.mapTypeId} mapOptions={this.state.mapOptions.zoom} scaleControl = {this.state.mapOptions.scaleControl}>

        <Marker onClick={this.onMarkerClick}
          name={'Current location'} />

        <InfoWindow onClose={this.onInfoWindowClose}>
          <div>
            <h1>{this.state.selectedPlace.name}</h1>
          </div>
        </InfoWindow>
      </Map>
    );
  }
}

export default GoogleApiWrapper({
  apiKey: (ApiKey)
})(MapContainer)