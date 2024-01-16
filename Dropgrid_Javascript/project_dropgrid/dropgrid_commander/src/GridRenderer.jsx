import React from 'react';

export default class GridRenderer extends React.Component {

    _isMounted = false;

    constructor(props) {
        super(props);
        this.state = {
            gridLines: this.props.gridLines,
            filledCells: [],
            Units: [],
            onclickFunction: this.props.onclickFunction,
            disabled: true
        }
    }
    componentDidMount() {
        this._isMounted = true;
        this.render();
    }

    componentWillUnmount() {
        this._isMounted = false;
    }

    onEvent(event){
        console.log("Clicked with event ",event);
        const x = event.clientX;
        const y = event.clientY;
        this.state.onclickFunction(x, y);
    }

    render() {
        console.log("rendering, this.state is ", this.state);
        console.log("rendering, this.props is ", this.props);
        const combinedClassName = ('grid-svg ' + (this.props.disabled ? 'no-pointer' : ''));
        return (
            <svg className = {combinedClassName} onClick={(event) => {
                this.onEvent(event);
              }} >
                {this.state.gridLines.map( (line,index) => {
                    return <line key = {index} x1={line.x1 + '%'} y1={line.y1 + '%'} x2={line.x2 + '%'} y2={line.y2 + '%'} strokeWidth={3} stroke='black' />
                })}
            </svg>
        )

    }
}