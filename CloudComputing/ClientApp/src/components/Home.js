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
        <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
        <tr>
        <th>Date</th>
        <th>Shop name</th> 
        <th>People actual</th>
        </tr>
        </thead>
        <tbody>
          {this.state.shopTraffics.map(shopTraffic =>
            <tr key={shopTraffic.id}>
        <td>{shopTraffic.date}</td>
        <td>{shopTraffic.shop_id}</td> 
        <td>{shopTraffic.current_people_quantity}</td>
        </tr>
  )}
</tbody>
    </table>
      </div>
    );
    }


    async populateData() {
    const response = await fetch('shoptraffic/getLastRecords');
        const data = await response.json();
        console.log(data);
    this.setState({ shopTraffics: data });
}
}
