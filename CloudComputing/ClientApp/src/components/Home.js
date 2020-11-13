import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  state = {
      shopTraffics: []
    };

  constructor(props) {
      super(props);
      this.populateData();
  }

  render () {
    return (
      <div>
        <h1>Hello, world!</h1>
            <p>Strona aktualnym natężeniem ruchu w sklepie</p>
            <p>Sklep pierwszy aktualna liczba osób:</p>
      </div>
    );
    }


    async populateData() {
   // const response = await fetch('shoptraffic/getLastRecords');
   // const data = await response.json();
   // this.setState({ shopTraffics: data });
}
}
