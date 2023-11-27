import {Map, InfoWindow, Marker, GoogleApiWrapper} from 'google-maps-react';
 import { APIKey } from './Config';
 import React from 'react';
export class MapContainer extends React.Component {
    constructor(props){
        super(props);
        this.state = {
            // Your initial state values go here
            selectedPlace : {
                name : "Test Name"
            }
          };
    }
  render() {
    return (
      <Map google={this.props.google} zoom={14}>
 
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
  apiKey: (APIKey)
})(MapContainer)